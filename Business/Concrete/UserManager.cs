using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
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
        IUserDal _userDal;
        public UserManager(IUserDal userDAL)
        {
            _userDal = userDAL;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User User)
        {
            _userDal.Add(User);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User User)
        {
            _userDal.Delete(User);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User User)
        {
            _userDal.Update(User);
            return new SuccessResult();
        }
        public IDataResult<List<User>> GetUsers()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(c => c.Id == id));
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return (_userDal.GetClaims(user));
        }

        public User GetByMail(string email)
        {
           return (_userDal.Get(u => u.Email == email));
        }
      
    }
}
