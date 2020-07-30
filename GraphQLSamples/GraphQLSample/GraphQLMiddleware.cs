using GraphQL;
using GraphQL.Common.Request;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace GraphQLSample
{
    public class GraphQLMiddleware : OwinMiddleware
    {
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executer;
        private readonly ISchema _schema;
        private readonly GraphQLSettings _settings;

        public GraphQLMiddleware(OwinMiddleware next)
            : base(next)
        {
            _writer = UnityConfig.Container.Resolve<IDocumentWriter>();
            _executer = UnityConfig.Container.Resolve<IDocumentExecuter>();
            _schema = UnityConfig.Container.Resolve<ISchema>();
        }

        public override async Task Invoke(IOwinContext context)
        {
            string body;
            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                body = await reader.ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<GraphQLRequest>(body);
                var result = await _executer.ExecuteAsync(options =>
                {
                    options.Schema = _schema;
                    options.Query = request.Query;
                    options.OperationName = request?.OperationName;
                    options.Inputs = request?.Variables.ToInputs();
                    options.UserContext = context.Authentication.User;
                    options.ValidationRules = DocumentValidator.CoreRules().Concat(new[] { new InputValidationRule() });
                    options.EnableMetrics = _settings.EnableMetrics;
                    if (_settings.EnableMetrics)
                    {
                        options.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                    }

                }).ConfigureAwait(false);

                var json = await _writer.WriteToStringAsync(result);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
                context.Response.Write(json);
            }
        }

    }

    public class GraphQLSettings
    {
        public PathString Path { get; set; } = new PathString("/api/graphql");

        public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }

        public bool EnableMetrics { get; set; }
    }

    public class InputValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            return (INodeVisitor)new EnterLeaveListener(x => { });
        }
    }
}