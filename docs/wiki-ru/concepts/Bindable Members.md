---
title: Привязываемые члены
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers
tags: [bindable-member, binding, generated, bindmode, source-generation]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/Bindable Members.md
translated_at: 2026-05-31
---

# Привязываемые члены

> Лёгкие обёртки над значениями, которые `[Bind]` создаёт для каждого поля — каждая хранит значение, инициирует `Changed` и связывает биндеры с ViewModel в одном конкретном [[BindMode]].

## Зачем это нужно

View должна реагировать на изменение значения ViewModel без рефлексии и обвязки `INotifyPropertyChanged`. Привязываемый член — это небольшой объект, который находится между полем `[Bind]` и наблюдающими за ним [[IBinder]]: он владеет текущим значением, инициирует `Changed` и умеет подключать/отключать биндеры ровно для одного направления привязки. Выбор конкретной обёртки для каждого поля позволяет рантайму оставаться экономным к аллокациям и избегать приведения типов на горячем пути.

## Как это работает

Каждая обёртка реализует `IBinderAdder` (`Mode` + `Add(IBinder)` → `IBinderRemover?`). Иерархия интерфейсов наслаивает возможности:

- `IBinderAdder` — `Mode` и `Add`
- `IReadOnlyValueBindableMember<out T>` — добавляет геттер `Value`
- `IReadOnlyBindableMember<out T>` — добавляет событие `Changed`
- `IBindableMember<T>` — добавляет сеттер `Value`

Четыре варианта [[BindMode]] различаются тем, что они принимают и как подключают биндеры:

- **OneWay** — получает/устанавливает `Value`; при `Add` один раз отправляет значение, затем для биндеров `OneWay` подписывает их `SetValue` на `Changed` (биндеры `OneTime` получают значение и возвращают `null`). Отклоняет обратные режимы.
- **TwoWay** — поддерживает все режимы, кроме `None`; прямое направление через `IBinder<T>`/`IAnyBinder`, обратное через `IReverseBinder<T>`/`IAnyReverseBinder`, вызывающее захваченный сеттер `Action<T>`.
- **OneTime** — синглтон для каждого `T`; `Get(value)` устанавливает значение, `Add` один раз отправляет его и возвращает `null` (без отключения). Только прямые режимы.
- **OneWayToSource** — имеет только сеттер `Action<T>`; обратные биндеры передают изменения View обратно во ViewModel. Только `TwoWay`/`OneWayToSource`.

Каждое направление представлено в трёх вариантах полезной нагрузки:

- **value** (`Classes/`) — ссылочные типы; одно событие `Action<T?>`.
- **struct** (`Struct/`) — `T : struct`; обобщённая база дополнительно инициирует событие `BoxedChanged`, так что подписчики `IBinder<TBoxed>` (по умолчанию `ValueType`) получают значение уже упакованным, избегая повторной упаковки.
- **enum** (`Enum/`) — `sealed`-подклассы базового struct с `TBoxed`, зафиксированным как `Enum`, так что `IBinder<Enum>` может привязываться наряду с типизированным `T`.

Таким образом, одно поле `[Bind]` даёт одну обёртку, выбранную по типу поля и его объявленному `BindMode`. (Вывод: вариант выбирает генератор; исходный код генератора находится в субмодуле [[Source Generator]].)

## Ключевые связи

- Создаётся во время [[ViewModel Generation]] из поля `[Bind]`.
- Потребляется [[Data Binding]] / [[Runtime Binding Resolution]], когда подключается [[View]].
- `Add`/`Remove` работают с [[IBinder]]; режимы определены в [[BindMode]].
- Помечается в профайлере под Unity (`PROFILER` define).

## Источник

`Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers` — `IBindableMember.cs`, `IBinderAdder.cs`, `IBinderRemover.cs`, а также папки `Classes/`, `Struct/`, `Enum/`.
