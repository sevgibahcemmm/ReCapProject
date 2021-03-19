using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from cars in context.Cars 
                     join brand in context.Brands
                        on cars.BrandId equals brand.BrandId
                    join color in context.Colors
                        on cars.ColorId equals color.ColorId
                    join carImage in context.CarImages 
                        on cars.CarId equals carImage.CarId
                        into dept from carImage in dept.DefaultIfEmpty()

                             select new CarDetailDto
                    {
                        CarId =cars.CarId,
                        Description = cars.Description,
                        BrandName = brand.BrandName,
                        ColorName = color.ColorName,
                        DailyPrice = cars.DailyPrice,
                        ImagePath = carImage.ImagePath
                        
                    };

                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
    }
}
