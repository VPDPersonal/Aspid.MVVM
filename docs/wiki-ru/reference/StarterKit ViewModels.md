---
title: ViewModels из StarterKit
type: reference
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/EmptyViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/ValueViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicViewModel.Create.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicPropertyData.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/DynamicPropertyFactory.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/IDynamicProperty.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/Dynamic/TwoWayDynamicProperty.cs
tags: [starterkit, viewmodel, dynamic, reference]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/reference/StarterKit ViewModels.md
translated_at: 2026-05-31
---

# ViewModels из StarterKit

> Готовые реализации [[IViewModel]], позволяющие выполнять привязку без написания собственного класса `[ViewModel]` вручную — для разовых значений, наборов свойств, определяемых в рантайме, или явной заглушки без поведения.

## Зачем это нужно

Стандартный путь во фреймворке — это класс, помеченный `[ViewModel]`, чьи привязываемые члены генерируются [[Source Generation Pipeline|генератором исходного кода]] (см. [[ViewModel to Generated Code]]). Это идеально подходит для фиксированных схем. StarterKit добавляет три прагматичные альтернативы для случаев, когда написание полноценного класса избыточно или невозможно.

## Три view model

### ValueViewModel\<T> (а также \<T1,T2>, \<T1,T2,T3>, \<T1,T2,T3,T4>)
Сгенерированная view model, содержащая от 1 до 4 независимых привязываемых значений с именами `Value` / `Value1`…`Value4`, каждое из которых помечено `[TwoWayBind]`. Поскольку класс — это `[ViewModel] partial`, генератор создаёт соответствующие привязываемые члены и хуки уведомления `OnValuePropertyChanged()`, которые вызываются сеттерами (невидимы в исходном коде — см. [[Bindable Members]]). Сеттеры опционально замыкаются накоротко через `EqualityComparer<T>.Default`, когда параметр конструктора `checkEquality` равен `true`. Используйте это, когда экрану нужно выставить пару значений без выделенного класса.

### DynamicViewModel
View model с модификатором `sealed`, члены которой неизвестны на этапе компиляции. Она хранит `Dictionary<string, IDynamicProperty>` и разрешает привязки по id в рантайме: `FindBindableMember` ищет `parameters.Id` и возвращает `IBinderAdder` свойства (см. [[Runtime Binding Resolution]]). Отсутствующие id возвращают `default` либо выбрасывают `ArgumentException`, когда `throwErrorIfNotFindProperty` равно `true`. Неявный оператор преобразует словарь напрямую в `DynamicViewModel`.

Статические перегрузки `Create<T1…T8>(...)` строят словарь из записей `DynamicPropertyData<T>` (каждая несёт `Id`, `Value` и [[BindMode]]); необязательный ведущий `bool` задаёт поведение при выбросе исключения. `DynamicPropertyFactory` сопоставляет режим конкретному `IDynamicProperty`:

| BindMode | Тип свойства | Базовый член |
|----------|--------------|----------------|
| `OneTime` | `OneTimeDynamicProperty<T>` | `OneTimeBindableMember<T>` |
| `OneWay` | `OneWayDynamicProperty<T>` | `OneWayBindableMember<T>` |
| `TwoWay` | `TwoWayDynamicProperty<T>` | `TwoWayBindableMember<T>` |

`None` и `OneWayToSource` отклоняются конструктором `DynamicPropertyData`. Обратите внимание, что `DynamicPropertyData<T>` — это `readonly ref struct`, поэтому он существует только в стеке и предназначен для встраиваемой передачи в `Create`.

### EmptyViewModel
Заглушка с модификатором `sealed` без поведения: `FindBindableMember` всегда возвращает `default`. Полезна как ненулевой заполнитель там, где [[View]] ожидает [[IViewModel]], но никакие данные ещё не должны привязываться.

## Ключевые связи

- Реализует [[IViewModel]]; используется [[View]] во время [[View Initialization]].
- Режимы свойств отражают [[BindMode]] и оборачивают те же типы привязываемых членов, описанные в [[Bindable Members]].
- `DynamicViewModel` — это гибкий в рантайме аналог генерируемой через [[ViewModel Generation]] модели.

## Исходный код

`Assets/Aspid/MVVM/StarterKit/Runtime/ViewModels/` — `ValueViewModel.cs`, `EmptyViewModel.cs` и `Dynamic/`.
