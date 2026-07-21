using NaiveBayesProject.Exceptions;
using NaiveBayesProject.Interface;
namespace NaiveBayesProject.Utils
{
    public class ReadFromCsv : IReadFromFile
    {
        public static List<Dictionary<string, string>> Read(string fileName)
        {
            string[] titleValues = [];
            List<Dictionary<string, string>> csvContent = new List<Dictionary<string, string>>();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "..", "..", "..", "input", fileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' was not found");
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string? line = reader.ReadLine();
                if (new FileInfo(path).Length == 0)
                {
                    throw new FileEmptyException("Error: empty file!");
                }
                if (line != null)
                {
                    titleValues = line.Split(",");
                }
                while (!reader.EndOfStream)
                {
                    Dictionary<string, string> dictOfLine = new Dictionary<string, string>();
                    string? NewLine = reader.ReadLine();
                    string[] values = NewLine!.Split(",");
                    for (int i = 0; i < titleValues.Count(); i++)
                    {
                        dictOfLine[titleValues[i]] = values[i];
                    }
                    csvContent.Add(dictOfLine);
                }
            }
            return csvContent;
        }
    }
}
