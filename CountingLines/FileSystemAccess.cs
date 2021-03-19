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
      //return File.ReadAllText(filePath);
      return File.ReadAllText("");
    }
    public string[] GetFilesInFolder(string folderPath, string searchPattern)
    {
      //return Directory.GetFiles(folderPath, searchPattern);
      return Directory.GetFiles("", "");
    }
  }
}