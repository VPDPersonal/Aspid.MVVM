---
title: DI Integration
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/ViewInitializerBase.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/ViewInitializer.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/Components/InitializeComponent.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Views/Initializers/Components/ResolveType.cs
  - Readme.md
tags: [di, zenject, vcontainer, view-initialization, starterkit]
updated_at: 2026-05-31
---

# DI Integration

> How a [[View]] gets its ViewModel from a Zenject or VContainer container instead of an Inspector reference — gated behind opt-in compile symbols so the core stays DI-free.

## Why it exists

A View needs a ViewModel to bind against. The StarterKit lets you wire that in the Inspector, but in DI-driven projects the ViewModel (and its dependencies) live in a container. Rather than hard-depend on Zenject or VContainer, Aspid keeps both behind conditional compilation so the framework compiles with neither installed.

## How it works

Everything is gated by two define symbols (set them in Player Settings):

- `ASPID_MVVM_ZENJECT_INTEGRATION`
- `ASPID_MVVM_VCONTAINER_INTEGRATION`

When a symbol is defined, three things light up in the StarterKit initializers (see [[View Initialization]]):

1. **Container capture** — `ViewInitializerBase` gains an injected field: Zenject's `DiContainer` (`[Zenject.Inject]`) or VContainer's `IObjectResolver` (`[VContainer.Inject]`). It forwards that container into each `InitializeComponent`.
2. **DI resolve path** — `InitializeComponent<T>` adds a `ResolveType.Di` enum value. When chosen in the Inspector, `GetComponent()` calls `ZenjectContainer?.TryResolve(type)` or `VContainerContainer?.TryResolve(type, out ...)`, where `type` comes from the abstract `GetTypeForDi()`. This is how a ViewModel is pulled from the container instead of a serialized `Mono` / `References` / `ScriptableObject` field.
3. **DI lifecycle stage** — `ViewInitializer` adds an `InitializeStage.DiConstructor` option. An injected method (`ZenjectConstructor` / `VContainerConstructor`) fires when the container builds the object, triggering initialization at inject time rather than `Awake` / `OnEnable` / `Start` / `Manual`.

Without either symbol, `ResolveType.Di` and `InitializeStage.DiConstructor` simply don't exist — the initializer behaves as a plain MonoBehaviour.

## Key relationships

- Drives [[View Initialization]]: DI is one `ResolveType` among `Mono`, `References`, `ScriptableObject`.
- Resolves the ViewModel consumed by a [[View]] / [[IViewModel]].
- Lives entirely in [[StarterKit ViewModels|StarterKit]]; the core [[Architecture]] has no DI dependency.
- Zenject / VContainer are [[External Dependencies]], not bundled.

## Source

- `StarterKit/.../Views/Initializers/ViewInitializerBase.cs` — injected container field, forwarding.
- `StarterKit/.../Views/Initializers/ViewInitializer.cs` — `DiConstructor` stage, inject methods.
- `StarterKit/.../Initializers/Components/InitializeComponent.cs` — `ResolveType.Di` resolution.
- `StarterKit/.../Initializers/Components/ResolveType.cs` — gated `Di` enum value.

> Note: the build also defines a `Zenject`-style integration for prefab View factories (e.g. `PrefabViewFactory`); that path appears to follow the same symbol-gated pattern but is not covered here.
