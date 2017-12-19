using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CheeseMVC.Models
{
    public class CheeseMenu
    {
       
        public int MenuID { get; set; }
        public Menu Menu { get; set; }
        public int CheeseID { get; set; }
        public Cheese Cheese { get; set; }
    }
}
