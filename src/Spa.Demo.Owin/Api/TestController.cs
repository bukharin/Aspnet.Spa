using System.Collections.Generic;
using System.Web.Http;

namespace Spa.Demo.Owin.Api
{
    public class TestController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new List<string>
                       {
                           "sample text 1",
                           "sample text 2",
                           "sample text 3"
                       };
        }
    }
}