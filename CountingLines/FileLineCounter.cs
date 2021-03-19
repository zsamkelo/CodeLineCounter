
namespace CountingLines
{
  public interface IFileLineCounter
  {
    int Count(string filePath);
  }

  public class FileLineCounter : IFileLineCounter
  {
    private readonly IFileSystemAccess _fileSystem;
    private readonly ILineCounter _lineCounter;

    public FileLineCounter(
      IFileSystemAccess fileSystem,
      ILineCounter lineCounter)
    {
      _fileSystem = fileSystem;
      _lineCounter = lineCounter;
    }

    public int Count(string filePath)
    {
            var sourceCode = _fileSystem.ReadAllText(filePath);
            var lines = _lineCounter.Count(sourceCode);
            return lines;
    }
  }
}