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

* **Unity 6000.0.53f1** or newer.
* Git for UPM packages installed from Git URLs.

## Installation

### Minimal source installation

The optional-dependency layout below describes the unreleased 1.1 source. Older preview tags keep their original dependency requirements.

1. Create a clean Unity project with Unity 6000.0.53f1 or newer.
2. In **Window → Package Manager**, choose **+ → Install package from disk**.
3. Select `Aspid.MVVM/Packages/tech.aspid.mvvm/package.json` in your source checkout.

The installed package is `Packages/tech.aspid.mvvm/`. Aspid.FastTools is required. Until it is installed, MVVM runtime, Editor, sample and test assemblies are excluded from compilation. With FastTools installed, the base assemblies work without Collections or UI packages. Keep your existing Unity module configuration; a binder for a removed module is unavailable until that module is installed again.

Install FastTools first: `https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview/1.0.0-rc.9`. It is not installed automatically by this Git distribution.

### Optional integration profiles

| Feature | Add in Package Manager |
| --- | --- |
| Counter and other uGUI/TMP samples | `com.unity.ugui` 2.0.0 or newer compatible with your Editor |
| UI Toolkit binders | `com.unity.modules.uielements` |
| Inspector tools | FastTools is required; Editor tools also use UI Toolkit and IMGUI |
| Observable collections | `https://github.com/VPDPersonal/Aspid.Collections.git#upm/1.1.1` |
| VirtualizedList | Collections and uGUI |
| AI, animation, audio, particles, physics, physics2d, video | The corresponding `com.unity.modules.*` module |

Localization, Addressables, VContainer and Zenject remain optional integrations. Collections 1.1.1 does not install Test Framework or NUnit into a runtime project. Earlier Collections versions can do so; upgrade the package before removing those test dependencies.

### Counter

For Counter, also install `com.unity.inputsystem` 1.20.0 and enable **Active Input Handling → Input System Package (New)** or **Both**. Import **Path 1. Counter** from the package's **Samples** tab, then import **TMP Essential Resources** if prompted. Open the imported Counter scene and enter Play mode. Follow the [Counter tutorial](../tutorials/01.%20Counter/README.md) for its two initialization variants.

### Published releases

The preview channel remains `https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview`. Use the installation instructions bundled with that release: a published tag does not contain unreleased source changes. Asset Store releases are imported through **Package Manager → My Assets**; do not assume the legacy `Assets/Aspid/MVVM/` path.

***

## Learning path

Each sample adds exactly one new concept. Every sample's `README.md` is its tutorial.

| # | Sample | New | Tutorial |
| - | ------ | --- | -------- |
| 1 | **Counter** | `[ViewModel]`, `[Bind]`, `[RelayCommand]`, `ViewInitializer` | [Counter](../tutorials/01.%20Counter/README.md) |
| 2 | **Greeter** | `MonoViewModel`, `[TwoWayBind]`, `[BindAlso]`, `On*Changed` | [Greeter](../tutorials/02.%20Greeter/README.md) |
| 3 | **Bind Modes** | four modes on one screen, your own `ITwoWayConverter` | [Bind Modes](../tutorials/03.%20BindModes/README.md) |
| 4 | **Stats** | commands with a parameter, `CanExecute`, draft → model | [Stats](../tutorials/04.%20Stats/README.md) |
| 5 | **Todo List** | a model, `ObservableList`, `CreateSync`, collection binders | [Todo List](../tutorials/05.%20TodoList/README.md) |
| 6 | **Custom Binder** | a binder for your own component, `[GenerateSerializableBinder]` | [Custom Binder](../tutorials/06.%20CustomBinder/README.md) |

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
        _counterView.DeinitializeViewSafely()?.DisposeViewModel();
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

* [Counter](../tutorials/01.%20Counter/README.md), the full tutorial with binder details
* [Greeter](../tutorials/02.%20Greeter/README.md), two-way binding: InputField → Text in real time
* [Architecture](02-architecture.md), the binding pipeline in detail
* [Binding Modes](03-binding-modes.md): OneWay, TwoWay, OneTime, OneWayToSource
* [StarterKit](StarterKit/README.md), every ready-made binder for Unity UI
