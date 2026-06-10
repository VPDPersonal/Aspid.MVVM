---
title: Биндеры CanvasGroup
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/CanvasGroupAlphaBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/CanvasGroupAlphaSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/Mono/CanvasGroupAlphaMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Alpha/Mono/CanvasGroupAlphaEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/Interactable/CanvasGroupInteractableBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/CanvasGroups/OneWayToSource/Mono/CanvasGroupToSourceMonoBinder.cs
tags: [binder, starterkit, unity, canvasgroup, ui]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/CanvasGroup Binders.md
translated_at: 2026-05-31
---

# Биндеры CanvasGroup

> Биндеры из StarterKit, управляющие Unity-компонентом `CanvasGroup` на основе состояния ViewModel — плавно показывают/скрывают панели, переключают интерактивность и блокируют raycast'ы — без написания связующего UI-кода.

`CanvasGroup` управляет целым поддеревом UI сразу: его свойствами `alpha`, `interactable`, `blocksRaycasts` и `ignoreParentGroups`. Это семейство биндеров предоставляет каждое из этих четырёх свойств как биндируемую цель, чтобы значение из ViewModel (float, bool или enum) напрямую отображалось на компонент.

## Таблица семейства

| Целевое свойство | Тип | Базовый биндер | [[BindMode]] по умолчанию |
|---|---|---|---|
| `alpha` | float | `TargetFloatBinder<CanvasGroup>` | `OneWay` |
| `interactable` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |
| `blocksRaycasts` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |
| `ignoreParentGroups` | bool | `TargetBoolBinder<CanvasGroup>` | `OneTime` |

Каждый член реализует запечатанную (sealed) обёртку get/set `Property` поверх соответствующего поля `CanvasGroup` — это единственная точка, где биндер обращается к Unity.

## Оси вариаций

Каждое свойство предлагается по стандартным осям StarterKit (см. [[Binder Base Classes]] и [[Mono Binders]]):

- **Plain vs Mono** — обычный `…Binder` — это `[Serializable]`-класс, используемый в коде/полях [[View]]; `…MonoBinder` — это MonoBehaviour с `[AddComponentMenu]`/`[AddBinderContextMenu]` для перетаскивания в Inspector (например, `CanvasGroup Binder – Alpha`).
- **Switcher** — `CanvasGroupAlphaSwitcherBinder` биндит *bool* и выбирает между значениями alpha `trueValue`/`falseValue` (например, 1 и 0 для показа/скрытия).
- **Enum / EnumGroup** — `…EnumMonoBinder` выбирает значение для каждого случая enum; `…EnumGroupMonoBinder` применяет его к *группе* компонентов `CanvasGroup`, что идеально подходит для переключения вкладок/страниц.
- **OneWayToSource** — `CanvasGroupToSourceMonoBinder` отправляет закешированную ссылку на `CanvasGroup` обратно в ViewModel при привязке (см. `OneWayToSource` в [[BindMode]]). Alpha Mono-биндер дополнительно поддерживает `OneWayToSource`, передавая текущее значение alpha обратно.

## Особенности поведения

- **Alpha ограничивается (clamp).** И `GetConvertedValue` (plain/mono), и `SetValue` (switcher/enum) вызывают `Mathf.Clamp01`, поэтому конвертеры не могут вывести alpha за пределы диапазона [0, 1]. Переопределяемые хуки должны вызывать `base`, чтобы сохранить это поведение.
- **TwoWay отклоняется.** Конструкторы `alpha` и `interactable` вызывают `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — у `CanvasGroup` нет события изменения, поэтому двусторонняя привязка не имеет смысла. Для обратной записи используйте `OneWayToSource`.
- Bool-биндеры принимают флаг `isInvert`, чтобы инвертировать исходное значение перед его применением.

## Исходный код

`StarterKit/Unity/Runtime/Binders/CanvasGroups/` — подпапки `Alpha/`, `Interactable/`, `BlocksRaycasts/`, `IgnoreParentGroups/`, `OneWayToSource/`. См. также [[Bool Converters]], [[Number Converters]] и более общий [[Binders Catalog]].
