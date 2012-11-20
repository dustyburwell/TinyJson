using System;

namespace TinyJson
{
   public class ArrayValue
   {
      public Object[] Value
      {
         get; private set;
      } 
      
      public ArrayValue(Object[] value)
      {
         Value = value;
      }
   }
}