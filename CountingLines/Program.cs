using System;
using System.IO;

namespace CountingLines
{
  class Program
  {
    static void Main(string[] args)
    {

      var system = new FileSystemAccess();
      var counter = new DirectoryLineCounter(
        system,
        new FileLineCounter(system, new LineCounter())
      );

      var lines = counter.Count(@"E:\Chillisoft\DeliberatePractice\game-of-life\GameOfLife");
      Console.WriteLine($"Number of lines in file = {lines}");

    }
  }
}
