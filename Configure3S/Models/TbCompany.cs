using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbCompany
    {
        public TbCompany()
        {
            TbUsers = new HashSet<TbUser>();
        }

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }

        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
