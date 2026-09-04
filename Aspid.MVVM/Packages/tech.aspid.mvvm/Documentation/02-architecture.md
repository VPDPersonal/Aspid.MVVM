# Architecture

How Aspid.MVVM is built: the MVVM pattern in Unity, the role of the Source Generator and the data binding pipeline.

## Contents

- [MVVM in Unity](#mvvm-in-unity)
- [Source Generation](#source-generation)
- [Binding pipeline](#binding-pipeline)
- [Reverse binding](#reverse-binding)
- [Architecture diagram](#architecture-diagram)
- [Key interfaces](#key-interfaces)

---

## MVVM in Unity

MVVM (Model-View-ViewModel) splits an application into three layers:

| Layer | Role | In Aspid.MVVM |
|------|------|-------------|
| **Model** | Business logic and data | Plain C# classes (POCO) |
| **ViewModel** | Adapts data for presentation | A class with `[ViewModel]` |
| **View** | Presentation and user input | `MonoView` + binders (MonoBehaviour) |

**Key principle:** the ViewModel does not know about the View. The View subscribes to ViewModel changes through the binding system. This makes it possible to:
- Test the ViewModel without Unity
- Change the View (UI) without touching the logic
- Show one ViewModel in several Views

---

## Source Generation

Aspid.MVVM uses Roslyn Incremental Source Generators to emit code at compile time. That is what gives **zero reflection** at runtime.

### What is generated

From a `partial` class with `[ViewModel]`:

```csharp
// Your code:
[ViewModel]
public sealed partial class PlayerViewModel
{
    [OneWayBind] private int _health;
    [TwoWayBind] private string _name;
    [RelayCommand] private void Attack() { /* ... */ }
}
```

The Source Generator produces:

```csharp
// Generated code (simplified):
partial class PlayerViewModel : IViewModel
{
    private OneWayBindableMember<int> _healthBindableMember;
    private TwoWayBindableMember<string> _nameBindableMember;
    private readonly IRelayCommand _attackCommand;

    public int Health
    {
        get => _health;
        private set { /* update + notify */ }
    }

    public string Name
    {
        get => _name;
        set { /* update + notify */ }
    }

    public IRelayCommand AttackCommand => _attackCommand;

    public FindBindableMemberResult FindBindableMember(
        in FindBindableMemberParameters parameters)
    {
        // Dispatch by id: plain string comparisons
        if (parameters.Id == "Health") return new(healthAdder);
        if (parameters.Id == "Name") return new(nameAdder);
        if (parameters.Id == "AttackCommand") return new(attackAdder);
        return default;
    }

    public void NotifyAll() { /* notifies every binding */ }
}
```

### Generation for the View

```csharp
// Your code:
[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder _health;
    [SerializeField] private MonoBinder[] _name;
}
```

The Source Generator implements `IView`: `Initialize`, `Deinitialize`, enumeration and binding of every declared binder.

---

## Binding pipeline

Step by step, what happens on `view.Initialize(viewModel)`:

### 1. The View asks for a BindableMember

For every binder field the View calls:
```csharp
var result = viewModel.FindBindableMember(
    new FindBindableMemberParameters("Health"));
```

`FindBindableMemberParameters` is a `ref struct` (zero allocations).

### 2. The ViewModel returns an IBinderAdder

`FindBindableMemberResult` carries an `IBinderAdder`, the interface used to attach a binder:

```csharp
public interface IBinderAdder
{
    BindMode Mode { get; }
    IBinderRemover? Add(IBinder binder);
}
```

### 3. The binder subscribes

`Binder.Bind(IBinderAdder)` calls `binderAdder.Add(this)`:
- The binder subscribes to the `Changed` event of the `BindableMember`
- The binder immediately receives the current value through `SetValue`

### 4. Data update

When a ViewModel property changes:
```
ViewModel.Health = 50
  → _healthBindableMember.Value = 50
    → Changed?.Invoke(50)
      → every IBinder<int>.SetValue(50)
        → UI updates
```

---

## Reverse binding

In **TwoWay** and **OneWayToSource** modes data can travel from the View to the ViewModel:

```
UI changes (the user types text)
  → IReverseBinder<string>.ValueChanged?.Invoke("new text")
    → TwoWayBindableMember.OnValueChanged("new text")
      → _setValue("new text")
        → ViewModel._name = "new text"
```

`TwoWayBindableMember` subscribes to `IReverseBinder<T>.ValueChanged` inside `Add()`.

---

## Architecture diagram

```
┌─────────┐     ┌──────────────┐     ┌─────────────────┐     ┌────────┐     ┌────┐
│  Model  │◄───►│  ViewModel   │◄───►│ BindableMember  │◄───►│ Binder │◄───►│ UI │
│ (C#)    │     │ [ViewModel]  │     │ (OneWay/TwoWay) │     │ (Mono) │     │    │
└─────────┘     └──────────────┘     └─────────────────┘     └────────┘     └────┘
                                                                  ▲
                                                                  │
                                                          ┌───────┴───────┐
                                                          │ IConverter    │
                                                          │ (optional)    │
                                                          └───────────────┘
```

**Data flow:**
- **OneWay:** Model → ViewModel → BindableMember → Binder → UI
- **TwoWay:** the same, plus UI → Binder → BindableMember → ViewModel
- **OneTime:** a single push of the value when binding
- **OneWayToSource:** UI → Binder → BindableMember → ViewModel

---

## Key interfaces

| Interface | Purpose |
|-----------|-----------|
| `IViewModel` | A single method, `FindBindableMember`: the entry point of binding |
| `IView` / `IView<T>` | `Initialize(viewModel)`, `Deinitialize()`, `ViewModel`: lifecycle control |
| `IBinder<T>` | `SetValue(T)`: receives a value from the ViewModel |
| `IReverseBinder<T>` | `ValueChanged` event: sends a value back to the ViewModel |
| `IAnyBinder` | `SetValue<T>(T)`: accepts any type (debugging and generic binders) |
| `IBinderAdder` | `Add(IBinder)`: attaches a binder to a BindableMember |
| `IBinderRemover` | `Remove(IBinder)`: detaches a binder |

### BindableMembers

| Class | Mode | Description |
|-------|-------|----------|
| `OneWayBindableMember<T>` | OneWay | Stores the value, `Changed` event, push on subscribe |
| `TwoWayBindableMember<T>` | TwoWay | Plus a subscription to `IReverseBinder.ValueChanged` |
| `OneTimeBindableMember<T>` | OneTime | Singleton per T, one-shot push, `Add` returns `null` |
| `OneWayToSourceBindableMember<T>` | OneWayToSource | Reverse binding only, no push to the View |

---

## See also

- [Getting Started](01-getting-started.md), an end-to-end example
- [Binding Modes](03-binding-modes.md), details of each mode
- [ViewModels](04-viewmodels.md), every generation attribute
- [Binders](06-binders.md), writing custom binders
