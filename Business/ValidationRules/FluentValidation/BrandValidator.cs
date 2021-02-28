using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).NotEmpty().WithMessage("Brand Name Alanı Boş Olamaz");
            RuleFor(b => b.BrandName).MinimumLength(2).WithMessage("Brand Name Alanı En az 2 Karekter olmalı"); 
        }
    }
}
