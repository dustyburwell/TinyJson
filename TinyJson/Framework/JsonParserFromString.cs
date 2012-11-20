namespace TinyJson.Framework
{
   public class JsonParserFromString : JsonParsers<string>
   {
      public override Parser<string, char> AnyChar
      {
         get 
         { 
            return input => {
                               if (input.Length > 0)
                                  return new Result<string, char>(input[0], input.Substring(1));

                               return null;
            };
         }
      }
   }
}