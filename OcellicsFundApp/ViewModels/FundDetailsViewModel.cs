using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OcellicsFundApp.Models;

namespace OcellicsFundApp.ViewModels
{
    public class FundDetailsViewModel
    {
        public IEnumerable<MonthlyReturn> MonthlyReturns { get; set; }
        public Fund Fund { get; set; }
    }
}