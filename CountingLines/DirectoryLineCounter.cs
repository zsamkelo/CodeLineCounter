namespace CountingLines
{
  public class DirectoryLineCounter
  {
    private readonly IFileSystemAccess _fileSystem;
    private readonly IFileLineCounter _fileLineCounter;

    public DirectoryLineCounter(
      IFileSystemAccess fileSystem,
      IFileLineCounter fileLineCounter)
    {
      _fileSystem = fileSystem;
      _fileLineCounter = fileLineCounter;
    }

    public int Count(string folderPath)
    {
      var totalLines = 0;
      var files = _fileSystem.GetFilesInFolder(folderPath, "*.cs");
      
      foreach (var file in files)
      {
        totalLines += _fileLineCounter.Count(file);
      }

      return totalLines;
    }
  }
}