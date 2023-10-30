using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapSackProb
{
    public class Chromosome
    {
        public int[] Genes { get; set; }
        public double Fitness { get; set; }

        public Chromosome(int[] genes)
        {
            Genes = genes;
        }

        public double CalculateFitness(Knapsack knapsack)
        {
            double totalValue = 0.0;
            double totalWeight = 0.0;

            for (int i = 0; i < Genes.Length; i++)
            {
                if (Genes[i] == 1)
                {
                    totalValue += knapsack.Items[i].Value;
                    totalWeight += knapsack.Items[i].Weight;
                }
            }

            if (totalWeight > knapsack.Capacity)
            {
                Fitness = 0.0; // Penalize solutions that exceed the capacity
            }
            else
            {
                Fitness = totalValue;
            }

            return Fitness;
        }

        public Chromosome Crossover(Chromosome parent2)
        {
            int crossoverPoint = new Random().Next(Genes.Length);
            int[] childGenes = new int[Genes.Length];

            for (int i = 0; i < crossoverPoint; i++)
            {
                childGenes[i] = Genes[i];
            }

            for (int i = crossoverPoint; i < Genes.Length; i++)
            {
                childGenes[i] = parent2.Genes[i];
            }

            return new Chromosome(childGenes);
        }

        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (new Random().NextDouble() < mutationRate)
                {
                    Genes[i] = 1 - Genes[i]; // Flip the gene
                }
            }
        }
    }
}
