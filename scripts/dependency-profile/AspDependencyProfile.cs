using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class AspDependencyProfile
{
    public static void Verify()
    {
        var installed = new HashSet<string>(PackageInfo.GetAllRegisteredPackages().Select(package => package.name));
        var enabled = installed.Contains("tech.aspid.fasttools");
        var assemblies = UnityEditor.Compilation.CompilationPipeline.GetAssemblies();
        foreach (var assembly in new[] { "Aspid.MVVM", "Aspid.MVVM.Unity", "Aspid.MVVM.StarterKit" })
        {
            if (assemblies.Any(item => item.name == assembly) != enabled)
                throw new InvalidOperationException(assembly + " does not match FastTools availability.");
        }
        if (!enabled)
        {
            if (assemblies.Any(item => item.name.StartsWith("Aspid.MVVM")))
                throw new InvalidOperationException("MVVM assemblies must be disabled without FastTools.");
            Debug.Log("ASPID_FASTTOOLS_ABSENT_PASS");
            Debug.Log("ASPID_DEPENDENCY_EDITOR_PASS");
            return;
        }
        var starterKit = AppDomain.CurrentDomain.GetAssemblies().Single(assembly => assembly.GetName().Name == "Aspid.MVVM.StarterKit");
        foreach (var type in new[] { "DynamicViewModel", "ViewInitializer", "CommandMonoBinder" })
        {
            if (starterKit.GetType("Aspid.MVVM.StarterKit." + type) == null)
                throw new InvalidOperationException(type + " is unavailable with FastTools installed.");
        }
        var probes = new Dictionary<string, string[]>
        {
            ["com.unity.modules.ai"] = new[] { "NavMeshAgentSpeedMonoBinder" },
            ["com.unity.modules.animation"] = new[] { "AnimatorSpeedMonoBinder" },
            ["com.unity.modules.audio"] = new[] { "AudioSourceVolumeMonoBinder" },
            ["com.unity.modules.particlesystem"] = new[] { "ParticleSystemEmissionEnabledMonoBinder" },
            ["com.unity.modules.video"] = new[] { "VideoPlayerPlaybackSpeedMonoBinder" },
            ["com.unity.modules.physics"] = new[] { "RigidbodyMassMonoBinder" },
            ["com.unity.modules.physics2d"] = new[] { "Rigidbody2DMassMonoBinder" },
            ["com.unity.modules.ui"] = new[] { "CanvasGroupAlphaMonoBinder" },
            ["com.unity.modules.uielements"] = new[] { "ElementSliderValueMonoBinder", "ElementButtonCommandMonoBinder" },
            ["com.unity.ugui"] = new[] { "SliderValueMonoBinder", "CanvasScalerUiScaleModeMonoBinder", "SelectableTransitionMonoBinder" },
            ["tech.aspid.fasttools"] = new[] { "EnumMonoBinder`1" },
            ["tech.aspid.collections"] = new[] { "ObservableListViewModelMonoBinder`1" }
        };
        foreach (var probe in probes)
        {
            foreach (var type in probe.Value)
            {
                var present = starterKit.GetType("Aspid.MVVM.StarterKit." + type) != null;
                if (present != installed.Contains(probe.Key))
                    throw new InvalidOperationException(type + " does not match the installed package " + probe.Key);
            }
        }
        Debug.Log("ASPID_DEPENDENCY_EDITOR_PASS");
    }

    public static void Build()
    {
        Verify();
        var scenePath = "Assets/Samples/Counter/Scenes/Counter (ViewInitializer).unity";
        if (!File.Exists(scenePath) || !PackageInfo.GetAllRegisteredPackages().Any(package => package.name == "tech.aspid.fasttools"))
        {
            scenePath = "Assets/Smoke.unity";
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, scenePath);
        }
        var args = Environment.GetCommandLineArgs();
        var outputIndex = Array.IndexOf(args, "-customBuildPath");
        var output = outputIndex >= 0 ? args[outputIndex + 1] : "Build/Smoke.app";
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = new[] { scenePath },
            locationPathName = output,
            target = EditorUserBuildSettings.activeBuildTarget,
            options = BuildOptions.Development
        });
        if (report.summary.result != BuildResult.Succeeded)
            throw new InvalidOperationException("Dependency profile Player build failed.");

        // GameCI validates custom build output using this success marker.
        Console.WriteLine("Build succeeded!");
    }
}
