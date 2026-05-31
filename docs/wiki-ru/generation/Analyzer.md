---
title: Анализатор
type: generation
status: active
source_paths:
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/TypeAnalyzers/PartialCodeFixProvider.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeAnalyzer.cs
  - Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/Aspid.MVVM.Analyzers/FieldAnalyzers/BindAttributeCodeFixProvider.cs
tags: [analyzer, roslyn, diagnostics, codefix, generation]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/generation/Analyzer.md
translated_at: 2026-05-31
---

# Анализатор

> Диагностики Roslyn и исправления кода в один клик, которые отлавливают две самые частые ошибки в Aspid прямо во время редактирования: забытый `partial` и прямое обращение к полю `[Bind]` вместо сгенерированного для него свойства.

## Зачем он нужен

Aspid построен на генерации кода, поэтому члены, которые *следует* использовать, не видны в написанном вручную исходном коде. Отсюда естественно возникают две ошибки: объявление типа `[ViewModel]`/`[View]` без `partial` (генератор не может добавить свою половину) и чтение или запись поля `[Bind]` напрямую вместо сгенерированного свойства (что обходит уведомление об изменениях). Анализатор подсвечивает обе проблемы как подчёркивания в IDE вместе с исправлениями задолго до того, как сборка завершится ошибкой. Поставляется как закоммиченная DLL — см. [[Committed DLLs]]; Unity потребляет DLL, а не исходники этого сабмодуля. См. также [[Must Be Partial]].

## Как он работает

Две пары `DiagnosticAnalyzer` + `CodeFixProvider`. Оба анализатора пропускают сгенерированный код и работают параллельно.

**PartialAnalyzer** регистрируется на объявлениях классов/структур. Для типа, несущего `Aspid.MVVM.ViewModelAttribute` или `Aspid.MVVM.ViewAttribute`, но без ключевого слова `partial`, он сообщает один из четырёх идентификаторов ошибок (класс против структуры × ViewModel против View):

| ID | Триггер | Серьёзность |
|----|---------|-------------|
| AM0003 | class + `[View]` не partial | Error |
| AM0004 | struct + `[View]` не partial | Error |
| AM0005 | class + `[ViewModel]` не partial | Error |
| AM0006 | struct + `[ViewModel]` не partial | Error |

`PartialCodeFixProvider` предлагает **"Make partial"** — он просто добавляет `AddModifiers(partial)` к объявлению.

**BindAttributeAnalyzer** регистрируется на каждом `IdentifierName`, разрешает его в `IFieldSymbol` и проверяет, несёт ли поле `[Bind]` или вариант с конкретным режимом (`OneWayBind`, `TwoWayBind`, `OneTimeBind`, `OneWayToSourceBind`). Если да, он сообщает:

| ID | Контекст | Серьёзность |
|----|----------|-------------|
| AM0001 | поле **слева** от присваивания (запись) | Error |
| AM0002 | любое другое чтение | Warning |

`BindAttributeCodeFixProvider` предлагает **"Use property '<Name>'"**, заменяя идентификатор на имя свойства в PascalCase (`_count` → `Count`, отбрасывая префиксы `_` / `m_`). Записи внутри конструктора намеренно исключены (`IsWithinConstructor`), поскольку сгенерированное свойство может быть ещё не подключено там.

> Примечание (вывод): исправление читает свойство диагностики `PropertyName`, если оно присутствует, иначе заново выводит PascalCase. Исходник анализатора выше, по-видимому, не устанавливает это свойство, так что на сегодня работает именно запасной путь.

## Ключевые связи

- Обеспечивает соблюдение контракта [[Must Be Partial]], от которого зависит [[Source Generator]].
- AM0001/AM0002 защищают сгенерированное свойство [[Bindable Members]] поверх его backing-поля `[Bind]`; о вариантах режимов см. [[BindMode]].
- Дополняет, но отделён от [[Source Generation Pipeline]] и [[Unity Generators]].

## Исходный код

`Aspid.MVVM.Analyzers/.../TypeAnalyzers/` (пара Partial) и `.../FieldAnalyzers/` (пара Bind). Собирается независимо от Unity в закоммиченную `Aspid.MVVM.Analyzers.dll`. См. [[Submodule Init]] и [[NET 9 SDK Pin]].
