---
title: Layout-биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/HorizontalOrVerticalLayoutGroup/Spacing/HorizontalOrVerticalLayoutSpacingBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/HorizontalOrVerticalLayoutGroup/Spacing/Mono/HorizontalOrVerticalLayoutSpacingMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/LayoutGroupPaddingBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/LayoutGroupPaddingSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/Padding/Mono/LayoutGroupPaddingEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/LayoutGroup/OneWayToSource/Mono/LayoutGroupToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/PaddingMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/Extensions/LayoutSetters.cs
tags: [binders, starterkit, layout, ui]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Layout Binders.md
translated_at: 2026-05-31
---

# Layout-биндеры

> Биндеры из StarterKit, которые управляют компонентами `LayoutGroup` из Unity UI на основе ViewModel — задают `padding` и `spacing` из данных вместо ручной настройки в инспекторе.

Они существуют потому, что UI, управляемый раскладкой (отступы, промежутки), часто должен реагировать на состояние: переключатели плотности, адаптивные отступы, тематические промежутки. Семейство оборачивает две цели Unity — `LayoutGroup.padding` и `HorizontalOrVerticalLayoutGroup.spacing` — так что [[Bindable Members|bindable-член]] может задавать их без рефлексии.

## Семейство

| Группа | Целевое свойство | Тип значения |
|---|---|---|
| `LayoutGroup` Padding | `LayoutGroup.padding` | `RectOffset` (или `int`/`float` через [[Converters\|INumberBinder]]) |
| HorizontalOrVertical Spacing | `HorizontalOrVerticalLayoutGroup.spacing` | `float` (также `INumberBinder`) |

## Оси вариантов

Каждая группа следует матрице биндеров StarterKit:

- **Plain vs Mono** — Plain-биндеры (`...Binder`, `[Serializable]`) получают свою цель через конструктор и привязываются в коде. Форма `...MonoBinder` — это `MonoBehaviour` (`[AddComponentMenu]` / `[AddBinderContextMenu]`), который определяет свою цель из `CachedComponent` и настраивается в инспекторе. См. [[Binder Base Classes]] и [[Mono Binders]].
- **Switcher** — `...SwitcherBinder` расширяет `SwitcherBinder<,,>` и переключается между `trueValue` / `falseValue` на основе привязанного `bool`.
- **Enum / EnumGroup** — `...EnumMonoBinder` (расширяет `EnumMonoBinder<,,>`) сопоставляет одно enum-значение значению раскладки; вариант `...EnumGroupMonoBinder` обрабатывает группу/набор enum-случаев.
- **OneWayToSource** — `LayoutGroupToSourceMonoBinder` (это `ComponentToSourceMonoBinder<LayoutGroup>`) отправляет ссылку на компонент *обратно* в ViewModel при установлении привязки. Plain-`MonoBinder` для spacing также поддерживает [[BindMode|BindMode.OneWayToSource]], проталкивая текущий spacing к источнику. См. [[Runtime Binding Resolution]].

## Примечательное поведение

- **`PaddingMode`** — padding-биндеры несут `[SerializeField] PaddingMode` (`All`, `Left`, `Right`, `Top`, `Bottom`), выбирающий, какие стороны обновлять. Общее расширение `LayoutSetters.SetPadding` записывает эти стороны и вызывает `LayoutRebuilder.MarkLayoutForRebuild`, поэтому раскладка действительно перестраивается.
- **Удобство с числами** — padding-биндеры реализуют `INumberBinder`: значение `int`/`long`/`float`/`double` применяется одинаково ко всем четырём сторонам (с переиспользованием кэшированного `RectOffset`). Spacing передаёт число напрямую.
- **Нет TwoWay** — plain-биндеры padding и spacing выбрасывают исключение при `BindMode.TwoWay` (`mode.ThrowExceptionIfMatches(BindMode.TwoWay)`); для обновлений источника используйте OneWayToSource.
- **Разделение по версиям Unity** — псевдонимы конвертеров отличаются: `IConverter<RectOffset?, RectOffset?>` на Unity 2023.1+, иначе интерфейс `IConverterRectOffset`.

Mono-варианты появляются в меню компонентов `Aspid/MVVM/Binders/UI/LayoutGroup/...`. См. полный [[Binders Catalog]] и обзор [[Data Binding]].

## Источник

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Layouts/` — разделено на `LayoutGroup/` (Padding, OneWayToSource) и `HorizontalOrVerticalLayoutGroup/` (Spacing), с общими `PaddingMode.cs` и `Extensions/LayoutSetters.cs`.
