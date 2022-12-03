using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.Model.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
