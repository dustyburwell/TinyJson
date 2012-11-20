namespace TinyJson.Framework
{
   public class Result<TInput, TValue> {
      public readonly TValue Value;
      public readonly TInput Rest;
      public Result(TValue value, TInput rest) { Value = value; Rest = rest; }
   }
}