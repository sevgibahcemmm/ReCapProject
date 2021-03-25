using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        [Key]
        public int Idd { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime? ProcessDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
    
}