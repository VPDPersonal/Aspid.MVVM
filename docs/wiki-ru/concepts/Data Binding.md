---
title: Привязка данных
type: concept
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IAnyBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/IAnyReverseBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Binder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/TargetBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/Binders/Impls/ViewBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderAdder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/IBinderRemover.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/OneWayBindableMember.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/Source/BindableMembers/Classes/TwoWayBindableMember.cs
tags: [binding, binder, bindable-member, lifecycle, concept]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/concepts/Data Binding.md
translated_at: 2026-05-31
---

# Привязка данных

> Как один элемент View подключается к одному члену ViewModel, кто и в какую сторону передаёт значения, и когда подписка разрывается.

## Зачем это нужно

Смысл MVVM в том, что View никогда не обращается напрямую к ViewModel и наоборот. *Binder* — это адаптер «один к одному», который закрывает этот разрыв: он владеет ссылкой на UI-элемент и знает, как скопировать значение в него (или из него), оставаясь при этом независимым от конкретной ViewModel. Благодаря этому привязки работают без рефлексии, с минимальными аллокациями и со статической типизацией.

## Как это работает

Binder реализует [[IBinder]]. Прямой поток (от ViewModel к View) обеспечивается методом `IBinder<T>.SetValue(T)`; обратный поток (от View к ViewModel) — событием `IReverseBinder<T>.ValueChanged`. `IAnyBinder` / `IAnyReverseBinder` — это нетипизированные «аварийные люки» (их контракт по значению проверяется только во время выполнения).

Каждый binder сообщает свой `Mode` ([[BindMode]]): по умолчанию `OneWay` для прямых binder-ов и `TwoWay` для обратных.

Точкой подключения на стороне ViewModel служит [[Bindable Members|bindable-член]] — сгенерированное поле, представленное как `IBinderAdder`. Привязка проходит через небольшой жизненный цикл в [[Binder Base Classes|Binder]]:

1. `Bind(IBinderAdder)` защищает от двойной привязки с помощью флага `IsBind` и вызывает `OnBinding()`.
2. `binderAdder.Add(this)` немедленно передаёт текущее значение, затем подписывает binder на событие `Changed` члена (а для `TwoWay`/`OneWayToSource` подписывает член на событие `ValueChanged` binder-а). Возвращает `IBinderRemover`.
3. `Unbind()` вызывает сохранённый remover, отсоединяя все подписки, после чего вызывает `OnUnbound()`.

`OneTime`-binder-ы получают значение однократно, и `Add` возвращает `null` — отписываться не от чего.

Здесь поставляются два готовых binder-а: [[Binder Base Classes|TargetBinder<TTarget>]] предоставляет сериализуемое поле `Target`, чтобы подклассы привязывались к конкретному объекту, а `ViewBinder` — это `IBinder<IViewModel?>`, который (де)инициализирует вложенный [[View]] при появлении дочерней ViewModel, обеспечивая вложенные представления.

## Ключевые связи

- Какой тип члена принимает какой режим: `OneWayBindableMember` отклоняет всё, кроме `OneWay`/`OneTime`; `TwoWayBindableMember` обрабатывает все четыре направления.
- Члены и их создание на основе `[Bind]`: см. [[Bindable Members]] и [[ViewModel to Generated Code]].
- Конкретные binder-ы описаны в [[Binders Catalog]] (например, [[Text Binders]], [[Slider Binders]]); команды используют [[Relay Commands]].
- Как `View` обнаруживает и вызывает `Bind` при запуске: [[Runtime Binding Resolution]] и [[View Initialization]].

## Исходный код

`Source/Binders/` (интерфейсы + `Binder`, `TargetBinder`, `ViewBinder`) и `Source/BindableMembers/` (`IBinderAdder`/`IBinderRemover`, классы `*BindableMember` для каждого режима).
