using CountingLines;
using NSubstitute;

namespace CountingLinesTest.Builders
{
  public class FakeFileLineCounterBuilder
  {
    private IFileLineCounter _fake;

    public FakeFileLineCounterBuilder()
    {
      _fake = Substitute.For<IFileLineCounter>();
    }

    public IFileLineCounter Build()
    {
      return _fake;
    }
    
    public FakeFileLineCounterBuilder WithCountReturning(string filePath, int lineCount)
    {
      _fake.Count(filePath).Returns(lineCount);

      return this;
    }

    public static void VerifyCountWasCalledOn(IFileLineCounter fake, string expectedFilePath)
    {
      fake.Received(1).Count(expectedFilePath);
    }
  }
}