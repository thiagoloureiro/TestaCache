using System.Collections.Generic;
using Nancy;

namespace CacheApi
{
    public class CustomerController : NancyModule
    {
        public CustomerController() : base("/customers")
        {
            var obj = new CustomerService();
            var ret = obj.ReturnCustomer();
            Get["/"] = _ => Response.AsJson(ret);
        }
    }
}