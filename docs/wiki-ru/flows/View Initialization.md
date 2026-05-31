---
title: Инициализация View
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/IView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Extensions/ViewExtensions.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/ViewAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Views/Generation/AsBinderAttribute.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Views/MonoView.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Views/MonoView.Instantiate.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/Binders/MonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Unity/Runtime/ViewModels/MonoViewModel.cs
tags: [flow, view, initialization, binding, lifecycle]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/flows/View Initialization.md
translated_at: 2026-05-31
---

# Инициализация View

> Как View подключает свои binder'ы к ViewModel — рантайм-рукопожатие, превращающее сериализованные списки binder'ов в живой двусторонний поток данных.

[[View]] бездействует, пока ему не передан [[IViewModel]]. Инициализация — это момент, когда binder'ы находят свой целевой bindable-член по имени и подписываются на него. Поскольку `[View]` генерируется исходным кодом, точка входа (`Initialize`) не видна в написанном вручную коде; вы видите только хуки.

## Пошаговый разбор

1. **Триггер.** Что-то поставляет ViewModel — напрямую через `view.Initialize(viewModel)`, через одну из статических перегрузок `MonoView.Instantiate<T>(...)` (создание экземпляра + инициализация одним вызовом) или через `IView.Reinitialize` посредством `ViewExtensions`. Контейнеры [[DI Integration]] делают тот же самый вызов.

2. **Сгенерированный `Initialize`.** Генератор `[View]` создаёт реализацию [[View|IView]]: он сохраняет `viewModel` в свойство `ViewModel` и вызывает частичный хук `OnInitializingInternal(viewModel)`. (Сгенерированное тело также подключает любые автоматически сгенерированные поля binder'ов, когда `AutoBinderFields` равно true — см. [[View]] и [[Source Generation Pipeline]].)

3. **Хук разветвляется по группам binder'ов.** В `MonoView` написанный вручную `OnInitializingInternal` проходит в цикле по сериализованному `_bindersList`. Каждая группа `Binders` сопоставляет имя члена `_name` (идентификатор binder'а) с массивом компонентов `MonoBinder`, настроенных в Inspector.

4. **Разрешение bindable-члена.** Каждая группа вызывает `viewModel.FindBindableMember(new FindBindableMemberParameters(_name))`. Если член не найден, группа молча пропускается — ошибочно написанный идентификатор просто ничего не делает. См. [[Runtime Binding Resolution]] и [[Bindable Members]].

5. **Привязка каждого MonoBinder.** При совпадении `_monoBinders.BindSafely(result, owner, _name)` вызывает каждый binder. `MonoBinder.Bind` пропускается, если `IsBound` (с записью ошибки в лог) или `!IsBind`, вызывает `OnBinding`, регистрирует себя через `IBinderAdder.Add` члена (сохраняя возвращённый `IBinderRemover`), устанавливает `IsBound`, затем вызывает `OnBound`. Направление определяет [[BindMode]] (по умолчанию `TwoWay`).

6. **Начальная отправка / подтягивание из источника.** Bindable-член отправляет своё текущее значение в каждый только что добавленный binder, поэтому UI сразу отражает состояние. (Это ответственность самого члена; см. [[Bindable Members]].)

## Освобождение (обратный путь)

`Deinitialize` (сгенерированный) вызывает `OnDeinitializingInternal`, который проходит по группам, вызывая `UnbindSafely`; каждый `MonoBinder.Unbind` выполняет `OnUnbinding`, вызывает сохранённый `IBinderRemover.Remove`, сбрасывает `IsBound`, затем `OnUnbound` и обнуляет `ViewModel`. `MonoView.OnDestroy` → `Dispose` → `Deinitialize`, поэтому уничтожение GameObject автоматически отсоединяет binder'ы. `ViewExtensions.DisposeView` предпочитает `IDisposable` обычной деинициализации.

## Заметки

- `MonoViewModel` сам по себе является `MonoBehaviour`; его `OnValidate` вызывает сгенерированный `NotifyAll()`, чтобы перерисовать связанные редакторы.
- Члены [[IRelayCommand]] привязываются через тот же путь `FindBindableMember`, что и члены-данные.

См. также: [[Data Binding]], [[Binder Base Classes]], [[ViewModel to Generated Code]].
