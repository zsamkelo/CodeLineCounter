using System.IO;

namespace CountingLines
{
  public interface IFileSystemAccess
  {
    string ReadAllText(string filePath);
  }

  public class FileSystemAccess : IFileSystemAccess
  {
    public string ReadAllText(string filePath)
    {
      return File.ReadAllText(filePath);
    }
  }
}