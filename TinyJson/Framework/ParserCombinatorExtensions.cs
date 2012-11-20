namespace TinyJson.Framework
{
   public static class ParserCombinatorExtensions
   {
      public static Parser<TInput, TValue> OR<TInput, TValue>(
         this Parser<TInput, TValue> parser1,
         Parser<TInput, TValue> parser2)
      {
         return input => {
                            var result = parser1(input);

                            if (result != null)
                               return result;

                            return parser2(input);
         };
      }
      public static Parser<TInput, TValue2> AND<TInput, TValue1, TValue2>(
         this Parser<TInput, TValue1> parser1,
         Parser<TInput, TValue2> parser2)
      {
         return input => {
                            var result = parser1(input);

                            return parser2(result.Rest);
         };
      }
   }
}