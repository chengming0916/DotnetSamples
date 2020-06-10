using NLog;
using SelfHost.App_Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace SelfHost.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);
            actionContext.ControllerContext.Configuration.Services.Replace(typeof(ITraceWriter), new TraceWriter());
            //GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new TraceWriter());
            //var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            var trace = actionContext.ControllerContext.Configuration.Services.GetTraceWriter();
            trace.Info(actionContext.Request, "Controller:"
                + actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine
                + "Action:" + actionContext.ActionDescriptor.ActionName, "JSON", actionContext.ActionArguments);
        }
    }
}
