using System;
using System.Collections.Generic;
using System.Linq;

namespace CheeseMVC.Models
{
    public class CheeseCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        IList<Cheese> Cheeses { get; set; }
    }
}
