using System;
using CountingLines;
using NUnit.Framework;

namespace CountingLinesTest
{
  [TestFixture]
  public class TestFileSystemAccess
  {
    [TestFixture]
    public class ReadAllText
    {
      [Test]
      public void GivenKnownPathToSampleTextFile_ShouldReturnKnownText()
      {
        // Arrange
        var path = @".\SampleData\file1_in_root.txt";
        var expected = @"This is the contents of the file 1 text file in the root folder

line 3!";
        var sut = CreateSut();
        // Act
        var actual = sut.ReadAllText(path);
        // Assert
        Assert.AreEqual(expected, actual);
      }
    }

    [TestFixture]
    public class GetFilesInFolder
    {
      [Test]
      public void EnsureMethodReturnsAllFilesInSubFolders()
      {
        // Arrange
        var expectedNumberOfFile = 6;
        var sut = CreateSut();
        // Act
        var actual = sut.GetFilesInFolder(@".\SampleData", "*.*");
        // Assert
        Assert.AreEqual(expectedNumberOfFile, actual.Length);
      }
    }

    private static FileSystemAccess CreateSut()
    {
      return new FileSystemAccess();
    }
  }
}