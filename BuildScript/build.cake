var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var msbuildLogger = Argument("msbuildLogger", "");

const string solution = "../TemaTre.Site.Resume.sln";


const string buildTask = "Build";
const string cleanTask = "Clean";
const string restoreTask = "Restore";
const string rebuildTask = "Rebuild";

private MSBuildSettings WithDefaults(MSBuildSettings settings, string configuration, string logger)
{
    settings.UseToolVersion(MSBuildToolVersion.VS2015)
        .SetMSBuildPlatform(MSBuildPlatform.x86)
        .SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal);

    if (string.IsNullOrWhiteSpace(logger))
    {
        return settings;
    }

    if (BuildSystem.IsRunningOnTeamCity)
    {
        settings.WithLogger(logger);
    }
    else
    {
        settings.AddFileLogger(new MSBuildFileLogger { LogFile = File(logger), Verbosity = Verbosity.Minimal });
    }

    return settings;
}


Task(cleanTask).Does(() => 
{
	var settings = new DeleteDirectorySettings { Recursive = true, Force = true };

	var paths = new List<DirectoryPath>();
    paths.AddRange(GetDirectories("./**/bin"));
    paths.AddRange(GetDirectories("./**/obj"));
	
	paths = paths.Where(p => DirectoryExists(p)).ToList();
	
	foreach (var path in paths)
    {
        Information( "Remove folder: " + System.IO.Path.GetFullPath(path.FullPath));
        DeleteDirectory(path, settings);
    }

});



Task(restoreTask)
    .IsDependentOn(cleanTask)
    .Does(() =>
{
    NuGetRestore(solution);
});


Task(buildTask)
    .IsDependentOn(restoreTask)
    .Does(() =>
{
    MSBuild(solution, settings => WithDefaults(settings, configuration, msbuildLogger));
});


Task(rebuildTask)
    .IsDependentOn(cleanTask)
	.IsDependentOn(buildTask)
    .Does(() =>
{
    MSBuild(solution, settings => WithDefaults(settings, configuration, msbuildLogger));
});


Task("Default")
  .Does(() =>
{
});

RunTarget(target);