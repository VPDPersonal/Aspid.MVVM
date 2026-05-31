---
title: Строковые конвертеры
type: converter
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/StringFormatConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/GenericToString.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/ObjectToStringConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/TimeSpanToStringConverter.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/CultureInfoMode.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Converters/Strings/Extensions/ToCultureStringExtensions.cs
tags: [converter, string, formatting, culture, starterkit]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/converters/String Converters.md
translated_at: 2026-05-31
---

# Строковые конвертеры

> Конвертеры из StarterKit, превращающие значения ViewModel в строки для отображения — применяют шаблоны `string.Format` и форматирование чисел с учётом культуры перед тем, как их покажет текстовый binder.

## Зачем это нужно

Bindable-член часто содержит сырое значение (число, `TimeSpan`, произвольный объект), но View хочет отформатированную надпись вроде `"Score: 1500"`. Вместо того чтобы засорять ViewModel презентационными строками, вы подключаете к binder'у сериализуемый конвертер. Эти конвертеры встраиваются в [[Data Binding]] и питают [[Text Binders]] / [[InputField Binders]], удерживая вопросы форматирования в слое View.

## Как это работает

Все члены — это `[Serializable]`-классы, реализующие интерфейс конвертера (`IConverter<TFrom?, string?>` или доменный маркер вроде `IConverterString`), поэтому они отображаются как настраиваемые прямо в инспекторе поля в Unity Inspector. Каждый предоставляет единственный метод `Convert(value)`, возвращающий `string?`. См. [[Converters]] для общей модели интерфейсов.

`StringFormatConverter` — string -> string. Применяет сериализованный `_format` через `string.Format(_format, value)`. Если `_format` пуст, возвращает значение без изменений. `_formatEmptyValues` (по умолчанию `false`) определяет, должны ли пустые/пробельные входные значения всё равно оборачиваться шаблоном; в противном случае они проходят насквозь нетронутыми.

`GenericToString<TFrom>` — база для преобразования значения в строку. Возвращает `null` для `null`-входа; при наличии `_format` вызывает `string.Format`, иначе вызывает `protected virtual ToStringValue(value)` (по умолчанию `value.ToString()`), который могут переопределять наследники.

`ObjectToStringConverter` (`GenericToString<object?>`) и `TimeSpanToStringConverter` (`GenericToString<TimeSpan>`) — sealed конкретные специализации, выставляющие обобщённый базовый класс через доменные маркер-интерфейсы, чтобы их можно было выбрать в инспекторе.

`CultureInfoMode` + `ToCultureStringExtensions` — сериализуемый enum (CurrentCulture, InvariantCulture и т. д.) плюс расширения, сопоставляющие его с `CultureInfo` и предоставляющие перегрузки `ToCultureString(this int/uint/long/double/float, CultureInfoMode)`. Они позволяют вызывающему коду форматировать числа под явно заданную культуру, не зашивая жёстко ссылки на `CultureInfo`. Похоже, что это вспомогательные средства, используемые [[Number Converters]], а не самостоятельные типы `IConverter`.

## Ключевые связи

- Реализуют интерфейсы [[Converters]]; находятся внутри binder'а, настроенного согласно [[BindMode]] (обычно OneWay к текстовому приёмнику).
- Родственные семейства: [[Number Converters]], [[Bool Converters]], [[Specific Converters]].
- `GenericToString<TFrom>` — переиспользуемая база; хук переопределения делает её расширяемой без новой обвязки.

## Исходники

`StarterKit/Runtime/Converters/Strings/` — `StringFormatConverter.cs`, `GenericToString.cs`, `ObjectToStringConverter.cs`, `TimeSpanToStringConverter.cs`, `CultureInfoMode.cs`, `Extensions/ToCultureStringExtensions.cs`.
