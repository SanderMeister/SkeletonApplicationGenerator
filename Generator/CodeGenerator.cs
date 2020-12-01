using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Generator
{
    public class CodeGenerator
    {
        public static void MakeFiles(int numberOfServices)
        {
            // var services = WorkflowHelper.GetWorkflow();
            for (int i = 0; i < numberOfServices; i++){
                var name = $"Ms{i}";
                var programFile = DotnetAppFilesGenerator.CreateProgramFile(name);
                CreateFile(name, "Program.cs", programFile);

                var startupFile = DotnetAppFilesGenerator.CreateStartupFile(name);
                CreateFile(name, "Startup.cs", programFile);

                var projectFile = DotnetAppFilesGenerator.CreateProjectFile(name);
                CreateFile(name, $"{name}.csproj", projectFile);

                Directory.CreateDirectory($"../{i}/Controllers");
                var controllerFile = DotnetAppFilesGenerator.CreateControllerFile(name);
                CreateFile(name, "Controller.cs", controllerFile);

                var appsettingsFile = DotnetAppFilesGenerator.CreateAppsettingsFile(name);
                CreateFile(name, "appsettings.json", appsettingsFile);

                var appsettingsDevFile = DotnetAppFilesGenerator.CreateAppsettingsDevFile(name);
                CreateFile(name, "appsettings.Development.json", appsettingsDevFile);

                Directory.CreateDirectory($"../{i}/Properties");
                var launchSettingsFile = DotnetAppFilesGenerator.CreateLaunchSettingsFile(name);
                CreateFile(name, "launchSettings.json", launchSettingsFile);     
            }    
        }

        public static void CreateFile(String dirName, String fileName, String code){
            using (FileStream fs = File.Create($"../{dirName}/{fileName}"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(code);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
