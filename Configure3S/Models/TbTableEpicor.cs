using System;
using System.Collections.Generic;

#nullable disable

namespace Configure3S.Models
{
    public partial class TbTableEpicor
    {
        public TbTableEpicor()
        {
            TbTaskDtls = new HashSet<TbTaskDtl>();
        }

        public int TableId { get; set; }
        public string TableName { get; set; }

        public virtual ICollection<TbTaskDtl> TbTaskDtls { get; set; }
    }
}
