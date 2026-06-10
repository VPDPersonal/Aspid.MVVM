---
title: Конвейер генерации кода
type: concept
status: active
source_paths:
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Views/ViewGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Binders/BinderGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Ids/IdGenerator.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/AddBinderContextMenuGenerator.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeAnalyzer.cs
  - CLAUDE.md
tags: [concept, source-generation, roslyn, generators, analyzer]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/Source Generation Pipeline.md
translated_at: 2026-05-31
---

# Конвейер генерации кода

> Механизм времени компиляции, который превращает маркеры `[ViewModel]`, `[Bind]`, `[RelayCommand]` и `[View]` в реальные члены C#: вы пишете намерение, а фреймворк пишет шаблонный код — с нулевой рефлексией во время выполнения.

## Зачем это нужно

MVVM — это в основном механический связующий код: уведомления об изменении свойств, поиск членов привязки, обёртки команд, связывание view с viewmodel. Писать всё это вручную медленно и чревато ошибками. Aspid переносит эту работу на **этап сборки** с помощью Roslyn, оставляя рантайм-код свободным от аллокаций и рефлексии. Более широкую картину см. в [[Architecture]].

## Как это работает

Совместно работают три отдельных инструмента Roslyn, каждый из которых поставляется как **закоммиченная DLL**, которую потребляет Unity — **Unity никогда не компилирует исходники генераторов** (они находятся в сабмодулях). После редактирования генератора необходимо пересобрать его solution и заново закоммитить DLL. См. [[Committed DLLs]], [[Submodule Init]], [[NET 9 SDK Pin]].

1. **[[Source Generator]]** (`Aspid.MVVM.Generators.dll`) — основные инкрементальные генераторы:
   - `ViewModelGenerator` реагирует на `[ViewModel]` и порождает [[ViewModel to Generated Code|реализацию IViewModel]]: члены привязки, сгенерированные свойства + уведомления об изменении, обёртки `RelayCommand` и тело `FindBindableMembers`. Он требует `partial` и отклоняет `static`-классы.
   - `ViewGenerator` реагирует на `[View]` и порождает связывание [[View]] (тоже только для `partial`).
   - `BinderGenerator` нацелен на `partial`-типы, базовый список которых подразумевает [[IBinder]], генерируя обвязку binder'а.
   - `IdGenerator` порождает константы-идентификаторы членов привязки, используемые в [[Runtime Binding Resolution]].
2. **[[Unity Generators]]** (`Aspid.MVVM.Unity.Generators.dll`) — вывод, специфичный для Unity. Например, `AddBinderContextMenuGenerator` читает `[AddBinderContextMenu]`, чтобы сгенерировать пункты контекстного меню редактора, добавляющие binder'ы к [[View]]. См. [[Unity Editor Tooling]].
3. **[[Analyzer]]** (`Aspid.MVVM.Analyzers.dll`) — диагностики + автоисправления, не генерация:
   - `PartialAnalyzer` выдаёт `AM0003`–`AM0006` (ошибки), когда класс или структура с `[View]`/`[ViewModel]` не является `partial` — см. [[Must Be Partial]].
   - `BindAttributeAnalyzer` выдаёт `AM0001` (ошибка) / `AM0002` (предупреждение), когда backing-поле, помеченное `[Bind]`, читается/записывается напрямую, а не через своё сгенерированное свойство.

Все генераторы являются `IIncrementalGenerator`: синтаксический предикат дёшево отфильтровывает кандидатные объявления, семантический шаг `Find` строит модель данных, а `RegisterSourceOutput` порождает код. Такое кэширование сохраняет инкрементальные компиляции быстрыми (предположение: стандартное поведение инкрементального генератора).

## Ключевые связи

- `[ViewModel]` + `[Bind]` + `[RelayCommand]` -> сгенерированные члены: [[ViewModel Generation]], [[Bindable Members]], [[Relay Commands]], [[BindMode]].
- `[View]` -> сгенерированное связывание: [[View Initialization]].
- Сгенерированные идентификаторы питают [[Runtime Binding Resolution]] и [[Data Binding]].
- `Aspid.Collections` — это внешний UPM-пакет (`tech.aspid.collections`), не производимый этим конвейером — см. [[External Dependencies]].

## Источники

- [[Source Generator]], [[Unity Generators]], [[Analyzer]], [[Committed DLLs]]
