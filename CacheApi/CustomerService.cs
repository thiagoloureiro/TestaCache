using System.Collections.Generic;
using TestaCache.Cache;
using TestaCache.Redis;

namespace CacheApi
{
    public class CustomerService
    {
        [RedisCacheableResult]
        public List<Customer> ReturnCustomer()
        {
            var lstCustomer = new List<Customer>();

            var customer = new Customer
            {
                Id = 1,
                Name = "Acme Inc",
                Email = "acme@email.com"
            };

            var customer1 = new Customer
            {
                Id = 2,
                Name = "Marvel Inc",
                Email = "Marvel@email.com"
            };

            lstCustomer.Add(customer);
            lstCustomer.Add(customer1);

            return lstCustomer;
        }
    }
}