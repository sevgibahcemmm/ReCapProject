using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService:IEntityServiceBase<Rental>
    {
        IDataResult<RentalDetailDto> GetRentalDetailByCarId(int carId);
        IDataResult<List<RentalDetailDto>> GetAllRentalDetail();

    }
}
