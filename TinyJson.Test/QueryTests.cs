using FluentAssertions;
using NUnit.Framework;

namespace TinyJson.Test
{
   public class QueryTests
   {
      [Test]
      public void IndexOneLevelDeep()
      {
         var val = (ObjectValue)Json.Parse("{ \"property\": 1234 }");
         var prop = (double)val["property"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeep()
      {
         var val = (ObjectValue)Json.Parse("{ \"property\": { \"property\": 1234 } }");
         var prop = (double)val["property.property"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexWithArrayIndex()
      {
         var val = (ObjectValue)Json.Parse("{ \"property\": [1234] }");
         var prop = (double)val["property[0]"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeepWithArrayIndex()
      {
         var val = (ObjectValue)Json.Parse("{ \"property\": { \"property\": [1234] }}");
         var prop = (double)val["property.property[0]"];

         prop.Should().Be(1234);
      }

      [Test]
      public void IndexTwoLevelsDeepWithArrayIndex2()
      {
         var val = (ObjectValue)Json.Parse("{ \"property\": [{ \"property\": 1234 }]}");
         var prop = (double)val["property[0].property"];

         prop.Should().Be(1234);
      }
   }
}