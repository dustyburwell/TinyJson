using FluentAssertions;
using NUnit.Framework;

namespace TinyJson.Test
{
   [TestFixture]
   public class ErrorTests
   {
      [Test]
      public void UnClosedObject()
      {
         new object().Invoking(o => Json.Parse("{ \"property\": 1234 "))
            .ShouldThrow<Json.ParseException>();
      }

      [Test]
      public void UnClosedArray()
      {
         new object().Invoking(o => Json.Parse("[ 1, 2, 3, 4"))
            .ShouldThrow<Json.ParseException>();
      }

      [Test]
      public void UnClosedString()
      {
         new object().Invoking(o => Json.Parse("\"blah blah"))
            .ShouldThrow<Json.ParseException>();
      }

//      [Test]
//      public void NotAValue()
//      {
//         new object().Invoking(o => Json.Parse("blah blah"))
//            .ShouldThrow<Json.ParseException>();
//      }
   }
}