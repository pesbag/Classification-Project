using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesProject.Utils
{
    class GetFeatureColumnNames
    {
        public static string[] Get(string filename)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "..", "..", "..", "input", filename);
            string[] allTitleValues = [];
            using (StreamReader reader = new StreamReader(path))
            {
                string? line = reader.ReadLine();
                if (line != null)
                {
                    allTitleValues = line.Split(",");
                }
                string[] onlyFeatureVaules = new string[allTitleValues.Length - 1];
                for (int i = 0; i < allTitleValues.Length - 1; i++)
                {
                    onlyFeatureVaules[i] = allTitleValues[i];
                }
                return onlyFeatureVaules;
            }
        }
    }
}
