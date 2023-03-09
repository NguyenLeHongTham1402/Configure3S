using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbTask
    {
        public TbTask()
        {
            TbSchedules = new HashSet<TbSchedule>();
            TbTaskDtls = new HashSet<TbTaskDtl>();
        }

        public int TaskId { get; set; }
        public int? SourceId { get; set; }
        public string CompanyId { get; set; }
        public string TaskName { get; set; }
        public string CreateUser { get; set; }

        public virtual TbUser C { get; set; }
        public virtual TbApiconfigure Source { get; set; }
        public virtual ICollection<TbSchedule> TbSchedules { get; set; }
        public virtual ICollection<TbTaskDtl> TbTaskDtls { get; set; }
    }
}
