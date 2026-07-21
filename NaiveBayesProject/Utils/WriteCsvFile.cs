using NaiveBayesProject.Interface;
namespace NaiveBayesProject.Utils
{
    class WriteCsvFile : IWriteToFile
    {
        public static void Write(List<Dictionary<string, string>> toWrite)
        {
            if (toWrite.Count == 0)
                return;
            string path = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "output", "predictions.csv");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(",", toWrite[0].Keys));
                foreach (Dictionary<string, string> row in toWrite)
                {
                    writer.WriteLine(string.Join(",", row.Values.Select(value => $"\"{value.Replace("\"", "\"\"")}\"")));
                }
            }
        }
    }
}
