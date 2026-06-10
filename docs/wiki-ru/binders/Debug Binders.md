---
title: Отладочные биндеры
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/DebugLogBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/Mono/DebugLogMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Debugs/Log/XmlExampleDoc-Debug-Log-1.1.0.xml
tags: [binder, starterkit, debug, logging, diagnostics]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Debug Binders.md
translated_at: 2026-05-31
---

# Отладочные биндеры

> Диагностический binder, который не трогает UI — он просто логирует каждое событие биндинга и каждое значение в консоль Unity, чтобы вы могли наблюдать, что на самом деле передаёт [[Bindable Members|bindable-член]].

## Зачем он нужен

Когда биндинг «ничего не делает», нужно понять, эмитит ли [[ViewModel]] значения вообще и как они выглядят. Вместо того чтобы редактировать ViewModel или подключать отладчик, вы добавляете отладочный binder к любому [[Bindable Members|bindable-члену]] и читаете консоль. Это вспомогательное средство для разработки, а не runtime-функциональность.

## Семейство

| Вариант | Базовый класс | Способ использования | Цель |
|---------|------|--------|--------|
| `DebugLogBinder` | [[Binder Base Classes\|Binder]] | поле `[SerializeField]` внутри [[View]] | консоль Unity (`Debug.Log`) |
| `DebugLogMonoBinder` | [[Mono Binders\|MonoBinder]] | перетаскиваемый компонент `MonoBehaviour` | консоль Unity (`Debug.Log`) |

Оба находятся в `Aspid.MVVM.StarterKit` и помечены как `sealed`.

## Оси вариаций

- **Plain или Mono** — единственная ось в этом семействе. `DebugLogBinder` — это `[Serializable]` обычный [[Binder Base Classes|Binder]], на который вы ссылаетесь из `[SerializeField]` во [[View]]; `DebugLogMonoBinder` — это `MonoBehaviour`, который вы добавляете через меню компонентов `Aspid/MVVM/Binders/Debug` и контекстное меню binder. Здесь нет вариантов Switcher, Enum/EnumGroup или отдельного OneWayToSource.
- **OneWayToSource встроен, а не является отдельным классом.** Оба реализуют `IAnyBinder` *и* `IAnyReverseBinder`, поэтому один экземпляр обрабатывает любое направление. Каждый помечен `[BindModeOverride(IsAll = true)]`, что означает, что он принимает любой [[BindMode]], объявленный членом.

## Заметное поведение

- `SetValue<T>(T value)` логирует `SetValue: <message>` для входящих значений (OneWay/TwoWay). В `DebugLogMonoBinder` этот метод также несёт атрибут `[BinderLog]`, который (вероятно) управляет генерируемым логированием через [[Unity Generators]].
- Событие `ValueChanged` (обратный канал / [[BindMode|OneWayToSource]]) логирует как при `add`, так и при `remove` подписчиков, поэтому вы видите, когда член подключается и отключается.
- Сериализуемое поле `_converter` (это [[Converters|конвертер]], по умолчанию `ObjectToStringConverter`) форматирует каждое значение; `GetMessage` откатывается к `value.ToString()`, когда конвертер не задан. Поле использует `[SerializeReferenceDropdown]`, поэтому конвертер можно выбрать в Inspector.
- `DebugLogMonoBinder` помечен как `partial` — биндеры StarterKit полагаются на [[Source Generation Pipeline|генераторы исходного кода]], поэтому сгенерированные члены не видны в этом написанном вручную файле (см. [[Must Be Partial]]).

## Источник

- `StarterKit/Unity/Runtime/Binders/Debugs/Log/DebugLogBinder.cs`
- `StarterKit/Unity/Runtime/Binders/Debugs/Log/Mono/DebugLogMonoBinder.cs`

См. также [[Binders Catalog]], [[Data Binding]], [[IBinder]].
