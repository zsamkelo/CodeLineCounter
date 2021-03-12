using System.IO;

namespace CountingLines
{
  public interface IFileSystemAccess
  {
    string ReadAllText(string filePath);
    string[] GetFilesInFolder(string folderPath, string searchPattern);
  }

  public class FileSystemAccess : IFileSystemAccess
  {
    public string ReadAllText(string filePath)
    {
      return File.ReadAllText(filePath);
    }
    public string[] GetFilesInFolder(string folderPath, string searchPattern)
    {
      return Directory.GetFiles(folderPath, searchPattern);
    }
  }
}