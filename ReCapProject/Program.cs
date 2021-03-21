using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ReCapProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("----------Brand List ------------");
            //BrandTest();
            //Console.WriteLine("----------Color List------------");
            //ColorTest();
            //Console.WriteLine("----------Car List ------------");
            //CarTest();
           // CarDetailTest();
            // CustomerAdded();
          //  UsersAdded();
            RentalAdded();
            Console.ReadLine();
        }

        private static void RentalAdded()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
          //  var result = rentalManager.GetRentalDetailslDto();
             var result = rentalManager.Add(new Rental { CarId = 1003, CustomerId = 1003, RentDate=DateTime.Now,ReturnDate=DateTime.Now});


            Console.WriteLine("----------User Detail List ------------");
            if (result.Success == true)
            {
                foreach (var rental in rentalManager.GetRentalDetails().Data)
                {
              //      Console.WriteLine("{0}--{1}--{2}--{3}", rental.CompanyName, rental.CarName, rental.ReturnDate, rental.RentDate);
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void UsersAdded()
        {
            UserManager userrManager = new UserManager(new EfUserDal());
           var result = userrManager.GetById(1);
          // var result = userrManager.Update(new User {Id=5, FirstName = "Bayram", LastName = "Ramazan",Email="veli@gov.tr",Password="0012541" });

            Console.WriteLine("----------User Detail List ------------");
            if (result.Success == true)
            {
                //foreach (var user in userrManager.GetById(1).Data)
                //{
                //    Console.WriteLine("{0}--{1}", user.FirstName, user.LastName);
                //}
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CustomerAdded()
        {
          //  CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

          ////  var result = customerManager.Update(new Customer {Id=1003, CompanyName = "4. Müşteri Güncellendi", UserId = 1 });

          //  Console.WriteLine("----------Customer Detail List ------------");
          //  if (result.Success == true)
          //  {
          //      //foreach (var carr in customerManager.GetAll().Data)
          //      //{
          //      //    Console.WriteLine("{0}--{1}", carr.CompanyName, carr.UserId);
          //      //}
          //      //Console.WriteLine(result.Message);
          //  }
          //  else
          //  {
          //      Console.WriteLine(result.Message);
          //  }
        }

        private static void CarDetailTest()
        {
            //CarManager car = new CarManager(new EfCarDal());

            ////var result = car.GetByDailyPrice();

            ////Console.WriteLine("----------Car Detail List ------------");
            ////if (result.Success == true)
            ////{
            ////    foreach (var carr in result.Data)
            ////    {
            ////        Console.WriteLine("{0}--{1}--{2}--{3}", carr.BrandName, carr.ColorName, carr.DailyPrice, carr.Description);
            ////    }
            ////    Console.WriteLine(result.Message);
            ////}
            ////else
            ////{
            ////    Console.WriteLine(result.Message);
            //}

        }
        private static void CarTest()
        {
            //CarManager car = new CarManager(new EfCarDal());

            ////carManager.Add(new Car { BrandId = 1003, ColorId = 1002,  ModelYear = "2021", DailyPrice = 125000, Description = "Yeni Kaydedilen Araba" });

            //foreach (var carr in car.GetAll().Data)
            //{
            //    Console.WriteLine(carr.Description);
            //    Console.WriteLine("----------------------");
            //}
          
        }
        private static void ColorTest()
        {
            //ColorManager color = new ColorManager(new EfColorDal());
            //foreach (var c in color.GetAll())
            //{
            //    Console.WriteLine(c.ColorName);
            //    Console.WriteLine("----------------------");
            //}
        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { BrandName = "Taksi" });
            //foreach (var car in brandManager.GetAll())
            //{
            //    Console.WriteLine(car.BrandName);
            //    Console.WriteLine("----------------------");
            //}

        }
    }


}
