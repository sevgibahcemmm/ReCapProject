using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.Name).NotEmpty();// isim alanı boş olamaz
            //RuleFor(p => p.ProductName).MinimumLength(2);//en az 2 karekter olmalı
            //RuleFor(p => p.UnitPrice).NotEmpty();// fiyat alanı boş olamaz
            //RuleFor(p => p.UnitPrice).GreaterThan(0);//0 dan büyük olmalı
            //RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryID == 1);// Category Id 1 olanın fiyatı 10 dan az olamaz
            RuleFor(p => p.Name).Must(StarwithA).WithMessage("Name A İle Başlamalı...");// adı alanı A ile başlamalı
        }

        private bool StarwithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
