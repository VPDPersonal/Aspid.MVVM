---
title: Архитектура
type: overview
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source
  - CLAUDE.md
  - Readme.md
tags: [overview, architecture]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/overview/Architecture.md
translated_at: 2026-05-31
---

# Архитектура

> Aspid.MVVM разделяет **View**, **ViewModel** и бизнес-логику в Unity, генерируя связующий код для биндингов на этапе сборки, благодаря чему биндинги работают **без рефлексии** и с минимальными аллокациями.

## Зачем это нужно

Классический MVVM в Unity тянет за собой биндинг на основе рефлексии (медленный, нагружающий GC) и кучу шаблонного кода (`INotifyPropertyChanged`, backing-поля, обёртки над командами). Aspid переносит эту работу на **этап сборки** с помощью source-генераторов Roslyn, поэтому во время выполнения остаются обычные вызовы методов.

## Три слоя

- **ViewModel** — `partial`-класс, помеченный `[ViewModel]`. Поля, помеченные `[Bind]`, становятся наблюдаемыми bindable-членами; методы, помеченные `[RelayCommand]`, становятся свойствами `IRelayCommand`. Генератор формирует реализацию [[IViewModel]]. → [[ViewModel Generation]]
- **View** — компонент, содержащий [[Binders Catalog|биндеры]]. Каждый binder — это небольшой объект, связывающий один элемент UI (`TMP_Text`, `Image`, `Toggle`, …) с одним bindable-членом в направлении, заданном через [[BindMode]].
- **Бизнес-логика** — обычный C#, который вызывает ViewModel; фреймворк никогда не вмешивается в неё.

## Как распространяется изменение

Поле ViewModel меняется → bindable-член генерирует уведомление → привязанные биндеры обновляют View (а для `TwoWay`/`OneWayToSource` правки во View возвращаются обратно). Направление задаётся для каждого биндинга через [[BindMode]]. Полное пошаговое описание работы на этапе сборки см. в [[ViewModel to Generated Code]].

## Где расположены части

- Базовые контракты и атрибуты: `Source/` (небольшой, стабильный, высокоценный слой) — [[IViewModel]], [[IRelayCommand]], [[IBinder]].
- Готовые биндеры и конвертеры: `StarterKit/` (~73% кода) — [[Binders Catalog]].
- Кодогенерация: три git-сабмодуля (source-генератор, Unity-генераторы, анализатор) — см. [[Source Generation Pipeline]].
- `Aspid.Collections` (наблюдаемые коллекции) — это **внешний UPM-пакет**, его нет в этом репозитории — см. [[External Dependencies]].

## Что нельзя ломать

Сгенерированные типы должны быть `partial` ([[Must Be Partial]]); DLL генераторов закоммичены и потребляются Unity ([[Committed DLLs]]).
