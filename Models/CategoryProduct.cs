using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{

    public class CategoryProduct
    {
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
