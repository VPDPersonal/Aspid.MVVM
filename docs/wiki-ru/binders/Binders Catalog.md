---
title: Каталог биндеров
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders
tags: [binders, catalog]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Binders Catalog.md
translated_at: 2026-05-31
---

# Каталог биндеров

> Стартовая страница для биндеров StarterKit — примерно 593 файла, распределённых по примерно 33 категориям UI-элементов. Документируется **по категориям, а не по файлам** (страница на каждый класс превратилась бы в более чем 500 дублирующихся заглушек).

## Что такое биндер

Небольшой объект, связывающий **один** UI-элемент View с **одним** биндируемым членом ViewModel в направлении, заданном [[BindMode]]. ViewModel предоставляет члены через [[IViewModel]]; биндеры разрешают их и подписываются на них.

## Оси вариативности (повторяются во всех категориях)

- **Обычный против `…MonoBinder`** — обычные биндеры создаются в коде; варианты `Mono` основаны на `MonoBehaviour` и настраиваются в инспекторе Unity.
- **`Switcher`** — выбирает между значениями на основе привязанного значения.
- **`Enum` / `EnumGroup`** — управляют свойством на основе перечисления.
- **`OneWayToSource`** — передают данные из View в ViewModel (например, поля ввода).

## Категории

Text ([[Text Binders]]), Images, RawImages, Toggles, Sliders, Scrollbars, Scrollrects, Dropdowns, InputFields, Buttons, Selectables, Transforms, Animators, AudioSources, CanvasGroups, Graphics, Renderers, LineRenderers, Layouts, GameObjects, Objects, Colliders, Behaviours, EventTriggers, UnityEvents, LocalizeStringEvents, VirtualizedLists, Collections, Casters, Generics, Debugs, Mono helpers.

> Страницы по категориям пишутся во время ингеста; неразрешённые ссылки выше помечают страницы, которые ещё предстоит создать.

## См. также

- Преобразования значений, применяемые внутри биндеров: [[Converters]].
- Как биндеры подключаются при запуске: [[View Initialization]].

## Источник

`StarterKit/Unity/Runtime/Binders/` (одна папка на категорию) и базовые классы биндеров на чистом C# в `StarterKit/Runtime/Binders/`.
