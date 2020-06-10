using Microsoft.Owin.Security.OAuth;
using Owin;
using SelfHost.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SelfHost
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Clear(); //禁用JSON
            config.Formatters.XmlFormatter.UseXmlSerializer = false;
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //禁用XML

            config.Filters.Add(new LogFilterAttribute());
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            app.UseWebApi(config);
            //app.UseNancy(); //Install-Package Nancy.Owin
        }
    }
}
