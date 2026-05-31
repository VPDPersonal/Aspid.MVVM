---
title: LocalizeStringEvent Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/LocalizeStringEventEntryBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/LocalizeStringEventEntrySwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/Mono/LocalizeStringEventEntryMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Entry/Mono/LocalizeStringEventEntryEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/Variable/LocalizeStringEventVariableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/OneWayToSource/Mono/LocalizeStringEventToSourceMonoBinder.cs
tags: [binder, starterkit, localization, unity]
updated_at: 2026-05-31
---

# LocalizeStringEvent Binders

> StarterKit binders that drive Unity's `LocalizeStringEvent` from a ViewModel — switching the localized table entry, or feeding Smart String variables — so localized text reacts to your data.

## Why it exists

Unity's localization package shows translated text via a `LocalizeStringEvent` component, whose `StringReference` holds both the entry key (which row in the table) and named *Smart String variables* (runtime substitutions). This family lets a ViewModel control either of those without hand-wiring, so the displayed entry or its `{variable}` placeholders update on value change. The whole folder is gated behind `#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION`, so it compiles only when the localization package is present.

## Family

| Group | Target property | Bound type | Variants |
|-------|-----------------|------------|----------|
| Entry | `StringReference.TableEntryReference` | `string` | Plain, Mono, Switcher (plain + Mono), Enum, EnumGroup |
| Variable | named Smart String `IVariable` | numbers, `bool`, `string`, `Object` | Plain, Mono |
| OneWayToSource | the bound property | — | Mono only |

## Variant axes

- **Entry vs Variable** — Entry rewrites which translation row is shown; Variable updates a named `Smart String` substitution (e.g. `IntVariable`, `StringVariable`, `ObjectVariable`) then calls `RefreshString()`. The Variable binder implements [[IBinder]]`<bool>`/`<string>`/`<Object>` plus `INumberBinder`, lazily creating the variable if absent.
- **Plain vs Mono** — Plain binders (e.g. `LocalizeStringEventEntryBinder`, subclassing `TargetStringBinder`/`TargetBinder`) are `[Serializable]` and constructed in code. Mono binders (subclassing `ComponentStringMonoBinder`/`TargetBinder` MonoBehaviours) are inspector components carrying `[AddComponentMenu]` / `[AddBinderContextMenu]`. See [[Mono Binders]].
- **Switcher** — picks between a `trueValue` and `falseValue` entry key from a bound `bool` (`SwitcherStringBinder`).
- **Enum / EnumGroup** — Enum maps an enum value to one entry; EnumGroup sets the entry on *each element* in a configured group of `LocalizeStringEvent`s.
- **OneWayToSource** — `LocalizeStringEventToSourceMonoBinder` (a `ComponentToSourceMonoBinder`) pushes the component's current value back to the ViewModel when binding is established.

## Notable behavior

- Entry/Variable binders reject [[BindMode|TwoWay]] in their constructors (`ThrowExceptionIfMatches`/`ThrowExceptionIfTwo`); valid modes are OneWay/OneTime, with the Entry Mono variant also supporting OneWayToSource (forwards the current `TableEntryReference` on bind).
- The Variable binder will `Add` a new variable under the configured name if the `StringReference` has none, and throws `InvalidCastException` if the existing variable's type mismatches.

## Source

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LocalizeStringEvents/` — `Entry/`, `Variable/`, `OneWayToSource/`. Shared bases live under [[Binder Base Classes]]; see also [[Text Binders]], [[String Converters]], [[Binders Catalog]].
