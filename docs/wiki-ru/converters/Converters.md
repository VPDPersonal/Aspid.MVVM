---
title: Конвертеры
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/IConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/ConverterExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/GenericFuncConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/SequenceConverters.cs
tags: [converter, starterkit, binding]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/converters/Converters.md
translated_at: 2026-05-31
---

# Конвертеры

> Небольшие однонаправленные преобразования, размещаемые между bindable-членом и binder'ом, которые меняют форму значения (например, `int` -> `string`, `null` -> `bool`), чтобы View мог отображать данные, о которых ViewModel вообще не должна знать.

## Зачем это нужно

Bindable-член предоставляет тип данных, имеющий смысл для бизнес-логики. Binder'у же нужен тип данных, который ожидает UI-элемент. Конвертеры закрывают этот разрыв, **не** засоряя ViewModel заботами View. Поскольку [[Data Binding]] в Aspid генерируется и работает без рефлексии, конвертеры — это обычные сериализуемые объекты, которые вы компонуете в Inspector, а не магия с разрешением во время выполнения.

## Как это работает

Контракт намеренно крошечный:

```csharp
public interface IConverter<in TFrom, out TTo>
{
    TTo Convert(TFrom value);
}
```

Вариантность `in`/`out` позволяет одному слоту конвертера принимать совместимые типы. Преобразование **однонаправленное** — `Convert` отображает `TFrom` в `TTo`. Метода `ConvertBack` нет, поэтому конвертеры естественно сочетаются с однонаправленными потоками (см. [[BindMode]]); обратное преобразование, судя по всему, требует отдельного конвертера.

Рядом с семействами живут три базовых строительных блока:

- **`GenericFuncConverter<TFrom, TTo>`** — адаптер, оборачивающий `Func<TFrom?, TTo?>` (или другой `IConverter`) как `IConverter`. Позволяет лямбдам, определённым в коде, подключаться к слоту конвертера.
- **`ConverterExtensions.ToConvert(...)`** — расширение, превращающее любой `Func<TFrom?, TTo?>` в `IConverter` через `GenericFuncConverter`.
- **`SequenceConverters<T>`** — выстраивает в цепочку `IConverter<T,T>[]`, применяя каждый по порядку. В Unity 2023.1+ массив использует `[SerializeReference]` с выпадающим списком, так что вы собираете конвейеры визуально.

## Ключевые связи

Конвертеры используются binder'ами, у которых есть поле конвертера (чаще всего [[Caster Binders]] и многие [[Text Binders|binder'ы из StarterKit]]). Binder читает исходное значение из bindable-члена, вызывает `Convert`, а затем передаёт результат в UI-элемент — поэтому конвертеры являются stateless-шагом внутри [[Runtime Binding Resolution]]. Они входят в состав StarterKit, а не ядра [[Architecture]].

Четыре семейства:

| Семейство | Назначение |
|--------|---------|
| [[Bool Converters]] | сравнения, null/пусто/число -> `bool` |
| [[Number Converters]] | арифметические операции над числовыми значениями |
| [[String Converters]] | `ToString`, форматирование, культура, `TimeSpan` |
| [[Specific Converters]] | типизированные bool/number/string варианты для конкретных привязок |

## Источник

`StarterKit/Runtime/Converters/` — `IConverter.cs`, `GenericFuncConverter.cs`, `ConverterExtensions.cs`, `SequenceConverters.cs`, а также подпапки `Bools/`, `Numbers/`, `Strings/`, `Specific/`.
