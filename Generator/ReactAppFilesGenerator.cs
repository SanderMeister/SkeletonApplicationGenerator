// using System;
// using System.Collections.Generic;
// using System.Text;
// using System.IO;
// using Generator.Models;
// using System.Text.RegularExpressions;

// namespace Generator
// {
//     public class CodeGeneratorHelper
//     {

//         public static string CreateProgramFile(string name)
//         {
//                 var code = new StringBuilder(@$"using System;
// using System.Threading.Tasks;
// using Grpc.Core;

// namespace {name}
// {{
//     public class Program 
//     {{
//         const int Port = 30051;

//         public static void Main(string[] args)
//         {{
//             Server server = new Server
//             {{
//                 Services = {{ {name}Service.BindService(new {name}ServiceImpl()) }},
//                 Ports = {{ new ServerPort(""localhost"", Port, ServerCredentials.Insecure) }}
//             }};
//             server.Start();

//             Console.WriteLine(""{name} Server listening on port "" + Port);
//             Console.WriteLine(""Press any key to stop the server..."");
//             Console.ReadKey();

//             server.ShutdownAsync().Wait();
//         }}
//     }}
// }}
// ").ToString();

//             return code;
//         }

//         public static string CreateServiceFile(string name)
//         {
//                 var code = new StringBuilder(@$"using System;
// using System.Threading.Tasks;
// using Grpc.Core;

// namespace {name}
// {{
//     public class {name}ServiceImplementatiom : {name}Service.{name}ServiceBase
//     {{
//         public override Task<{name}Reply> Request({name}Command request, ServerCallContext context)
//         {{
//             return Task.FromResult(new {name}Reply {{ Data = ""Hello"" }});
//         }}
//     }}
// }}
// ").ToString();

//             return code;
//         }


//         public static string CreateProjectFile(string name)
//         {
//                 var csproj = new StringBuilder(@$"<Project Sdk=""Microsoft.NET.Sdk.Web"">

//   <PropertyGroup>
//     <TargetFramework>net5.0</TargetFramework>
//   </PropertyGroup>

//   <ItemGroup>
//     <PackageReference Include=""Google.Protobuf"" Version=""3.14.0"" />
//     <PackageReference Include=""Grpc.Core"" Version=""2.33.1"" />
//     <PackageReference Include=""Grpc.Tools"" Version=""2.33.1"">
//       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
//       <PrivateAssets>all</PrivateAssets>
//     </PackageReference>
//   </ItemGroup>

//   <ItemGroup>
//     <Protobuf Include=""../../ProtoFiles/{"*"}.proto"" />
//   </ItemGroup>

// </Project>
// ").ToString();

//             return csproj;
//         }

//         public static string CreateProtoFile(string name)
//         {;
//           var proto = new StringBuilder(@$"syntax = ""proto3"";

// service {name}Service {{
//   rpc Request ({name}Command) returns ({name}Reply) {{}}
// }}

// // The request message to the service.
// message {name}Command {{
//   string data = 1;
// }}

// // The response message from the service
// message {name}Reply {{
//   string data = 1;
// }}
// ").ToString();

//             return proto;
//         }
//     }
// }
