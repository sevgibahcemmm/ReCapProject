using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDAL;
        public ColorManager(IColorDal colorDAL)
        {
            _colorDAL = colorDAL;
        }

        public IResult Add(Color color)
        {
            _colorDAL.Add(color);
            return new SuccessResult();
        }
        public IResult Delete(Color color)
        {
            _colorDAL.Delete(color);
            return new SuccessResult();
        }
        public IResult Update(Color color)
        {
            _colorDAL.Update(color);
            return new SuccessResult();
        }
        public IDataResult<List<Color>> GetColors()
        {
            return new SuccessDataResult<List<Color>>(_colorDAL.GetAll());
        }
        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDAL.Get(c => c.ColorId == id));
        }

    }
}
