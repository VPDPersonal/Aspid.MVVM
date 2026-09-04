# View Initializers

`ViewInitializer` initializes a View from the Inspector, without bootstrap code.

## Contents

- [Overview](#overview)
- [ViewInitializer](#viewinitializer)
- [InitializeStage](#initializestage)
- [ViewInitializerManual](#viewinitializermanual)
- [InitializeComponent](#initializecomponent)
- [Inspector setup](#inspector-setup)

---

## Overview

Instead of a Bootstrap script:

```csharp
// Without ViewInitializer a script is needed:
private void Awake()
{
    var viewModel = new PlayerViewModel();
    _view.Initialize(viewModel);
}
```

use the `ViewInitializer` component and configure everything in the Inspector:

1. Add `ViewInitializer` to a GameObject
2. Set the View (a `MonoView` component)
3. Set the ViewModel (a component, a ScriptableObject or DI)
4. Pick the initialization stage (Awake, Start, OnEnable, Manual, Di)

---

## ViewInitializer

Fully automatic initialization:

```csharp
public class ViewInitializer : ViewInitializerBase
{
    // Serialized fields, configured in the Inspector:
    // - View component(s)
    // - ViewModel source(s)
    // - InitializeStage
    // - _isDisposeViewOnDestroy
    // - _isDisposeViewModelOnDestroy
}
```

---

## InitializeStage

Defines when initialization happens:

| Stage | When | Deinitialization |
|------|-------|-----------------|
| `Awake` | In `Awake()` | In `OnDestroy()` |
| `Start` | In `Start()` | In `OnDestroy()` |
| `OnEnable` | In `OnEnable()` | In `OnDisable()` |
| `Manual` | On `Initialize()` | On `Deinitialize()` |
| `DiConstructor` | When the DI container injects | In `OnDestroy()` |

### Awake (default)

Initialization in `Awake`. Fits most cases.

### OnEnable / OnDisable

Handy for screens that are toggled: the View initializes on activation and deinitializes on deactivation:

```
GameObject.SetActive(true)  → OnEnable → Initialize
GameObject.SetActive(false) → OnDisable → Deinitialize
```

### Manual

Initialization is driven from code:

```csharp
[SerializeField] private ViewInitializer _initializer;

public void Show(IViewModel viewModel)
{
    _initializer.Initialize(); // Uses the ViewModel configured in the Inspector
}

public void Hide()
{
    _initializer.Deinitialize();
}
```

### DiConstructor

The ViewModel is resolved from a DI container. See [DI Integration](12-di-integration.md).

---

## ViewInitializerManual

A simplified variant that needs an explicit call from code:

```csharp
[SerializeField] private ViewInitializerManual _initializer;

public void Show(IViewModel viewModel)
{
    _initializer.Initialize(viewModel);
}

public void Hide()
{
    _initializer.Deinitialize();
}
```

**Differences from ViewInitializer:**
- No `InitializeStage`, manual call only
- The ViewModel is passed to `Initialize(IViewModel)` directly
- Cannot be initialized twice without deinitializing

---

## InitializeComponent

The View/ViewModel source setting inside ViewInitializer:

### ResolveType

| Type | Description |
|-----|----------|
| `Component` | A Component reference in the Inspector |
| `Reference` | `[SerializeReference]`, for POCO objects |
| `ScriptableObject` | A ScriptableObject reference |
| `Di` | Resolution through a DI container (Zenject/VContainer) |

### ViewModelInitializeComponent

The ViewModel specialization:
- `ResolveType.Component` points at a `MonoViewModel` component
- `ResolveType.ScriptableObject` points at a `ScriptableViewModel`
- `ResolveType.Di` uses a `TypeSelector` to pick the ViewModel type from the container

---

## Inspector setup

### Step 1: Add ViewInitializer

Add the `ViewInitializer` component to a GameObject.

### Step 2: Set the Stage

Pick the `InitializeStage` (Awake by default).

### Step 3: Set the View

In the Views section add the View components (`MonoView`).

### Step 4: Set the ViewModel

In the ViewModel section pick the `ResolveType` and the source:
- **Mono**: drag a `MonoViewModel` from the Inspector
- **ScriptableObject**: drag a `ScriptableViewModel`
- **Di**: pick the type for DI resolution

### Step 5: Cleanup options

- `_isDisposeViewOnDestroy`: deinitialize the View in OnDestroy
- `_isDisposeViewModelOnDestroy`: call Dispose on the ViewModel

---

## See also

- [Views](05-views.md), initialization from code
- [DI Integration](12-di-integration.md), DiConstructor with Zenject/VContainer
