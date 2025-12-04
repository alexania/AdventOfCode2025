namespace Day4
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var input = File.ReadLines("input2.txt").ToArray();

      var rowLength = input[0].Length;
      var numRolls = 4;
      var availableRolls = 0;
      var round = 1;

      Console.WriteLine($"Round {round}");
      (var newInput, var removedRolls) = RemoveRolls(input, rowLength, numRolls);
      availableRolls += removedRolls;

      Console.WriteLine($"Removed {removedRolls} Rolls - {availableRolls} Total");

      while (removedRolls > 0)
      {
        input = newInput;

        Console.WriteLine($"\nRound {++round}");
        (newInput, removedRolls) = RemoveRolls(input, rowLength, numRolls);
        availableRolls += removedRolls;

        Console.WriteLine($"Removed {removedRolls} Rolls - {availableRolls} Total");
      }

      Console.WriteLine($"\nAvailable Rolls: {availableRolls}");
    }

    private static (string[] newInput, int removedRolls) RemoveRolls(string[] input, int rowLength, int numRolls)
    {
      var newInput = new string[input.Length];
      var removedRolls = 0;
      for (var i = 0; i < input.Length; i++)
      {
        var newRow = "";
        for (var j = 0; j < rowLength; j++)
        {
          if (input[i][j] == '.' || input[i][j] == 'x')
          {
            newRow += '.';
            Console.Write('.');
            continue;
          }

          var surrounding = GetSurrounding(input, rowLength, i, j);
          if (surrounding < numRolls)
          {
            newRow += 'x';
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('x');
            Console.ResetColor();
            removedRolls++;
          }
          else
          {
            newRow += '@';
            Console.Write('@');
          }
        }
        Console.WriteLine();
        newInput[i] = newRow;
      }
      return (newInput, removedRolls);
    }

    private static int GetSurrounding(string[] input, int rowLength, int i, int j)
    {
      var surrounding = 0;
      for (var row = Math.Max(0, i - 1); row <= Math.Min(input.Length - 1, i + 1); row++)
      {
        for (var col = Math.Max(0, j - 1); col <= Math.Min(rowLength - 1, j + 1); col++)
        {
          if (row == i && col == j)
            continue;

          if (input[row][col] == '@')
          {
            surrounding++;
          }
        }
      }

      return surrounding;
    }
  }
}
