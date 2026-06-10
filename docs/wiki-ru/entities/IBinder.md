---
title: IBinder
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/ViewBinder.cs
tags: [binder, contract, lifecycle, core]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/entities/IBinder.md
translated_at: 2026-05-31
---

# IBinder

> Контракт, который реализует каждый binder: он умеет подключаться к bindable-члену [[ViewModel]], получать значения и отключаться — это провод между UI слоя View и состоянием ViewModel.

## Зачем это нужно

[[ViewModel]] предоставляет типизированные [[Bindable Members]], но ничего не знает об UI. Binder — это адаптер, который подписывается на один из таких членов и передаёт его значение куда-либо (в `Text`, `Slider`, другой View). `IBinder` — это минимальный контракт, позволяющий фреймворку единообразно управлять этим соединением, **без рефлексии** — [[Source Generator]] связывает конкретные binder'ы с членами по типу.

## Как это работает

`IBinder` определяет только жизненный цикл:

- `Mode` — режим [[BindMode]] (по умолчанию `OneWay`), задающий направление потока данных.
- `Bind(IBinderAdder)` — регистрирует binder в ViewModel; adder возвращает `IBinderRemover` для отключения.
- `Unbind()` — выполняет отключение.

Передача значений надстраивается поверх через обобщённые под-интерфейсы:

- `IBinder<in T>.SetValue(T?)` — получает значение **от** ViewModel (OneWay).
- `IReverseBinder<out T>` — предоставляет событие `ValueChanged`, чтобы передавать значения **обратно** в ViewModel (по умолчанию `Mode` равен `TwoWay`). См. [[Data Binding]] и заметки про `OneWayToSource` в [[BindMode]].

`Binder` (`[Serializable]`, abstract) — это стандартная базовая реализация. Он владеет сериализуемым полем `_mode`, отслеживает `IsBound`, защищает от двойного связывания (логирует ошибку в Unity, выбрасывает исключение в остальных случаях), учитывает переопределяемый барьер `IsBind` и предоставляет hook-методы `OnBinding`/`OnBound`/`OnUnbinding`/`OnUnbound` для подклассов. Он также подключает маркеры Unity Profiler и partial-хуки отладки (`OnBoundDebug`/`OnUnboundDebug`). Конкретные binder'ы реализуют `IBinder<T>` и выполняют свою работу внутри `SetValue`.

В каталоге `Impls/` находятся два примечательных базовых подкласса:

- `TargetBinder<TTarget>` — добавляет сериализуемую ссылку `Target` (с проверкой на null), с которой работают производные binder'ы. Большинство binder'ов из StarterKit (например, [[Text Binders]], [[Slider Binders]]) наследуются от него. См. [[Binder Base Classes]].
- `ViewBinder : IBinder<IViewModel?>` — в `SetValue` деинициализирует свой `IView`, а затем повторно инициализирует его с входящей ViewModel (или просто деинициализирует, если значение null). Именно так [[View]] вкладывает другую View, привязанную к дочерней ViewModel; связано с [[View Initialization]].

## Ключевые связи

- Получает значения от [[Bindable Members]], сгенерированных для полей с `[Bind]` (см. [[ViewModel to Generated Code]]).
- Регистрируется и разрешается во время выполнения через [[Runtime Binding Resolution]].
- Семейства UI-компонентов смотрите в [[Binders Catalog]] и [[Mono Binders]].
- Reverse-binder'ы передают данные обратно в ViewModel; команды привязываются отдельно через [[IRelayCommand]].

## Источник

- `Source/Binders/IBinder.cs` — `IBinder`, `IBinder<T>`
- `Source/Binders/IReverseBinder.cs` — контракт обратной привязки
- `Source/Binders/Binder.cs` — базовый жизненный цикл
- `Source/Binders/Impls/TargetBinder.cs`, `Impls/ViewBinder.cs`
