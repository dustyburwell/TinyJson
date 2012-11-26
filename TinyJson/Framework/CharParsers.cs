namespace TinyJson.Framework
{
   using System;

   public abstract class CharParsers<TInput> : Parsers<TInput>
   {
      public abstract Parser<TInput, char> AnyChar { get; }
      public Parser<TInput, char> Char(char ch)
      {
         return from c in AnyChar where c == ch select c;
      }
      public Parser<TInput, char> Char(Predicate<char> pred)
      {
         return from c in AnyChar where pred(c) select c;
      }
   }
}