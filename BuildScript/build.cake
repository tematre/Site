var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var msbuildLogger = Argument("msbuildLogger", "");

const string solution = "../TemaTre.Site.Resume.sln";


const string buildTask = "Build";
const string cleanTask = "Clean";
const string restoreTask = "Restore";
const string rebuildTask = "Rebuild";


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
    DotNetCoreRestore(solution);
});


Task(buildTask)
    .IsDependentOn(restoreTask)
    .Does(() =>
{
    DotNetCoreMSBuild(solution);
});


Task(rebuildTask)
    .IsDependentOn(cleanTask)
	.IsDependentOn(buildTask)
    .Does(() =>
{
    DotNetCoreMSBuild(solution);
});


Task("Default")
  .Does(() =>
{
});

RunTarget(target);