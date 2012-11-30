namespace TinyJson
{
   public class ArrayValue
   {
      public object[] Value
      {
         get; private set;
      } 
      
      public object this[int index]
      {
         get { return Value[index]; }
      }

      public ArrayValue(object[] value)
      {
         Value = value;
      }
   }
}