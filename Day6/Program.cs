namespace Day6
{
  internal class Program
  {
    static void Main()
    {
      var input = File.ReadLines("input2.txt").ToArray();
      var values = input[..^1];

      var operations = input.Last();

      long grandTotal = 0;
      var current = new List<long>();
      for (var i = operations.Length - 1; i >= 0; i--)
      {
        var op = operations[i];
        var value = "";
        for (var j = 0; j < values.Length; j++)
        {
          value += values[j][i];
        }
        current.Add(long.Parse(value));

        if (op == '+')
        {
          var total = current.Sum();
          grandTotal += total;

          Console.WriteLine($"{string.Join(" + ", current)} = {total}");
          current = [];
          i--; // Skip the empty row.
        }
        else if (op == '*')
        {
          var total = current.Aggregate((t, v) => t * v);
          grandTotal += total;

          Console.WriteLine($"{string.Join(" * ", current)} = {total}");
          current = [];
          i--; // Skip the empty row.
        }
      }

      Console.WriteLine($"Grand Total: {grandTotal}");
    }

    public static void Part1()
    {
      var input = File.ReadLines("input2.txt").ToArray();
      var values = input[..^1].Select(t => t.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

      var operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

      long grandTotal = 0;
      for (var i = 0; i < operations.Length; i++)
      {
        // 0 - Adding; 1 - Multiplying;
        var operation = operations[i] == "+" ? 0 : 1;
        long total = operation;
        for (var j = 0; j < values.Length; j++)
        {
          var value = long.Parse(values[j][i]);
          Console.Write(value);
          if (operation == 0)
          {
            total += value;
            Console.Write(" + ");
          }
          else
          {
            total *= value;
            Console.Write(" * ");
          }
        }

        grandTotal += total;
        Console.WriteLine($" = {total}");
      }

      Console.WriteLine($"Grand Total: {grandTotal}");
    }
  }
}
