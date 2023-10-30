using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapSackProb
{
    public class Knapsack
    {
        public double Capacity { get; set; }
        public List<Item> Items { get; set; }

        public Knapsack(double capacity, List<Item> items)
        {
            Capacity = capacity;
            Items = items;
        }

    }
}
