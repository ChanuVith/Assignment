using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Entities
{
    public class BlacklistedToken
    {
        [Key]
        public int Id { get; set; }
        public string? Token { get; set; }
    }
}
