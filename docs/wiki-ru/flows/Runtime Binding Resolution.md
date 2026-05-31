---
title: Разрешение привязок во время выполнения
type: flow
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/IViewModel.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberParameters.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/ViewModels/FindBindableMemberResult.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderAdder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderRemover.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IReadOnlyBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/OneWayBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/TwoWayBindableMember.cs
tags: [flow, binding, runtime, viewmodel, binder]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/flows/Runtime Binding Resolution.md
translated_at: 2026-05-31
---

# Разрешение привязок во время выполнения

> Как binder во время выполнения находит целевой член ViewModel и начинает передавать значения — без рефлексии, только сгенерированный поиск плюс подписка на делегаты.

Это тот самый момент, когда привязка действительно происходит: [[View]] передаёт ViewModel своим binder'ам, каждый binder запрашивает свой член по id, а соответствующий [[Bindable Members|bindable-член]] подключает делегаты `Action<T>` в соответствии с [[BindMode]]. См. [[View Initialization]], чтобы понять, что запускает шаг 1.

## Пошаговый разбор

1. **Стартует инициализация View.** Когда View инициализируется с ViewModel (см. [[View Initialization]]), он перебирает свои binder'ы и для каждого вызывает `FindBindableMember` у [[IViewModel]], передавая `FindBindableMemberParameters`, который несёт лишь `Id` члена (строку).

2. **ViewModel разрешает член.** `IViewModel.FindBindableMember` **генерируется** из полей `[Bind]` (невидим в написанном вручную коде; см. [[ViewModel to Generated Code]]). Он возвращает `FindBindableMemberResult` — `IsFound` равно `true` только тогда, когда его `Adder` (объект `IBinderAdder`) не равен null. Промах даёт пустой результат, и binder остаётся непривязанным.

3. **Binder привязывается через adder.** `Binder.Bind(IBinderAdder)` защищается от двойной привязки и от `IsBind == false`, вызывает хук `OnBinding`, а затем вызывает `adder.Add(this)`. Возвращённый `IBinderRemover` сохраняется для последующего отключения; `IsBound` становится true, и срабатывает `OnBound`.

4. **Adder подписывается в соответствии с BindMode.** `IBinderAdder` — это сам bindable-член ([[Bindable Members|OneWayBindableMember]], [[Bindable Members|TwoWayBindableMember]] и т. д.). `Add` читает `binder.Mode` и подключает делегаты соответствующим образом:
   - **OneTime** — однократно проталкивает текущее значение через `SetValue`, затем возвращает `null` (нет подписки, нечего удалять).
   - **OneWay** — `SetValue(current)` плюс `Changed += binder.SetValue` для будущих обновлений.
   - **OneWayToSource** — подписывает член на событие `ValueChanged` binder'а (IReverseBinder); View проталкивает значения только в ViewModel.
   - **TwoWay** — оба варианта выше.
   `OneWayBindableMember` принимает только OneWay/OneTime (остальные бросают исключение); `TwoWayBindableMember` поддерживает все режимы, кроме `None`.

5. **Значения текут.** Прямое направление: установка `Value` члена вызывает `Changed`, что приводит к вызову каждого подписанного `SetValue`. Обратное направление: binder вызывает `ValueChanged`, член выполняет своё действие `setValue`, записывая значение обратно в поле ViewModel. Binder'ы, реализующие `IAnyBinder`/`IAnyReverseBinder`, используют нетипизированные перегрузки с `object`; член приводит значение к `T`.

6. **Отвязка.** При деинициализации View `Binder.Unbind` вызывает `Remove` у сохранённого remover'а, отсоединяя те же делегаты, а затем сбрасывает `IsBound`. У OneTime-binder'ов remover отсутствует, поэтому ничего не происходит.

## Источники

- [[IViewModel]], `FindBindableMemberResult` — точка входа разрешения
- [[IBinder]], `Binder` — жизненный цикл bind/unbind ([[Binder Base Classes]])
- `OneWayBindableMember` / `TwoWayBindableMember` — подписка для каждого режима ([[Bindable Members]])
- Связано: [[Data Binding]], [[BindMode]]
