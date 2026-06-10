---
title: Специализированные конвертеры
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Numbers/IConverterIntToFloat.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Numbers/NumberConverterSpecificExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/IConverterIntToBool.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Bool/BoolConverterSpecificExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Specific/Strings/IConverterTimeSpanToString.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/IConverter.cs
tags: [converter, starterkit, binding, type-safety]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/converters/Specific Converters.md
translated_at: 2026-05-31
---

# Специализированные конвертеры

> Строго типизированные интерфейсы-маркеры (например, `IConverterIntToFloat`), которые фиксируют оба конца преобразования, чтобы поле binder могло требовать конвертер строго заданной формы, а не произвольный `IConverter<,>`.

## Зачем это нужно

[[Converters|IConverter]]`<in TFrom, out TTo>` (один метод, `TTo Convert(TFrom)`) является обобщённым. Unity не может сериализовать открытый обобщённый тип в слот инспектора, а автору binder нужно поле, принимающее *только* конвертеры «int → float», а не любой конвертер. Папка `Specific` решает эту задачу, объявляя закрытые именованные интерфейсы-маркеры — `IConverterIntToFloat : IConverter<int, float>` — достаточно конкретные для слота сериализованной ссылки и при этом удовлетворяющие обобщённому контракту.

## Как это работает

Каждый файл — это однострочный интерфейс, который закрывает обобщённый тип над конкретной парой. Они сгруппированы по семейству *целевого* типа:

| Группа | Количество | Форма | Примеры |
|-------|-------|-------|----------|
| Numbers | 16 | все сочетания `int`/`long`/`float`/`double` | `IConverterInt`, `IConverterIntToFloat`, `IConverterDoubleToLong` |
| Bool | 6 | `int`/`long`/`float`/`double`/`object?`/`string?` → `bool` | `IConverterIntToBool`, `IConverterStringToBool` |
| Strings | 3 | `string?`/`object?`/`TimeSpan` → `string?` | `IConverterString`, `IConverterTimeSpanToString` |

Пары одинаковых типов (`IConverterInt : IConverter<int,int>`) существуют для того, чтобы преобразование «на месте» по-прежнему удовлетворяло типизированному слоту.

Каждая группа поставляется со статическим классом `*SpecificExtensions` (например, `NumberConverterSpecificExtensions`) с двумя фабриками на каждый интерфейс:

- `ToConvert(this Func<TFrom,TTo>)` — оборачивает лямбду.
- `ToConvertSpecific(this IConverter<TFrom,TTo>)` — переоборачивает существующий обобщённый конвертер.

Обе возвращают закрытый интерфейс, построенный на приватном sealed-классе, наследующем от `GenericFuncConverter<TFrom,TTo>`. Так, `((Func<int,float>)(i => i / 100f)).ToConvert()` даёт `IConverterIntToFloat`, готовый к помещению в поле binder.

Это обычные runtime-типы — никакой генерации `[ViewModel]`/`[Bind]` здесь не задействовано. По всей видимости, они существуют исключительно для того, чтобы дать слою [[Bindable Members]] / binder проверяемые на этапе компиляции слоты конвертеров; числовая матрица сделана исчерпывающей именно для того, чтобы любое межчисловое приведение, которое может понадобиться [[Number Converters|числовому binder]], уже имело имя.

## Ключевые связи

- Закрывает [[Converters|IConverter]]`<TFrom,TTo>` над конкретными парами.
- Дополняет богатые поведением семейства: [[Bool Converters]], [[Number Converters]], [[String Converters]] — те несут *логику* преобразования; эти несут только *типовую идентичность*.
- Используются binder'ами, выбирающими конвертер — см. [[Binder Base Classes]], [[Image Binders]], [[Slider Binders]].

## Исходный код

`StarterKit/Runtime/Converters/Specific/{Bool,Numbers,Strings}/` — 25 интерфейсов-маркеров плюс 3 фабричных класса `*SpecificExtensions`.
