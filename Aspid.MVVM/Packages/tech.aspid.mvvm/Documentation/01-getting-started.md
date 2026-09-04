# Getting Started

A step-by-step guide from installation to the first working example.

## Contents

* [Requirements](#requirements)
* [Installation](#installation)
* [Learning path](#learning-path)
* [First example: Counter](#first-example-counter)
* [How it works](#how-it-works)
* [Next steps](#next-steps)

***

## Requirements

* **Unity 2022.3** or newer
* **.NET Standard 2.0** (the framework's target)
* **Source Generators** support in Unity (built in since 2022.3)

## Installation

### From the Unity Asset Store

1. Open the [Aspid.MVVM page](https://assetstore.unity.com/packages/slug/298463) in the Unity Asset Store
2. Import the package into your project
3. Make sure the `Assets/Aspid/MVVM/` folder was created

### From source

```bash
git clone https://github.com/VPDPersonal/Aspid.MVVM.git
cd Aspid.MVVM
git submodule update --init --recursive
```

> [!IMPORTANT]
> The project uses git submodules. Without `git submodule update --init --recursive` the code does not compile.

***

## Learning path

Each sample adds exactly one new concept. Every sample's `README.md` is its tutorial.

| # | Sample | New | Tutorial |
| - | ------ | --- | -------- |
| 1 | **Counter** | `[ViewModel]`, `[Bind]`, `[RelayCommand]`, `ViewInitializer` | [Counter](../Samples~/01.%20Counter/README.md) |
| 2 | **Greeter** | `MonoViewModel`, `[TwoWayBind]`, `[BindAlso]`, `On*Changed` | [Greeter](../Samples~/02.%20Greeter/README.md) |
| 3 | **Bind Modes** | four modes on one screen, your own `ITwoWayConverter` | [Bind Modes](../Samples~/03.%20BindModes/README.md) |
| 4 | **Stats** | commands with a parameter, `CanExecute`, draft → model | [Stats](../Samples~/04.%20Stats/README.md) |
| 5 | **Todo List** | a model, `ObservableList`, `CreateSync`, collection binders | [Todo List](../Samples~/05.%20TodoList/README.md) |
| 6 | **Custom Binder** | a binder for your own component, `[GenerateSerializableBinder]` | [Custom Binder](../Samples~/06.%20CustomBinder/README.md) |

***

## First example: Counter

A button increments a counter and the number is shown in a text. The smallest example that shows the three core concepts of the framework.

### Step 1: ViewModel

The ViewModel holds data and logic. The Source Generator writes all the binding code.

```csharp
using Aspid.MVVM;

// [ViewModel] marks the class for the Source Generator.
// The class must be partial.
[ViewModel]
public sealed partial class CounterViewModel
{
    // [OneWayBind]: data flows from the ViewModel to the View only.
    // The generator emits a Count property whose setter notifies binders.
    [OneWayBind] private int _count;

    // [RelayCommand]: the generator emits an IncrementCommand property of type IRelayCommand.
    [RelayCommand]
    private void Increment() => Count++;
}
```

**What the Source Generator produces:**

| Source | Generated |
| ------ | --------- |
| `[ViewModel]` on the class | `IViewModel` implementation, `FindBindableMember` |
| `[OneWayBind] int _count` | `Count` property with a notifying setter, `OnCountChanging` / `OnCountChanged` hooks |
| `[RelayCommand] void Increment()` | `IncrementCommand` property of type `IRelayCommand` |

### Step 2: View

The View declares which binders connect to which ViewModel members.

```csharp
using UnityEngine;
using Aspid.MVVM;

// [View] marks the class for the Source Generator.
// The class must be partial.
[View]
public sealed partial class CounterView : MonoView
{
    // The field name matches the ViewModel field name.
    // The generator binds them by name.
    [SerializeField] private MonoBinder _count;

    // An array of binders: several UI elements trigger the same action.
    [SerializeField] private MonoBinder[] _increment;
}
```

> [!NOTE]
> **Naming rule:** the View field name without the `_`, `m_` or `s_` prefix must match the ViewModel member. `_count` binds to `Count`.

### Step 3: Bootstrap

Bootstrap connects the View and the ViewModel:

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private CounterView _counterView;

    private void Awake()
    {
        var viewModel = new CounterViewModel();
        _counterView.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        _counterView.DeinitializeView()?.DisposeViewModel();
    }
}
```

### Step 4: Inspector setup

1. Create a GameObject with the `CounterView` component
2. Add a child with a `TextMonoBinder` and drag it into the `_count` field
3. Add a child with a `Button` and a `ButtonCommandMonoBinder` and drag it into the `_increment` array
4. On the Bootstrap object assign the `CounterView` reference

***

## How it works

After `view.Initialize(viewModel)`:

1. **View** walks its binders and calls `viewModel.FindBindableMember(id)` for each
2. **ViewModel** (generated code) finds the `BindableMember` by id without reflection
3. **Binder** subscribes and receives the current value
4. When `Count` changes, the binder updates the `Text`
5. When the button is pressed, `ButtonCommandMonoBinder` calls `IncrementCommand.Execute()`

```text
ViewModel ──► BindableMember ──► Binder ──► UI
               (no reflection, direct calls)
```

> [!NOTE]
> The Source Generator emits direct calls at compile time: no reflection, no allocations.

***

## Next steps

* [Counter](../Samples~/01.%20Counter/README.md), the full tutorial with binder details
* [Greeter](../Samples~/02.%20Greeter/README.md), two-way binding: InputField → Text in real time
* [Architecture](02-architecture.md), the binding pipeline in detail
* [Binding Modes](03-binding-modes.md): OneWay, TwoWay, OneTime, OneWayToSource
* [StarterKit](StarterKit/README.md), every ready-made binder for Unity UI
