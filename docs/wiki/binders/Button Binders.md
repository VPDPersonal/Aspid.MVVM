---
title: Button Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/Command/ButtonCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/Command/Mono/ButtonCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/OneWayToSource/Mono/ButtonToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Commands/CanExecuteView/InteractableMode.cs
tags: [binder, starterkit, button, command, ui]
updated_at: 2026-05-31
---

# Button Binders

> Wire a Unity UI `Button` to an [[IRelayCommand]] so a click executes a ViewModel command — and the button's enabled/visible state follows the command's `CanExecute`.

Unlike most binders, which push a *value* to a target, the Button family binds a *behaviour*: it forwards `Button.onClick` into a command and reflects that command's executability back onto the button. This is the canonical way to invoke [[Relay Commands]] from the UI.

## Family

| Variant | Base | Binds | Notable |
|---|---|---|---|
| `ButtonCommandBinder` / `ButtonCommandBinder<T..T4>` | [[Binder Base Classes\|TargetBinder<Button>]] | `IRelayCommand`, `IRelayCommand<T..T4>` | Plain serializable binder; constructor takes the `Button` directly |
| `ButtonCommandMonoBinder` | [[Mono Binders\|ComponentMonoBinder<Button>]] | `IRelayCommand` **and** `IRelayCommand<bool>` | Component on the same GameObject; bool overload receives `true` on click |
| `ButtonCommandMonoBinder<T..T4>` | `ComponentMonoBinder<Button>` | `IRelayCommand<T..T4>` | **abstract** — needs a concrete sealed subclass to be usable |
| `ButtonToSourceMonoBinder` | [[Mono Binders\|ComponentToSourceMonoBinder<Button>]] | (sends the `Button` itself) | OneWayToSource: pushes the component reference up to the ViewModel |

## Variant axes

- **Plain vs Mono.** The plain `ButtonCommandBinder` is a `[Serializable]` field you supply a `Button` to (used inside a [[View]] or composed in code). The `…MonoBinder` is a MonoBehaviour you drop on the button's GameObject; it resolves its `CachedComponent` automatically and ships with `[AddComponentMenu]` entries.
- **Command arity (0–4 params).** Each variant has a non-generic form plus `<T>`, `<T1,T2>`, `<T1,T2,T3>`, `<T1,T2,T4>`. Generic forms expose serialized `Param1…Param4` fields that are forwarded to `Execute(...)` on click.
- **OneWayToSource.** `ButtonToSourceMonoBinder` is the inverse direction: instead of receiving a command it hands the `Button` reference to the ViewModel via `ComponentToSourceMonoBinder<Button>` (see [[BindMode]] `OneWayToSource`).

> Switcher and Enum / EnumGroup variants exist for other Selectable families (see [[Toggle Binders]] / [[Dropdown Binders]]); the Button folder itself only ships the Command and OneWayToSource variants above.

## Notable behavior

- **`CanExecute` → interactable.** An `InteractableMode` (`None`, `Visible`, `Interactable`, `Custom`) controls how `CanExecuteChanged` is reflected: toggle `Button.interactable`, toggle `gameObject.SetActive`, or delegate to a `[SerializeReference] ICanExecuteView` (`Custom`). Default is `Interactable`.
- **No TwoWay.** Constructors call `mode.ThrowExceptionIfTwo()` — `BindMode.TwoWay` is rejected.
- **Lifecycle.** `OnBound` subscribes to `onClick`; `OnUnbound` removes the listener and passes `null` to `SetValue` to detach the command and unsubscribe `CanExecuteChanged` (via `CommandBinderExtensions.UpdateCommand`). The generic Mono variants are abstract, so the source-generated [[View]] wiring targets your concrete subclass.

## Source

`StarterKit/Unity/Runtime/Binders/Buttons/` — `Command/ButtonCommandBinder.cs`, `Command/Mono/ButtonCommandMonoBinder.cs`, `OneWayToSource/Mono/ButtonToSourceMonoBinder.cs`. Related: [[IRelayCommand]], [[IBinder]], [[Binder Base Classes]], [[Selectable Binders]], [[Binders Catalog]].
