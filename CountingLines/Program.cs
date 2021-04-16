using System;
using System.IO;

namespace CountingLines
{
  class Program
  {
    static void Main(string[] args)
    {
      var system = new FileSystemAccess();
      var fileLineCounter = new FileLineCounter(system, new LineCounter());
      var directoryLineCounter = new DirectoryLineCounter(
        system,
        fileLineCounter
      );

      var lines = 0;

      if (args.Length >= 2 && args[0] == "-f")
      {
        lines = fileLineCounter.Count(args[1]);
      }

      if (args.Length >= 2 && args[0] == "-d")
      {
        lines = directoryLineCounter.Count(args[1]);
      }

      Console.WriteLine($"Number of lines found = {lines}");

    }
  }
}
