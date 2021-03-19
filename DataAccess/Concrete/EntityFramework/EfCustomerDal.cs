using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapProjectContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetail()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from us in context.Users
                    join cus in context.Customers
                        on us.Id equals cus.UserId
                    select new CustomerDetailDto
                    {
                        UserId = us.Id,
                        UserName = us.FirstName + " " + us.LastName,
                        Email = us.Email,
                        CompanyName = cus.CompanyName
                    };
                return result.ToList();
            }
        }
    }
}