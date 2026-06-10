---
title: Биндеры AudioSource
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/AudioSourceVolumeBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/AudioSourceVolumeSwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/Mono/AudioSourceVolumeMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Volume/Mono/AudioSourceVolumeEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/Loop/AudioSourceLoopBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/MinMaxDistance/AudioSourceDistanceMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/OneWayToSource/Mono/AudioSourceToSourceMonoBinder.cs
tags: [binders, starterkit, audiosource, unity]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/AudioSource Binders.md
translated_at: 2026-05-31
---

# Биндеры AudioSource

> Биндеры из StarterKit, которые управляют отдельными свойствами `UnityEngine.AudioSource` (громкость, высота тона, зацикливание, дистанции, группа микшера...) на основе состояния ViewModel, так что поведение звука реагирует на данные, а не на ручной код.

## Зачем это нужно

`AudioSource` предоставляет около 20 настраиваемых полей. Вместо того чтобы вручную писать код, проталкивающий значения ViewModel в каждое из них, это семейство предоставляет готовый binder под каждое свойство. Каждая вложенная папка нацелена ровно на один член `AudioSource`, и каждый binder просто переопределяет геттер/сеттер `Property` (или `SetValue`), указывающий на этот член. Общая логика — преобразование, [[BindMode]], кэширование цели — живёт в [[Binder Base Classes]], поэтому сами эти классы остаются крошечными.

## Как это работает

Каждая папка свойства выбирает базовый класс, соответствующий типу её значения (`Float`, `Bool`, `Object`, enum). Существует две физические формы:

- **Обычные биндеры** (`[Serializable]`, например `AudioSourceVolumeBinder : TargetFloatBinder<AudioSource>`) — конструируются в коде с целью + [[BindMode]], используются как сериализуемые поля binder внутри [[View]].
- **MonoBinders** (например `AudioSourceVolumeMonoBinder : ComponentFloatMonoBinder<AudioSource>`) — MonoBehaviour-компоненты, навешиваемые на GameObject; они кэшируют компонент (`CachedComponent`) и несут `[AddComponentMenu]` / `[AddBinderContextMenu]`, чтобы редактор мог их добавлять. См. [[Mono Binders]].

Примечательное поведение: свойства с диапазоном самоограничиваются. `AudioSourceVolumeBinder` переопределяет `GetConvertedValue` на `Mathf.Clamp(..., 0, 1)`; документация предупреждает, что переопределения обязаны вызывать `base`. Обычные биндеры также вызывают `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` — `TwoWay` отклоняется, потому что `AudioSource` не генерирует событий об изменении.

## Оси вариантов

| Вариант | Пример | Роль |
|---------|---------|------|
| Plain | `AudioSourceVolumeBinder` | код/сериализация, одно значение -> свойство |
| Mono | `AudioSourceVolumeMonoBinder` | компонент, то же самое через `CachedComponent` |
| Switcher | `AudioSourceVolumeSwitcherBinder` | выбирает одно из двух значений по привязанному `bool` |
| Enum | `AudioSourceVolumeEnumMonoBinder` | сопоставляет привязанный enum со значением |
| EnumGroup | `AudioSourceVolumeEnumGroupMonoBinder` | управляется enum, групповая форма |
| OneWayToSource | `AudioSourceToSourceMonoBinder` | проталкивает ссылку на `AudioSource` обратно в ViewModel |

Не у каждого свойства есть все варианты — `Loop` (это `bool`) поставляется только в формах plain/Mono; `Volume`/`AudioClip` добавляют Switcher'ы; числовые свойства добавляют Mono-формы Enum/EnumGroup. `MinMaxDistance` несёт enum `AudioSourceDistanceMode` (`Min`/`Max`/`Range`), выбирающий, в какое поле дистанции писать. Единственный binder `OneWayToSource` работает только со ссылкой (без папки под каждое свойство).

## Ключевые связи

- Базовые классы: [[Binder Base Classes]] (`TargetFloatBinder`, `SwitcherFloatBinder`, `ComponentFloatMonoBinder`, `EnumFloatMonoBinder`, `ComponentToSourceMonoBinder`).
- Концепции: [[BindMode]], [[Data Binding]], [[IBinder]], [[Converters]].
- Родственные семейства: [[Renderer Binders]], [[Animator Binders]], [[Behaviour Binders]] следуют идентичному паттерну «один binder на свойство».

## Источник

`Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/AudioSources/` — одна папка на каждое свойство `AudioSource`, каждая с вариантами plain + `Mono/`. См. [[Binders Catalog]].
