using Model;
using NaiveBayesProject.Exceptions;
using NaiveBayesProject.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NaiveBayesProject.pipeline;

public class PipeLine
{
    private readonly string TrainPath;
    private readonly string? CsvInput;
    public NaiveBayesModel Model;
    public List<Dictionary<string, string>> LoadModel;
    public string TargetColumns;
    public string[] FeatureColumns;
    public PipeLine(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Error: No training file provided");
        }
        
        TrainPath = args[0];// this path contain tarin.csv file
        CheckSuffixFile(TrainPath);
        
        if (args.Length == 1)
        {
            // interactive moode
            Console.WriteLine($"Running interactive mode with training file: {TrainPath}");
        }
        else if (args.Length == 2)
        {
            // batch moode
            CsvInput = args[1]; // this path contain input.csv file
            CheckSuffixFile(CsvInput);
            Console.WriteLine($"Running batch mode with train file: {TrainPath} and input file: {CsvInput}");
        }
        else if (args.Length > 2)
        {
            throw new TooMuchArgumentException("Error: too mutch argouments in Command line");
        }
        LoadModel = CsvHandler.CsvReader(TrainPath);
        if (LoadModel.Count == 0) { throw new InvalidOperationException("Error can not operate an empty data"); }
        Console.WriteLine($"Model trained on {LoadModel.Count} rows");
        TargetColumns = CsvHandler.GetTargetColumnName(TrainPath);
        FeatureColumns = CsvHandler.GetFeatureColumnNames(TrainPath);
        Model = new NaiveBayesModel(LoadModel, TargetColumns);
    }
    
    public void CheckSuffixFile(string path)
    {
        if (!path.EndsWith(".csv"))
        {
            throw new FileNameFormatIllegal("Error: input file should end with .csv");
        }
    }

    public void BatchRunner()
    {
        try
        {
            List<Dictionary<string, string>> toFix = CsvHandler.CsvReader(CsvInput);
            List<Dictionary<string, string>> fixedTable = new();
            int rowcount = 1;
            foreach (Dictionary<string, string> line in toFix)
            {
                string answer = Model.Predict(line);
                Dictionary<string, string> fixedLine = line;
                fixedLine.Add(TargetColumns, answer);
                fixedTable.Add(fixedLine);
                Console.WriteLine($"row {rowcount}: {string.Join(", ", line.Values)} -> {answer}");
                rowcount++;
            }
            CsvHandler.WriteCsvFile(fixedTable);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (FileEmptyException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void InteractiveRunner()
    {
        Console.WriteLine("Please enter an input, enter white space to exit");
        bool stopProgram = false;
        Dictionary<string, string> lineDict;
        while (!stopProgram)
        {
            lineDict = new Dictionary<string, string>();
            for (int i=0;i<FeatureColumns.Length;i++)
            {
                Console.WriteLine($"Enter {FeatureColumns[i]}");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Stop the program...");
                    stopProgram = true;
                    break;
                }
                lineDict[FeatureColumns[i]] = char.ToUpper(input[0]) + input[1..];
            }
            if (!stopProgram)
            {
                string result = Model.Predict(lineDict);
                lineDict[TargetColumns] = result;
                string resultMultiLine = string.Join("\n", lineDict.Select(k => $"{k.Key}: {k.Value}"));
                Console.WriteLine(resultMultiLine);
            }
        }
    }
}

