﻿using System;

namespace Pwc.Domain
{
    public class Entity
    {
        public bool Deleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
