using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OcellicsFundApp.Models
{
    public class MonthlyReturn
    {
        public int Id { get; set; }

        public double MonthToDate { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EffectiveDate { get; set; }

        public Fund Fund { get; set; }

        public int FundId { get; set; }

        public double? ThreeMonth { get; set; }

        public double? SixMonth { get; set; }

        public double? YearToDate { get; set; }

        public double? OneYear { get; set; }

        public double? TwoYear { get; set; }

        public double? SinceInception { get; set; }

        public double? ThreeMonthVolatility { get; set; }

        public double? SixMonthVolatility { get; set; }

        public double? YearToDateVolatility { get; set; }

        public double? OneYearVolatility { get; set; }

        public double? TwoYearVolatility { get; set; }

        public double? SinceInceptionVolatility { get; set; }

        public double? ThreeMonthRiskAdjusted { get; set; }

        public double? SixMonthRiskAdjusted { get; set; }

        public double? YearToDateRiskAdjusted { get; set; }

        public double? OneYearRiskAdjusted { get; set; }

        public double? TwoYearRiskAdjusted { get; set; }

        public double? SinceInceptionRiskAdjusted { get; set; }

    }
}