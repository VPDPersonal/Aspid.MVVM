---
title: Биндеры Renderer
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Materials/RendererMaterialsBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Materials/RendererMaterialsSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/MaterialsColor/RendererMaterialColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/MaterialsColor/Mono/RendererMaterialsColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/OneWayToSource/Mono/RendererToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Renderers/Extensions/RendererSetters.cs
tags: [binders, starterkit, renderer, materials, color]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Renderer Binders.md
translated_at: 2026-05-31
---

# Биндеры Renderer

> Биндеры из StarterKit, которые управляют массивом `material`/`materials` компонента `UnityEngine.Renderer` и цветом шейдера для каждого материала на основе значений из ViewModel.

## Что привязывается

Две различные цели на `Renderer`:

- **Материалы** — `Renderer.material` (один) или `Renderer.materials` (массив). Принимает `Material`, `Material[]` или `IReadOnlyCollection<Material>`. Присваивание проходит через расширение `RendererSetters.SetMaterials`, которое очищает значение при null/пустом наборе, использует `material` для одного элемента и применяет необязательный поэлементный `IConverter<Material, Material>`.
- **Цвет материала** — именованное цветовое свойство шейдера (по умолчанию `"_BaseColor"`, настраивается), устанавливаемое на *все* материалы рендерера через `Shader.PropertyToID`. Базовый класс — [[Binder Base Classes|TargetColorBinder]]`<Renderer>`.

## Семейство

| Папка | Цель | Базовый класс |
|--------|--------|-----------|
| `Materials/` | `material` / `materials` | [[Binder Base Classes|TargetBinder]]`<Renderer>` |
| `MaterialsColor/` | цветовое свойство шейдера | `TargetColorBinder<Renderer>` |
| `OneWayToSource/` | сам компонент `Renderer` | `ComponentToSourceMonoBinder<Renderer>` |

## Оси вариаций

Каждая цель следует паттерну StarterKit (см. [[Mono Binders]], [[Binders Catalog]]):

- **Plain против Mono** — обычный `Serializable`-биндер (например, `RendererMaterialsBinder`, `RendererMaterialColorBinder`) создаётся в коде; родственный `*MonoBinder` в папке `Mono/` — это сериализуемый в инспекторе MonoBehaviour с `[AddComponentMenu]` / `[AddBinderContextMenu]`.
- **Switcher** — `RendererMaterialsSwitcherBinder` / `RendererMaterialsColorSwitcherBinder` наследуют `SwitcherBinder<Renderer, T>`: переключаются между `trueValue` и `falseValue` на основе привязанного `bool`.
- **Enum / EnumGroup** — `*EnumMonoBinder` сопоставляет значения enum с настройками одного рендерера; `*EnumGroupMonoBinder` (например, `RendererMaterialsColorEnumGroupMonoBinder`) применяется к *группе* рендереров на основе привязанного enum.
- **OneWayToSource** — `RendererToSourceMonoBinder` отправляет закэшированную ссылку на `Renderer` обратно во ViewModel. Отдельно сам `RendererMaterialsBinder` поддерживает [[BindMode|BindMode.OneWayToSource]] через `IReverseBinder<Material>` / `IReverseBinder<Material[]>`, передавая текущие материалы в `OnBound` и `null` в `OnUnbound`.

## Особенности поведения

- `RendererMaterialsBinder` объявляет `[BindModeOverride(OneWay, OneTime, OneWayToSource)]` и выбрасывает исключение при `TwoWay`; цветовые биндеры также отклоняют `TwoWay`.
- Цветовые биндеры лениво кэшируют ID свойства (`Shader.PropertyToID`), поэтому имя из инспектора разрешается один раз.
- Все варианты принимают необязательный [[Converters|конвертер]], применяемый перед присваиванием.

## Исходный код

`StarterKit/Unity/Runtime/Binders/Renderers/` — подпапки `Materials/`, `MaterialsColor/`, `OneWayToSource/`, `Extensions/` (вспомогательный класс `RendererSetters`). См. также [[Graphic Binders]] и [[LineRenderer Binders]] для связанных визуальных целей.
