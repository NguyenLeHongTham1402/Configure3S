using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbApiconfigure
    {
        public TbApiconfigure()
        {
            TbTasks = new HashSet<TbTask>();
        }

        public int SourceId { get; set; }
        public string CompanyId { get; set; }
        public string Apikey { get; set; }
        public string Urlapi { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<TbTask> TbTasks { get; set; }
    }
}
