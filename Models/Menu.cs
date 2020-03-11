using System;
using System.Collections.Generic;

namespace venezia.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Menuitem = new HashSet<Menuitem>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Menuitem> Menuitem { get; set; }
    }
}
