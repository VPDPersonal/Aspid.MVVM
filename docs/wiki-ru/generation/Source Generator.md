---
title: Генератор исходного кода
type: generation
status: active
source_paths:
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/ViewModels/ViewModelGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Views/ViewGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Binders/BinderGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Ids/IdGenerator.cs
  - Aspid.MVVM.Generators/Aspid.MVVM.Generators/Aspid.MVVM.Generators/Generators/Descriptions/Classes.Aspid.cs
tags: [generation, roslyn, source-generator, viewmodel, view, binder]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/generation/Source Generator.md
translated_at: 2026-05-31
---

# Генератор исходного кода

> Инкрементальный генератор Roslyn, который превращает маркеры `[ViewModel]`, `[View]`, binder'ы и `[BindId]` в инфраструктуру MVVM — благодаря чему фреймворк выполняет привязку без какой-либо рефлексии. Он поставляется как **закоммиченная DLL**, а не как исходный код.

## Зачем он нужен

Ручное написание реализаций `IViewModel`, связывание bindable-членов, кэширование binder'ов и константы строковых идентификаторов — это повторяющаяся и подверженная ошибкам работа. Генератор создаёт всё это на этапе компиляции, сохраняя привязки свободными от аллокаций и рефлексии. Поскольку Unity использует собранный артефакт ([[Committed DLLs]]), исходный код генератора живёт в submodule ([[Submodule Init]]) и должен пересобираться с помощью [[NET 9 SDK Pin|.NET 9 SDK]] и заново коммититься после изменений.

## Как это работает

Каждый суб-генератор — это отдельный `[Generator]`, реализующий `IIncrementalGenerator`. Они регистрируют провайдер атрибутов/синтаксиса, выполняют синтаксический предикат, строят модель данных, а затем генерируют partial-классы:

| Суб-генератор | Триггер | Что генерирует |
|---|---|---|
| **ViewModels** | `[ViewModel]` на `partial`-классе | реализацию `IViewModel`, bindable-члены, `FindBindableMember`, сгенерированные свойства + уведомления об изменениях, свойства команд `[RelayCommand]` |
| **Views** | `[View]` на `partial`-классе | `Initialize`, кэшированные поля binder'ов, обобщённую инициализацию view |
| **Binders** | `partial`-тип со списком базовых типов (предположительно `IBinder`) | partial `BinderLog` (отладочное логирование в редакторе) |
| **Ids** | `partial`-тип с атрибутами в стиле `[BindId]` | `Aspid.MVVM.Generated.Ids` с константами идентификаторов `const string` |
| **CreateFrom** | атрибут `[CreateFrom]` (здесь папка является заглушкой) | ничего в этом репозитории |
| **Descriptions** | н/д | общие метаданные: таблицы имён типов/пространств имён/атрибутов |

ViewModels и Views используют `ForAttributeWithMetadataName` (быстрый путь на основе атрибутов); Binders и Ids используют `CreateSyntaxProvider`. Все предикаты требуют `partial` и отклоняют `static` ([[Must Be Partial]]). Генератор **Ids** работает на основе коллекции: он собирает идентификаторы по всем типам через `provider.Collect()` и пишет один общий класс `Ids`.

**Descriptions** — это не генератор: `Classes.Aspid.cs` и его собратья представляют собой таблицы констант ([[IViewModel]], [[BindMode]], [[IBinder]], имена атрибутов), на которые ссылаются другие генераторы, чтобы генерируемый код использовал правильные полностью квалифицированные имена.

**CreateFrom** здесь содержит только заглушку; активная обработка `[CreateFrom]`, по-видимому, живёт в [[Unity Generators]] (этот репозиторий лишь объявляет имя `CreateFromAttribute`).

## Ключевые связи

- Детали генерации ViewModel: [[ViewModel to Generated Code]], [[ViewModel Generation]].
- Генерация и runtime view: [[View]], [[View Initialization]], [[Runtime Binding Resolution]].
- Генерация команд: [[Relay Commands]], [[IRelayCommand]].
- Спутник для валидации: [[Analyzer]] (диагностики, например, отсутствие `partial`).
- Сквозной обзор: [[Source Generation Pipeline]], [[Architecture]].

## Исходный код

`Aspid.MVVM.Generators/.../Generators/` — `{ViewModels,Views,Binders,Ids,Descriptions,CreateFrom}/`. Каждый суб-генератор разбит на `*.cs` (`Initialize` + предикат), `*.Find.cs` (модель данных) и `*.Generate.cs` (генерация). Собранная DLL: `Aspid.MVVM.Generators.dll` в `Assets/Aspid/MVVM/`.
