using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.Model.Dto.Report
{
    public class ReportDefinitionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public int MaxRow { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public string XmlDefinition { get; set; }
    }
}
