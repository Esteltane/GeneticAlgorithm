using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapSackProb
{
    public class Program
    {
        public static Knapsack ReadDataFromFile(string filePath)
        {
            List<Item> items = new List<Item>();
            double knapsackCapacity = 0.0;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (double.TryParse(reader.ReadLine(), out knapsackCapacity))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 2 && double.TryParse(parts[0], out double weight) && double.TryParse(parts[1], out double value))
                            {
                                items.Add(new Item(weight, value));
                            }
                            else
                            {
                                Console.WriteLine("Invalid item data: " + line);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid knapsack capacity data.");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }

            return new Knapsack(knapsackCapacity, items);
        }

        static void Main()
        {
            string filePath = "knapsack_data.txt"; // Replace with the actual file path
            Knapsack knapsack = ReadDataFromFile(filePath);

            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(
                knapsack,
                populationSize: 100,
                mutationRate: 0.01,
                crossoverRate: 0.7
            );

            geneticAlgorithm.InitializePopulation();
            geneticAlgorithm.Run(maxGenerations: 100);

            Chromosome bestSolution = geneticAlgorithm.GetBestSolution();
            Console.WriteLine("Best solution: " + string.Join(", ", bestSolution.Genes));
            Console.WriteLine("Total value: " + bestSolution.Fitness);
        }
    }
}
