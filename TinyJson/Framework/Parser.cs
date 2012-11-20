namespace TinyJson.Framework
{
   public delegate Result<TInput, TValue> Parser<TInput, TValue>(TInput input);
}