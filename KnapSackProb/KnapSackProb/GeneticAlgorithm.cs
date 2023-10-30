namespace KnapSackProb
{
    public class GeneticAlgorithm
    {
        private Population population;
        private Knapsack knapsack;
        private double mutationRate;
        private double crossoverRate;

        public GeneticAlgorithm(Knapsack knapsack, int populationSize, double mutationRate, double crossoverRate)
        {
            this.knapsack = knapsack;
            this.mutationRate = mutationRate;
            this.crossoverRate = crossoverRate;
            population = new Population(populationSize, knapsack.Items.Count);
        }

        public void InitializePopulation()
        {
            population = new Population(population.Chromosomes.Count, knapsack.Items.Count);
        }

        public void Run(int maxGenerations)
        {
            
                for (int generation = 0; generation < maxGenerations; generation++)
                {
                    // Evaluation: Calculate fitness for each chromosome
                    foreach (Chromosome chromosome in population.Chromosomes)
                    {
                        chromosome.CalculateFitness(knapsack);
                    }

                    // Selection: Choose parents for crossover
                    List<Chromosome> selectedParents = RouletteWheelSelection();

                    // Crossover: Create offspring
                    List<Chromosome> offspring = Crossover(selectedParents);

                    // Mutation: Introduce genetic diversity
                    Mutation(offspring);

                    // Replacement: Select individuals for the next generation
                    Replacement(offspring);

                    // Update population for the next generation
                    population.Chromosomes = offspring;
                }
            
        }
        private List<Chromosome> Crossover(List<Chromosome> parents)
        {
            List<Chromosome> offspring = new List<Chromosome>();

            // Perform crossover on selected parents and add offspring to the list.
            foreach (Chromosome parent1 in parents)
            {
                Chromosome parent2 = parents[new Random().Next(parents.Count)]; // Randomly select a second parent
                Chromosome child = parent1.Crossover(parent2);
                offspring.Add(child);
            }

            return offspring;
        }

        private void Mutation(List<Chromosome> offspring)
        {
            // Apply mutation to each chromosome in the offspring.
            foreach (Chromosome chromosome in offspring)
            {
                chromosome.Mutate(mutationRate);
            }
        }

        private void Replacement(List<Chromosome> offspring)
        {
            // Generational replacement: Replace the entire population with offspring
            population.Chromosomes = offspring;
        }

        private List<Chromosome> RouletteWheelSelection()
        {
            List<Chromosome> selectedParents = new List<Chromosome>();
            double totalFitness = population.Chromosomes.Sum(chromosome => chromosome.Fitness);

            while (selectedParents.Count < population.Chromosomes.Count)
            {
                double randomValue = new Random().NextDouble() * totalFitness;
                double cumulativeFitness = 0.0;

                foreach (Chromosome chromosome in population.Chromosomes)
                {
                    cumulativeFitness += chromosome.Fitness;
                    if (cumulativeFitness >= randomValue)
                    {
                        selectedParents.Add(chromosome);
                        break;
                    }
                }
            }

            return selectedParents;
        }

        private List<Chromosome> TournamentSelection(int tournamentSize)
        {
            List<Chromosome> selectedParents = new List<Chromosome>();

            while (selectedParents.Count < population.Chromosomes.Count)
            {
                List<Chromosome> tournamentParticipants = new List<Chromosome>();

                for (int i = 0; i < tournamentSize; i++)
                {
                    Chromosome participant = population.Chromosomes[new Random().Next(population.Chromosomes.Count)];
                    tournamentParticipants.Add(participant);
                }

                // Select the best participant from the tournament
                Chromosome bestParticipant = tournamentParticipants.MaxBy(chromosome => chromosome.Fitness);
                selectedParents.Add(bestParticipant);
            }

            return selectedParents;
        }



        public Chromosome GetBestSolution()
        {
            Chromosome bestSolution = population.Chromosomes[0];
            double bestFitness = bestSolution.Fitness;

            foreach (Chromosome chromosome in population.Chromosomes)
            {
                if (chromosome.Fitness > bestFitness)
                {
                    bestSolution = chromosome;
                    bestFitness = chromosome.Fitness;
                }
            }

            return bestSolution;
        }
    }
}