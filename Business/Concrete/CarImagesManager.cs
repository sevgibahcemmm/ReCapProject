﻿using Business.Abstract;
using Business.Business.BusinessAspects.Autofac;
using Business.Constains;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [CacheAspect()]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImageGetAllSuccess);
        }

        [CacheAspect()]
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.ImgId == id),
                Messages.CarImageGetByIdSuccess);
        }

        //[SecuredOperation("admin")]
        //[ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspects("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimit(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult("başarılı mesajı buraya gelecek");
        }

        //[SecuredOperation("admin")]
        //[ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspects("ICarImageService.Get")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult("başarılı mesajı buraya gelecek");
        }

        //[SecuredOperation("admin")]
        //[ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspects("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageExists(carImage.ImgId));
            if (result != null)
            {
                return result;
            }
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        [CacheAspect()]
        public IDataResult<List<CarImage>> GetAllByCarId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id),
                Messages.CarImageGetAllSuccess);
        }


        #region Car Image Business Rules

        //car image business rules
        private IResult CheckImageExists(int imageId)
        {
            var result = _carImageDal.Get(i => i.ImgId == imageId);
            if (result == null)
            {
                return new ErrorResult(Messages.CarImageExistsError);
            }

            return new SuccessResult();
        }

        private IResult CheckImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageImageLimitError);
            }

            return new SuccessResult();
        }

        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = @"images\Default.png";
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImage> { new CarImage { CarId = id, ImagePath = path.Replace("\\", "/"), Date = DateTime.Now } };
            }
            return _carImageDal.GetAll(p => p.CarId == id);
        }


        #endregion

    }
}
