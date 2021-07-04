using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OcellicsFundApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}