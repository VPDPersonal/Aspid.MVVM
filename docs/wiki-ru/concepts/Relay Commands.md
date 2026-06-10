---
title: Relay-команды
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/IRelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/RelayCommand.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/Extensions/RelayCommandExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Commands/Extensions/RelayCommandExtensions.Action.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/RelayCommandAttribute.cs
tags: [concept, commands, relaycommand, source-generation]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/Relay Commands.md
translated_at: 2026-05-31
---

# Relay-команды

> Relay-команда — это переиспользуемое привязываемое действие: метод, представленный в виде объекта, который кнопка (или любой binder) может вызвать и который способен сам себя отключить, когда выполнение недопустимо.

## Зачем это нужно

Элементы UI не должны вызывать методы ViewModel напрямую — им нужна стабильная привязываемая ссылка, которая к тому же отвечает на вопрос «можно ли мне выполниться прямо сейчас?». Relay-команда упаковывает делегат *execute* вместе с необязательным предикатом *canExecute*, поэтому [[Button Binders|кнопка]] может одновременно запускать действие и делать себя неактивной. Это сохраняет паттерн команды дешёвым по аллокациям и свободным от рефлексии, что соответствует целям [[Data Binding]] во Aspid.

## Как это работает

Контракт описан в [[IRelayCommand]]: `Execute()`, `CanExecute()`, событие `CanExecuteChanged` и `NotifyCanExecuteChanged()`. Существуют беспараметрические и обобщённые перегрузки (`IRelayCommand<T>` вплоть до `IRelayCommand<T1,T2,T3,T4>`), каждая из которых добавляет типизированные параметры в `Execute`/`CanExecute`.

`RelayCommand` (и `RelayCommand<T>` …) — это запечатанная (sealed) реализация. Она оборачивает callback выполнения `Action`/`Action<T>` плюс необязательный предикат `Func<bool>`/`Func<T,bool>`. `Execute()` запускает действие только если `CanExecute()` возвращает true; null-предикат означает «всегда выполнимо». Под Unity вызовы оборачиваются в маркер профайлера (вырезается через `ASPID_MVVM_UNITY_PROFILER_DISABLED`).

Две статические заглушки позволяют избежать проверок на null: `RelayCommand.Empty` (никогда не выполнима) и `RelayCommand.EmptyExecution` (выполнима, но ничего не делает).

Три способа получить команду:
1. **Сгенерированная** — примените `[RelayCommand]` к методу в классе с `[ViewModel]`. [[Source Generation Pipeline|Генератор]] создаёт свойство `IRelayCommand` (перегрузка выбирается по количеству параметров метода), оборачивающее этот метод. `[RelayCommand(CanExecute = nameof(...))]` подключает метод-вентиль, возвращающий `bool`.
2. **Вариант с Action** — расширение `someAction.CreateCommand(canExecute)` строит `RelayCommand` из делегатов вручную.
3. **Прямой** — `new RelayCommand(execute, canExecute)`.

`RelayCommandExtensions` также предоставляет `GetSelfOrEmpty()` / `GetSelfOrEmptyExecution()` для null-безопасного отката к заглушкам.

## Ключевые связи

- Помечается атрибутом `RelayCommandAttribute` (только методы, `AllowMultiple`), обрабатывается совместно с [[ViewModel to Generated Code]].
- Потребляется binder'ами, такими как [[Button Binders]], через [[IBinder]] / [[Bindable Members]].
- Концептуально аналогична [[Bindable Members|полям [Bind]]], но для действий, а не для состояния.
- `NotifyCanExecuteChanged()` — это командный аналог уведомления об изменении [[BindMode|OneWay]].

## Исходники

- `Source/Commands/IRelayCommand.cs` — интерфейсы (0–4 параметра)
- `Source/Commands/RelayCommand.cs` — запечатанные реализации + `Empty`/`EmptyExecution`
- `Source/Commands/Extensions/` — `CreateCommand`, `GetSelfOrEmpty`
- `Source/ViewModels/Generation/RelayCommandAttribute.cs` — маркер `[RelayCommand]`
