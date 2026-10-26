<img src="Images/Aspid.MVVMHeaderImage.png" alt="Aspid.MVVM" />

[![Unity 6000.0+](Images/status-badge-unity.svg)](https://assetstore.unity.com/packages/slug/298463)
[![Preview 1.1.0-beta.1](Images/status-badge-preview.svg)](https://github.com/VPDPersonal/Aspid.MVVM/releases/tag/v1.1.0-beta.1)
[![MIT License](Images/status-badge-license.svg)](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE)

Aspid.MVVM is a high-performance MVVM framework for Unity, built on Source Generator: it separates business logic from presentation without reflection, boxing or boilerplate code.

[Documentation](https://vpdpersonal.github.io/Aspid.MVVM/docs)

## Installation

In **Window → Package Manager**, choose **+ → Install package from git URL…**, paste this URL and click **Install**:

```text
https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview
```

The URL installs the latest preview; **Update** in the Package Manager installs the next one.

Requires Unity 6000.0 or newer. If you got the package from the Asset Store, skip the git URL and install it from **My Assets** in the Package Manager. The full walk-through is in [Getting Started](01-getting-started.md).

## Features

### Core

#### [ViewModels](04-viewmodels.md)

The Source Generator turns a field with `[Bind]` into a notifying property. No base class, no wrappers, no boilerplate.

```csharp
[ViewModel]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;
}
```

#### [Binding Modes](03-binding-modes.md)

OneWay, TwoWay, OneTime and OneWayToSource. The ViewModel sets the upper bound, every binder picks its own mode in the Inspector.

```csharp
[OneWayBind] private int _health;
[TwoWayBind] private string _inputText;
```

#### [Commands](07-commands.md)

`[RelayCommand]` turns a method into a command with `CanExecute` support and up to four parameters.

```csharp
[RelayCommand(CanExecute = nameof(CanSubmit))]
private void Submit() { }

private bool CanSubmit() => !string.IsNullOrEmpty(Text);
```

#### [Views](05-views.md)

A View is a MonoBehaviour that hands ViewModel members to binders. The generator writes the initialization code.

```csharp
[View]
public sealed partial class PlayerView : MonoView
{
    [SerializeField] private MonoBinder[] _health;
}
```

### StarterKit

#### [Binders](06-binders.md)

Ready-made binders for uGUI, TextMeshPro, Transform, Animator, AudioSource and more connect a ViewModel member to a component property.

```csharp
public interface IBinder<in T> : IBinder
{
    void SetValue(T value);
}
```

#### [Converters](08-converters.md)

Transform values on their way to the View without altering the ViewModel. Pick one in the Inspector or write your own.

```csharp
public interface IConverter<in TFrom, out TTo> : IConverter
{
    TTo Convert(TFrom value);
}
```

#### [Collections](09-collections.md)

Observable lists, dictionaries, sets, stacks and queues with filtering, sorting and synchronization between dependent collections.

```csharp
IReadOnlyObservableListSync<TodoItemViewModel> items =
    todos.CreateSync(item => new TodoItemViewModel(item));
```

#### [Dynamic ViewModel](10-dynamic-viewmodel.md)

Builds a typed ViewModel at runtime for simple structured data, without a dedicated class.

```csharp
var viewModel = new DynamicViewModel();
IDynamicProperty<int> health = viewModel.Add("Health", 100);
view.Initialize(viewModel);
```

### Integration & tooling

#### [View Initializers](11-view-initializers.md)

Initialize a View from the Inspector, without bootstrap code. [DI integration](12-di-integration.md) resolves the ViewModel through Zenject or VContainer.

```csharp
// Without ViewInitializer a script is needed:
private void Awake() => _view.Initialize(new PlayerViewModel());
```

#### [Analyzers](13-analyzers.md)

Roslyn analyzers flag a missing `partial`, a wrong `CanExecute` signature or a backing field used where the property was meant, with code fixes.

```text
AMVVM001  A class with [ViewModel] must be partial
AMVVM005  The CanExecute method/property was not found
```

## Resources

- [Tutorials](../Samples~/01.%20Counter/README.md) — six samples that teach one concept each, from a counter to a custom binder.
- [API reference](https://vpdpersonal.github.io/Aspid.MVVM/api/Aspid.MVVM) — package types, methods and properties.
- [Changelog](https://vpdpersonal.github.io/Aspid.MVVM/changelog) — changes and fixes by version.
- [Source generators](https://github.com/VPDPersonal/Aspid.MVVM.Generators) — the Roslyn generators and analyzers behind the attributes.

## Help and support

Report bugs or ask questions in [GitHub Issues](https://github.com/VPDPersonal/Aspid.MVVM/issues). Include your Unity version, package version and steps to reproduce a problem.

If the package helps you, star it on [GitHub](https://github.com/VPDPersonal/Aspid.MVVM).

Distributed under the [MIT License](https://github.com/VPDPersonal/Aspid.MVVM/blob/main/LICENSE).

## Support the project

Aspid.MVVM is developed in spare time. If it helps you, support its development on the [Unity Asset Store](https://assetstore.unity.com/packages/slug/298463).
