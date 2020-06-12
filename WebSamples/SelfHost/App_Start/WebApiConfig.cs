using Microsoft.Owin.Security.OAuth;
using SelfHost.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SelfHost.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

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
        }
    }
}
