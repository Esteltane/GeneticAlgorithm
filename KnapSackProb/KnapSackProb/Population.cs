using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapSackProb
{
    public class Population
    {
        public List<Chromosome> Chromosomes { get; set; }

        public Population(int populationSize, int chromosomeLength)
        {
            Chromosomes = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                int[] genes = new int[chromosomeLength];
                for (int j = 0; j < chromosomeLength; j++)
                {
                    genes[j] = new Random().Next(2); // Initialize with random genes (0 or 1)
                }

                Chromosomes.Add(new Chromosome(genes));
            }
        }
    }
}
