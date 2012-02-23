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
        List<Hashtable> standard_deviations;

        // Goal values for each metric (average)
        List<Hashtable> goal_values;

        public Person(string name)
        {
            identifier = name;

            // Initialize
            metrics = new List<Hashtable>();
            standard_deviations = new List<Hashtable>();
            goal_values = new List<Hashtable>();
        }

        // Order to minimize standard deviation to determine which metrics identify this person best
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

                // Calculate std deviation
                standard_deviations.Add(metric, StandardDeviation(metric_score));
                metric_goals.Add(metric, metric_score.Average());
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

        private static double StandardDeviation(List<double> valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 1));
        }
    }

}
