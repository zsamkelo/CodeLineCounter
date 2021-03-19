using System;
using System.Linq;

namespace CountingLines
{
  public interface ILineCounter
  {
    int Count(string code);
  }

  public class LineCounter : ILineCounter
  {
        public int Count(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return 0;
            }

            // ReSharper disable once ReplaceWithSingleCallToCount
            var lines = code
                .Replace("\r", "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var counter = 0;
            var insideComment = false;
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                
                if (IsSingleLineComment(trimmedLine))
                {
                    continue;
                }

                if (IfSingleLineContainsMultiLineComment(trimmedLine))
                {
                    trimmedLine = RemoveMultiLineCommentFromLine(trimmedLine);
                }

                if (IsEndOfMultiLineCommnet(trimmedLine))
                {
                    insideComment = false;
                    continue;
                } 

                if (IsEndOfMultiLineCommentInTheMendleOfCode(trimmedLine))
                {
                    insideComment = false;
                } 

                if (insideComment)
                {
                    continue;
                }
                
                if (IsMultiLineComment(trimmedLine))
                {
                    insideComment = true;
                    continue;
                } 
                if (IsMultiLineCommentAfterLineOfCode(trimmedLine))
                {
                    insideComment = true;
                }

                counter++;
            }

            return counter;
        }

        private static string RemoveMultiLineCommentFromLine(string trimmedLine)
        {
            var numberOfComments = trimmedLine.Count(f => f == '/') / 2;
            for (int i = 0; i < numberOfComments; i++)
            {
                var startOfComment = trimmedLine.IndexOf("/*");
                var endOfComment = trimmedLine.IndexOf("*/");
                if (startOfComment < 0) continue;
                string strToRemove = trimmedLine.Substring(startOfComment, endOfComment - startOfComment + 2);
                trimmedLine = trimmedLine.Replace(strToRemove, "");
            }

            return trimmedLine;
        }

        private static bool IfSingleLineContainsMultiLineComment(string trimmedLine)
        {
            return trimmedLine.Contains("/*") && trimmedLine.Contains("*/");
        }

        private static bool IsEndOfMultiLineCommentInTheMendleOfCode(string line)
        {
            return line.IndexOf("*/")>=0 && line.IndexOf("*/") < line.Length - 2;
        }

        private static bool IsEndOfMultiLineCommnet(string line)
        {
            return line.IndexOf("*/") >= 0 && line.IndexOf("*/")==line.Length - 2;
        }

        private static bool IsMultiLineCommentAfterLineOfCode(string line)
        {
            return line.IndexOf("/*")>0;
        }

        private static bool IsMultiLineComment(string line)
        {
            return line.IndexOf("/*")==0;
        }

        private static bool IsSingleLineComment(string line)
        {
            return line.StartsWith("//");
        }
    }
}
