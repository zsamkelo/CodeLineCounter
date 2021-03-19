using CountingLines;
using NUnit.Framework;

namespace CountingLinesTest
{
    public class TestLineCounter
    {
        [TestCase("/**/var ha = 123;/**/",1)]
        [TestCase("/*asdas*/var ha = 123;/*sadasd*/",1)]
        [TestCase("/*asdas*/var ha = 123;/*sadasd*/var ha = 123;/*5344345*/", 1)]
        [TestCase("/**/var ha = 123;/*sadasd*/",1)]
        [TestCase("/*dsssdfsdsdf*/var sameAsAboveButJustInCase = 123;",1)]
        [TestCase("/**/var sameAsAbove;/**/ButJustInCase = 123;",1)]
        public void GivenManyMultiLineCommentsOnASingleLine_ShouldReturnOne(string code, int expected)
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(code);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenEmptyStringShouldReturnZero()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count("");
            Assert.AreEqual(0, actual);
        }

        private static LineCounter CreateLineCounter()
        {
            var linecounter = new LineCounter();
            return linecounter;
        }

        [Test]
        public void GivenOneLineOfCodeShouldReturnOne()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count("x = 1;");
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void GivenTwoLinesOfCodeShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"x = 1;
y = 2;");
            Assert.AreEqual(2, actual);
        }       
        
        [Test]
        public void GivenTwoLinesOfCodeAndEmptyLineInBetweenShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"x = 1;

y = 2;");
            Assert.AreEqual(2, actual);
        }       
        
        [Test]
        public void GivenThreeLinesOfCodeShouldReturnThree()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"x = 1;
z = 3;
y = 2;");
            Assert.AreEqual(3, actual);
        }        
        
        [Test]
        public void GivenThreeLinesOfCodeStartingWithNewLineShouldReturnThree()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
x = 1;
z = 3;
y = 2;");
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void GivenTwoLinesOfCodeWithOneLineContainingTwoVariablesShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"x = 1;z = 3;
y = 2;");
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void GivenThreeLinesOfCodeWithOneLineCommentedOutShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
x = 1;
//z = 3;
y = 2;");
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void GivenThreeLinesOfCodeWithSecondLineEndingWithACommentShouldReturn3()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
x = 1;
e = 9;//z = 3;
y = 2;");
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void GivenThreeIndentedLinesOfCodeWithOneLineCommentedOutShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
    x = 1;
    //z = 3;
    y = 2;");
            Assert.AreEqual(2, actual);
        }
        [Test]
        public void GivenFourLinesOfCodeWithTwoLinesCommentedOutShouldReturnTwo()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
    x = 1;
/*b = 5;
    z = 3;*/
    y = 2;");
            Assert.AreEqual(2, actual);
        } 
        
        [Test]
        public void GivenFourLinesOfCodeWithThreeLinesCommentedOutShouldReturnOne()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
    x = 1;
/*b = 5;
    z = 3;
    y = 2;*/");
            Assert.AreEqual(1, actual);
        }       
        
        [Test]
        public void GivenStartOfCommentNotOfStartOfLineShouldStillStartTheComment()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
    x = 1;/*b = 5;
    z = 3;
    y = 2;*/");
            Assert.AreEqual(1, actual);
        }        
        
        [Test]
        public void GivenEndOfCommentNotOfEndOfLineShouldStillCountTheLine()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"
    x = 1;/*b = 5;
    z = 3;*/y = 2;");
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void GivenOpenCurlyBracket_ShouldCountAsALine()
        {
            var linecounter = CreateLineCounter();
            var actual = linecounter.Count(@"{");
            Assert.AreEqual(1, actual);
        }
    }
}