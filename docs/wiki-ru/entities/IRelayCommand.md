---
title: IRelayCommand
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs
tags:
  - command
  - entity
  - viewmodel
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/entities/IRelayCommand.md
translated_at: 2026-05-31
---

# IRelayCommand

> Контракт команды, который ViewModel предоставляет наружу, чтобы View могла вызвать действие и узнать, разрешено ли оно в данный момент — привязывается через Button/Selectable binder'ы.

## Зачем это нужно

MVVM держит View «глупой»: кнопка не должна вызывать бизнес-логику напрямую. Вместо этого ViewModel предоставляет объект команды, который binder может выполнить через `Execute()` и опросить через `CanExecute()`, а также событие `CanExecuteChanged`, по которому UI повторно переоценивает состояние (например, делает кнопку неактивной) при его изменении. `IRelayCommand` — это и есть такой контракт; он аналогичен `ICommand` из WPF, но при этом облегчён по аллокациям и не использует рефлексию, что соответствует целям производительности Aspid.

## Как это работает

`IRelayCommand.cs` объявляет пять вариантов арности — беспараметрический `IRelayCommand` плюс обобщённые `IRelayCommand<T>` … `IRelayCommand<T1,T2,T3,T4>`. Каждый из них содержит:

- `bool CanExecute(...)` — предикат-условие выполнения.
- `void Execute(...)` — запускает действие.
- `event Action<...> CanExecuteChanged` — вызывается, когда возможность выполнения могла измениться.
- `void NotifyCanExecuteChanged()` — вызывает это событие вручную.

`RelayCommand.cs` предоставляет sealed-реализации. Каждая оборачивает `Action` (execute) и необязательный `Func<bool>` (`canExecute`). Конструктор бросает `ArgumentNullException`, если `execute` равен null. Метод `Execute()` внутри сначала повторно проверяет `CanExecute()` и ничего не делает, если тот возвращает false; null-предикат трактуется как «всегда выполнимо» (`?? true`).

Для каждой арности существуют два переиспользуемых синглтона: `Empty` (выполнить нельзя) и `EmptyExecution` (выполняется, но ничего не делает) — удобные заглушки в стиле null-объектов там, где требуется не-null команда.

Когда задан `UNITY_2022_1_OR_NEWER` и профилировщик не отключён, вызовы оборачиваются в `Marker()` профилировщика для диагностики.

## Связь с генерацией [RelayCommand]

Вы редко пишете `new RelayCommand(...)` вручную. Пометка метода атрибутом `[RelayCommand]` заставляет [[Source Generator]] сгенерировать лениво создаваемое свойство `IRelayCommand` на `partial`-классе, подключая ваш метод как `execute`, а необязательный метод `CanExecute*` — как предикат. О стороне написания кода и сгенерированных членах см. [[Relay Commands|Relay-команды]]. Класс-хозяин должен быть `partial` ([[Must Be Partial]]).

## Ключевые связи

- Выставляется наружу через [[ViewModel]] / [[IViewModel]] как привязываемый член ([[Bindable Members]]).
- Потребляется [[Button Binders]] и [[Selectable Binders]] через [[Runtime Binding Resolution]].
- Написание кода + сгенерированный вывод: [[Relay Commands|Relay-команды]], [[ViewModel to Generated Code]].

## Источник

- `Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs`
- `Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs`
