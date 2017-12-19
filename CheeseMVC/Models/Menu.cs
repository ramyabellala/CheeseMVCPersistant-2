using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CheeseMVC.Models
{
    public class Menu
    {
       
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<CheeseMenu> CheeseMenus { get; set; }
    }
}
