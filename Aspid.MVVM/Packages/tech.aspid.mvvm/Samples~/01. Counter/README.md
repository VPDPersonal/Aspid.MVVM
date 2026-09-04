# Counter

The smallest possible Aspid.MVVM setup: a button increments a number shown in a text.

**You learn:** `[ViewModel]`, `[Bind]`, `[RelayCommand]`, `MonoView`, two ways to hand a ViewModel to a View.

| Scene | How the View gets its ViewModel |
|---|---|
| `Counter (Bootstrap)` | `Bootstrap.cs` creates the ViewModel and calls `Initialize` by hand. |
| `Counter (ViewInitializer)` | The `ViewInitializer` component does it without code; the ViewModel is edited in the Inspector. |

Files: `Scripts/CounterViewModel.cs`, `Scripts/CounterView.cs`, `Scripts/Bootstrap.cs`.

## What we build

```
[ - ]  [ + ]  [ Reset ]   →   Count: 5
```

## Step 1: ViewModel

```csharp
[ViewModel]
[Serializable]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;

    [RelayCommand]
    private void Increment() => Count++;

    [RelayCommand]
    private void Decrement() => Count--;

    [RelayCommand]
    private void Reset() => Count = 0;
}
```

What the Source Generator adds to the other half of the `partial` class:

| From | Generated |
|---|---|
| `[ViewModel]` | The `IViewModel` implementation and a reflection-free `FindBindableMember(string id)`. |
| `[Bind] int _count` | A `Count` property whose setter notifies binders, plus `partial void OnCountChanging/OnCountChanged` hooks. |
| `[RelayCommand] void Increment()` | An `IncrementCommand` property of type `IRelayCommand`. |

Write `Count++`, not `_count++`: only the generated property notifies the UI. The analyzer warns when a field is used where the property is meant.

`[Serializable]` is not required by the framework. It is here so the ViewModel can be edited inside `ViewInitializer` in the second scene.

## Step 2: View

```csharp
[View]
public sealed partial class CounterView : MonoView
{
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _count;

    [RequireBinder(typeof(IRelayCommand))]
    [SerializeField] private MonoBinder[] _incrementCommand;

    [RequireBinder(typeof(IRelayCommand))]
    [SerializeField] private MonoBinder[] _decrementCommand;

    [RequireBinder(typeof(IRelayCommand))]
    [SerializeField] private MonoBinder[] _resetCommand;
}
```

Fields are matched to ViewModel members by name, ignoring the `_`, `m_` and `s_` prefixes:

| View field | ViewModel member |
|---|---|
| `_count` | `Count` |
| `_incrementCommand` | `IncrementCommand` |

- `MonoBinder[]` lets several UI elements bind to one member. A single `MonoBinder` works when there is exactly one.
- `[RequireBinder]` filters the Inspector so an incompatible binder cannot be dropped into the field.

## Step 3: Bootstrap

```csharp
public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private CounterView _counterView;

    private void Awake() =>
        _counterView.Initialize(new CounterViewModel());

    private void OnDestroy() =>
        _counterView.DeinitializeView()?.DisposeViewModel();
}
```

`DeinitializeView()` unbinds the binders and returns the ViewModel. `DisposeViewModel()` calls `Dispose()` when the ViewModel implements `IDisposable`. Counter does not need it, but the pair is the habit to keep.

The second scene has no `Bootstrap` at all: `ViewInitializer` holds a serialized `CounterViewModel` and initializes the View itself.

## Step 4: Scene

```
Counter
├── Bootstrap            (Bootstrap.cs)          — first scene only
└── Counter UI
    ├── CounterView      (CounterView.cs)
    ├── Count Text       (TextMonoBinder)
    ├── Increment Button (Button + ButtonCommandMonoBinder)
    ├── Decrement Button (Button + ButtonCommandMonoBinder)
    └── Reset Button     (Button + ButtonCommandMonoBinder)
```

In the Inspector drag each binder into the matching `CounterView` field. Every extra command costs one method in the ViewModel and one field in the View.

## Summary

| Concept | What we did |
|---|---|
| `[ViewModel]` | Marked the class for generation |
| `[Bind]` | `_count` became a notifying `Count` property |
| `[RelayCommand]` | Methods became commands for buttons |
| `MonoView` | The View declares its binders as fields |
| Bootstrap vs `ViewInitializer` | Code or Inspector, same result |

Next: [Greeter](../02.%20Greeter/README.md), a ViewModel living on a GameObject with two-way binding.

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
