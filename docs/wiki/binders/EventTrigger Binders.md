---
title: EventTrigger Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/Command/EventTriggerCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/Command/Mono/EventTriggerCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/EventTriggers/OneWayToSource/Mono/EventTriggerToSourceMonoBinder.cs
tags: [binder, starterkit, eventtrigger, command]
updated_at: 2026-05-31
---

# EventTrigger Binders

> Wire Unity's `EventTrigger` pointer/UI events (PointerDown, Drag, Scroll, etc.) to ViewModel [[IRelayCommand|relay commands]] — letting a View react to raw event data without writing MonoBehaviour event handlers.

## Why it exists

Unity's `EventTrigger` exposes many `EventSystem` events (`PointerEnter`, `Drag`, `Submit`...) that buttons/toggles don't surface. These binders let a single configured `EventTriggerType` route to a [[Relay Commands|relay command]], so input handling lives in the ViewModel instead of glue code. See [[Relay Commands]] and [[Bindable Members]].

## Family

| Variant | Base class | What it binds |
|---|---|---|
| `EventTriggerCommandBinder` | `TargetBinder<EventTrigger>` (Plain) | executes a command on event fire |
| `EventTriggerCommandMonoBinder` | `ComponentMonoBinder<EventTrigger>` (Mono) | same, as a serialized MonoBehaviour |
| `EventTriggerCommandBinder<T1..T4>` / `...MonoBinder<T1..T4>` | abstract Plain / Mono | adds 1–4 extra forwarded params |
| `EventTriggerToSourceMonoBinder` | `ComponentToSourceMonoBinder<EventTrigger>` | OneWayToSource: pushes the component to the VM |

## Variant axes

- **Plain vs Mono** — Plain (`[Serializable]`) lives inside a [[View]] as a serialized field; Mono is a standalone component (`[AddComponentMenu]` "Aspid/MVVM/Binders/.../EventTrigger"). Both share identical event/command logic; see [[Binder Base Classes]] and [[Mono Binders]].
- **Command** — the core family. Each non-generic binder implements three [[IBinder]] overloads: `IRelayCommand` (no arg), `IRelayCommand<BaseEventData>` (receives the fired event), and `IRelayCommand<EventTriggerType>` (receives the configured type). The generic `<T1..T4>` variants are **abstract** — you must subclass with a concrete sealed type — and prepend serialized `Param1..Param4` to the command call. All three matching command kinds fire together on trigger.
- **OneWayToSource** — `EventTriggerToSourceMonoBinder` is empty; it inherits the base behavior of sending the cached `EventTrigger` to the ViewModel. Use it when the VM needs the component itself rather than reacting to events.
- **No Switcher / Enum / EnumGroup variants** exist in this folder (unlike some other binder families).

## Notable behavior

- On bind (`OnBound`) the binder creates an `EventTrigger.Entry` with `eventID = _event`, hooks its callback, and appends it to `Target.triggers`. On unbind it removes the entry and passes `null` to every `SetValue` overload to detach commands.
- An optional `ICanExecuteView _customInteractable` (a `[SerializeReference]` dropdown) reflects `CanExecute` state — disabling UI when the command can't run. It subscribes to `CanExecuteChanged`.
- `TwoWay` is rejected: the constructor calls `mode.ThrowExceptionIfTwo()`. See [[BindMode]].
- Mono variants re-evaluate `CanExecute` in `OnValidate`, so editor changes refresh interactable state live.

## Source

- `.../Binders/EventTriggers/Command/EventTriggerCommandBinder.cs`
- `.../Command/Mono/EventTriggerCommandMonoBinder.cs`
- `.../OneWayToSource/Mono/EventTriggerToSourceMonoBinder.cs`

Related: [[Button Binders]], [[UnityEvent Binders]], [[Binders Catalog]].
