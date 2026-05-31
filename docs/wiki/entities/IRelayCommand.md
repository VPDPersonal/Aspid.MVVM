---
title: IRelayCommand
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs
tags:
  - command
  - entity
  - viewmodel
updated_at: 2026-05-31
---

# IRelayCommand

> The command contract a ViewModel exposes so a View can invoke an action and ask whether it is currently allowed — bound by Button/Selectable binders.

## Why it exists

MVVM keeps the View dumb: a button should not call business logic directly. Instead the ViewModel exposes a command object the binder can `Execute()` and poll with `CanExecute()`, plus a `CanExecuteChanged` event so the UI re-evaluates (e.g. disables a button) when state shifts. `IRelayCommand` is that contract; it parallels WPF's `ICommand` but is allocation-light and reflection-free, matching Aspid's performance goals.

## How it works

`IRelayCommand.cs` declares five arities — parameterless `IRelayCommand` plus generic `IRelayCommand<T>` … `IRelayCommand<T1,T2,T3,T4>`. Each member is:

- `bool CanExecute(...)` — gate predicate.
- `void Execute(...)` — runs the action.
- `event Action<...> CanExecuteChanged` — raised when executability may have changed.
- `void NotifyCanExecuteChanged()` — raises that event manually.

`RelayCommand.cs` provides the sealed implementations. Each wraps an `Action` (execute) and an optional `Func<bool>` (`canExecute`). Construction throws `ArgumentNullException` if `execute` is null. `Execute()` internally re-checks `CanExecute()` first and no-ops if it returns false; a null predicate is treated as always-executable (`?? true`).

Two reusable singletons exist per arity: `Empty` (cannot execute) and `EmptyExecution` (executes but does nothing) — handy null-object placeholders where a non-null command is required.

When `UNITY_2022_1_OR_NEWER` is set and the profiler is not disabled, calls are wrapped in a profiler `Marker()` for diagnostics.

## Relation to [RelayCommand] generation

You rarely write `new RelayCommand(...)` by hand. Marking a method with `[RelayCommand]` makes the [[Source Generator]] emit a lazily-constructed `IRelayCommand` property on the `partial` class, wiring your method as `execute` and an optional `CanExecute*` method as the predicate. See [[Relay Commands]] for the authoring side and generated members. The host class must be `partial` ([[Must Be Partial]]).

## Key relationships

- Surfaced through a [[ViewModel]] / [[IViewModel]] as a bindable member ([[Bindable Members]]).
- Consumed by [[Button Binders]] and [[Selectable Binders]] via [[Runtime Binding Resolution]].
- Authoring + generated output: [[Relay Commands]], [[ViewModel to Generated Code]].

## Source

- `Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs`
- `Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs`
