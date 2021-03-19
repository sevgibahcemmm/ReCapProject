﻿using Business.Abstract;
using Business.Constains;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rental;


        //true müsaitlik anlamında;
        public RentalManager(IRentalDal rental)
        {
            _rental = rental;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("Kullanici")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental Tentity)
        {
            IResult results = BusinessRules.Run(CheckIfCarInUse(Tentity.CarId));
            if (results != null)
            {
                return results;
            }

            _rental.Add(Tentity);
            return new SuccessResult(Messages.RentalAdded);
        }

        //rental tablosundan silindi.
        public IResult Delete(Rental rental)
        {
            IResult results = BusinessRules.Run(CheckIfDelete(rental.RentalId));
            if (results != null)
            {
                return results;
            }
            _rental.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);

        }

        //bu metot çagrıldıgında arac teslim edildi.
        //teslim edilme tarihi verildi.
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Deliver(int rentalId)
        {
            IResult results = BusinessRules.Run(CheckIfDeliver(rentalId));
            if (results != null)
            {
                return results;
            }

            return new SuccessResult(Messages.RentalDelivered);

        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rental.GetAll());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rental.Get(p => p.RentalId == Id));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> InUse()
        {
            return new SuccessDataResult<List<Rental>>(_rental.GetAll(p => p.ReturnDate == null));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> NotInUse()
        {
            return new SuccessDataResult<List<Rental>>(_rental.GetAll(p => p.ReturnDate != null));
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental Tentity)
        {
            _rental.Update(Tentity);
            return new SuccessResult(Messages.RentalUpdated);
        }
        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rental.GetRentalDetails());


        }

        private IResult CheckIfCarInUse(int carId)
        {
            //bu degerlere sahip bir sey döndürüyorsa arac kullanımdadır
            var result = _rental.Get(p => p.CarId == carId && p.ReturnDate == null);
            if (result != null)
            {
                return new ErrorResult(Messages.RentalBusy);
            }
            return new SuccessResult();

        }

        private IResult CheckIfDelete(int Id)
        {
            var result = _rental.Get(p => p.RentalId == Id);
            if (result == null)
            {
                return new ErrorResult(Messages.NoRecording);
            }
            if (result.ReturnDate == null)
            {
                return new ErrorResult(Messages.RentalBusy);
            }
            return new SuccessResult();
        }

        private IResult CheckIfDeliver(int rentalId)
        {
            var result = _rental.Get(p => p.RentalId == rentalId);
            if (result.ReturnDate != null)
            {
                return new ErrorResult(Messages.NoRecording);
            }
            result.ReturnDate = DateTime.Now.Date;
            Update(result);
            return new SuccessResult();
        }
    }
}