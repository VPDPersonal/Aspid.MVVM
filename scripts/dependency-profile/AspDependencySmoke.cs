using Aspid.MVVM;
using System.Collections;
using System.Linq;
using UnityEngine;

public sealed class AspDependencySmoke : MonoBinder, IBinder<int>
{
    private int _value;

    public void SetValue(int value) =>
        _value = value;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Run()
    {
        var target = new GameObject("Aspid dependency smoke").AddComponent<AspDependencySmoke>();
        target.StartCoroutine(target.Check());
    }

    private IEnumerator Check()
    {
        yield return null;
        yield return null;
        try
        {
            CheckBindings();
            CheckCounter();
            Debug.Log("ASPID_DEPENDENCY_SMOKE_PASS");
            Application.Quit(0);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            Application.Quit(1);
        }
    }

    private void CheckBindings()
    {
        var target = this;
        var member = new OneWayBindableMember<int>(17);
        target.Bind(new BindContext(member, target));
        if (target._value != 17) throw new System.InvalidOperationException("Initial binding failed.");
        member.Value = 23;
        if (target._value != 23) throw new System.InvalidOperationException("Binding update failed.");
        target.Unbind(new UnbindContext(target));
        member.Value = 29;
        if (target._value != 23) throw new System.InvalidOperationException("Unbind failed.");
    }

    private void CheckCounter()
    {
        var views = FindObjectsByType<MonoView>(FindObjectsSortMode.None);
        if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.StartsWith("Counter")) return;
        var view = views.Single(item => item.ViewModel?.GetType().Name == "CounterViewModel");
        var model = view.ViewModel ?? throw new System.InvalidOperationException("Counter was not initialized.");
        var count = model.GetType().GetProperty("Count", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        var before = (int)count.GetValue(model);
        var command = (IRelayCommand)model.GetType().GetProperty("IncrementCommand", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(model);
        command.Execute();
        var after = (int)count.GetValue(model);
        if (after != before + 1) throw new System.InvalidOperationException("Counter command failed.");
        var texts = FindObjectsByType<Component>(FindObjectsSortMode.None).Where(component => component != null)
            .Select(component => component.GetType().GetProperty("text")?.GetValue(component) as string);
        if (!texts.Contains(after.ToString())) throw new System.InvalidOperationException("Counter text was not updated.");
        Debug.Log("ASPID_COUNTER_SAMPLE_PASS");
    }
}
