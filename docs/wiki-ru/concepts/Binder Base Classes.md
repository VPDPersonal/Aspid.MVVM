---
title: Базовые классы биндеров
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/TargetProperty/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/TargetProperty/TargetFloatBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/OneWayValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/TwoWayValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Values/OneWayToSourceValue.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Switchers/SwitcherBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericOneWayBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericCasterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Generics/GenericOneWayToSourceBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/Binders/Casters/AnyToStringCasterBinder.cs
tags: [concept, binders, starterkit, base-classes]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/Binder Base Classes.md
translated_at: 2026-05-31
---

# Базовые классы биндеров

> Чисто C#-строительные блоки в каталоге `StarterKit/Runtime/Binders`, которые переиспользует каждая конкретная категория биндеров, благодаря чему любому биндеру, привязанному к конкретному UI, остаётся написать лишь крошечный аксессор к цели.

## Зачем это нужно

Без общих базовых классов каждому Unity-биндеру ([[Text Binders]], [[Slider Binders]] и т. д.) пришлось бы заново реализовывать обвязку [[IBinder]]/`IReverseBinder`: валидацию режима привязки, опциональное [[Converters|преобразование]] и обратную запись в `OnBound`. Эти пять семейств берут на себя весь этот шаблонный код, так что конкретные биндеры предоставляют лишь аксессор `Property` или делегат-сеттер.

## Как это работает

Все семейства наследуются от `Binder` (который хранит `Mode` и предоставляет хуки `OnBound`/`OnUnbinding`) и проверяют запрошенный [[BindMode]] в своём конструкторе (`ThrowExceptionIfTwo`/`ThrowExceptionIfNone`).

- **TargetProperty** — абстрактный `TargetBinder<TTarget, TProperty>` привязывается к `Property { get; set; }`, который вы переопределяете. `SetValue` записывает свойство; в режиме `OneWayToSource` метод `OnBound` считывает его обратно в ViewModel. Вариант с тремя аргументами добавляет сериализуемый `IConverter`. Типизированные сокращения (`TargetFloatBinder`, `TargetIntBinder`, `TargetBoolBinder`, `TargetStringBinder`) реализуют мультитиповые интерфейсы вроде `INumberBinder`, так что один биндер принимает `int`/`long`/`float`/`double`.
- **Values** — конкретные `OneWayValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` (`TwoWayValue`, зафиксированный на одном режиме) и `OneTimeValue`. Они *хранят* последнее значение (сериализуемое `_value`), вызывают `Changed`/`ValueChanged` и предлагают неявное преобразование в `T`. Используются как сериализуемые поля внутри сгенерированных View, а не путём наследования.
- **Switchers** — `SwitcherBinder<T>` / `<TTarget,T>` / `<TTarget,T,TConverter>` принимают привязанный `bool` и передают заранее настроенное `_trueValue` или `_falseValue` в абстрактный `SetValue(T)`. Двусторонний режим отклоняется.
- **Generics** — `GenericOneWayBinder`, `GenericTwoWayBinder`, `GenericOneWayToSourceBinder`, `GenericCasterBinder` оборачивают делегаты (`Action<T>`, `Func<T>`) вместо переопределений. Перегрузка `<TTarget,...>` хранит цель отдельно, чтобы избежать захвата замыкания на Unity-компонентах.
- **Casters** — `GenericCasterBinder<TFrom,TTo>` плюс готовые кастеры (`AnyToStringCasterBinder` через `IAnyBinder`, `StringToBoolCasterBinder`) запускают `IConverter` между привязанным и целевым типами.

## Ключевые связи

- Реализуют [[IBinder]] / `IReverseBinder`; используются во время [[Runtime Binding Resolution]].
- Преобразование делегируется [[Converters]] (`IConverter<TFrom,TTo>`).
- Конкретные Unity-семейства ([[Image Binders]], [[Transform Binders]], [[Toggle Binders]], …) расширяют их; см. [[Binders Catalog]] и [[Mono Binders]].
- `[BindModeOverride]` ограничивает выпадающий список режимов в инспекторе (см. [[BindMode]]).

## Источник

`StarterKit/Runtime/Binders/{TargetProperty,Values,Switchers,Generics,Casters}/`
