using NaiveBayesProject.Interface;
using NaiveBayesProject.Utils;

namespace NaiveBayesProject.Utils
{
    internal class WriteCsvFile : IWriteToFile
    {
        public static void Write(List<Dictionary<string, string>> toWrite)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "..", "..", "..", "output", "output.csv");
            using (StreamWriter writer = new StreamWriter(path))
            {
                List<string> keys = toWrite[0].Keys.ToList();
                writer.WriteLine(String.Join(",", keys));
                foreach (Dictionary<string, string> dict in toWrite)
                {
                    List<string> line = dict.Values.ToList();
                    writer.WriteLine(String.Join(",", line));
                }
            }
        }
    }
}