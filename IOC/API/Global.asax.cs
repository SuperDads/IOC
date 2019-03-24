using Autofac.Integration.WebApi;
using Core.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //disable "X-AspNetMvc-Version" header name
            //MvcHandler.DisableMvcResponseHeader = true;
            //initialize engine context
            EngineContext.Initialize(false);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Singleton<IEngine>.Instance.Container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}