using CountingLines;
using CountingLinesTest.Builders;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CountingLinesTest
{
  [TestFixture]
  public class TestFileLineCounter
  {
    [Test]
    public void ShouldReadAllLinesInGivenFile()
    {
      var expectedPath = @"c:\test_files\code.cs";

      var fileSystem = new FakeFileSystemAccessBuilder()
        .Build();
      
      var sut = CreateSut(fileSystem: fileSystem);
      sut.Count(expectedPath);

      FakeFileSystemAccessBuilder.VerifyReadAllTextWasCalledOn(fileSystem, expectedPath);
    }

    [Test]
    public void ShouldCallLineCounterWithContentsOfFile()
    {
      var expectedPath = @"c:\test_files\code.cs";
      var sourceCode = @"var x = 1";

      var fileSystem = new FakeFileSystemAccessBuilder()
        .WithReadAllTextReturning(expectedPath, sourceCode)
        .Build();

      var counter = new FakeLineCounterBuilder()
        .Build();

      var sut = CreateSut(fileSystem: fileSystem, lineCounter: counter);
      sut.Count(expectedPath);

      FakeLineCounterBuilder.VerifyCountWasCalledOn(counter, sourceCode);
    }

    [Test]
    public void GivenPathWithKnownContents_ShouldReturnExpectedLines()
    {
      var expectedPath = @"c:\test_files\code.cs";
      var sourceCode = @"var x = 1";
      var expectedLineCount = 123;

      var fileSystem = new FakeFileSystemAccessBuilder()
        .WithReadAllTextReturning(expectedPath, sourceCode)
        .Build();

      var counter = new FakeLineCounterBuilder()
        .WithCountReturning(sourceCode, expectedLineCount)
        .Build();

      var sut = CreateSut(fileSystem: fileSystem, lineCounter: counter);

      var actual = sut.Count(expectedPath);

      Assert.AreEqual(expectedLineCount, actual);
    }

    private static FileLineCounter CreateSut(
      IFileSystemAccess fileSystem = null,
      ILineCounter lineCounter = null
    )
    {
      fileSystem ??= new FakeFileSystemAccessBuilder().Build();
      lineCounter ??= new FakeLineCounterBuilder().Build();

      return new FileLineCounter(fileSystem, lineCounter);
    }
  }
}

