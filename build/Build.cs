using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[GitHubActions("continuous", GitHubActionsImage.UbuntuLatest, On = new[] { GitHubActionsTrigger.Push },
	InvokedTargets = new[] { nameof(Test) })]
[GitHubActions("release", GitHubActionsImage.UbuntuLatest,
	OnPushBranches = new[] { "release" }, InvokedTargets = new[] { nameof(CreateNugetPackage) },
	PublishArtifacts = true, ImportSecrets = new[] { "NUGETAPIKEY" })]
class Build : NukeBuild {
	/// Support plugins are available for:
	///   - JetBrains ReSharper        https://nuke.build/resharper
	///   - JetBrains Rider            https://nuke.build/rider
	///   - Microsoft VisualStudio     https://nuke.build/visualstudio
	///   - Microsoft VSCode           https://nuke.build/vscode
	public static int Main() => Execute<Build>(x => x.Compile);

	[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[Secret] [Parameter("The ApiKey for nuget.org")] readonly string NugetApiKey = "";

	[Solution(GenerateProjects = true)] readonly Solution Solution;
	[GitRepository] readonly GitRepository GitRepository;
	[GitVersion(NoFetch = true)] readonly GitVersion GitVersion;

	AbsolutePath SourceDirectory => RootDirectory / "src";
	AbsolutePath TestsDirectory => RootDirectory / "test";
	AbsolutePath OutputDirectory => RootDirectory / "output";

	Target Clean => _ => _
		.Before(Restore)
		.Executes(() =>
		{
			SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
			TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
			EnsureCleanDirectory(OutputDirectory);
		});

	Target Restore => _ => _
		.Executes(() =>
		{
			DotNetRestore(s => s
				.SetProjectFile(Solution));
		});

	Target Compile => _ => _
		.DependsOn(Restore)
		.Executes(() =>
		{
			DotNetBuild(s => s
				.SetProjectFile(Solution)
				.SetConfiguration(Configuration)
				.EnableNoRestore());
		});

	AbsolutePath TestResultDirectory => OutputDirectory / "testresults";
	IEnumerable<Project> TestProjects => Solution.GetProjects("*.Tests");

	Target Test => _ => _
		.DependsOn(Compile)
		.Produces(TestResultDirectory / "*.trx")
		.Executes(() =>
		{
			DotNetTest(_ => _
				.SetProjectFile(Solution)
				.SetConfiguration(Configuration)
				.EnableNoRestore()
				.CombineWith(TestProjects, (_, p) => _
					.SetProjectFile(p)
					.SetLoggers($"trx;LogFileName={TestResultDirectory / p.Name}.trx"))
			);
		});

	AbsolutePath NugetPackageFullPath => OutputDirectory / $"{Solution.src.Sirups_Dapper_NodaTime.Name}.{GitVersion.SemVer}.nupkg";

	Target CreateNugetPackage => _ => _
		.DependsOn(Compile)
		.Produces(OutputDirectory / "*.nupkg")
		.Executes(() =>
		{
			DotNetPack(_ => _
				.SetConfiguration(Configuration)
				.EnableNoBuild()
				.EnableNoRestore()
				.SetProject(Solution.src.Sirups_Dapper_NodaTime)
				.SetOutputDirectory(OutputDirectory)
				.EnableIncludeSource()
				.EnableIncludeSymbols()
				.SetVersion(GitVersion.SemVer));
		});

	Target PushNugetPackage => _ => _
		.DependsOn(CreateNugetPackage)
		.Executes(() =>
		{
			DotNetNuGetPush(_ => _
				.SetApiKey(NugetApiKey)
				.SetTargetPath(NugetPackageFullPath)
				.SetSource("nuget.org")
				.EnableForceEnglishOutput());
		});
}