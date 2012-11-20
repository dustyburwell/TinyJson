using System.IO;

namespace TinyJson.Framework
{
   public class JsonParserFromTextReader : JsonParsers<TextReader>
   {
      public override Parser<TextReader, char> AnyChar
      {
         get
         {
            return input => {
                               if (input.Peek() < 0)
                                  return null;

                               return new Result<TextReader, char>((char)input.Read(), input);
            };
         }
      }
   }
}