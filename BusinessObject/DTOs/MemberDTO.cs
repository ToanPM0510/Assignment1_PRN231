using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        [Required]
        public string? Email { get; set; }

        [StringLength(30)]
        public string? CompanyName { get; set; }

        [StringLength(15)]
        public string? City { get; set; }

        [StringLength(15)]
        public string? Country { get; set; }

        [Required, StringLength(15)]
        public string? Password { get; set; }
    }
}