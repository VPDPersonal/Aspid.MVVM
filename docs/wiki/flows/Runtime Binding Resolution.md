---
title: Runtime Binding Resolution
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberParameters.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberResult.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderAdder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderRemover.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IReadOnlyBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/OneWayBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/TwoWayBindableMember.cs
tags: [flow, binding, runtime, viewmodel, binder]
updated_at: 2026-05-31
---

# Runtime Binding Resolution

> How a binder, at runtime, locates the ViewModel member it targets and starts moving values — no reflection, just generated lookup plus delegate subscription.

This is the moment binding actually happens: a [[View]] hands a ViewModel to its binders, each binder asks for its member by id, and the matching [[Bindable Members|bindable member]] wires up `Action<T>` delegates per [[BindMode]]. See [[View Initialization]] for what triggers step 1.

## Walkthrough

1. **View init kicks off.** When a View is initialized with a ViewModel (see [[View Initialization]]), it iterates its binders and, for each, calls `FindBindableMember` on the [[IViewModel]], passing a `FindBindableMemberParameters` carrying just the member `Id` (a string).

2. **ViewModel resolves the member.** `IViewModel.FindBindableMember` is **generated** from `[Bind]` fields (invisible in hand-written source; see [[ViewModel to Generated Code]]). It returns a `FindBindableMemberResult` — `IsFound` is `true` only when its `Adder` (an `IBinderAdder`) is non-null. A miss yields an empty result and the binder stays unbound.

3. **Binder binds via the adder.** `Binder.Bind(IBinderAdder)` guards against double-binding and against `IsBind == false`, fires the `OnBinding` hook, then calls `adder.Add(this)`. The returned `IBinderRemover` is stored for later teardown; `IsBound` flips true and `OnBound` fires.

4. **Adder subscribes per BindMode.** The `IBinderAdder` is the bindable member itself ([[Bindable Members|OneWayBindableMember]], [[Bindable Members|TwoWayBindableMember]], etc.). `Add` reads `binder.Mode` and wires delegates accordingly:
   - **OneTime** — pushes the current value via `SetValue` once, then returns `null` (no subscription, nothing to remove).
   - **OneWay** — `SetValue(current)` plus `Changed += binder.SetValue` for future updates.
   - **OneWayToSource** — subscribes the member to the binder's `ValueChanged` event (IReverseBinder); View pushes to ViewModel only.
   - **TwoWay** — both of the above.
   `OneWayBindableMember` accepts only OneWay/OneTime (others throw); `TwoWayBindableMember` supports all modes except `None`.

5. **Values flow.** Forward: setting the member's `Value` raises `Changed`, invoking each subscribed `SetValue`. Reverse: the binder raises `ValueChanged`, the member runs its `setValue` action back into the ViewModel field. Binders implementing `IAnyBinder`/`IAnyReverseBinder` use untyped `object` overloads; the member casts to `T`.

6. **Unbind.** On View deinit, `Binder.Unbind` calls the stored remover's `Remove`, detaching the same delegates, then clears `IsBound`. OneTime binders have no remover, so nothing happens.

## Source

- [[IViewModel]], `FindBindableMemberResult` — resolution entry
- [[IBinder]], `Binder` — bind/unbind lifecycle ([[Binder Base Classes]])
- `OneWayBindableMember` / `TwoWayBindableMember` — per-mode subscription ([[Bindable Members]])
- Related: [[Data Binding]], [[BindMode]]
