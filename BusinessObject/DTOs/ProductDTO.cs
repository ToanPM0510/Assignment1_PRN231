using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, StringLength(50)]
        public string? ProductName { get; set; }

        public float Weight { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}