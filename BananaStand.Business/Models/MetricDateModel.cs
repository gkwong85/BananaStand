using System;
using System.ComponentModel.DataAnnotations;

namespace BananaStand.Business.Models
{
    public class MetricDateModel
    {
        [Required(ErrorMessage = "Start Date required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date required")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
