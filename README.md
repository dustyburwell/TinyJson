Well, that was fun, but shortly after starting TinyJson, I found [SimpleJson][5].
It's much more thorough and complete than TinyJson at this point. I don't plan on
continuing development of TinyJson because I have nothing to add here. :sparkles:

## Welcome to TinyJson

Introducing TinyJson: a self-contained, single-file Json parser written in C#.
TinyJson is designed to be a easy to use Json parser with no barriers to entry 
for library and small project developers. The goals of TinyJson are simple:

1. Provide a simple API for Json parsing and querying
2. Provide a file that can be included rather than a library to reference

### Motivation

Up to .Net 4.0 the only Json handling capability included in the BCL is
the [DataContractJsonSerializer][1]. It has known, documented [deficiencies][2]
and requires that you deseralize a whole type. Not to mention that, in this
developers opinion, it's a pain to use.That may be what you're looking for. If
so, go for it.

In .Net 4.5 Microsoft has added [JsonValue][3] to System.Runtime.Serialization.dll. 
It looks pretty promissing but, unless you're in a position to use .Net 4.5, 
this does you no good.

The current state of the art in dealing with Json appears to be NewtonSoft's
[Json.Net][4]. It's pretty great. But, let's face it, it's hosted on CodePlex.
Also, it requires that you add a reference to a Nuget package and dll.

Json.Net is great for most scenarios, but the simplified inclusion of TinyJson
enables its use in the development of libraries and small projects without 
forcing yet another dependency on consumers (dependency hell, anyone?).

Whether you're building a GitHub API client or an Asp.Net module for OAuth
authentication, chances are you're going to need to parse some Json. Do so
with TinyJson and it will make your own library easier to reference in the
future.

### Usage

TinyJson is a parser, not a serializer or deserializer...yet. Usage is as simple
as

    object value = TinyJson.Json.Parse(@"{""property"": 1234}");

`value` will be an `ObjectValue`, `ArrayValue`, string, double, bool, or null
depending on whether the input represents an object, array, string, number, bool,
or null, respectively. If the input was a primitive type, simply cast and use.
If, on the other hand, your input represents a Json object then you are going
to be lucky enough to use TinyJson's object query syntax. It's not that fancy
really. It's just 'dot' notation with array indexers.

#### Query Examples

Nested 'dot' notation

    var val = (ObjectValue)Json.Parse("{ \"property\": { \"property\": \"abc123\" } }");
    var prop = (string)val["property.property"];
    prop.Should().Be("abc123");
    
'dot' notation with array indexer

    var val = (ObjectValue)Json.Parse("{ \"property\": { \"property\": [1234] }}");
    var prop = (double)val["property.property[0]"];
    prop.Should().Be(1234);

[1]: http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer.aspx
[2]: http://stackoverflow.com/questions/4559991/any-way-to-make-datacontractjsonserializer-serialize-dictionaries-properly
[3]: http://msdn.microsoft.com/en-us/library/system.json.jsonvalue(v=vs.110).aspx
[4]: http://json.codeplex.com/
[5]: https://github.com/facebook-csharp-sdk/simple-json
