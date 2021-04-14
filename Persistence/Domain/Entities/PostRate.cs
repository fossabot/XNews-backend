﻿using System;

#nullable disable

namespace Domain.Entities
{
    public partial class PostRate
    {
        public Guid PostRateId { get; set; }
        public double Rate { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
