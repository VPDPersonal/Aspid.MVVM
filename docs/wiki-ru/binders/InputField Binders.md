---
title: Биндеры InputField
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
lang: ru
translated_from: docs/wiki/binders/InputField Binders.md
translated_at: 2026-05-31
---

# Биндеры InputField

> Биндеры из StarterKit, которые связывают TMPro `TMP_InputField` с членами ViewModel — текст в обоих направлениях, вызов команд и свойства конфигурации редактора (тип контента/ввода/строки, валидация символов).

## Что биндится

Всё это семейство нацелено на `TMP_InputField`. Флагманский `InputFieldBinder` биндит `TMP_InputField.text`, поддерживает `OneWay`, `TwoWay` и `OneWayToSource` (`[BindModeOverride(IsAll = true)]`), а также реализует `INumberBinder` / `IReverseBinder<string>` / `INumberReverseBinder`, благодаря чему может форматировать `int`/`long`/`float`/`double` (через `CultureInfoMode`) и передавать текст из view обратно в источник. Направление view-to-source подписывается на настраиваемое событие `UpdateInputFieldEvent` (`OnValueChanged`, `OnEndEdit`, `OnSubmit`, `OnSelect`, `OnDeselect`). Весь файл закрыт условием `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`.

## Семейство

| Подпапка | Целевое свойство | Заметные варианты |
|-----------|-----------------|------------------|
| `Text/` | `text` | `InputFieldBinder` (обычный), `InputFieldMonoBinder` (Mono) |
| `Command/` | события → команда | `InputFieldCommandBinder` + обобщённые `<T>`, `<T1,T2>`, `<T1,T2,T3>` |
| `Content/` | `contentType` | Binder, Switcher, Enum, EnumGroup |
| `InputType/` | `inputType` | Binder, Switcher (+ Mono) |
| `LineType/` | `lineType` | Binder, Switcher (+ Mono) |
| `CharacterValidation/` | `characterValidation` | Binder, Switcher (+ Mono) |
| `OneWayToSource/` | привязанное значение → VM | `InputFieldToSourceMonoBinder` |

## Оси вариантов

- **Обычный против Mono** — обычные биндеры (например, `InputFieldBinder`) наследуются от [[Binder Base Classes|TargetBinder]] и являются `[Serializable]`-полями; Mono-варианты (`InputFieldMonoBinder`) наследуются от `ComponentMonoBinder<TMP_InputField>` и представляют собой навешиваемые компоненты [[Mono Binders|MonoBinder]] с `[AddComponentMenu]` / `[AddBinderContextMenu]`. См. [[Mono Binders]].
- **Switcher** — `SwitcherBinder<TMP_InputField, TEnum>` выбирает между `trueValue`/`falseValue` на основе привязанного `bool` (только `OneWay`; вызывает `ForceLabelUpdate()` после применения).
- **Enum / EnumGroup** — `EnumMonoBinder` задаёт свойство из привязанного enum; `EnumGroupMonoBinder` применяет вычисленное значение ко всей группе полей.
- **OneWayToSource** — `InputFieldToSourceMonoBinder` представляет собой лёгкий `ComponentToSourceMonoBinder<TMP_InputField>`, который передаёт текущее привязанное значение в ViewModel в момент привязки.

## Заметное поведение

- `SetValue` подавляет повторное уведомление (`_isNotifyValueChanged`), чтобы программные изменения текста не возвращались эхом в источник.
- Числовые события срабатывают только когда `contentType` равен `IntegerNumber`/`DecimalNumber`.
- `InputFieldCommandBinder` отражает `IRelayCommand.CanExecute()` через `InteractableMode` (`Interactable`/`Visible`/`Custom`); `TwoWay` не допускается. См. [[Relay Commands]], [[IRelayCommand]].
- Mono-биндер текста повторно подписывается в `OnValidate` во время игры, поэтому правки в редакторе вступают в силу на лету.

## Исходники

См. [[Binders Catalog]], [[IBinder]], [[BindMode]], [[Data Binding]], [[Converters]], [[Dropdown Binders]].
