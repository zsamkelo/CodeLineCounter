using System;
using System.IO;

namespace CountingLines
{
  class Program
  {
    static void Main(string[] args)
    {
      var filePath = @"E:\Chillisoft\DeliberatePractice\ironpdf\ironpdf\Program.cs";

      var fileCounter = new FileLineCounter(
        new FileSystemAccess(),
        new LineCounter()
      );

      var lines = fileCounter.Count(filePath);
      Console.WriteLine($"Number of lines in file = {lines}");
    }
  }
}
