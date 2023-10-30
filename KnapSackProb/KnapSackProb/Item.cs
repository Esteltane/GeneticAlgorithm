using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapSackProb
{
    public class Item
    {
        public double Weight { get; set; }
        public double Value { get; set; }

        public Item(double weight, double value)
        {
            Weight = weight;
            Value = value;
        }
    }
}
