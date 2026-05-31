---
title: Relay Commands
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/Extensions/RelayCommandExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/Extensions/RelayCommandExtensions.Action.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/RelayCommandAttribute.cs
tags: [concept, commands, relaycommand, source-generation]
updated_at: 2026-05-31
---

# Relay Commands

> A relay command is a reusable, bindable action: a method exposed as an object that a button (or any binder) can invoke and that can disable itself when execution is not allowed.

## Why it exists

UI elements should not call ViewModel methods directly — they need a stable, bindable handle that also answers "can I run right now?". A relay command packages an *execute* delegate with an optional *canExecute* predicate, so a [[Button Binders|button]] can both trigger the action and grey itself out. This keeps the command pattern allocation-cheap and free of reflection, matching Aspid's [[Data Binding]] goals.

## How it works

The contract lives in [[IRelayCommand]]: `Execute()`, `CanExecute()`, the `CanExecuteChanged` event, and `NotifyCanExecuteChanged()`. There are parameterless and generic overloads (`IRelayCommand<T>` through `IRelayCommand<T1,T2,T3,T4>`), each adding typed parameters to `Execute`/`CanExecute`.

`RelayCommand` (and `RelayCommand<T>` …) is the sealed implementation. It wraps an `Action`/`Action<T>` execute callback plus an optional `Func<bool>`/`Func<T,bool>` predicate. `Execute()` runs the action only if `CanExecute()` returns true; a null predicate means "always executable". Under Unity it wraps calls in a profiler marker (compiled out via `ASPID_MVVM_UNITY_PROFILER_DISABLED`).

Two static placeholders avoid null checks: `RelayCommand.Empty` (never executable) and `RelayCommand.EmptyExecution` (executable, does nothing).

Three ways to obtain one:
1. **Generated** — apply `[RelayCommand]` to a method in a `[ViewModel]` class. The [[Source Generation Pipeline|generator]] emits an `IRelayCommand` property (overload chosen by the method's parameter count) wrapping that method. `[RelayCommand(CanExecute = nameof(...))]` wires a `bool`-returning gate method.
2. **Action variant** — `someAction.CreateCommand(canExecute)` extension builds a `RelayCommand` from delegates by hand.
3. **Direct** — `new RelayCommand(execute, canExecute)`.

`RelayCommandExtensions` also offers `GetSelfOrEmpty()` / `GetSelfOrEmptyExecution()` for null-safe fallback to the placeholders.

## Key relationships

- Marked by `RelayCommandAttribute` (methods only, `AllowMultiple`), processed alongside [[ViewModel to Generated Code]].
- Consumed by binders such as [[Button Binders]] via [[IBinder]] / [[Bindable Members]].
- Conceptually parallel to [[Bindable Members|[Bind] fields]] but for actions rather than state.
- `NotifyCanExecuteChanged()` is the command analogue of [[BindMode|OneWay]] change notification.

## Source

- `Source/Commands/IRelayCommand.cs` — interfaces (0–4 params)
- `Source/Commands/RelayCommand.cs` — sealed implementations + `Empty`/`EmptyExecution`
- `Source/Commands/Extensions/` — `CreateCommand`, `GetSelfOrEmpty`
- `Source/ViewModels/Generation/RelayCommandAttribute.cs` — the `[RelayCommand]` marker
