using Business.Abstract;
using Business.Constains;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDAL;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDAL = brandDal;
        }

        public IResult Add(Brand brand)
        {
            _brandDAL.Add(brand);
            return new SuccessResult(Messages.Added);
        }
        public IResult Delete(Brand brand)
        {
            _brandDAL.Delete(brand);
            return new SuccessResult(Messages.Deleted);
        }
        public IResult Update(Brand brand)
        {
            _brandDAL.Update(brand);
            return new SuccessResult(Messages.Updated);
        }
        public IDataResult<List<Brand>> GetBrands()
        {
            return new SuccessDataResult<List<Brand>>(_brandDAL.GetAll());
        }
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDAL.Get(b => b.BrandId == id));
        }

    }
}
