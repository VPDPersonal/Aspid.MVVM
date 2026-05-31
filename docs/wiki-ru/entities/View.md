---
title: View
type: entity
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/IView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/ViewAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/AsBinderAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Extensions/ViewExtensions.cs
tags: [view, mvvm, source-generation, binding]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/entities/View.md
translated_at: 2026-05-31
---

# View

> Презентационная сторона MVVM: тип, который принимает [[IViewModel]] и связывает его bindable-члены с UI-биндерами. Помечается `[View]`, и большая часть его обвязки генерируется исходным генератором.

## Зачем это нужно

ViewModel предоставляет состояние и команды, но ничего не знает об UI. View выступает мостом: он получает [[IViewModel|ViewModel]], соединяет каждый [[Bindable Members|bindable-член]] с одним или несколькими экземплярами [[IBinder]] и разрывает эти связи при деинициализации. Писать такую обвязку вручную для каждого экрана — занятие повторяющееся и подверженное ошибкам, поэтому Aspid генерирует её из атрибута `[View]` — оставляя объявление View сосредоточенным на том, *какие* биндеры он содержит, а не на том, *как* они связываются.

## Как это работает

`IView` задаёт контракт: nullable-свойство `ViewModel`, а также `Initialize(IViewModel)` и `Deinitialize()`. `IView<in T>` добавляет строго типизированную перегрузку `Initialize(T)` для конкретного типа ViewModel.

Вы пишете `partial`-класс (или структуру), помеченный атрибутом `[ViewAttribute|[View]]`. Источниковый генератор (это [[Committed DLLs|закоммиченная DLL]], а не исходный код в этом репозитории) формирует реализацию `IView` / `IView<T>`: поле-хранилище `ViewModel`, тела `Initialize`/`Deinitialize` и вызовы привязки, которые подключают каждый bindable-член к его биндеру.

`[AsBinderAttribute|[AsBinder]]` помечает поле или свойство типа `[View]`. Он указывает тип [[IBinder]] (плюс необязательные `arguments` конструктора), который генератор должен использовать для привязки этого члена. Ссылка на `Type` сохраняется только под `UNITY_EDITOR || DEBUG`.

`ViewAttribute.AutoBinderFields` (по умолчанию `true`) указывает генератору также формировать поля-биндеры для любых bindable-членов `IView<TViewModel>`, ещё не объявленных на View — это удобно для разметки, управляемой через инспектор. Установите его в `false` для View, которые связывают биндеры вручную.

`ViewExtensions` добавляет вспомогательные методы жизненного цикла поверх любого `IView`: `Reinitialize` (деинициализация, затем при необходимости инициализация с новым ViewModel), `DeinitializeView` (деинициализация и возврат прежнего ViewModel) и `DisposeView` (освобождение `IDisposable`-View, иначе деинициализация). Все они возвращают ранее связанный ViewModel. Освобождение оборачивается в Unity `ProfilerMarker`, когда профилирование включено.

## Ключевые связи

- Потребляет [[IViewModel]], передаваемый в `Initialize`.
- Привязывает члены к экземплярам [[IBinder]]; см. [[Runtime Binding Resolution]] и [[View Initialization]].
- `[View]` обрабатывается [[Source Generator|генератором исходного кода]]; см. [[ViewModel to Generated Code]].
- Должен быть `partial` — см. [[Must Be Partial]].
- Конкретные UI-биндеры находятся в [[Binders Catalog]].

## Исходники

- `Source/Views/IView.cs`
- `Source/Views/Generation/ViewAttribute.cs`
- `Source/Views/Generation/AsBinderAttribute.cs`
- `Source/Views/Extensions/ViewExtensions.cs`
