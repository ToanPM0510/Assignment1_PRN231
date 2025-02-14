using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class OrderDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public float Discount { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; } = null;

        [ForeignKey("ProductId")]
        public Product? Product { get; set; } = null;
    }
}