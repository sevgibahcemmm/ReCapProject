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

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(
                CheckIfImageLimitExpired(carImage.CarId),
                CheckIfImageExtensionValid(file)
            );

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }
        

        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfImageExists(carImage.ImgId)
            );
            if (result != null)
            {
                return result;
            }
            string path = GetById(carImage.ImgId).Data.ImagePath;
            FileHelper.Delete(path);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IResult DeleteByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Any())
            {
                foreach (var carImage in result)
                {
                    Delete(carImage);
                }
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarHaveNoImage);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarHaveNoImage(carId));
        }

        public IDataResult<CarImage> GetById(int imgId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.ImgId == imgId));
        }

        public IResult Update(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(
                CheckIfImageLimitExpired(carImage.CarId),
                CheckIfImageExtensionValid(file),
                CheckIfImageExists(carImage.ImgId)
            );

            if (result != null)
            {
                return result;
            }

            CarImage oldCarImage = GetById(carImage.ImgId).Data;
            carImage.ImagePath = FileHelper.Update(file, oldCarImage.ImagePath);
            carImage.Date = DateTime.Now;
            carImage.CarId = oldCarImage.CarId;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
        

        private List<CarImage> CheckIfCarHaveNoImage(int carId)
        {
            string path = @"\Images\default.png";
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (!result.Any())
                return new List<CarImage> { new CarImage { CarId = carId, ImagePath = path } };
            return result;
        }

        private IResult CheckIfImageLimitExpired(int carId)
        {
            int result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
                return new ErrorResult(Messages.ImageLimitExpiredForCar);
            return new SuccessResult();
        }

        private IResult CheckIfImageExtensionValid(IFormFile file)
        {
            bool isValidFileExtension = Messages.ValidImageFileTypes.Any(t => t == Path.GetExtension(file.FileName).ToUpper());
            if (!isValidFileExtension)
                return new ErrorResult(Messages.InvalidImageExtension);
            return new SuccessResult();
        }

        private IResult CheckIfImageExists(int id)
        {
            if (_carImageDal.IsExist(id))
                return new SuccessResult();
            return new ErrorResult(Messages.CarImageMustBeExists);
        }
    }
}