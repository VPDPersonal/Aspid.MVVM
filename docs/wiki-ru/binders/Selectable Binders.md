---
title: Биндеры Selectable
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/SelectableInteractableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/Mono/SelectableInteractableMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/Interactable/Mono/SelectableInteractableEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/SelectableColorBlockBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/SelectableColorBlockSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/Mono/SelectableColorBlockMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Selectables/ColorBlock/Mono/SelectableColorBlockEnumMonoBinder.cs
tags: [binder, starterkit, ugui, selectable]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Selectable Binders.md
translated_at: 2026-05-31
---

# Биндеры Selectable

> Управляйте UGUI-компонентом `Selectable` (базой для Button, Toggle, Slider, Dropdown, InputField) из ViewModel — включайте/отключайте взаимодействие или подменяйте его цветовые состояния.

## Что связывается

`UnityEngine.UI.Selectable` предоставляет две связываемые поверхности, и семейство делится по ним:

- **Interactable** — bool-свойство `Selectable.interactable`. Используйте его, чтобы блокировать элемент управления по флагу из ViewModel (например, кнопка «Submit», активная только когда форма валидна).
- **ColorBlock** — структура `Selectable.colors` (оттенки normal/highlighted/pressed/disabled). Привяжите её, чтобы реактивно перекрашивать элемент управления.

Поскольку каждый интерактивный UGUI-виджет *является* `Selectable`, эти биндеры работают единообразно со всеми ними, дополняя типозависимые [[Button Binders]], [[Toggle Binders]], [[Slider Binders]], [[Dropdown Binders]] и [[InputField Binders]].

## Таблица семейства

| Binder | Свойство | База | Значение |
|---|---|---|---|
| `SelectableInteractableBinder` | `interactable` | `TargetBoolBinder<Selectable>` | bool |
| `SelectableInteractableMonoBinder` | `interactable` | `ComponentBoolMonoBinder<Selectable>` | bool |
| `SelectableInteractableEnumMonoBinder` | `interactable` | `EnumMonoBinder<…>` | enum→bool |
| `SelectableInteractableEnumGroupMonoBinder` | `interactable` | `EnumGroupMonoBinder<Selectable, bool>` | enum→group |
| `SelectableColorBlockBinder` | `colors` | `TargetBinder<…>` | ColorBlock |
| `SelectableColorBlockMonoBinder` | `colors` | `ComponentMonoBinder<…>` | ColorBlock |
| `SelectableColorBlockSwitcherBinder` | `colors` | `SwitcherBinder<…>` | bool→pair |
| `SelectableColorBlockEnumMonoBinder` / `…EnumGroupMonoBinder` | `colors` | `EnumMonoBinder` / `EnumGroupMonoBinder` | enum→value |

## Оси вариантов

- **Plain против Mono** — Plain-типы `…Binder` — это `[Serializable]` C#-объекты, конструируемые в коде/инспекторе на основе переданного `Selectable`. Варианты `…MonoBinder` — это MonoBehaviour-компоненты, которые кешируют собственный компонент (`CachedComponent`) и поставляются с записями `[AddComponentMenu]` / `[AddBinderContextMenu]` для связывания перетаскиванием. См. [[Mono Binders]] и [[Binder Base Classes]].
- **Switcher** (только ColorBlock) — `SwitcherBinder` переключает `colors` между `trueValue` и `falseValue` на основе связанного bool. Он защищён директивой `UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION`.
- **Enum / EnumGroup** — Enum-варианты определяют значение свойства по связанному enum; EnumGroup применяет вычисленное значение к *группе* целей (например, стилизация при эксклюзивном выборе). Оба здесь доступны только в Mono-варианте.
- **OneWayToSource** — Mono-варианты документируют поддержку [[BindMode|OneWayToSource]]: при связывании они отправляют текущее значение элемента управления обратно в ViewModel. Interactable также поддерживает флаг `isInvert`.

## Примечательное поведение

- Оба plain-базовых биндера вызывают `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — **TwoWay отвергается**, поскольку `Selectable` не генерирует событие изменения для непрерывного обратного чтения.
- Значения по умолчанию различаются по назначению: Interactable по умолчанию использует `BindMode.OneTime`, ColorBlock — `BindMode.OneWay`.
- Алиас конвертера ColorBlock переключается между обобщённым `IConverter<ColorBlock, ColorBlock>` (2023.1+) и `IConverterColorBlock` на более старых версиях Unity — это косвенность через [[Converters]], а не изменение поведения.

## Исходный код

`StarterKit/Unity/Runtime/Binders/Selectables/` (`Interactable/`, `ColorBlock/`, плюс их подпапки `Mono/`). Связываемые члены на стороне ViewModel приходят из [[Bindable Members]] через [[Source Generation Pipeline|сгенерированный]] код. См. также [[IBinder]] и [[Binders Catalog]].
