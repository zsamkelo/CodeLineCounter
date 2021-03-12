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

      var lines = 0;

      if (args.Length >= 2 && args[0] == "-f")
      {
        //count a file
      }

      if (args.Length >= 2 && args[0] == "-d")
      {
        lines = counter.Count(args[1]);
      }

      Console.WriteLine($"Number of lines in file = {lines}");

    }
  }
}
