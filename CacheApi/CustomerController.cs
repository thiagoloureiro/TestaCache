using Nancy;
using System;

namespace CacheApi
{
    public class CustomerController : NancyModule
    {
        public CustomerController() : base("/customers")
        {
            try
            {
                var obj = new CustomerService();

                Get["/"] = _ =>
                {
                    var ret = obj.ReturnCustomer();
                    return Response.AsJson(ret);
                };

                Post["/"] = _ =>
                {
                    obj.UpdateCustomer();
                    return Response.AsJson("Cleared");
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}