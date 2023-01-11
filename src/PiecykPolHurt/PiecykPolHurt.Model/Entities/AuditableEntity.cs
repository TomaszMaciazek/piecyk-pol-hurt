﻿namespace PiecykPolHurt.Model.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
