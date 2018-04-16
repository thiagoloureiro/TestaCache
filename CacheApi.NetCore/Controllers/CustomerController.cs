using CacheApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CacheApi.NetCore.Controllers
{
    [Route("api/customers")]
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