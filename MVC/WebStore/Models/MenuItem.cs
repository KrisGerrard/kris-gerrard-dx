using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuItemPath { get; set; }
        public Nullable<int> ParentItemId { get; set; }
        public virtual ICollection<MenuItem> ChildMenuItems { get; set; }
        public MenuItem()
        {
            this.ChildMenuItems = new List<MenuItem>();
        }
    }
}