namespace NaiveBayesProject.Interface
{
public interface IWriteToFile { static abstract void Write(List<Dictionary<string, string>> toWrite); }
public interface IReadFromFile { static abstract List<Dictionary<string, string>> Read(string fileName); }

}