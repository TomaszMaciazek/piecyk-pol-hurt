using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.Model.Entities
{
    public class ReportDefinition : AuditableEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]

        public string Group { get; set; }

        public string Description { get; set; }

        [Required]
        public int MaxRow { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Version { get; set; }

        [Required]
        public string XmlDefinition { get; set; }
    }
}
