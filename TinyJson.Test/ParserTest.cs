using System;
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
         var val = Json.Parse("");

         val.Should().BeNull();
      }

      [Test]
      public void Number()
      {
         var val = Json.Parse("1234");

         ((double) val).Should().Be(1234);
      }

      [Test]
      public void True()
      {
         var val = Json.Parse("true");

         ((bool) val).Should().BeTrue();
      }

      [Test]
      public void False()
      {
         var val = Json.Parse("false");

         ((bool)val).Should().BeFalse();
      }

      [Test]
      public void Null()
      {
         var val = Json.Parse("null");

         val.Should().BeNull();
      }

      [Test]
      public void NumberWithWhitespace()
      {
         var val = Json.Parse("   \t\r\n1234");

         ((double)val).Should().Be(1234);
      }

      [Test]
      public void String()
      {
         var val = Json.Parse("\"Hello, world!\"");

         ((string)val).Should().Be("Hello, world!");
      }

      [Test]
      public void StringWithWhitespace()
      {
         var val = Json.Parse("    \t\r\n\"Hello, world!\"");

         ((string)val).Should().Be("Hello, world!");
      }

      [Test]
      public void StringWithEscapeCharacters()
      {
         var val = Json.Parse("\"Hello,\\r\\n\\t\\\"\\\\\\/ world!\"");

         ((string)val).Should().Be("Hello,\r\n\t\"\\/ world!");
      }

      [Test]
      public void ArrayOfNumbers()
      {
         var val = Json.Parse("[1234, 6789]");

         ((double) ((ArrayValue) val).Value[0]).Should().Be(1234);
         ((double) ((ArrayValue) val).Value[1]).Should().Be(6789);
      }

      [Test]
      public void ArrayWithWhitespace()
      {
         var val = Json.Parse("\r\n\t     [1234, 6789]");

         ((double)((ArrayValue)val).Value[0]).Should().Be(1234);
         ((double)((ArrayValue)val).Value[1]).Should().Be(6789);
      }

      [Test]
      public void ArrayOfBool()
      {
         var val = Json.Parse("[true, false, true]");

         ((bool)((ArrayValue)val).Value[0]).Should().Be(true);
         ((bool)((ArrayValue)val).Value[1]).Should().Be(false);
         ((bool)((ArrayValue)val).Value[2]).Should().Be(true);
      }

      [Test]
      public void ArrayOfNull()
      {
         var val = Json.Parse("[null, null]");

         ((ArrayValue)val).Value[0].Should().Be(null);
         ((ArrayValue)val).Value[1].Should().Be(null);
      }

      [Test]
      public void ArrayOfStrings()
      {
         var val = Json.Parse("[\"abc\", \"123\"]");

         ((string)((ArrayValue)val).Value[0]).Should().Be("abc");
         ((string)((ArrayValue)val).Value[1]).Should().Be("123");
      }

      [Test]
      public void ArrayOfArrays()
      {
         var val = Json.Parse("[[1], [2], [3]]");

         ((double)((ArrayValue)((ArrayValue)val).Value[0]).Value[0]).Should().Be(1);
         ((double)((ArrayValue)((ArrayValue)val).Value[1]).Value[0]).Should().Be(2);
         ((double)((ArrayValue)((ArrayValue)val).Value[2]).Value[0]).Should().Be(3);
      }

      [Test]
      public void MixedArray()
      {
         var val = Json.Parse("[1234, true, null, \"ABCD\", [1]]");

         ((double)((ArrayValue)val).Value[0]).Should().Be(1234);
         ((bool)((ArrayValue)val).Value[1]).Should().Be(true);
         ((ArrayValue)val).Value[2].Should().BeNull();
         ((string)((ArrayValue)val).Value[3]).Should().Be("ABCD");
         ((double)((ArrayValue)((ArrayValue)val).Value[4]).Value[0]).Should().Be(1);
      }

      [Test]
      public void EmptyObject()
      {
         var val = Json.Parse("{   \r\n\t}");

         ((ObjectValue) val).Value.Should().BeEmpty();
      }

      [Test]
      public void Object()
      {
         var val = Json.Parse("{ \"property\" : 1234 }");

         ((double)((ObjectValue) val).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void ObjectWithWhitespace()
      {
         var val = Json.Parse("{  \r\n\t \"property\" \r\n\t : \t\r\n 1234 \r\t\n }");

         ((double)((ObjectValue)val).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void NestedObject()
      {
         var val = Json.Parse("{ \"property\": { \"property\": 1234 } }");

         ((double)((ObjectValue)((ObjectValue)val).Value["property"]).Value["property"]).Should().Be(1234);
      }

      [Test]
      public void Property()
      {
         var val = new JsonParserFromString().Prop("\"property\":1234");
         val.Value.Key.Should().Be("property");
         ((double)val.Value.Value).Should().Be(1234);
      }
   }
}
