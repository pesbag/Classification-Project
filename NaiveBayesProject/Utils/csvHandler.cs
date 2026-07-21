using NaiveBayesProject.Exceptions;
namespace NaiveBayesProject.Utils;
public class CsvHandler
{
    public static List<Dictionary<string, string>> CsvReader(string fileName)
    {
        string[] titleValues = [];
        List<Dictionary<string, string>> csvContent = new List<Dictionary<string, string>>();
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDir, "..", "..", "..", "input", fileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The file '{path}' was not found.");
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
                var NewLine = reader.ReadLine();
                var values = NewLine.Split(",");
                for (int i = 0; i < titleValues.Count(); i++)
                {
                    dictOfLine[titleValues[i]] = values[i];
                }
                csvContent.Add(dictOfLine);
            }
        }  
        return csvContent;
    }
    public static string GetTargetColumnName(string filename)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDir, "..", "..", "..", "input", filename);
        string[] titleValues =[] ;
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
    public static string[] GetFeatureColumnNames(string filename)
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
            for (int i=0;i< allTitleValues.Length - 1; i++)
            {
                onlyFeatureVaules[i] = allTitleValues[i];
            }
            return onlyFeatureVaules;
        }
    }
    public static void WriteCsvFile(List<Dictionary<string, string>> toWrite)
    {
        if (toWrite.Count == 0)
            return;
        string path = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,"output","fixedTable.csv");
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(string.Join(",", toWrite[0].Keys));
            foreach (Dictionary<string, string> row in toWrite)
            {
                writer.WriteLine(string.Join(",",row.Values.Select(value => $"\"{value.Replace("\"", "\"\"")}\"")));
            }
        }
    }
}
