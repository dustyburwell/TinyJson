using FluentAssertions;
using NUnit.Framework;
using TinyJson.Framework;

namespace TinyJson.Test
{
   [TestFixture]
   public class ParserTest
   {
      [Test]
      public void EmptyIsNull()
      {
         var val = Json.InnerParse("");

         val.Should().BeNull();
      }

      [Test]
      public void Number()
      {
         var val = Json.InnerParse("1234");

         ((double) val).Should().Be(1234);
      }

      [Test]
      public void True()
      {
         var val = Json.InnerParse("true");

         ((bool) val).Should().BeTrue();
      }

      [Test]
      public void False()
      {
         var val = Json.InnerParse("false");

         ((bool)val).Should().BeFalse();
      }

      [Test]
      public void Null()
      {
         var val = Json.InnerParse("null");

         val.Should().BeNull();
      }

      [Test]
      public void NumberWithWhitespace()
      {
         var val = Json.InnerParse("   \t\r\n1234");

         ((double)val).Should().Be(1234);
      }

      [Test]
      public void String()
      {
         var val = Json.InnerParse("\"Hello, world!\"");

         ((string)val).Should().Be("Hello, world!");
      }

      [Test]
      public void StringWithWhitespace()
      {
         var val = Json.InnerParse("    \t\r\n\"Hello, world!\"");

         ((string)val).Should().Be("Hello, world!");
      }

      [Test]
      public void ArrayOfNumbers()
      {
         var val = Json.InnerParse("[1234, 6789]");

         ((double) ((ArrayValue) val).Value[0]).Should().Be(1234);
         ((double) ((ArrayValue) val).Value[1]).Should().Be(6789);
      }

      [Test]
      public void ArrayWithWhitespace()
      {
         var val = Json.InnerParse("\r\n\t     [1234, 6789]");

         ((double)((ArrayValue)val).Value[0]).Should().Be(1234);
         ((double)((ArrayValue)val).Value[1]).Should().Be(6789);
      }

      [Test]
      public void ArrayOfBool()
      {
         var val = Json.InnerParse("[true, false, true]");

         ((bool)((ArrayValue)val).Value[0]).Should().Be(true);
         ((bool)((ArrayValue)val).Value[1]).Should().Be(false);
         ((bool)((ArrayValue)val).Value[2]).Should().Be(true);
      }

      [Test]
      public void ArrayOfNull()
      {
         var val = Json.InnerParse("[null, null]");

         ((ArrayValue)val).Value[0].Should().Be(null);
         ((ArrayValue)val).Value[1].Should().Be(null);
      }

      [Test]
      public void ArrayOfStrings()
      {
         var val = Json.InnerParse("[\"abc\", \"123\"]");

         ((string)((ArrayValue)val).Value[0]).Should().Be("abc");
         ((string)((ArrayValue)val).Value[1]).Should().Be("123");
      }

      [Test]
      public void ArrayOfArrays()
      {
         var val = Json.InnerParse("[[1], [2], [3]]");

         ((double)((ArrayValue)((ArrayValue)val).Value[0]).Value[0]).Should().Be(1);
         ((double)((ArrayValue)((ArrayValue)val).Value[1]).Value[0]).Should().Be(2);
         ((double)((ArrayValue)((ArrayValue)val).Value[2]).Value[0]).Should().Be(3);
      }

      [Test]
      public void MixedArray()
      {
         var val = Json.InnerParse("[1234, true, null, \"ABCD\", [1]]");

         ((double)((ArrayValue)val).Value[0]).Should().Be(1234);
         ((bool)((ArrayValue)val).Value[1]).Should().Be(true);
         ((ArrayValue)val).Value[2].Should().BeNull();
         ((string)((ArrayValue)val).Value[3]).Should().Be("ABCD");
         ((double)((ArrayValue)((ArrayValue)val).Value[4]).Value[0]).Should().Be(1);
      }

      [Test]
      public void EmptyObject()
      {
         var val = Json.InnerParse("{   \r\n\t}");

         ((ObjectValue) val).Value.Should().BeEmpty();
      }

      [Test]
      public void Object()
      {
         var val = Json.InnerParse("{ \"property\" : 1234 }");

         ((double)((ObjectValue) val).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void ObjectWithWhitespace()
      {
         var val = Json.InnerParse("{  \r\n\t \"property\" \r\n\t : \t\r\n 1234 \r\t\n }");

         ((double)((ObjectValue)val).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void NestedObject()
      {
         var val = Json.InnerParse("{ \"property\": { \"property\": 1234 } }");

         ((double)((ObjectValue)((ObjectValue)val).Value["property"]).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void Property()
      {
         var val = new JsonParserFromString().Prop("\"property\":1234");
         val.Value.Key.Should().Be("property");
         ((double)val.Value.Value).Should().Be(1234);
      }

      [Test]
      public void IndexOneLevelDeep()
      {
         var val = (ObjectValue)Json.InnerParse("{ \"property\": 1234 }");
         var prop = (double) val["property"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeep()
      {
         var val = (ObjectValue)Json.InnerParse("{ \"property\": { \"property\": 1234 } }");
         var prop = (double)val["property.property"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexWithArrayIndex()
      {
         var val = (ObjectValue)Json.InnerParse("{ \"property\": [1234] }");
         var prop = (double)val["property[0]"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeepWithArrayIndex()
      {
         var val = (ObjectValue)Json.InnerParse("{ \"property\": { \"property\": [1234] }}");
         var prop = (double)val["property.property[0]"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeepWithArrayIndex2()
      {
         var val = (ObjectValue)Json.InnerParse("{ \"property\": [{ \"property\": 1234 }]}");
         var prop = (double)val["property[0].property"];

         prop.Should().Be(1234);
      }
   }
}
