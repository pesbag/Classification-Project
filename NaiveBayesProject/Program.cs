using NaiveBayesProject.Exceptions;
using NaiveBayesProject.pipeline;
using System;
//using NaiveBayesProject.input;
namespace NaiveBayesProject;
public class Program
{
    public static void Main(string[] args) 
    {
        try
        {
            PipeLine pipeLine = new PipeLine(args);
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

    }
}