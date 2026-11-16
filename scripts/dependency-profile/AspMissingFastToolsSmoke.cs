using System;
using System.Linq;
using UnityEngine;

internal static class AspMissingFastToolsSmoke
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Run()
    {
        if (AppDomain.CurrentDomain.GetAssemblies().Any(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM")))
        {
            Debug.LogError("MVVM assemblies must be excluded from the Player without FastTools.");
            Application.Quit(1);
            return;
        }

        Debug.Log("ASPID_FASTTOOLS_ABSENT_PASS");
        Debug.Log("ASPID_DEPENDENCY_SMOKE_PASS");
        Application.Quit(0);
    }
}
