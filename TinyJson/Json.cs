using System;
using System.IO;
using TinyJson.Framework;

namespace TinyJson
{
   public static class Json
   {
      public static Object Parse<T>(string json)
      {
         return Parse<T>(new StringReader(json));
      }

      public static Object Parse<T>(Stream stream)
      {
         return Parse<T>(new StreamReader(stream));
      }

      public static Object Parse<T>(TextReader rdr)
      {
         return InnerParse(rdr);
      }

      public static Object InnerParse(TextReader rdr)
      {
         return InnerParse(rdr.ReadToEnd());
      }

      public static Object InnerParse(string json)
      {
         var result = new JsonParserFromString().All(json);

         if (result == null)
            return null;

         return result.Value;
      }
   }
}
