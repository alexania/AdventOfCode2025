namespace Day2
{
  internal static class Extensions
  {
    public static TResult Reduce<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TResult, TSource, TResult> func,
        TResult initialValue)
    {
      ArgumentNullException.ThrowIfNull(source);
      ArgumentNullException.ThrowIfNull(func);

      TResult accumulator = initialValue;

      foreach (var item in source)
      {
        accumulator = func(accumulator, item);
      }

      return accumulator;
    }
  }
}
