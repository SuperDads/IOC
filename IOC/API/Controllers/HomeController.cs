using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class HomeController : ApiController
    {

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent("ok123", Encoding.UTF8, string.Format("application/{0}", "json"));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
