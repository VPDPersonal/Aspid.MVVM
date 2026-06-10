---
title: Генераторы Unity
type: generation
status: active
source_paths:
  - Aspid.MVVM.Unity.Generators/Readme.md
  - Aspid.MVVM.Unity.Generators/Directory.Build.targets
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/AddBinderContextMenuGenerator.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/ContextMenu/ContextMenuData.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/Descriptions/Defines.UnityEngine.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Generators/Descriptions/Classes.UnityEngine.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Helpers/CodeWriter.cs
  - Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Aspid.MVVM.Unity.Generators/Helpers/Extensions/Declarations/TypeDeclarationSyntaxExtensions.cs
tags: [generation, unity, roslyn, codegen, editor]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/generation/Unity Generators.md
translated_at: 2026-05-31
---

# Генераторы Unity

> Отдельный сабмодуль Roslyn-генератора (`Aspid.MVVM.Unity.Generators`), который генерирует код, предназначенный только для Unity-редактора, плюс общий слой кодогенерации `Aspid.Generator.Helpers` (CodeWriter, расширения для символов/синтаксиса), используемый во всех генераторах Aspid.

## Зачем он нужен

Основная генерация ([[Source Generator]]) не зависит от движка. Специфичные для Unity удобства — например, добавление binder в контекстное меню Inspector по правому клику — зависят от типов `UnityEngine`/`UnityEditor` и должны быть защищены директивой `#if UNITY_EDITOR`. Вынос этого в отдельный генератор сохраняет ядро чистым, при этом по-прежнему избавляя авторов [[IBinder|binder]] от ручного шаблонного кода.

## Как это работает

Единственный генератор здесь — `AddBinderContextMenuGenerator`, `IIncrementalGenerator`, запускаемый атрибутом `[AddBinderContextMenu]` через `ForAttributeWithMetadataName`. Для каждого аннотированного `class` он строит запись `ContextMenuData` (декларация, символ, `Path`/`SubPath` меню, приоритет), а затем генерирует внутренний статический класс `__<Name>Editor`, содержащий метод `[MenuItem("CONTEXT/<Type>/<path>", priority=...)]`. Вызов этого пункта меню обращается к `gameObject.AddComponent(...)`, так что целевой компонент можно добавить прямо из контекстного меню другого компонента. Вся генерация обёрнута в `#if UNITY_EDITOR`. Когда явный `Path` не задан, генератор откатывается к значению `[AddComponentMenu]` класса, иначе — к `Add <Type> Binder/<Name>`.

Более насыщенная половина сабмодуля — это `Aspid.Generator.Helpers`, общий инструментарий кодогенерации:

- **`CodeWriter`** — обёртка над `IndentedTextWriter`, предоставляющая `Append`/`AppendLine`/`AppendMultiline`, `BeginBlock`/`EndBlock`, а также `BeginIndentScope`/`BeginBlockScope` с поддержкой `Dispose`. Возвращает Roslyn-`SourceText`.
- **Расширения синтаксиса** (`TypeDeclarationSyntaxExtensions` и т. д.) — выводят `DeclarationText` (модификаторы, `class`/`struct`, обобщённые аргументы), имя пространства имён и имена выходных файлов из узлов декларации.
- **Расширения символов** (`SymbolExtensions`, `TypeSymbolExtensions`) — поиск атрибутов (`HasAnyAttribute`), обход базовых типов/интерфейсов, глобальные отображаемые строки.
- **Описания** (`Classes`, `Defines`, `Namespaces`, `General`) — типизированные константы вроде `MenuItem`, `MonoBehaviour`, `UNITY_EDITOR`, благодаря которым генераторы ссылаются на типы Unity по имени, без жёстко закодированных строк.

Эти хелперы (вероятно, общие также для основного [[Source Generator]] и [[Analyzer]]) — то, из чего собрана любая генерация Aspid.

## Ключевые связи

- Потребляет `[AddBinderContextMenu]`, определённый в основном рантайме (`Unity/Runtime/ContextMenus/`).
- `Directory.Build.targets` копирует собранную DLL в `Assets/Aspid/MVVM/Unity/` — см. [[Committed DLLs]]; Unity загружает DLL, а не исходный код.
- Структура сабмодуля и инициализация: [[Submodule Init]]; требование к SDK: [[NET 9 SDK Pin]].
- Стоит рядом с [[ViewModel Generation]] и [[Architecture]] в [[Source Generation Pipeline]].

## Исходный код

`Aspid.MVVM.Unity.Generators/` (сабмодуль) — `Generators/ContextMenu/` и `Helpers/`.
