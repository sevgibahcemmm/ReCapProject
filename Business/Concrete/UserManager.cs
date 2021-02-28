using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDAL;
        public UserManager(IUserDal userDAL)
        {
            _userDAL = userDAL;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User User)
        {
            _userDAL.Add(User);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User User)
        {
            _userDAL.Delete(User);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User User)
        {
            _userDAL.Update(User);
            return new SuccessResult();
        }
        public IDataResult<List<User>> GetUsers()
        {
            return new SuccessDataResult<List<User>>(_userDAL.GetAll());
        }
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDAL.Get(c => c.Id == id));
        }
    }
}
