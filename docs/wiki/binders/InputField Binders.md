---
title: InputField Binders
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/Text/InputFieldBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/Text/Mono/InputFieldMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/Command/InputFieldCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/Content/InputFieldContentTypeSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/Content/Mono/InputFieldContentTypeEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/InputFields/OneWayToSource/Mono/InputFieldToSourceMonoBinder.cs
tags: [binder, starterkit, inputfield, tmp, unity-ui]
updated_at: 2026-05-31
---

# InputField Binders

> StarterKit binders that connect a TMPro `TMP_InputField` to ViewModel members — text in both directions, command triggering, and editor-config properties (content/input/line type, character validation).

## What it binds

The whole family targets `TMP_InputField`. The flagship `InputFieldBinder` binds `TMP_InputField.text`, supports `OneWay`, `TwoWay`, and `OneWayToSource` (`[BindModeOverride(IsAll = true)]`), and also implements `INumberBinder` / `IReverseBinder<string>` / `INumberReverseBinder` so it can format `int`/`long`/`float`/`double` (via `CultureInfoMode`) and push view text back to the source. The view-to-source direction subscribes to a configurable `UpdateInputFieldEvent` (`OnValueChanged`, `OnEndEdit`, `OnSubmit`, `OnSelect`, `OnDeselect`). The whole file is gated by `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`.

## Family

| Subfolder | Target property | Notable variants |
|-----------|-----------------|------------------|
| `Text/` | `text` | `InputFieldBinder` (plain), `InputFieldMonoBinder` (Mono) |
| `Command/` | events → command | `InputFieldCommandBinder` + generic `<T>`, `<T1,T2>`, `<T1,T2,T3>` |
| `Content/` | `contentType` | Binder, Switcher, Enum, EnumGroup |
| `InputType/` | `inputType` | Binder, Switcher (+ Mono) |
| `LineType/` | `lineType` | Binder, Switcher (+ Mono) |
| `CharacterValidation/` | `characterValidation` | Binder, Switcher (+ Mono) |
| `OneWayToSource/` | bound value → VM | `InputFieldToSourceMonoBinder` |

## Variant axes

- **Plain vs Mono** — plain binders (e.g. `InputFieldBinder`) derive from [[Binder Base Classes|TargetBinder]] and are `[Serializable]` fields; Mono variants (`InputFieldMonoBinder`) derive from `ComponentMonoBinder<TMP_InputField>` and are drop-on [[Mono Binders|MonoBinder]] components with `[AddComponentMenu]` / `[AddBinderContextMenu]`. See [[Mono Binders]].
- **Switcher** — `SwitcherBinder<TMP_InputField, TEnum>` picks between a `trueValue`/`falseValue` from a bound `bool` (`OneWay` only; calls `ForceLabelUpdate()` after applying).
- **Enum / EnumGroup** — `EnumMonoBinder` sets the property from a bound enum; `EnumGroupMonoBinder` applies the resolved value across a group of fields.
- **OneWayToSource** — `InputFieldToSourceMonoBinder` is a thin `ComponentToSourceMonoBinder<TMP_InputField>` that pushes the current bound value to the ViewModel at bind time.

## Notable behavior

- `SetValue` suppresses re-notification (`_isNotifyValueChanged`) so programmatic text changes don't echo back to the source.
- Numeric events only fire when `contentType` is `IntegerNumber`/`DecimalNumber`.
- `InputFieldCommandBinder` reflects `IRelayCommand.CanExecute()` via `InteractableMode` (`Interactable`/`Visible`/`Custom`); `TwoWay` is rejected. See [[Relay Commands]], [[IRelayCommand]].
- The Mono text binder re-subscribes in `OnValidate` during play so editor tweaks take effect live.

## Source

See [[Binders Catalog]], [[IBinder]], [[BindMode]], [[Data Binding]], [[Converters]], [[Dropdown Binders]].
