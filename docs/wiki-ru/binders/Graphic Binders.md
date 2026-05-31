---
title: Биндеры Graphic
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/GraphicColorBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/GraphicColorSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Color/Mono/GraphicColorEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/ColorComponent/GraphicColorComponentBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/ColorComponent.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/Material/GraphicMaterialSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/OneWayToSource/Mono/GraphicToSourceMonoBinder.cs
tags:
  - binder
  - starterkit
  - unity-ui
  - graphic
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Graphic Binders.md
translated_at: 2026-05-31
---

# Биндеры Graphic

> Биндеры из StarterKit, управляющие базовым классом `Graphic` из Unity UI — цветом, отдельным цветовым каналом и материалом — так что значение из ViewModel окрашивает любой UI-графический элемент (Image, RawImage, Text и т. д.) без написания связующего кода вручную.

## Зачем это нужно

`Graphic` — общий базовый класс для каждого отрисовываемого элемента Unity UI. Привязка к нему (а не к конкретному подтипу) позволяет одному биндеру обслуживать всю иерархию UI. Семейство покрывает три вещи, которые графический элемент предоставляет ViewModel: его `color`, один канал этого цвета и его `material`. См. [[Data Binding]] и [[Binder Base Classes]].

## Таблица семейства

| Целевая ось | Привязываемое свойство | Plain-база | Mono-база |
|-------------|----------------|------------|-----------|
| Цвет | `Graphic.color` | `TargetColorBinder<Graphic>` | `ComponentColorMonoBinder<Graphic>` |
| Цветовой канал | `Graphic.color` R/G/B/A (float) | `TargetFloatBinder<Graphic>` | (есть Mono-варианты) |
| Материал | `Graphic.material` | `SwitcherBinder<Graphic, Material, …>` | `…MonoBinder<Graphic>` |

## Оси вариативности

- **Plain против Mono.** «Plain»-биндеры с атрибутом `[Serializable]` (например, `GraphicColorBinder`) создаются в коде и вкладываются внутрь [[View]]. Варианты `…MonoBinder` — это компоненты `MonoBehaviour`, настраиваемые в инспекторе; они несут `[AddComponentMenu]` / `[AddBinderContextMenu]`, благодаря чему контекстное меню автоматически создаёт их с нужным сериализованным полем (`m_Color`). См. [[Mono Binders]].
- **Switcher.** `…SwitcherBinder` привязывается к **булеву** значению и переключает целевой объект между двумя предустановленными значениями (`trueColor`/`falseColor` или двумя `Material`). Один bool → два исхода.
- **Enum / EnumGroup.** `…ColorEnumMonoBinder` сопоставляет привязанный **enum** цвету. Вариант **EnumGroup** применяет вычисленное значение к *каждому* `Graphic` в сериализованной группе, а не только к одному — удобно для оформления (темизации) панели элементов сразу.
- **OneWayToSource.** `GraphicToSourceMonoBinder` меняет направление: при привязке он отправляет закэшированную ссылку на `Graphic` обратно во ViewModel (см. [[BindMode]] `OneWayToSource`). `GraphicColorMonoBinder` дополнительно инициализирует источник текущим цветом при привязке в этом режиме.

## Примечательное поведение

- **TwoWay отклоняется.** Конструкторы для цвета и цветового компонента вызывают `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — графические элементы являются приёмниками вывода, поэтому допустимы только `OneWay`/`OneTime`/`OneWayToSource`.
- **Цветовой канал** использует enum `ColorComponent` (R/G/B/A) вместе с `GraphicExtensions.GetColorComponent`/`SetColorComponent`; привязываемое значение — одиночный `float`, что позволяет анимировать только альфу.
- **Конвертер материала зависит от версии:** Unity 2023.1+ использует `IConverter<Material?, Material?>`; в более старых версиях он откатывается к `IConverterMaterial`.

## Исходный код

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Graphics/` — подпапки `Color/`, `ColorComponent/`, `Material/`, каждая с папкой `Mono/`, а также `OneWayToSource/` и общая `Extensions/`.

Связанное: [[Image Binders]], [[RawImage Binders]], [[Text Binders]], [[Renderer Binders]], [[Converters]], [[Binders Catalog]].
