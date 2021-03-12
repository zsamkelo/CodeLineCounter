﻿using System;
using System.Linq;

namespace CountingLines
{
    public class LineCounter
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

        private static bool IsEndOfMultiLineCommentInTheMendleOfCode(string line)
        {
            return line.IndexOf("*/")>=0 && line.IndexOf("*/") < line.Length - 2;
        }

        private static bool IsEndOfMultiLineCommnet(string line)
        {
            return line.IndexOf("*/")==line.Length - 2;
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