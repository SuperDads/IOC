using BLL;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class HomeController : ApiController
    {
        private readonly TestBLL testBLL;

        public HomeController(TestBLL testBLL)
        {
            this.testBLL = testBLL;
        }

        public HttpResponseMessage Get()
        {
            string s1 = testBLL.Get();

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(testBLL.Get(), Encoding.UTF8, string.Format("application/{0}", "json"));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
