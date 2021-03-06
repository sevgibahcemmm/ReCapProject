﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService:IEntityServiceBase<Customer>
    {
        IDataResult<List<CustomerDetailDto>> GetAllDetails();
    }
}
