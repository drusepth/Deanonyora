using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Deanony
{
    class Person
    {
        // String identifying this person, commonly their name
        string identifier;

        // Raw metric data from analyzing the text
        List<Hashtable> metrics;

        // Standard deviations for each metric
        Hashtable standard_deviations;

        // Goal values for each metric (average)
        Hashtable goal_values;

        // Weights for each metric specific per-person
        Hashtable metric_weights;

        public Person(string name)
        {
            identifier = name;

            // Initialize
            metrics = new List<Hashtable>();
            standard_deviations = new Hashtable();
            goal_values = new Hashtable();
            metric_weights = new Hashtable();
        }

        // Calculate expected values and standard deviations off each metric
        public void CalculateGoalValues(string[] metric_list)
        {
            foreach (string metric in metric_list)
            {
                // Grab the metric's score from each test
                List<double> metric_score = new List<double>();
                for (int i = 0; i < metrics.Count; i++)
                {
                    double result = Convert.ToDouble(metrics[i][metric]);
                    metric_score.Add(result);
                }

                // Calculate goal (average for now), and std deviation
                goal_values.Add(metric, metric_score.Average());
                standard_deviations.Add(metric, Calculate.StandardDeviation(metric_score));
            }

            CalculateMetricWeights();
        }

        // Calculate metric weights based on standard deviations
        public void CalculateMetricWeights()
        {
            Hashtable reverser = new Hashtable();
            List<string> metric_list = new List<string>();
            double metric_total = 0;

            // Grab metrics used and their value sum
            foreach (var metric in standard_deviations.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                metric_list.Add(metric.Key.ToString());
                metric_total += (double)metric.Value;
            }

            Console.WriteLine("TOTAL:::::::::: " + metric_total);
            
            // Add initial weights that will be reversed after
            for (int i = 0; i < metric_list.Count; i++)
            {
                reverser.Add(metric_list[i], (double)standard_deviations[metric_list[i]] / metric_total);
            }

            // Associate first metric with the weight of the last one, second with second last, etc
            int size = metric_list.Count - 1;
            for (int i = 0; i < metric_list.Count; i++)
            {
                metric_weights.Add(metric_list[size - i], reverser[metric_list[i]]);
            }
        }

        // Compare against another Person for metric similarity over a specific list
        public double Compare(Person other, string[] metric_list)
        {
            Console.WriteLine("---------");
            Console.WriteLine("Comparing Anon Answer to {0} metrics", Name);
            int matching_metrics = 0;
            double accuracy = 0;
            foreach (string metric in metric_list)
            {
                double mv = (double)(other.Metrics[0][metric]);
                Console.Write("{0}: {1} ", metric, mv);

                // If we're within reasonable standard deviation
                if (Math.Abs((double)goal_values[metric] - mv) < (double)standard_deviations[metric])
                {
                    matching_metrics++;
                    accuracy += (double)metric_weights[metric];
                    Console.WriteLine("(Acceptable)");
                }
                else
                {
                    Console.WriteLine("(Unacceptable)");
                }
            }

            Console.WriteLine("---------");
            Console.WriteLine("{0} Answer matches {1}'s style on {2}/{3} metrics ({4:f3}% sure)", 
                other.Name, this.Name, matching_metrics, metric_list.Length, accuracy * 100);

            return (double)matching_metrics / metric_list.Length;
        }

        /* Shizzle for printing shizzle and debug whatnot */
        public void PrintStdDevs() {
        // Print out the std devs, ordered
            Console.WriteLine("Standard deviations for metrics performed on " + Name + "\n");
            foreach (var score in standard_deviations.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }
        }

        public void PrintGoals()
        {
            Console.WriteLine("Goal values for metrics performed on " + Name + "\n");
            foreach (var score in goal_values.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }
        }

        public void PrintWeights()
        {
            Console.WriteLine("Metric weights for metrics performed on " + Name + "\n");
            foreach (var score in metric_weights.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }
        }

        public string Name
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public List<Hashtable> Metrics
        {
            get { return metrics; }
            set { metrics = value; }
        }
    }

}
