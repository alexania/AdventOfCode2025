namespace Day7
{
  internal class Program
  {
    static int _totalRows = 0;
    static int _totalCols = 0;
    static char[][] _grid;
    static long[,] _cache;


    static void Main(string[] args)
    {
      var input = File.ReadLines("input2.txt").ToArray();
      _grid = input.Select(t => t.ToCharArray()).ToArray();
      _totalRows = _grid.Length;

      var row = 0;
      var start = _grid[row];
      _totalCols = start.Length;
      long totalTimelines = 0;

      _cache = new long[_totalRows, _totalCols];
      for (var i = 0; i < _totalCols; i++)
      {
        var cell = start[i];
        if (cell == 'S')
        {
          totalTimelines = StartBeam(row + 2, i);
        }
      }
      Console.WriteLine($"Total Timelines: {totalTimelines}");
    }

    private static long StartBeam(int row, int col)
    {
      if (row >= _totalRows)
      {
        return 1;
      }
      if (_cache[row, col] > 0)
      {
        return _cache[row, col];
      }

      long totalTimelines = 0;
      var cell = _grid[row][col];
      if (cell == '.')
      {
        totalTimelines += StartBeam(row + 2, col);
      }
      else if (cell == '^')
      {
        if (col > 0)
        {
          totalTimelines += StartBeam(row + 2, col - 1);
        }
        if (col < _totalCols - 1)
        {
          totalTimelines += StartBeam(row + 2, col + 1);
        }
      }

      _cache[row, col] = totalTimelines;

      return totalTimelines;
    }

    private void Part1()
    {
      var input = File.ReadLines("input1.txt").ToArray();
      var grid = input.Select(t => t.ToCharArray()).ToArray();

      var row = 0;
      var start = grid[row];
      var totalSplits = 0;
      for (var i = 0; i < start.Length; i++)
      {
        var cell = start[i];
        if (cell == 'S')
        {
          StartBeam1(ref totalSplits, grid, row + 1, i);
        }
      }

      foreach (var line in grid)
      {
        Console.WriteLine(line);
      }

      Console.WriteLine($"Total Beams: {totalSplits}");
    }

    private static void StartBeam1(ref int totalSplits, char[][] grid, int row, int col)
    {
      if (row == grid.Length)
      {
        return;
      }

      var cell = grid[row][col];
      if (cell == '.')
      {
        grid[row][col] = '|';
        StartBeam1(ref totalSplits, grid, row + 1, col);
      }
      else if (cell == '^')
      {
        totalSplits++;
        if (col > 0)
        {
          grid[row][col - 1] = '|';
          StartBeam1(ref totalSplits, grid, row + 1, col - 1);
        }
        if (col < grid[row].Length - 1)
        {
          grid[row][col + 1] = '|';
          StartBeam1(ref totalSplits, grid, row + 1, col + 1);
        }
      }
    }
  }
}
