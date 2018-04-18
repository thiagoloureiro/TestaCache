using API.NetCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.NetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public object Get()
        {
            var test = new CustomerService();
            var ret = test.ReturnCustomer();
            var retRedis = test.ReturnCustomer2();

            return Json(ret);
        }
    }
}