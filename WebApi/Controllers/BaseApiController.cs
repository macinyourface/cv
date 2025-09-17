using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist(string returnMessage = null)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound, returnMessage);
        }

        public HttpResponseMessage AlreadyExists(string returnMessage)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, returnMessage);
        }

        public HttpResponseMessage ValueCannotBeNull(string returnMessage)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, returnMessage);
        }
    }
}