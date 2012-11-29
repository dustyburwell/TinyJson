namespace TinyJson
{
   using System;
   using Framework;

   public static class Json
   {
      public static Object Parse(string json)
      {
         var result = new JsonParserFromString().All(json);

         if (result == null)
            return null;

         return result.Value;
      }

      public class ParseException : Exception
      {
         public ParseException(string message)
            : base(message)
         {
            
         }
      }
   }
}
