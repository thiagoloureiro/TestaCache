using CacheApi.Model;
using System.Collections.Generic;
using TestaCache.NetStandard.Attributes;
using TestaCache.NetStandard.Redis.Attributes;

namespace API.NetCore.Service
{
    public class CustomerService
    {
        [CacheableResult(600)] // 600 Seconds, 10 Minutes cache Policy
                               // [RedisCacheableResult] // 600 Seconds, 10 Minutes cache Policy
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

        [RedisCacheableResult]
        public List<dynamic> ReturnCustomer2()
        {
            var lstCustomer = new List<dynamic>();

            var customer = new Customer
            {
                Id = 12,
                Name = "Acme In2c",
                Email = "acme@email.com2"
            };

            var customer1 = new Customer
            {
                Id = 22,
                Name = "Marvel Inc2",
                Email = "Marvel@email.com2"
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