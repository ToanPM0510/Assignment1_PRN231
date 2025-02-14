using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
	public class CategoryDTO
    {
        [Required(ErrorMessage = "CategoryName is required.")]
        [StringLength(50)]
        public string? CategoryName { get; set; }
    }
}
