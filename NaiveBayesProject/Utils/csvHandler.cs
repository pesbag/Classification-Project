using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NaiveBayesProject.Interface;
using NaiveBayesProject.Exceptions;

namespace NaiveBayesProject.Utils;
public class CsvHandler
{
    public static List<Dictionary<string, string>> CsvReader(string fileName)
    {
        string[] titleValues = [];
        List<Dictionary<string, string>> csvContent = new List<Dictionary<string, string>>();
        string path =Path.Combine("input",fileName);
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
    public static string GetTargetColumnName(string fileName)
    {
        string[] titleValues =[];
        string path = Path.Combine("input", fileName);
        using (StreamReader reader = new StreamReader(path))
        {
            string? line = reader.ReadLine();
            if (line != null)
            {
                titleValues = line.Split(",");
            }
        }
        int IndexOfLatestColumnName = titleValues.Length;
        return titleValues[IndexOfLatestColumnName-1];
    } 
    public static string[] GetFeatureColumnNames(string fileName)
    {
        string[] allTitleValues = [];
        string path = Path.Combine("input", fileName);
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
}
