
namespace Day2
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var input = File.ReadLines("input2.txt").First();
      var ranges = input.Split(',').Select(ParseRange);

      long invalidSum = 0;
      foreach (var range in ranges)
      {
        for (long i = range.First; i <= range.Last; i++)
        {
          if (!IsValid(i.ToString()))
          {
            invalidSum += i;
            Console.WriteLine($"Invalid code: {i}");
          }
        }
      }

      Console.WriteLine($"Sum of invalid codes: {invalidSum}");
    }

    private static Range ParseRange(string input, int index)
    {
      var parts = input.Split('-');
      var first = long.Parse(parts[0]);
      var last = long.Parse(parts[1]);
      return new(first, last);
    }

    private static bool IsValid(string code)
    {
      if (code[0] == '0')
      {
        return false;
      }

      var length = code.Length;
      for (var i = 2; i <= length; i++)
      {
        if (HasRepeats(code, i))
        {
          return false;
        }
      }

      return true;
    }

    private static bool HasRepeats(string code, int numChunks)
    {
      var length = code.Length;
      if (length % numChunks != 0)
      {
        return false;
      }

      var chunkSize = (int)(length * 1f / numChunks);
      var chunks = Chunk(code, chunkSize);

      var firstChunk = chunks.First();
      return chunks.Reduce((acc, chunk) => acc && chunk == firstChunk, true);
    }

    static IEnumerable<string> Chunk(string str, int chunkSize)
    {
      for (int i = 0; i < str.Length; i += chunkSize)
        yield return str.Substring(i, chunkSize);
    }
  }

  public record Range(long First, long Last);
}
