using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesProject.Utils
{
    class GetTargetColumnName
    {
        public static string Get(string filename)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "..", "..", "..", "input", filename);
            string[] titleValues = [];
            using (StreamReader reader = new StreamReader(path))
            {

                string? line = reader.ReadLine();
                if (line != null)
                {
                    titleValues = line.Split(",");
                }
            }
            int IndexOfLatestColumnName = titleValues.Length;
            return titleValues[IndexOfLatestColumnName - 1];
        }
    }
}
