using System;
using System.ComponentModel.DataAnnotations;

namespace BananaStand.Business.Models
{
    public class BananaModel
    {
        [Required(ErrorMessage = "Banana Amount is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive number")]
        public int Bananas { get; set; }

        [Required(ErrorMessage = "Transaction date is required")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }
    }
}
