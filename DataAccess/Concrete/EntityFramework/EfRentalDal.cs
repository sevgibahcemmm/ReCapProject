using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {

            public List<RentalDetailDto> GetRentalDetails()
            {
                using (ReCapProjectContext context = new ReCapProjectContext())
                {
                    var result = from rental in context.Rentals
                        join car in context.Cars
                            on rental.CarId equals car.CarId
                        join customer in context.Customers
                            on rental.CustomerId equals customer.UserId
                        join user in context.Users
                            on customer.UserId equals user.Id
                        select new RentalDetailDto
                        {
                            RentalId = rental.RentalId,
                            Descripton = car.Description,
                            CompanyName = customer.CompanyName,
                            RentDate = rental.RentDate,
                            ReturnDate = rental.ReturnDate,
                            UserName = user.FirstName + " " + user.LastName,
                            DailyPrice = car.DailyPrice
                        };
                    return result.ToList();
                }
            }

        }
    }