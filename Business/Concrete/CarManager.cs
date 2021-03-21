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
        private readonly ICarDal _carDal;
        private readonly ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carImageService = carImageService;
            _carDal = carDal;
        }
        [CacheRemoveAspect("Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car Tentity)
        {

            _carDal.Add(Tentity);
            return new SuccessResult(Messages.Ekleme);
        }

        [CacheRemoveAspect("Get")]
        [TransactionScopeAspect]
        [PerformanceAspect(0)]
        public IResult AddTransactionTest(Car entity)
        {

            _carDal.Add(entity);
            if (entity.BrandId == 1002)
            {
                throw new Exception("");
            }

            entity.CarId = 0;
            entity.Description = "TransactionTest" + entity.Description;
            _carDal.Add(entity);
            return new SuccessResult(Messages.Ekleme);
        }

        public IResult Delete(Car Tentity)
        {
            _carDal.Delete(Tentity);
            return new SuccessResult(Messages.Silme);
        }

        public IDataResult<List<Car>> GetAll()
        {
            List<Car> cars = _carDal.GetAll();
            if (cars.Count == 0)
            {
                return new ErrorDataResult<List<Car>>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<List<Car>>(cars, Messages.Listeleme);
            }
        }

        public IDataResult<Car> GetById(int Id)
        {
            Car car = _carDal.Get(p => p.CarId == Id);
            if (car == null)
            {
                return new ErrorDataResult<Car>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<Car>(car, Messages.Listeleme);
            }
        }

        public IDataResult<CarDetailAndImagesDto> GetCarDetailAndImagesDto(int carId)
        {
            var result = _carDal.GetCarDetail(carId);
            var imageResult = _carImageService.GetAllByCarId(carId);
            if (result == null || imageResult.Success == false)
            {
                return new ErrorDataResult<CarDetailAndImagesDto>(Messages.Basarısız);
            }

            var carDetailAndImagesDto = new CarDetailAndImagesDto
            {
                Car = result,
                CarImages = imageResult.Data
            };

            return new SuccessDataResult<CarDetailAndImagesDto>(carDetailAndImagesDto, Messages.Listeleme);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail();
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.Listeleme);
            }
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.BrandId == brandId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.Listeleme);
            }
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandIdAndColorId(int brandId, int colorId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.BrandId == brandId && p.ColorId == colorId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.Listeleme);
            }
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int colorId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.ColorId == colorId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.Basarısız);
            }
            else
            {
                return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.Listeleme);
            }
        }

        public IResult Update(Car Tentity)
        {
            _carDal.Update(Tentity);
            return new SuccessResult(Messages.Guncelleme);
        }
    }
}