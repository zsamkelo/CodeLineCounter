using CountingLines;
using CountingLinesTest.Builders;
using NSubstitute;
using NUnit.Framework;

namespace CountingLinesTest
{
  [TestFixture]
  public class TestDirectoryLineCounter
  {
    [Test]
    public void ShouldGetListOfAllFilesInGivenFolder()
    {
      var path = @"c:\test_files\";

      var fileSystem = new FakeFileSystemAccessBuilder()
        .Build();

      var sut = CreateSut(fileSystem: fileSystem);
      sut.Count(path);

      FakeFileSystemAccessBuilder.VerifyGetFilesInFolderWasCalledOn(fileSystem, path);
    }

    [Test]
    public void ShouldOnlyGetDotCsFilesFromFileSystem()
    {
      var path = @"c:\test_files\";
      var pattern = "*.cs";

      var fileSystem = new FakeFileSystemAccessBuilder()
        .Build();

      var sut = CreateSut(fileSystem: fileSystem);
      sut.Count(path);

      FakeFileSystemAccessBuilder.VerifyGetFilesInFolderWasCalledOn(fileSystem, path, pattern);
    }

    [Test]
    public void ShouldCallFileLineCounterForEachFileInTheFolder()
    {
      var path = @"c:\test_files\";
      var filePath1 = @"c:\test_files\program.cs";
      var filePath2 = @"c:\test_files\logic.cs";

      var fileSystem = new FakeFileSystemAccessBuilder()
        .WithGetFilesInFolderReturning(path, new []{ filePath1 , filePath2 })
        .Build();

      var fileLineCounter = new FakeFileLineCounterBuilder()
        .Build();

      var sut = CreateSut(fileSystem: fileSystem, fileLineCounter: fileLineCounter);
      sut.Count(path);

      FakeFileLineCounterBuilder.VerifyCountWasCalledOn(fileLineCounter, filePath1);
      FakeFileLineCounterBuilder.VerifyCountWasCalledOn(fileLineCounter, filePath2);
    }

    [Test]
    public void ShouldSumAllLineCountsTogetherAndReturn()
    {
      var path = @"c:\test_files\";
      var filePath1 = @"c:\test_files\program.cs";
      var file1Lines = 10;
      var filePath2 = @"c:\test_files\logic.cs";
      var file2Lines = 7;

      var fileSystem = new FakeFileSystemAccessBuilder()
        .WithGetFilesInFolderReturning(path, new []{ filePath1 , filePath2 })
        .Build();

      var fileLineCounter = new FakeFileLineCounterBuilder()
        .WithCountReturning(filePath1, file1Lines)
        .WithCountReturning(filePath2, file2Lines)
        .Build();

      var sut = CreateSut(fileSystem: fileSystem, fileLineCounter: fileLineCounter);
      var actual = sut.Count(path);

      var expected = 17;
      Assert.AreEqual(expected, actual);
    }


    private static DirectoryLineCounter CreateSut(
      IFileSystemAccess fileSystem = null,
      IFileLineCounter fileLineCounter = null
    )
    {
      fileSystem ??= new FakeFileSystemAccessBuilder().Build();
      fileLineCounter ??= Substitute.For<IFileLineCounter>();

      return new DirectoryLineCounter(fileSystem, fileLineCounter);
    }
  }
}