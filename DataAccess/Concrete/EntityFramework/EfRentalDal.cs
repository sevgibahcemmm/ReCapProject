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
        public List<RentalDetailDto> GetAllRentalDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rent in context.Rentals
                             join car in context.Cars
                                 on rent.CarId equals car.CarId
                             join brand in context.Brands
                                 on car.BrandId equals brand.BrandId
                             join customer in context.Customers
                                 on rent.CustomerId equals customer.CustomerId
                             join user in context.Users
                                 on customer.UserId equals user.Id
                             select new RentalDetailDto
                             {
                                 RentalId = rent.RentalId,
                                 CarName = brand.BrandName + car.CarName,
                                 CustomerFullName = user.FirstName + user.LastName,
                                 RentDate = rent.RentDate,
                                 ReturnDate = rent.ReturnDate
                             };
                return result.ToList();
            }
        }

        public List<RentalDetailDto> GetAllRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result =
                    from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                    join customer in context.Customers
                        on rental.CustomerId equals customer.CustomerId
                    join user in context.Users
                        on customer.UserId equals user.Id
                    join car in context.Cars
                        on rental.CarId equals car.CarId
                    join brand in context.Brands
                        on car.BrandId equals brand.BrandId
                    join color in context.Colors
                        on car.ColorId equals color.ColorId
                    select new RentalDetailDto
                    {
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate,
                        RentalId = rental.RentalId,
                        BrandName = brand.BrandName,
                        CarDesctiption = car.Description,
                        ColorName = color.ColorName,
                        CompanyName = customer.CompanyName,
                        DailyPrice = car.DailyPrice,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ModelYear = car.ModelYear
                    };

                return result.ToList();
            }
        }
    }
}
