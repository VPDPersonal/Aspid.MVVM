---
title: Debug Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/DebugLogBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/Mono/DebugLogMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/XmlExampleDoc-Debug-Log-1.1.0.xml
tags: [binder, starterkit, debug, logging, diagnostics]
updated_at: 2026-05-31
---

# Debug Binders

> A diagnostic binder that does not touch the UI — it just logs every binding event and value to the Unity console, so you can watch what a [[Bindable Members|bindable member]] actually pushes.

## Why it exists

When a binding "does nothing", you need to know whether the [[ViewModel]] is emitting values at all, and what they look like. Instead of editing the ViewModel or attaching a debugger, you drop a Debug binder onto any [[Bindable Members|bindable member]] and read the console. It is a development aid, not a runtime feature.

## Family

| Variant | Base | Use as | Target |
|---------|------|--------|--------|
| `DebugLogBinder` | [[Binder Base Classes\|Binder]] | `[SerializeField]` field inside a [[View]] | Unity console (`Debug.Log`) |
| `DebugLogMonoBinder` | [[Mono Binders\|MonoBinder]] | drag-on `MonoBehaviour` component | Unity console (`Debug.Log`) |

Both live in `Aspid.MVVM.StarterKit` and are `sealed`.

## Variant axes

- **Plain vs Mono** — the only axis present in this family. `DebugLogBinder` is a `[Serializable]` plain [[Binder Base Classes|Binder]] you reference from a `[SerializeField]` in a [[View]]; `DebugLogMonoBinder` is a `MonoBehaviour` you attach via the `Aspid/MVVM/Binders/Debug` component menu and the binder context menu. No Switcher, Enum/EnumGroup, or dedicated OneWayToSource variants exist here.
- **OneWayToSource is built in, not a separate class.** Both implement `IAnyBinder` *and* `IAnyReverseBinder`, so a single instance handles every direction. Each is marked `[BindModeOverride(IsAll = true)]`, meaning it accepts any [[BindMode]] the member declares.

## Notable behavior

- `SetValue<T>(T value)` logs `SetValue: <message>` for inbound values (OneWay/TwoWay). On `DebugLogMonoBinder` this method also carries `[BinderLog]`, which (likely) drives generated logging via the [[Unity Generators]].
- The `ValueChanged` event (the reverse / [[BindMode|OneWayToSource]] channel) logs on both `add` and `remove` of subscribers, so you see when the member hooks and unhooks.
- A serialized `_converter` (a [[Converters|converter]], defaulting to `ObjectToStringConverter`) formats each value; `GetMessage` falls back to `value.ToString()` when no converter is set. The field uses `[SerializeReferenceDropdown]` so a converter can be picked in the Inspector.
- `DebugLogMonoBinder` is `partial` — the StarterKit binders rely on the [[Source Generation Pipeline|source generators]], so generated members are invisible in this hand-written file (see [[Must Be Partial]]).

## Source

- `StarterKit/Unity/Runtime/Binders/Debugs/Log/DebugLogBinder.cs`
- `StarterKit/Unity/Runtime/Binders/Debugs/Log/Mono/DebugLogMonoBinder.cs`

See also [[Binders Catalog]], [[Data Binding]], [[IBinder]].
