using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using TestaCache.AOP;
using TestaCache.Cache;

namespace CacheApi
{
    public class CustomerService : IDynamicMetaObjectProvider
    {
        [CacheableResult]
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

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new AspectWeaver(parameter, this);
        }
    }
}