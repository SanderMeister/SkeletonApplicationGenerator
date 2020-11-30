using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Generator
{
    public class CodeGenerator
    {
        public static void MakeFiles()
        {
            // var services = WorkflowHelper.GetWorkflow();
            // foreach(Service service in services){
                // var name = Regex.Replace(service.ServiceName, @"\s+", "");
                // Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                // name = rgx.Replace(name, "");
                // name = char.ToUpper(name[0]) + name.Substring(1);

                var name = "test";
                var programFile = DotnetAppFilesGenerator.CreateProgramFile(name);
                CreateFile("Program.cs", programFile);

                var startupFile = DotnetAppFilesGenerator.CreateStartupFile(name);
                CreateFile("Startup.cs", programFile);

                var projectFile = DotnetAppFilesGenerator.CreateProjectFile(name);
                CreateFile("Test.csproj", projectFile);

                Directory.CreateDirectory($"../DotnetApp/Controllers");
                var controllerFile = DotnetAppFilesGenerator.CreateControllerFile(name);
                CreateFile("Controller.cs", controllerFile);

                var appsettingsFile = DotnetAppFilesGenerator.CreateAppsettingsFile(name);
                CreateFile("appsettings.json", appsettingsFile);

                var appsettingsDevFile = DotnetAppFilesGenerator.CreateAppsettingsDevFile(name);
                CreateFile("appsettings.Development.json", appsettingsDevFile);

                Directory.CreateDirectory($"../DotnetApp/Properties");
                var launchSettingsFile = DotnetAppFilesGenerator.CreateLaunchSettingsFile(name);
                CreateFile("launchSettings.json", launchSettingsFile);         
        }

        public static void CreateFile(String name, String code){
            using (FileStream fs = File.Create($"../DotnetApp/{name}"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(code);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
