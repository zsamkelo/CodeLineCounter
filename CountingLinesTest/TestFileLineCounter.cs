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
        [TestCase(@"c:\test_files\code.cs")]
        [TestCase(@"e:\images\image.cs")]
        [TestCase(@"z:\music\song.cs")]
        public void ShouldReadAllLinesInGivenFile(string filePath)
        {
            var fileSystem = new FakeFileSystemAccessBuilder()
            .Build();

            var sut = CreateSut(fileSystem: fileSystem);
            sut.Count(filePath);

            FakeFileSystemAccessBuilder.VerifyReadAllTextWasCalledOn(fileSystem, filePath);
        }

        [TestCase(@"c:\test_files\code.cs", @"var x = 1")]
        [TestCase(@"e:\images\image.cs", @"var c = 465")]
        [TestCase(@"z:\music\song.cs", @"var q = 98")]
        public void ShouldCallLineCounterWithContentsOfFile(string filePath, string sourceCode)
        {
            var fileSystem = new FakeFileSystemAccessBuilder()
            .WithReadAllTextReturning(filePath, sourceCode)
            .Build();

            var counter = new FakeLineCounterBuilder()
              .Build();

            var sut = CreateSut(fileSystem: fileSystem, lineCounter: counter);
            sut.Count(filePath);

            FakeLineCounterBuilder.VerifyCountWasCalledOn(counter, sourceCode);
        }

        [TestCase(@"c:\test_files\code.cs", @"var x = 1", 697)]
        [TestCase(@"e:\images\image.cs", @"var c = 465", 785)]
        [TestCase(@"z:\music\song.cs", @"var q = 98", 10)]
        public void GivenPathWithKnownContents_ShouldReturnExpectedLines(string expectedPath, string sourceCode, int expectedLineCount)
        {
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

