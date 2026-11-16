using System;
using System.Collections;
using System.Linq;
using UnityEngine;

internal sealed class AspMissingFastToolsSmoke : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Run()
    {
        var target = new GameObject("Aspid missing FastTools smoke").AddComponent<AspMissingFastToolsSmoke>();
        target.StartCoroutine(target.Check());
    }

    private IEnumerator Check()
    {
        yield return null;
        yield return null;
        if (AppDomain.CurrentDomain.GetAssemblies().Any(assembly => assembly.GetName().Name.StartsWith("Aspid.MVVM")))
        {
            Debug.LogError("MVVM assemblies must be excluded from the Player without FastTools.");
            Application.Quit(1);
            yield break;
        }

        Debug.Log("ASPID_FASTTOOLS_ABSENT_PASS");
        Debug.Log("ASPID_DEPENDENCY_SMOKE_PASS");
        Application.Quit(0);
    }
}
