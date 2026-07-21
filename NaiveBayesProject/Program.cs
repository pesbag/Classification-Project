using NaiveBayesProject.Exceptions;
using NaiveBayesProject.pipeline;
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
            else if (args.Length == 2) 
            {
                pipeLine.BatchRunner();
            }
            else
            {
                throw new TooMuchArgumentException("Maximum 2 files");
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
    }
}
