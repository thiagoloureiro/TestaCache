using Nancy;

namespace CacheApi
{
    public class CustomerController : NancyModule
    {
        public CustomerController() : base("/customers")
        {
            dynamic obj = new CustomerService();
            obj.ReturnCustomer();
            Get["/"] = _ => Response.AsJson("ok");
        }
    }
}