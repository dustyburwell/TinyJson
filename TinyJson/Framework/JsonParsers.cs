namespace TinyJson.Framework
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   public abstract class JsonParsers<TInput> : CharParsers<TInput>
   {
      protected JsonParsers()
      {
         Whitespace = Rep(Char(' ').OR(Char('\t').OR(Char('\n')).OR(Char('\r'))));
         Id = from w in Whitespace
              from c in Char(char.IsLetter)
              from cs in Rep(Char(char.IsLetter))
              select cs.Aggregate(c.ToString(), (acc, ch) => acc + ch);
         WsChr = chr => Whitespace.AND(Char(chr));
         PString = from begin in WsChr('"')
                   from cs in Rep(Char(c => c != '\"'))
                   from end in Char('"')
                   select (Object)(cs.Aggregate(string.Empty, (acc, ch) => acc + ch));
         PNumber = from whitespace in Whitespace
                   from n in Char(char.IsDigit)
                   from ns in Rep(Char(char.IsDigit))
                   select (Object)double.Parse(ns.Aggregate(n.ToString(), (acc, ch) => acc + ch));
         PArray = from begin in WsChr('[')
                  from values in
                     Rep(from value in Value
                         from comma in Rep(Char(','))
                         select value)
                  from end in WsChr(']')
                  select (Object)new ArrayValue(values);
         PObject = from begin in WsChr('{')
                   from props in
                      Rep(from prop in Prop
                          from comma in Rep(Char(','))
                          select prop)
                   from end in WsChr('}')
                   select (Object)new ObjectValue(props.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
         Prop = from identifier in PString
                from colon in WsChr(':')
                from value in Value
                from comma in Rep(Char(','))
                select new KeyValuePair<string, Object>((string)identifier, value);
         PBool = from id in Id
                 where (id == "true" || id == "false")
                 select (Object)bool.Parse(id);
         PNull = from id in Id
                 where (id == "null")
                 select (Object)null;
         Value = PNumber
            .OR(PArray)
            .OR(PString)
            .OR(PObject)
            .OR(PBool)
            .OR(PNull);
         All = Value;
      }

      public Parser<TInput, char[]> Whitespace;
      public Func<char, Parser<TInput, char>> WsChr;
      public Parser<TInput, string> Id;
      public Parser<TInput, Object> Value;
      public Parser<TInput, Object> PString;
      public Parser<TInput, Object> PNumber;
      public Parser<TInput, Object> PObject;
      public Parser<TInput, Object> PArray;
      public Parser<TInput, Object> PBool;
      public Parser<TInput, Object> PNull;
      public Parser<TInput, KeyValuePair<string, Object>> Prop;
      public Parser<TInput, Object> All;
   }
}