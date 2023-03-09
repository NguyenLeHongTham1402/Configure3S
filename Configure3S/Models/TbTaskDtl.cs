using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbTaskDtl
    {
        public int TaskDtlId { get; set; }
        public int? TaskId { get; set; }
        public int? TableId { get; set; }

        public virtual TbTableEpicor Table { get; set; }
        public virtual TbTask Task { get; set; }
    }
}
