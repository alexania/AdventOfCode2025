namespace Day5
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var input = File.ReadLines("input2.txt").ToArray();

      var freshRanges = new List<FreshRange>();
      var freshMode = true;
      long freshCount = 0;
      var minRange = long.MaxValue;
      var maxRange = long.MinValue;

      foreach (var line in input)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          freshMode = false;
          break;
        }

        if (freshMode)
        {
          var range = line.Split('-');
          var start = long.Parse(range[0]);
          var end = long.Parse(range[1]);
          freshRanges.Add(new(start, end));

          if (start < minRange)
            minRange = start;
          if (end > maxRange)
            maxRange = end;
        }
        else
        {
          var value = long.Parse(line);
          foreach (var freshRange in freshRanges)
          {
            if (freshRange.InRange(value))
            {
              Console.WriteLine($"{value,15} found in {freshRange}.");
              freshCount++;
              break;
            }
          }
        }
      }

      var maxFreshRanges = new List<FreshRange>();
      foreach (var freshRange in freshRanges.OrderBy(t => t.Start))
      {
        // Find a fresh range that full encompasses this one.
        var overlapRange = maxFreshRanges.FirstOrDefault(t => t.Start <= freshRange.Start && t.End >= freshRange.End);
        if (overlapRange != null)
        {
          // This fresh range is already full accounted for.
          Console.WriteLine($"{freshRange,32}: overlaps {overlapRange}");
          continue;
        }

        // Find a fresh range where it overlaps with either the start or the end.
        var overlapRanges = maxFreshRanges.Where(t => 
          (freshRange.Start <= t.Start && freshRange.End >= t.Start) ||
          (freshRange.End >= t.End && freshRange.Start <= t.End)).ToArray();

        if (overlapRanges.Any())
        {
          // if it overlaps the Start.
          var newOverlap = freshRange;
          foreach (var overlap in overlapRanges)
          {
            newOverlap = newOverlap.Merge(overlap);
            maxFreshRanges.Remove(overlap);
          }
          maxFreshRanges.Add(newOverlap);
          continue;
        }

        // otherwise, it's a new range.
        Console.WriteLine($"{freshRange,32}: NEW");
        maxFreshRanges.Add(new(freshRange.Start, freshRange.End));
      }

      foreach (var freshRange in maxFreshRanges)
      {
        Console.WriteLine($"{freshRange,32} : {freshRange.Size}");
        freshCount += freshRange.Size;
      }

      // Not 334552644886626, too low
      //BruteForce(ref freshRanges, ref freshCount, minRange, maxRange);

      Console.WriteLine($"Fresh Count: {freshCount}");
    }

    private static void BruteForce(ref List<FreshRange> freshRanges, ref int freshCount, long minRange, long maxRange)
    {
      var rangeSize = maxRange - minRange;
      var rangeStep = Math.Floor(rangeSize / 100m);
      for (var value = minRange; value <= maxRange; value++)
      {
        foreach (var freshRange in freshRanges)
        {
          if (freshRange.InRange(value))
          {
            //Console.WriteLine($"{value,15} found in {freshRange}.");
            freshCount++;
            break;
          }
        }

        if (value % rangeStep == 0)
        {
          Console.Write($".");
        }
      }
    }
  }

  internal class FreshRange(long start, long end)
  {
    public long Size => End - Start + 1;
    public long Start { get; } = start;
    public long End { get; } = end;

    public bool InRange(long value)
    {
      return value >= Start && value <= End;
    }

    public FreshRange Merge(FreshRange range)
    {
      var newRange = new FreshRange(Math.Min(Start, range.Start), Math.Max(End, range.End));
      Console.WriteLine($"MERGE: ({this,31}) + ({range,31}): ({newRange,31})");
      return newRange;
    }

    public override string ToString()
    {
      return $"{Start,15}-{End,15}";
    }
  }
}
