using System;
using System.Collections.Generic;

namespace venezia.Models
{
    public partial class Section
    {
        public Section()
        {
            Menuitem = new HashSet<Menuitem>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }

        public virtual ICollection<Menuitem> Menuitem { get; set; }
    }
}
