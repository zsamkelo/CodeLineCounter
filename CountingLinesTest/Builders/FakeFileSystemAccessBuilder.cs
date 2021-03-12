using CountingLines;
using NSubstitute;

namespace CountingLinesTest.Builders
{
  public class FakeFileSystemAccessBuilder
  {
    private IFileSystemAccess _fake;

    public FakeFileSystemAccessBuilder()
    {
      _fake = Substitute.For<IFileSystemAccess>();
    }

    public IFileSystemAccess Build()
    {
      return _fake;
    }

    public FakeFileSystemAccessBuilder WithReadAllTextReturning(string path, string fileContents)
    {
      _fake.ReadAllText(path).Returns(fileContents);

      return this;
    }
    public FakeFileSystemAccessBuilder WithGetFilesInFolderReturning(string path, string[] files)
    {
      _fake.GetFilesInFolder(path, Arg.Any<string>()).Returns(files);

      return this;
    }

    public static void VerifyReadAllTextWasCalledOn(IFileSystemAccess fake, string expectedPath)
    {
      fake.Received(1).ReadAllText(expectedPath);
    }

    public static void VerifyGetFilesInFolderWasCalledOn(IFileSystemAccess fake, string expectedPath)
    {
      fake.Received(1).GetFilesInFolder(expectedPath, Arg.Any<string>());
    }

    public static void VerifyGetFilesInFolderWasCalledOn(IFileSystemAccess fake, string expectedPath, string expectedPattern)
    {
      fake.Received(1).GetFilesInFolder(expectedPath, expectedPattern);
    }
  }
}