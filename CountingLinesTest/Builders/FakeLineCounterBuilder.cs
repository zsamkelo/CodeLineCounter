using CountingLines;
using NSubstitute;

namespace CountingLinesTest.Builders
{
  public class FakeLineCounterBuilder
  {
    private ILineCounter _fake;

    public FakeLineCounterBuilder()
    {
      _fake = Substitute.For<ILineCounter>();
    }

    public ILineCounter Build()
    {
      return _fake;
    }
    
    public FakeLineCounterBuilder WithCountReturning(string contents, int lineCount)
    {
      _fake.Count(contents).Returns(lineCount);

      return this;
    }

    public static void VerifyCountWasCalledOn(ILineCounter fake, string expectedContents)
    {
      fake.Received(1).Count(expectedContents);
    }
  }
}