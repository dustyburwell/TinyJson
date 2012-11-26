namespace TinyJson
{
   public class ArrayValue
   {
      public object[] Value
      {
         get; private set;
      } 
      
      public ArrayValue(object[] value)
      {
         Value = value;
      }
   }
}