using Business.Abstract;
using Business.Business.BusinessAspects.Autofac;
using Business.Constains;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDAL;
        public CarManager(ICarDal carDAL)
        {
            _carDAL = carDAL;
        }
        [SecuredOperation("car.add,Admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            _carDAL.Add(car);
            return new SuccessResult(Messages.Added);

        }
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            _carDAL.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Car car)
        {
            _carDAL.Update(car);
            return new SuccessResult(Messages.Updated);
        }
       // [SecuredOperation("car.list,admin")]
      //  [ValidationAspect(typeof(CarValidator))]
        //[CacheAspect]
        //[PerformanceAspect(5)]
        //[LogAspect(typeof(FileLogger))]
        //[CacheAspect(duration: 10)]
      
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(5000);
            if (DateTime.Now.Hour == 03)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(), Messages.Listed);

        }

        public IDataResult<List<CarDetailDto>> GetDetailAll()
        {
            //if (DateTime.Now.Hour == 03)
            //{
            //    return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<CarDetailDto>>(_carDAL.GetCarDetailDtos(), Messages.Listed);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDAL.Get(c => c.Id == id));
        }
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(c => c.BrandId == id));
        }
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDAL.GetAll(c => c.ColorId == id));
        }

    }
}
