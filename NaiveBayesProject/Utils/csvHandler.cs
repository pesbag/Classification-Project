using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NaiveBayesProject.Interface;

namespace NaiveBayesProject.Utils;
public class CsvHandler
{
    public static List<Dictionary<string, string>> CsvReader(string path)
    {
        string[] titleValues = [];
        Dictionary<string, string> dictOfLine = new Dictionary<string, string>();
        List<Dictionary<string, string>> csvContent = new List<Dictionary<string, string>>();
        using (StreamReader reader = new StreamReader(path))
        {
            string? line = reader.ReadLine();
            if (line != null)
            {
                titleValues = line.Split(",");
            }
            while (!reader.EndOfStream)
            {
                var NewLine = reader.ReadLine();
                var values = NewLine.Split(",");
                for (int i = 0; i < titleValues.Count(); i++)
                {
                    dictOfLine[titleValues[i]] = values[i];
                }
                csvContent.Append(dictOfLine);
            }
        }
        return csvContent;
    }
    public static string GetTargetColumnName(string path)
    {
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
        return titleValues[IndexOfLatestColumnName];
    } 
    public static string[] GetFeatureColumnNames(string path)
    {
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
    public static void WriteFile(List<Dictionary<string, string>> toWrite)
    {
        if (toWrite.Count == 0)
            return;
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output", "fixedTable.csv");
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
