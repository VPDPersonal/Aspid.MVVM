---
title: Биндеры Transform
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/TransformPositionBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/TransformPositionSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/Position/Mono/TransformPositionEnumGroupMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/Transforms/OneWayToSource/Mono/TransformToSourceMonoBinder.cs
tags: [binders, starterkit, transform, rect-transform]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Transform Binders.md
translated_at: 2026-05-31
---

# Биндеры Transform

> Биндеры из StarterKit, которые управляют пространственным свойством `Transform` / `RectTransform` — позицией, поворотом, масштабом, привязанной позицией, размером — на основе значений из ViewModel.

## Что биндится

Эти биндеры связывают один компонент `Transform` (или `RectTransform`) с привязанным членом. Большинство свойств имеют тип `Vector3`/`Quaternion`/`Vector2`, поэтому это семейство строится поверх обобщённых целей [[Binder Base Classes|TargetVector3Binder<Transform>]], `SwitcherVector3Binder<Transform>` и аналогичных. Каждый биндер записывает значение через вспомогательные методы-расширения `Transform` (например, `SetPosition(value, space)`), поэтому поле `Space` выбирает мировые или локальные координаты там, где это применимо.

## Таблица семейства

| Целевое свойство | Папка с исходниками |
|---|---|
| `position` / `localPosition` | `Transforms/Position` |
| `eulerAngles` | `Transforms/EulerAngels` |
| `rotation` | `Transforms/Rotation` |
| `localScale` | `Transforms/Scale` |
| ссылка на `Transform` → источник | `Transforms/OneWayToSource` |
| `anchoredPosition` | `RectTransforms/AnchoredPosition` |
| `sizeDelta` | `RectTransforms/SizeDelta` |

## Оси вариантов

Каждое свойство существует во всех вариантах стандартной матрицы StarterKit (см. [[Mono Binders]] про разделение Plain/Mono):

- **Plain** — класс `[Serializable]` (например, `TransformPositionBinder`), используемый как сериализуемое поле внутри [[View]]. Принимает цель + конвертер + [[BindMode]] в своём конструкторе.
- **Mono** — компонент `MonoBinder`, который вы добавляете на GameObject (например, `TransformPositionMonoBinder`), обнаруживаемый через `[AddComponentMenu]` / `[AddBinderContextMenu]`.
- **Switcher** — выбирает между `trueValue` и `falseValue` на основе привязанного `bool` (`TransformPositionSwitcherBinder`, `…SwitcherMonoBinder`).
- **Enum / EnumGroup** — только Mono; сопоставляет привязанный enum с целями для каждого значения. `EnumGroup` применяет значение ко всем элементам настроенной группы через переопределённый `SetValue` (`TransformPositionEnumGroupMonoBinder`).
- **OneWayToSource** — `TransformToSourceMonoBinder` наследуется от `ComponentToSourceMonoBinder<Transform>`; передаёт кешированную ссылку на `Transform` *в* ViewModel при установлении привязки, а не читает из неё.

## Заметное поведение

- Биндеры только для записи запрещают двусторонний поток: `TransformPositionBinder` вызывает `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` в своём конструкторе (предполагается, что это справедливо для вариантов position/rotation/scale). Используйте биндер OneWayToSource, чтобы передавать данные в обратном направлении.
- Значения `Space` по умолчанию различаются — `Space.World` для Plain-биндера позиции, также `World` в EnumGroup; проверяйте сериализуемое поле для каждого варианта.
- `RectTransform/SizeDelta` добавляет enum `SizeDeltaMode` для управления тем, какие оси `sizeDelta` записываются (предполагается на основе `SizeDeltaMode.cs` из этой папки).

## Исходники

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Transforms/` — `Transforms/` (Position, EulerAngels, Rotation, Scale, OneWayToSource) и `RectTransforms/` (AnchoredPosition, SizeDelta). См. [[Binders Catalog]] и [[Data Binding]].
