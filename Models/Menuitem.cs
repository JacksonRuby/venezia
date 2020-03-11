using System;
using System.Collections.Generic;

namespace venezia.Models
{
    public partial class Menuitem
    {
        public uint Id { get; set; }
        public uint? MenuId { get; set; }
        public uint? SectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Section Section { get; set; }
    }
}
