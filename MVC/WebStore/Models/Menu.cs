using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class Menu
    {
        public List<MenuItem> Items { get; set; }
        public Menu()
        {
            Items = new List<MenuItem>();
        }
    }
}