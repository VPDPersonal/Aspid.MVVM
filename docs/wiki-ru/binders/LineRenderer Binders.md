---
title: Биндеры LineRenderer
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/LineRendererColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/LineRendererColorSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Color/Mono/LineRendererColorSwitcherMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/OneWayToSource/Mono/LineRendererToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/LineRendererColorMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/LineRenderers/Extensions/LineRendererColorSetter.cs
tags: [binders, starterkit, linerenderer, color]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/LineRenderer Binders.md
translated_at: 2026-05-31
---

# Биндеры LineRenderer

> Управляйте свойствами `startColor`/`endColor` компонента Unity `LineRenderer` через член `Color` из ViewModel — удобно для подсветки траекторий, связей или визуализации лучей/лазеров, которые меняются в зависимости от состояния.

## Зачем это нужно

`LineRenderer` задаёт свой оттенок двумя отдельными свойствами (`startColor`, `endColor`), а не единым цветом материала. Это семейство оборачивает оба свойства за единым целевым биндингом `Color`, чтобы ViewModel могла перекрашивать линию, ничего не зная о двухконцевом API Unity. Какой из концов (или оба) изменяется, определяется сериализуемым значением `LineRendererColorMode` (`Start`, `End`, `StartAndEnd`).

## Семейство

| Вариант | Базовый класс | Биндит |
|---|---|---|
| `LineRendererColorBinder` | `TargetColorBinder<LineRenderer>` | `Color` → конец(ы) |
| `LineRendererColorMonoBinder` | `ComponentColorMonoBinder<LineRenderer>` | `Color` → конец(ы) |
| `LineRendererColorSwitcherBinder` | `SwitcherColorBinder<LineRenderer>` | `bool` → `Color` для true/false |
| `LineRendererColorSwitcherMonoBinder` | `SwitcherColorBinder` (Mono) | `bool` → `Color` для true/false |
| `LineRendererColorEnumMonoBinder` | `EnumColorMonoBinder<LineRenderer>` | `enum` → `Color` для каждого значения |
| `LineRendererColorEnumGroupMonoBinder` | `EnumGroupColorMonoBinder<LineRenderer>` | `enum` → сгруппированные `Color` |
| `LineRendererToSourceMonoBinder` | `ComponentToSourceMonoBinder<LineRenderer>` | ссылка на компонент → ViewModel |

## Оси вариаций

- **Plain против Mono** — Plain-биндеры (`[Serializable]`, без `[AddComponentMenu]`) существуют как сериализуемые поля в пользовательском [[View]]; Mono-варианты — это самостоятельные компоненты `MonoBehaviour` (см. [[Mono Binders|Mono-биндеры]]), которые вы добавляете на GameObject и настраиваете в Inspector через `[AddComponentMenu]`/`[AddBinderContextMenu]`.
- **Switcher** — отображает связанный `bool` в один из двух настроенных цветов (`trueValue`/`falseValue`) вместо того, чтобы передавать `Color` напрямую.
- **Enum / EnumGroup** — `Enum` выбирает цвет для каждого значения перечисления для одного рендерера; `EnumGroup` применяет вычисленный цвет ко всем элементам группы.
- **OneWayToSource** — `LineRendererToSourceMonoBinder` отправляет кэшированную ссылку на `LineRenderer` обратно в ViewModel при установлении биндинга (см. [[BindMode]] `OneWayToSource`). Plain-биндер цвета запрещает `BindMode.TwoWay`; `LineRendererColorMonoBinder` дополнительно поддерживает обратное чтение текущего цвета ([[BindMode]] `OneWayToSource`).

## Особенности поведения

- Общая логика чтения/записи находится в расширениях `LineRendererSetters.SetColor`/`GetColor` и определяется значением `LineRendererColorMode`.
- `GetColor` выбрасывает `ArgumentOutOfRangeException` для `StartAndEnd` (неоднозначно, какой из концов читать). Поэтому любой путь обратного чтения (геттер `Property`, `OneWayToSource`) должен использовать `Start` или `End`; использование `StartAndEnd` имеет смысл только для односторонних биндингов, работающих лишь на запись.
- Все варианты разрешаются во время выполнения через [[Runtime Binding Resolution]], как и остальные биндеры из [[Binders Catalog|Каталога биндеров]].

## Исходный код

`StarterKit/Unity/Runtime/Binders/LineRenderers/` — `Color/` (plain + `Mono/`), `OneWayToSource/Mono/`, `LineRendererColorMode.cs`, `Extensions/`. Сравните с семейством цвета материала в [[Renderer Binders|Биндеры Renderer]] и общими [[Graphic Binders|Биндеры Graphic]]. См. [[Binder Base Classes|Базовые классы биндеров]] для `TargetColorBinder`/`SwitcherColorBinder`/`EnumColorMonoBinder`.
