using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{  //InMemory formatta GetById, GetAll, Add, Update, Delete oprasyonlarını yazınız.
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> {
            new Car{Id=1,ColorId=1,BrandId=1,DailyPrice=125000,ModelYear=2020,Description="Güzel Bir Araba"},
             new Car{Id=2,ColorId=2,BrandId=2,DailyPrice=205000,ModelYear=1980,Description="Güzel Bir Araba"},
              new Car{Id=3,ColorId=2,BrandId=2,DailyPrice=250000,ModelYear=2010,Description="Güzel Bir Araba"},
               new Car{Id=4,ColorId=3,BrandId=3,DailyPrice=250000,ModelYear=2011,Description="Güzel Bir Araba"},
                new Car{Id=5,ColorId=4,BrandId=3,DailyPrice=71000,ModelYear=2012,Description="Güzel Bir Araba"},
                 new Car{Id=6,ColorId=1,BrandId=2,DailyPrice=35000,ModelYear=2021,Description="Güzel Bir Araba"},

            };

        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = null;
            carToDelete = _cars.SingleOrDefault(s => s.Id == car.Id);

            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void GetById(Car car)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailDtos(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(s => s.Id == car.Id);
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;

        }
    }
}
