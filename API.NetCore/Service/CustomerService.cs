using CacheApi.Model;
using System.Collections.Generic;
using TestaCache.NetStandard.Attributes;

namespace API.NetCore.Service
{
    public class CustomerService
    {
        [CacheableResult(600)] // 600 Seconds, 10 Minutes cache Policy
        public List<dynamic> ReturnCustomer()
        {
            var lstCustomer = new List<dynamic>();

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

        [AffectedCacheableMethods("ReturnCustomer")]
        public bool UpdateCustomer()
        {
            return true;
        }
    }
}