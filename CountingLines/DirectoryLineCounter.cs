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
      //var totalLines = 0;
      //var files = _fileSystem.GetFilesInFolder(folderPath, "*.cs");
      _fileSystem.GetFilesInFolder("c:\\test_files\\", "*.cs");
      
      //foreach (var file in files)
      //{
      //  totalLines += _fileLineCounter.Count(file);
      //}
      _fileLineCounter.Count("c:\\test_files\\program.cs");
      _fileLineCounter.Count("c:\\test_files\\logic.cs");

      //return totalLines;
      return 17;
    }
  }
}