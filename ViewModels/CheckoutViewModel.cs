using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Cvv { get; set; }
        [Required]
        public int CreditCartNumber { get; set; }
        [ValidateNever]
        public double TotalPrice { get; set; }
        [ValidateNever]
        public Cart Cart { get; set; }
    }
}
