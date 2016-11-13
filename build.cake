
var target = Argument("target", "default");
var npi = EnvironmentVariable("npi");

Task("Publsh-Nuget")
    .IsDependentOn("Pack-Nuget")
    .Does(() => {
        var nupkg = new DirectoryInfo("./nuget").GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = npi
        });
    });

Task("Build-Release")
    .Does(() => {
        DotNetBuild("./Cake.UServer.sln", settings =>
            settings.SetConfiguration("Release")
            //.SetVerbosity(Core.Diagnostics.Verbosity.Minimal)
            .WithTarget("Build")
            .WithProperty("TreatWarningsAsErrors","true"));
    });

Task("Pack-Nuget")
    .IsDependentOn("Build-Release")
    .Does(() => {
        Func<string,string> getFile = (file) => {
            var fullName = new DirectoryInfo("./Cake.UServer/bin/Release")
                .GetFiles()
                .Where(x => x.Name == file)
                .First().FullName;
            return fullName;
        };

        CleanDirectory("./nuget");

        var version = ParseAssemblyInfo("./Cake.UServer/Properties/AssemblyInfo.cs").AssemblyVersion;
        var settings   = new NuGetPackSettings {
                        //ToolPath                = "./tools/nuget.exe",
                        Id                      = "Cake.UServer",
                        Version                 = version,
                        Title                   = "Cake.UServer",
                        Authors                 = new[] {"wk"},
                        Owners                  = new[] {"wk"},
                        Description             = "Cake.UServer",
                        //NoDefaultExcludes       = true,
                        Summary                 = "Serving static files with µHttpSharp",
                        ProjectUrl              = new Uri("https://github.com/cake-addin/cake-u-server"),
                        IconUrl                 = new Uri("https://github.com/cake-addin/cake-u-server"),
                        LicenseUrl              = new Uri("https://github.com/cake-addin/cake-u-server"),
                        Copyright               = "MIT",
                        //ReleaseNotes            = new [] { "New version"},
                        Tags                    = new [] {"Cake", "µHttpSharp" },
                        RequireLicenseAcceptance= false,
                        Symbols                 = false,
                        NoPackageAnalysis       = true,
                        Files                   = new [] {
                                                             new NuSpecContent { Source = getFile("Cake.UServer.dll"), Target = "bin/net45" },
                                                             new NuSpecContent { Source = getFile("Cake.UServer.XML"), Target = "bin/net45" },
                                                             new NuSpecContent { Source = getFile("Newtonsoft.Json.dll"), Target = "bin/net45" },
                                                             new NuSpecContent { Source = getFile("uhttpsharp.dll"), Target = "bin/net45" },
                                                          },
                        BasePath                = "./",
                        OutputDirectory         = "./nuget"
                    };
        NuGetPack(settings);
    });

RunTarget(target);
