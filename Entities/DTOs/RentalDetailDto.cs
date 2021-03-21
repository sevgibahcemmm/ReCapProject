using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalId { get; set; }
        public string CarName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        //Customer Table
        public string CompanyName { get; set; }

        //User Table
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //Brand Table
        public string BrandName { get; set; }

        //Color Table
        public string ColorName { get; set; }

        //Car Table
        public string CarDesctiption { get; set; }

        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }


    }
}
