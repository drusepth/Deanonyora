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
                "Spacing Frequency", "Special Character Frequency", 
                //"Word Count", "Words Per Sentence", "Characters Per Word",
                "Preposition Frequency", "Pronoun Frequency", "Determiner Frequency",
                "Conjunction Frequency", "Attribution Frequency", "Link Frequency", "1-letter Words", 
                "2-letter Words", "3-letter Words", "4-letter Words", "5-letter Words", "6-letter Words",
                "7-letter Words", "8-10-letter Words", "11-20-letter Words"
            };

            // Start with our training data
            var trainer = new Person("Yishan Wong");
            trainer.Metrics = Data.ReadCorpus.AboutPerson(trainer.Name, 25);
            trainer.CalculateGoalValues(metrics);

            // Debugging: print stuff out
            trainer.PrintGoals();
            trainer.PrintStdDevs();
            trainer.PrintWeights();

            // Look at the Anon Answer and compare it to the expected values
            var anon = new Person("Anon");
            anon.Metrics = Data.ReadCorpus.AboutPerson("Anon", 1);

            // Compare anon answer to training data
            trainer.Compare(anon, metrics);

            // Pause
            Console.Read();
        }
    }
}
