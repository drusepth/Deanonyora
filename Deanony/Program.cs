using System;
using System.IO;
using Deanony.Metrics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Deanony
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of metrics to use for this person
            var metrics = new string[] { 
                "Vowel Frequency", "Consonant Frequency", "Digit Frequency", "Punctuation Frequency",
                "Spacing Frequency", "Special Character Frequency", "Word Count", "Characters Per Word",
                "Words Per Sentence", "Preposition Frequency", "Pronoun Frequency", "Determiner Frequency",
                "Conjunction Frequency", "Attribution Frequency", "Link Frequency", "1-letter Words", 
                "2-letter Words", "3-letter Words", "4-letter Words", "5-letter Words", "6-letter Words",
                "7-letter Words", "8-10-letter Words", "11-20-letter Words"
            };

            var trainer = new Person("Yishan Wong");
            trainer.Metrics = Data.ReadCorpus.AboutPerson(trainer.Name, 20);

            Hashtable metric_scores = new Hashtable();
            Hashtable metric_goals = new Hashtable();

            

            // Print out the std devs, ordered
            Console.WriteLine("Standard deviations for metrics performed on " + "Yishan Wong" + "\n");
            foreach (var score in metric_scores.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }

            Console.WriteLine("Goal values for metrics performed on " + "Yishan Wong" + "\n");
            foreach (var score in metric_goals.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList())
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }

            // Look at the Anon Answer and compare it to the expected values
            List<Hashtable> anon_metrics = Data.ReadCorpus.AboutPerson("Anon", 1);

            Console.WriteLine("---------");
            Console.WriteLine("Comparing Anon Answer to Yishan Wong metrics");
            int matching_metrics = 0;
            foreach (string metric in metrics)
            {
                double mv = (double)(anon_metrics[0][metric]);
                Console.Write("{0}: {1} ", metric, mv);

                // If we're within reasonable standard deviation
                if (Math.Abs((double)metric_goals[metric] - mv) < (double)metric_scores[metric])
                {
                    matching_metrics++;
                    Console.WriteLine("(Acceptable)");
                }
                else
                {
                    Console.WriteLine("(Unacceptable)");
                }
            }

            Console.WriteLine("---------");
            Console.WriteLine("Anon Answer matches Yishan Wong's style on {0}/{1} metrics ({2:f3}%)", matching_metrics, metrics.Length, (double)matching_metrics / metrics.Length);

            // Pause
            Console.Read();
        }
    }
}
