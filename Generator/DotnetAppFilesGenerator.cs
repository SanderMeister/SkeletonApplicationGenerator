using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Generator
{
    public class DotnetAppFilesGenerator
    {

        public static string CreateProgramFile(string name)
        {
                var code = new StringBuilder(@$"using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace {name}
{{
    public class Program 
    {{
        const int Port = 30051;

        public static void Main(string[] args)
        {{
            Server server = new Server
            {{
                Services = {{ {name}Service.BindService(new {name}ServiceImpl()) }},
                Ports = {{ new ServerPort(""localhost"", Port, ServerCredentials.Insecure) }}
            }};
            server.Start();

            Console.WriteLine(""{name} Server listening on port "" + Port);
            Console.WriteLine(""Press any key to stop the server..."");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }}
    }}
}}
").ToString();

            return code;
        }


        public static string CreateStartupFile(string name)
        {
            var code = new StringBuilder(@$"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace {name}
{{
    public class Startup
    {{
        public Startup(IConfiguration configuration)
        {{
            Configuration = configuration;
        }}

        public IConfiguration Configuration {{ get; }}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {{

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {{
                c.SwaggerDoc(""v1"", new OpenApiInfo {{ Title = ""TodoApi"", Version = ""v1"" }});
            }});
        }}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {{
            if (env.IsDevelopment())
            {{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""TodoApi v1""));
            }}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {{
                endpoints.MapControllers();
            }});
        }}
    }}
}}
").ToString();

            return code;
        }

        public static string CreateProjectFile(string name)
        {
            var code = new StringBuilder(@$"<Project Sdk=""Microsoft.NET.Sdk.Web"">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.AspNetCore.Authentication.JwtBearer"" Version=""5.0.0"" NoWarn=""NU1605"" />
    <PackageReference Include=""Microsoft.AspNetCore.Authentication.OpenIdConnect"" Version=""5.0.0"" NoWarn=""NU1605"" />
    <PackageReference Include=""Swashbuckle.AspNetCore"" Version=""5.6.3"" />
  </ItemGroup>

</Project>
").ToString();

            return code;
        }

        public static string CreateControllerFile(string name)
        {
            var code = new StringBuilder(@$"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace {name}.Controllers
{{
    [ApiController]
    [Route(""[controller]"")]
    public class WeatherForecastController : ControllerBase
    {{
        private static readonly string[] Summaries = new[]
        {{
            ""Freezing"", ""Bracing"", ""Chilly"", ""Cool"", ""Mild"", ""Warm"", ""Balmy"", ""Hot"", ""Sweltering"", ""Scorching""
        }};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {{
            _logger = logger;
        }}

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {{
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {{
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }})
            .ToArray();
        }}
    }}
}}
").ToString();

            return code;
        }

        public static string CreateAppsettingsFile(string name)
        {
            var code = new StringBuilder(@$"{{
  ""Logging"": {{
    ""LogLevel"": {{
      ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }}
  }},
  ""AllowedHosts"": ""*""
}}
").ToString();

            return code;
        }

        public static string CreateAppsettingsDevFile(string name)
        {
            var code = new StringBuilder(@$"{{
  ""Logging"": {{
    ""LogLevel"": {{
      ""Default"": ""Information"",
      ""Microsoft"": ""Warning"",
      ""Microsoft.Hosting.Lifetime"": ""Information""
    }}
  }}
}}
").ToString();

            return code;
        }

        public static string CreateLaunchSettingsFile(string name)
        {
            var code = new StringBuilder(@$"{{
  ""$schema"": ""http://json.schemastore.org/launchsettings.json"",
  ""iisSettings"": {{
    ""windowsAuthentication"": false,
    ""anonymousAuthentication"": true,
    ""iisExpress"": {{
      ""applicationUrl"": ""http://localhost:12443"",
      ""sslPort"": 44378
    }}
  }},
  ""profiles"": {{
    ""IIS Express"": {{
      ""commandName"": ""IISExpress"",
      ""launchBrowser"": true,
      ""launchUrl"": ""swagger"",
      ""environmentVariables"": {{
        ""ASPNETCORE_ENVIRONMENT"": ""Development""
      }}
    }},
    ""TodoApi"": {{
      ""commandName"": ""Project"",
      ""dotnetRunMessages"": ""true"",
      ""launchBrowser"": true,
      ""launchUrl"": ""swagger"",
      ""applicationUrl"": ""https://localhost:5001;http://localhost:5000"",
      ""environmentVariables"": {{
        ""ASPNETCORE_ENVIRONMENT"": ""Development""
      }}
    }}
  }}
}}
").ToString();

            return code;
        }
    }
}
