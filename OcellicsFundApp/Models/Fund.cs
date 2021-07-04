using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OcellicsFundApp.Models
{
    public class Fund
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Fund Name")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime LastTradeDate { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}