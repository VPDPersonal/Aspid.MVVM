# DI Integration

Aspid.MVVM supports Zenject and VContainer for resolving ViewModels through Dependency Injection.

## Contents

- [Overview](#overview)
- [Zenject](#zenject)
- [VContainer](#vcontainer)
- [DiConstructor](#diconstructor)

---

## Overview

DI integration lets you:
- Resolve ViewModels from a DI container
- Inject dependencies into ViewModels
- Use `ViewInitializer` with `InitializeStage.DiConstructor`

Two DI frameworks are supported:
- **Zenject** (Extenject)
- **VContainer**

---

## Zenject

### Step 1: Define the compilation symbol

In `Project Settings → Player → Scripting Define Symbols` add:

```
ASPID_MVVM_ZENJECT_INTEGRATION
```

### Step 2: Register the ViewModel in the container

```csharp
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerViewModel>().AsSingle();
        Container.Bind<InventoryViewModel>().AsSingle();
    }
}
```

### Step 3: Configure ViewInitializer

1. Add `ViewInitializer` to a GameObject
2. Set `InitializeStage` → **DiConstructor**
3. In the ViewModel section set `ResolveType` → **Di**
4. In `TypeSelector` pick the ViewModel type (for example `PlayerViewModel`)

Zenject injects the container into `ViewInitializerBase` through `[Inject]`.

### A ViewModel with Zenject

```csharp
[ViewModel]
public partial class PlayerViewModel
{
    [OneWayBind] private string _name;
    [OneWayBind] private int _health;

    private readonly IPlayerService _playerService;

    // Zenject injects IPlayerService
    public PlayerViewModel(IPlayerService playerService)
    {
        _playerService = playerService;
        _name = playerService.Name;
        _health = playerService.Health;
    }
}
```

---

## VContainer

### Step 1: Define the compilation symbol

```
ASPID_MVVM_VCONTAINER_INTEGRATION
```

### Step 2: Register the ViewModel

```csharp
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerViewModel>(Lifetime.Scoped);
        builder.Register<InventoryViewModel>(Lifetime.Scoped);
    }
}
```

### Step 3: Configure ViewInitializer

Same as Zenject: in the Inspector with `InitializeStage.DiConstructor`.

---

## DiConstructor

`InitializeStage.DiConstructor` is a special stage where:

1. The DI container injects itself into `ViewInitializerBase`
2. On initialization a `ViewModelInitializeComponent` with `ResolveType.Di` asks the container
3. The container creates the ViewModel with all dependencies resolved
4. The View is initialized with that ViewModel

### ViewModelInitializeComponent with Di

In the Inspector:
- `ResolveType` → **Di**
- `TypeSelector` → pick the concrete ViewModel type

`TypeSelector` shows the type name (a string) by which the container finds the registration.

---

## Without DI: manual initialization

If you do not use DI:
- The Bootstrap pattern with `view.Initialize(viewModel)`, see [Getting Started](01-getting-started.md)
- `ViewInitializer` with `ResolveType.Component` or `ResolveType.ScriptableObject`
- `ViewInitializerManual` with the ViewModel passed from code

---

## See also

- [View Initializers](11-view-initializers.md), initialization details
- [ViewModels](04-viewmodels.md), creating a ViewModel
- [Getting Started](01-getting-started.md), initialization without DI
