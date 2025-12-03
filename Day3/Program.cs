
using static System.Net.Mime.MediaTypeNames;

namespace Day3
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var input = File.ReadLines("input2.txt");

      long totalJoltage = 0;
      foreach (var bank in input)
      {
        var largest = GetLargest(bank, 12);
        var joltage = long.Parse(string.Join("", largest.Select(t => t.joltage)));
        totalJoltage += joltage;

        WriteWithHighlights(bank, joltage, [.. largest.Select(t => t.position)]);
      }

      Console.WriteLine($"Total Joltage: {totalJoltage}");
    }

    private static void WriteWithHighlights(string text, long joltage, params int[] indexes)
    {
      for (int k = 0; k < text.Length; k++)
      {
        if (indexes.Contains(k))
        {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write(text[k]);
          Console.ResetColor(); // reset back to default
        }
        else
        {
          Console.Write(text[k]);
        }
      }

      Console.WriteLine($" - {joltage}");
    }

    private static (int joltage, int position)[] GetLargest(string bank, int numBatteries)
    {
      var largest = new (int joltage, int position)[numBatteries];
      for (int i = 0; i < bank.Length; i++)
      {
        var battery = bank[i];
        var joltage = battery - '0';

        for (var rank = 0; rank < numBatteries; rank++)
        {
          if (i > bank.Length - numBatteries + rank)
          {
            continue;
          }

          if (CompareRank(largest, rank, numBatteries, joltage, i))
          {
            break;
          }
        }
      }

      return largest;
    }

    private static bool CompareRank((int joltage, int position)[] largest, int rank, int numBatteries, int joltage, int position)
    {
      if (joltage > largest[rank].joltage)
      {
        for (var i = rank + 1; i < numBatteries; i++)
        {
          largest[i] = (joltage: 0,  position: -1);
        }
        largest[rank] = (joltage, position);
        return true;
      }

      return false;
    }
  }
}
