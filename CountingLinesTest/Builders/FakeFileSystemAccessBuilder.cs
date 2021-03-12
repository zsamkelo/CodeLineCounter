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

    public static void VerifyReadAllTextWasCalledOn(IFileSystemAccess fake, string expectedPath)
    {
      fake.Received(1).ReadAllText(expectedPath);
    }
  }
}