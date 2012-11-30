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

[1]: http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer.aspx
[2]: http://stackoverflow.com/questions/4559991/any-way-to-make-datacontractjsonserializer-serialize-dictionaries-properly
[3]: http://msdn.microsoft.com/en-us/library/system.json.jsonvalue(v=vs.110).aspx
[4]: http://json.codeplex.com/