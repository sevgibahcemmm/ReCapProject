using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constains;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        public IResult ReceivePayment(Payment payment)
        {
            if (payment.Amount > 5000)
            {
                return new ErrorResult(Messages.Basarısız);
            }

            return new SuccessResult(Messages.Ekleme);
        }
    }
}