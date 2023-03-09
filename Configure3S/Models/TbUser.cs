using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbTasks = new HashSet<TbTask>();
        }

        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual TbCompany Company { get; set; }
        public virtual ICollection<TbTask> TbTasks { get; set; }
    }
}
