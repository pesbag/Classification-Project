using NaiveBayesProject.Exceptions;
using NaiveBayesProject.pipeline;
using System;
using NaiveBayesProject.Utils;
//using NaiveBayesProject.input;
namespace NaiveBayesProject;
public class Program
{
    public static void Main(string[] args) 
    {
        try
        {
            PipeLine pipeLine = new PipeLine(args);
            if (args.Length == 1)
            {
                pipeLine.InteractiveRunner();
            }
            else
            {
                pipeLine.BatchRunner();
            }
        }
        catch (TooMuchArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (FileNameFormatIllegal ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Console.WriteLine("Enter to main");
        //List<Dictionary<string, string>> loadModel=CsvHandler.CsvReader(args[0]);
        //Console.WriteLine(args[0]);
        //Console.WriteLine("load model in main");
        //Console.WriteLine($"length of list: {loadModel.Count}");
        //foreach (var x in loadModel)
        //{
        //    Console.WriteLine(x.ToString());
        //}
    }
}