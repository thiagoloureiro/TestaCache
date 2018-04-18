using API.NetCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.NetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        // GET api/values
        [HttpGet]
        public object Get()
        {
            var obj = new CustomerService();
            var ret = obj.ReturnCustomer();
            return Json(ret);
        }
    }
}