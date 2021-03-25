using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService:IEntityServiceBase<Car>
    {
        IDataResult<List<CarDetailDto>> GetCarsByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarsByColorId(int id);
        IDataResult<List<CarDetailDto>> GetCarsDetailDto();
        IDataResult<List<CarDetailDto>> GetCarByIdDetailDto(int id);
        IDataResult<List<CarDetailDto>> GetCarsByColorAndBrandId(int colorId, int brandId);
        IResult TransactionalOperation(Car car);
    }

}
