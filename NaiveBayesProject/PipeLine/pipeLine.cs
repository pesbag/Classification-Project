using Model;
using NaiveBayesProject.Exceptions;
using NaiveBayesProject.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesProject.pipeline;

public class PipeLine
{
    private readonly string TrainPath;
    private readonly string? CsvInput;
    public NaiveBayesModel Model;
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
        List<Dictionary<string, string>> loadModel = CsvHandler.CsvReader(TrainPath);
        if (loadModel.Count == 0) { throw new InvalidOperationException("Error: can not operate an empty data"); }
        Console.WriteLine($"Model trained on {loadModel.Count} rows");
        string targetColumns = CsvHandler.GetTargetColumnName(TrainPath);
        Model = new NaiveBayesModel(loadModel, targetColumns);
    } 

    public void CheckSuffixFile(string path)
    {
        if (!path.EndsWith(".csv"))
        {
            throw new FileNameFormatIllegal("Error: input file should end with .csv");
        }
    }
    public void InteractiveRunner()
    {
        
    }
    public void BatchRunner()
    {
        
    }
}
