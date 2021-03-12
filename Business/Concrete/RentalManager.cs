using Business.Abstract;
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

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDAL;
        public RentalManager(IRentalDal rentalDAL)
        {
            _rentalDAL = rentalDAL;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            _rentalDAL.Add(rental);
            return new SuccessResult();
        }
        public IResult Delete(Rental rental)
        {
            _rentalDAL.Delete(rental);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDAL.Update(rental);
            return new SuccessResult();
        }
        public IDataResult<List<Rental>> GetRentals()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDAL.GetAll());
        }
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDAL.Get(r => r.Id == id));
        }
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            //if (DateTime.Now.Hour == 02)
            //{
            //    return new ErrorDataResult<List<RentalDetailDto>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDAL.GetRentalDetails());
        }
    }
} 


