---
title: ViewModel
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/ViewModelAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BaseBindAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/BindAlsoAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/AccessAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Generation/RelayCommandAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/Extensions/ViewModelExtensions.cs
tags: [viewmodel, entity, attributes, source-generation]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/entities/ViewModel.md
translated_at: 2026-05-31
---

# ViewModel

> `partial`-класс (или структура), помеченный `[ViewModel]`, который хранит состояние `[Bind]` и методы `[RelayCommand]`; генератор исходного кода превращает его в [[IViewModel]].

## Зачем это нужно

ViewModel — это единица, которую разработчик на самом деле пишет: состояние и поведение на стороне представления для экрана, виджета или элемента списка. Она позволяет выразить, *что* именно UI предоставляет (поля, команды), без ручного написания механики уведомлений об изменениях, обёрток для свойств или поиска членов, к которым должен привязываться [[View]]. Генератор убирает этот шаблонный код, поэтому ViewModel остаётся обычным классом, сосредоточенным на логике.

## Как это работает

Вы помечаете тип атрибутом `ViewModelAttribute` (допустим на `class` или `struct`, `Inherited = false`). Внутри него вы аннотируете члены:

- `[Bind]` на поле — генератор создаёт привязываемое свойство. Режим по умолчанию — `BindMode.TwoWay` для изменяемых полей и `BindMode.OneTime` для полей `readonly`; аргумент режима переопределяет это (см. [[BindMode]]). `[Bind]` наследуется от `BaseBindAttribute`; родственные сокращения — `[OneWayBind]`, `[TwoWayBind]`, `[OneTimeBind]`, `[OneWayToSourceBind]`.
- `[RelayCommand]` на методе — генератор создаёт соответствующее свойство [[IRelayCommand]] (обобщённая перегрузка выбирается по количеству параметров). Его поле `CanExecute` указывает имя метода-условия, возвращающего `bool`. `AllowMultiple = true`.
- `[BindAlso("PropertyName")]` — также вызывает событие изменения другого сгенерированного свойства, когда изменяется это поле (требует сопутствующего атрибута привязки).
- `[Access(...)]` — переопределяет модификатор `private`, заданный по умолчанию для аксессоров сгенерированного свойства.

Поскольку генератор создаёт *вторую* partial-часть типа, класс **должен быть `partial`** (см. [[Must Be Partial]]). Сгенерированная часть реализует [[IViewModel]] — главным образом `FindBindableMember(...)`, рантайм-хук, который View использует для поиска членов. Механика этой генерации описана в [[ViewModel Generation]] и [[ViewModel to Generated Code]].

`ViewModelExtensions.DisposeViewModel(...)` предоставляет вспомогательный метод жизненного цикла: он освобождает ViewModel, если она реализует `IDisposable`, обёрнутый в маркер профилировщика. *(Вывод: освобождение является опциональным — `[ViewModel]` сам по себе не реализует `IDisposable`.)*

## Ключевые связи

- Реализует [[IViewModel]] (контракт) через сгенерированный код.
- Члены `[Bind]` проявляются как [[Bindable Members]], потребляемые через [[Data Binding]].
- Методы `[RelayCommand]` становятся [[Relay Commands]] / [[IRelayCommand]].
- Привязывается к UI с помощью [[View]] во время [[View Initialization]] / [[Runtime Binding Resolution]].
- Готовые предустановленные ViewModel находятся в [[StarterKit ViewModels]].

## Исходники

- `Source/ViewModels/Generation/ViewModelAttribute.cs` — маркер.
- `Source/ViewModels/Generation/` — `Bind`, `RelayCommand`, `BindAlso`, `Access`, сокращения режимов.
- `Source/ViewModels/IViewModel.cs`, `Extensions/ViewModelExtensions.cs`.
