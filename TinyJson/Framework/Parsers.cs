using System.Linq;

namespace TinyJson.Framework
{
   public abstract class Parsers<TInput>
   {
      public Parser<TInput, TValue> Succeed<TValue>(TValue value)
      {
         return input => new Result<TInput, TValue>(value, input);
      }
      public Parser<TInput, TValue[]> Rep<TValue>(Parser<TInput, TValue> parser)
      {
         return Rep1(parser)
            .OR(Succeed(new TValue[0]));
      }
      public Parser<TInput, TValue[]> Rep1<TValue>(Parser<TInput, TValue> parser)
      {
         var q = from x in parser
                 from xs in Rep(parser)
                 select (new[] { x }).Concat(xs).ToArray();

         return q;
      }
   }
}