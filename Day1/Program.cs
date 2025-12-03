
namespace Day1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var input = File.ReadAllLines("input2.txt");
      var dial = 50;
      var password = 0;

      foreach (var rotation in input)
      {
        var direction = rotation[0] == 'L' ? -1 : 1;
        var steps = int.Parse(rotation[1..]);

        var oldDial = dial;
        (dial, var clicks) = Rotate(direction, steps, oldDial);
        Console.WriteLine($"Rotated from {oldDial} to {dial} ({rotation}) + {clicks} password");

        password += clicks;
      }

      Console.WriteLine($"The password is: {password}");
    }

    private static (int newDial, int clicks) Rotate(int direction, int steps, int dial)
    {
      var newValue = dial + (direction * steps);
      var newDial = (newValue + 10000000) % 100;
      var distance = direction > 0 ? 100 - dial : dial;

      if (steps < distance)
      {
        return (newDial, 0);
      }
      if (steps == distance)
      {
        return (newDial, 1);
      }

      var clicks = 1;
      steps = steps - distance;

      clicks += (int)Math.Floor(steps / 100m);
      if (dial == 0 && direction == -1)
      {
        clicks--;
      }

      return (newDial, clicks);
    }
  }
}
