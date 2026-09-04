# Dynamic ViewModel

MVVM without source generation, for data whose shape is only known at runtime.

**You learn:** `DynamicViewModel`, `IDynamicProperty<T>`, a plain `MonoView` with binders listed by id.

**Assumes:** [Counter](../01.%20Counter/README.md).

Script: `Scripts/StopwatchBootstrap.cs`. Scene: `Scenes/Dynamic ViewModel.unity`.

## When you need it

Properties come from a config, a server or a level editor, and there is no `[ViewModel]` class to write for them. `DynamicViewModel` declares properties by a string id, and binders find them by the same id.

## Stopwatch

```csharp
public sealed class StopwatchBootstrap : MonoBehaviour
{
    [SerializeField] private MonoView _view;

    private IDynamicProperty<string> _elapsed;
    private IDynamicProperty<int> _laps;

    private void Awake()
    {
        var viewModel = new DynamicViewModel();

        viewModel.Add("Title", "Stopwatch", BindMode.OneTime);
        _elapsed = viewModel.Add("Elapsed", Format(0f));
        _laps = viewModel.Add("Laps", 0);

        viewModel.Add<IRelayCommand>("LapCommand", new RelayCommand(() => _laps.Value++), BindMode.OneTime);

        _view.Initialize(viewModel);
    }

    private void Update()
    {
        if (!_isRunning) return;

        _seconds += Time.deltaTime;
        _elapsed.Value = Format(_seconds);
    }
}
```

- `Add<T>(id, value, mode)` returns an `IDynamicProperty<T>`, the handle used to read and write the value. Binders follow it.
- The default mode is `OneWay`. Commands and constants are fine with `OneTime`.
- A command is just another value: `Add<IRelayCommand>(...)`.

## View without a class

The scene has a plain `MonoView`. Its **Binders** list maps each id (`Title`, `Elapsed`, `Laps`, `LapCommand`, …) to binders and a value type. No `[View]`, no `[ViewModel]`, no generator.

## What you give up

- The analyzer: a typo in `"Elapsd"` is found at runtime, not at compile time.
- Type checks at the boundary: `Add("Laps", 0)` and a binder typed `string` disagree on the first value.

`DynamicViewModel` is for truly dynamic data, not a way to save one class.

More in [Dynamic ViewModel](../../Documentation/10-dynamic-viewmodel.md).

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
