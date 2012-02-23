using System;
using System.IO;
using Deanony.Metrics;
using System.Collections;
using System.Collections.Generic;

namespace Deanony.Data
{
    class ReadCorpus
    {
        public static List<Hashtable> AboutPerson(string name, int quantity)
        {
            // Grab test data
            List<Hashtable> training_data = new List<Hashtable>();
            for (int i = 1; i < quantity + 1; i++)
            {
                string data = File.ReadAllText("../../Corpus/" + SanitizeName(name) + "-" + i + ".txt");

                training_data.Add(new Hashtable()
                {
                    {"Vowel Frequency", FrequencyAnalysis.VowelFrequency(data)},
                    {"Consonant Frequency", FrequencyAnalysis.ConsonantFrequency(data)},
                    {"Digit Frequency", FrequencyAnalysis.DigitFrequency(data)},
                    {"Punctuation Frequency", FrequencyAnalysis.PunctuationFrequency(data)},
                    {"Spacing Frequency", FrequencyAnalysis.SpacingFrequency(data)},
                    {"Special Character Frequency", FrequencyAnalysis.SpecialCharacterFrequency(data)},
                    {"Word Count", FrequencyAnalysis.WordCount(data)},
                    {"Characters Per Word", FrequencyAnalysis.CharactersPerWord(data)},
                    {"Words Per Sentence", FrequencyAnalysis.WordsPerSentence(data)},
                    {"Preposition Frequency", FrequencyAnalysis.PrepositionFrequency(data)},
                    {"Pronoun Frequency", FrequencyAnalysis.PronounFrequency(data)},
                    {"Determiner Frequency", FrequencyAnalysis.DeterminerFrequency(data)},
                    {"Conjunction Frequency", FrequencyAnalysis.ConjunctionFrequency(data)},
                    {"Attribution Frequency", FrequencyAnalysis.AttributionFrequency(data)},
                    {"Link Frequency", FrequencyAnalysis.LinkFrequency(data)},
                    {"1-letter Words", FrequencyAnalysis.WordsOfLength(1, 1, data)},
                    {"2-letter Words", FrequencyAnalysis.WordsOfLength(2, 2, data)},
                    {"3-letter Words", FrequencyAnalysis.WordsOfLength(3, 3, data)},
                    {"4-letter Words", FrequencyAnalysis.WordsOfLength(4, 4, data)},
                    {"5-letter Words", FrequencyAnalysis.WordsOfLength(5, 5, data)},
                    {"6-letter Words", FrequencyAnalysis.WordsOfLength(6, 6, data)},
                    {"7-letter Words", FrequencyAnalysis.WordsOfLength(7, 7, data)},
                    {"8-10-letter Words", FrequencyAnalysis.WordsOfLength(8, 10, data)},
                    {"11-20-letter Words", FrequencyAnalysis.WordsOfLength(11, 20, data)}
                });

            }

            return training_data;
        }

        public static string SanitizeName(string name)
        {
            return name.Replace(' ', '-');
        }
    }
}
