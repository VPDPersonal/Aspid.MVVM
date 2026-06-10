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

[[Text Binders|Text]], [[Image Binders|Images]], [[RawImage Binders|RawImages]], [[Toggle Binders|Toggles]], [[Slider Binders|Sliders]], [[Scrollbar Binders|Scrollbars]], [[Scrollrect Binders|Scrollrects]], [[Dropdown Binders|Dropdowns]], [[InputField Binders|InputFields]], [[Button Binders|Buttons]], [[Selectable Binders|Selectables]], [[Transform Binders|Transforms]], [[Animator Binders|Animators]], [[AudioSource Binders|AudioSources]], [[CanvasGroup Binders|CanvasGroups]], [[Graphic Binders|Graphics]], [[Renderer Binders|Renderers]], [[LineRenderer Binders|LineRenderers]], [[Layout Binders|Layouts]], [[GameObject Binders|GameObjects]], [[Object Binders|Objects]], [[Collider Binders|Colliders]], [[Behaviour Binders|Behaviours]], [[EventTrigger Binders|EventTriggers]], [[UnityEvent Binders|UnityEvents]], [[LocalizeStringEvent Binders|LocalizeStringEvents]], [[VirtualizedList Binders|VirtualizedLists]], [[Collection Binders|Collections]], [[Caster Binders|Casters]], [[Generic Binders|Generics]], [[Debug Binders|Debugs]], [[Mono Binders|Mono helpers]].

> Каждая категория ссылается на свою страницу; все биндеры семейства разделяют оси вариативности выше.

## См. также

- Преобразования значений, применяемые внутри биндеров: [[Converters]].
- Как биндеры подключаются при запуске: [[View Initialization]].

## Источник

`StarterKit/Unity/Runtime/Binders/` (одна папка на категорию) и базовые классы биндеров на чистом C# в `StarterKit/Runtime/Binders/`.
