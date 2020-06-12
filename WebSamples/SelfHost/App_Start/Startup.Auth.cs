using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SelfHost.Models;
using SelfHost.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SelfHost
{
	public partial class Startup
	{
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		public static string PublicClientId { get; private set; }

		public void ConfigureAuth(IAppBuilder app)
		{
			app.CreatePerOwinContext(() => UnityConfig.Container.Resolve<ApplicationDbContext>());
			app.CreatePerOwinContext(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>((options, context) =>
			{
				var manager = UnityConfig.Container.Resolve<ApplicationUserManager>();
				var dataProtectionProvider = options.DataProtectionProvider;
				if (dataProtectionProvider != null)
				{
					manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("PACM.ApiServer"));
				}
				return manager;
			}));

			// 使应用程序可以使用 Cookie 来存储已登录用户的信息
			// 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
			app.UseCookieAuthentication(new CookieAuthenticationOptions());
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// 针对基于 OAuth 的流配置应用程序
			PublicClientId = "self";
			OAuthOptions = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/Token"),
				Provider = new ApplicationOAuthProvider(PublicClientId),
				AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
				//在生产模式下设 AllowInsecureHttp = false
#if DEBUG
				AllowInsecureHttp = true
#else
                AllowInsecureHttp = false
#endif
			};
			app.UseOAuthBearerTokens(OAuthOptions);

			//app.UseNancy(); //Install-Package Nancy.Owin
		}
	}
}
