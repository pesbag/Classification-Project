using System;
namespace NaiveBayesProject.Exceptions;
public class TooMuchArgumentException:Exception
{
    public TooMuchArgumentException(string message):base(message){}
}
public class FileNameFormatIllegal : Exception
{
    public FileNameFormatIllegal(string message) : base(message) { }
}