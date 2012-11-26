namespace TinyJson
{
   using System;
   using System.Collections.Generic;
   using System.Text.RegularExpressions;

   public class ObjectValue
   {
      public IDictionary<string, Object> Value
      {
         get; private set;
      } 
      
      public ObjectValue(IDictionary<string, Object> value)
      {
         Value = value;
      }

      public Object this[string path]
      {
         get
         {
            if (string.IsNullOrEmpty(path))
               return this;

            var paths = path.Split(new[] {'.'}, 2);

            try
            {
               var match = Regex.Match(paths[0], @"(.*)\[(\d+)\]");
               object value;

               if (match.Success)
               {
                  value = Value[match.Groups[1].Value];

                  if (match.Groups.Count == 3 && value is ArrayValue)
                     value = ((ArrayValue) value).Value[int.Parse(match.Groups[2].Value)];
               }
               else
               {
                  value = Value[paths[0]];
               }

               if (paths.Length == 2 && value is ObjectValue)
                  return ((ObjectValue)value)[paths[1]];

               return value;
            }
            catch (Exception)
            {
               throw new PathNotFound();
            }
         }
      }

      public class PathNotFound : Exception
      {
      }
   }
}