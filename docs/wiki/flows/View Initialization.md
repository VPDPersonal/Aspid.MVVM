---
title: View Initialization
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/IView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Extensions/ViewExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/ViewAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/AsBinderAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Views/MonoView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Views/MonoView.Instantiate.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Binders/MonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/ViewModels/MonoViewModel.cs
tags: [flow, view, initialization, binding, lifecycle]
updated_at: 2026-05-31
---

# View Initialization

> How a View attaches its binders to a ViewModel — the runtime handshake that turns serialized binder lists into live, two-way data flow.

A [[View]] is dormant until it is handed an [[IViewModel]]. Initialization is the moment binders look up their target bindable member by name and subscribe. Because `[View]` is source-generated, the entry point (`Initialize`) is invisible in hand-written code; you only see the hooks.

## Walkthrough

1. **Trigger.** Something supplies a ViewModel — `view.Initialize(viewModel)` directly, one of the static `MonoView.Instantiate<T>(...)` overloads (instantiate + initialize in one call), or `IView.Reinitialize` via `ViewExtensions`. [[DI Integration]] containers do the same call.

2. **Generated `Initialize`.** The `[View]` generator emits the [[View|IView]] implementation: it stores `viewModel` into the `ViewModel` property and invokes the partial hook `OnInitializingInternal(viewModel)`. (The generated body also wires any auto-generated binder fields when `AutoBinderFields` is true — see [[View]] and [[Source Generation Pipeline]].)

3. **Hook fans out to binder groups.** In `MonoView`, the hand-written `OnInitializingInternal` loops over the serialized `_bindersList`. Each `Binders` group pairs a member `_name` (the binder ID) with an array of `MonoBinder` components configured in the Inspector.

4. **Resolve the bindable member.** Each group calls `viewModel.FindBindableMember(new FindBindableMemberParameters(_name))`. If the member is not found, the group is skipped silently — a misspelled ID simply does nothing. See [[Runtime Binding Resolution]] and [[Bindable Members]].

5. **Bind each MonoBinder.** On a hit, `_monoBinders.BindSafely(result, owner, _name)` invokes each binder. `MonoBinder.Bind` skips if `IsBound` (logging an error) or `!IsBind`, calls `OnBinding`, registers itself via the member's `IBinderAdder.Add` (keeping the returned `IBinderRemover`), sets `IsBound`, then calls `OnBound`. The [[BindMode]] (default `TwoWay`) governs direction.

6. **Initial push / source pull.** The bindable member pushes its current value into each newly added binder, so the UI reflects state immediately. (This is the member's responsibility; see [[Bindable Members]].)

## Teardown (reverse path)

`Deinitialize` (generated) calls `OnDeinitializingInternal`, which loops groups calling `UnbindSafely`; each `MonoBinder.Unbind` runs `OnUnbinding`, invokes its stored `IBinderRemover.Remove`, clears `IsBound`, then `OnUnbound`, and nulls `ViewModel`. `MonoView.OnDestroy` → `Dispose` → `Deinitialize`, so destroying the GameObject auto-detaches. `ViewExtensions.DisposeView` prefers `IDisposable` over plain deinitialize.

## Notes

- A `MonoViewModel` is itself `MonoBehaviour`; its `OnValidate` calls the generated `NotifyAll()` to repaint bound editors.
- [[IRelayCommand]] members bind through the same `FindBindableMember` path as data members.

See also: [[Data Binding]], [[Binder Base Classes]], [[ViewModel to Generated Code]].
