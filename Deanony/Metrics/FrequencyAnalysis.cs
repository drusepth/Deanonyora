using System;
using System.Linq;
using System.Collections.Generic;

namespace Deanony.Metrics
{
    static class FrequencyAnalysis
    {
        public static double SpacingFrequency(string data)
        {
            char[] spaces = { ' ', '\n', '\t', '\r' };
            return (Convert.ToDouble(Frequency(spaces, data)) / data.Length);
        }

        public static double VowelFrequency(string data)
        {
            char[] vowels = {'a', 'e', 'i', 'o', 'u'};
            return (Convert.ToDouble(Frequency(vowels, data)) / data.Length);
        }

        public static double ConsonantFrequency(string data)
        {
            char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p',
                                  'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
            return (Convert.ToDouble(Frequency(consonants, data)) / data.Length);
        }

        public static double DigitFrequency(string data)
        {
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return (Convert.ToDouble(Frequency(digits, data)) / data.Length);
        }

        public static double PunctuationFrequency(string data)
        {
            char[] punctuation = { '.', ',', '?', '!', ';', '-', '"', '(', ')', ':' };
            return (Convert.ToDouble(Frequency(punctuation, data)) / data.Length);
        }

        public static double SpecialCharacterFrequency(string data)
        {
            char[] special = { '$', '^', '#', '@', '&', '~' };
            return (Convert.ToDouble(Frequency(special, data)) / data.Length);
        }

        public static double PrepositionFrequency(string data)
        {
            string[] prepositions = { 
                                        "about", "across", "against", "along", "around",
                                        "at", "behind", "beside", "besides", "by", "despite",
                                        "down", "during", "for", "from", "in", "inside", "into",
                                        "near", "of", "off", "on", "onto", "over", "through", 
                                        "to", "toward", "with", "within", "without"
                                    };
            return (Convert.ToDouble(Frequency(prepositions, data)) / ContentExtraction.Words(data).Length);
        }

        public static double PronounFrequency(string data)
        {
            string[] pronouns = { 
                                        "I", "you", "he", "me", "her", "him", "my", "mine", "her",
                                        "hers", "his", "myself", "himself", "herself", "anything",
                                        "everything", "anyone", "everyone", "ones", "such", "it",
                                        "we", "they", "us", "them", "our", "ours", "their", "theirs",
                                        "itself", "ourselves", "themselves", "something", "nothing",
                                        "someone"
                                    };
            return (Convert.ToDouble(Frequency(pronouns, data)) / ContentExtraction.Words(data).Length);
        }

        public static double DeterminerFrequency(string data)
        {
            string[] determiners = { 
                                        "the", "some", "this", "that", "every", "all", "both", "one",
                                        "first", "other", "next", "many", "much", "more", "most", 
                                        "several", "no", "a", "an", "any", "each", "half", "twice",
                                        "two", "second", "another", "last", "few", "little", "less",
                                        "least", "own"
                                    };
            return (Convert.ToDouble(Frequency(determiners, data)) / ContentExtraction.Words(data).Length);
        }

        public static double ConjunctionFrequency(string data)
        {
            string[] conjunctions = {
                                        "and", "but", "after", "when", "as", "because", "if", "what", "where",
                                        "which", "how", "than", "or", "so", "before", "since", "while", 
                                        "although", "though", "who", "whose"
                                    };
            return (Convert.ToDouble(Frequency(conjunctions, data)) / ContentExtraction.Words(data).Length);
        }

        public static double AttributionFrequency(string data)
        {
            string[] tags = {
                                        "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]"
                                    };
            return (Convert.ToDouble(SuffixFrequency(tags, data)) / 
                ContentExtraction.Words(data).Length);
        }

        public static double LinkFrequency(string data)
        {
            string[] prefixes = {
                                    "http://", "https", "www."
                                };
            return (Convert.ToDouble(PrefixFrequency(prefixes, data)) / ContentExtraction.Words(data).Length);
        }

        public static double WordCount(string data)
        {
            return ContentExtraction.Words(data).Length;
        }

        public static double CharactersPerWord(string data)
        {
            string[] words = ContentExtraction.Words(data);

            if (words.Length == 0) { return 0; }

            double word_length = 0;
            foreach (string word in words)
            {
                word_length += word.Length;
            }

            return (word_length / words.Length);
        }

        public static double WordsPerSentence(string data)
        {
            string[] sentences = ContentExtraction.Sentences(data);

            if (sentences.Length == 0) { return 0; }

            int word_count = 0;
            foreach (string sentence in sentences)
            {
                word_count += ContentExtraction.Words(sentence).Length;
            }

            return (Convert.ToDouble(word_count) / sentences.Length);
        }

        public static double WordsOfLength(int min, int max, string data)
        {
            string[] words = ContentExtraction.Words(data);

            int count = 0;
            foreach (string word in words)
            {
                if (word.Length >= min && word.Length <= max)
                {
                    count++;
                }
            }

            return (double)count / ContentExtraction.Words(data).Length;
        }

        private static int Frequency(char[] chars, string data)
        {
            int count = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (chars.Contains<char>(data[i]))
                {
                    count++;
                }
            }

            return count;
        }

        private static int Frequency(string[] words, string data)
        {
            int count = 0;

            string[] corpus_words = ContentExtraction.Words(data);

            foreach (string word in corpus_words) 
            {
                if (words.Contains(word))
                {
                    count++;
                }
            }

            return count;
        }

        private static int PrefixFrequency(string[] prefixes, string data)
        {
            int count = 0;

            string[] corpus_words = ContentExtraction.Words(data);

            foreach (string word in corpus_words)
            {
                foreach (string prefix in prefixes)
                {
                    if (word.Length - prefix.Length < 0)
                    {
                        continue;
                    }

                    if (word.Substring(0, prefix.Length) == prefix)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static int SuffixFrequency(string[] suffixes, string data)
        {
            int count = 0;

            string[] corpus_words = ContentExtraction.Words(data);

            foreach (string word in corpus_words)
            {
                foreach (string prefix in suffixes)
                {
                    if (word.Length - prefix.Length < 0)
                    {
                        continue;
                    }

                    if (word.Substring(word.Length - prefix.Length) == prefix)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

    }
}
