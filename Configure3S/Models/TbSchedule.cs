using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbSchedule
    {
        public int ScheduleId { get; set; }
        public int? TaskId { get; set; }
        public string CompanyId { get; set; }
        public string Type { get; set; }
        public DateTime? TimeStart { get; set; }
        public int? Interval { get; set; }

        public virtual TbTask Task { get; set; }
    }
}
