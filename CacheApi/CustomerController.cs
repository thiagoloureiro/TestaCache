using System;
using Nancy;

namespace CacheApi
{
    public class CustomerController : NancyModule
    {
        public CustomerController() : base("/customers")
        {
            try
            {
                var obj = new CustomerService();
                var ret = obj.ReturnCustomer();
                Get["/"] = _ => Response.AsJson(ret);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}