---
title: Биндеры Scrollbar
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/Command/ScrollbarCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/Command/Mono/ScrollBarCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Scrollbars/OneWayToSource/Mono/ScrollbarsToSourceMonoBinder.cs
tags: [binders, starterkit, scrollbar, command, ugui]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Scrollbar Binders.md
translated_at: 2026-05-31
---

# Биндеры Scrollbar

> Биндеры из StarterKit, связывающие uGUI-компонент `Scrollbar` с ViewModel: вызывают [[IRelayCommand]] при каждом перетаскивании или передают сам компонент в ViewModel.

## Что связывается

Это семейство работает с `UnityEngine.UI.Scrollbar`. В отличие от большинства семейств StarterKit, набор Scrollbar **не имеет варианта-сеттера значения** (нет биндера, который записывает `float` в скроллбар). Вместо этого он **ориентирован на команды**: каждый биндер слушает `Scrollbar.onValueChanged` и выполняет связанную команду, передавая текущее значение `Scrollbar.value`. Таким образом, Scrollbar здесь рассматривается как жест ввода, а не как отображаемое значение.

## Таблица семейства

| Вариант | Класс | Базовый класс | Связывает | Направление |
|---|---|---|---|---|
| Command (Plain) | `ScrollbarCommandBinder` (+ `<T>`, `<T1,T2>`, `<T1,T2,T3>`) | `TargetBinder<Scrollbar>` | `IRelayCommand<...>` | значение -> команда |
| Command (Mono) | `ScrollBarCommandMonoBinder` (+ обобщённые арности, abstract) | `ComponentMonoBinder<Scrollbar>` | `IRelayCommand<...>` | значение -> команда |
| OneWayToSource (Mono) | `ScrollbarsToSourceMonoBinder` | `ComponentToSourceMonoBinder<Scrollbar>` | `Scrollbar` | биндер -> ViewModel |

## Оси вариантов

- **Plain vs Mono** — Plain (`ScrollbarCommandBinder`) помечен `[Serializable]` и создаётся в коде с явным `Scrollbar target`. Mono — это `MonoBehaviour`, размещаемый на сцене (`[AddComponentMenu]`, `[AddBinderContextMenu]`), который получает свой target через `CachedComponent`. См. [[Mono Binders]] и [[Binder Base Classes]].
- **Арность команды** — обобщённые формы `<T>`, `<T1,T2>`, `<T1,T2,T3>` передают 1–3 дополнительных сериализуемых значения `Param` вместе со значением скроллбара. Обобщённые Mono-варианты являются `abstract` (требуется закрытый подкласс для сериализации конкретных `T`); обобщённые Plain-варианты являются конкретными.
- **Числовая перегрузка** — каждый биндер реализует четыре интерфейса `IBinder<IRelayCommand<...>>` (`int`, `long`, `float`, `double`); та команда, которая была связана, получает значение, приведённое к её типу. При установке нескольких побеждает `float`.
- **OneWayToSource** — `ScrollbarsToSourceMonoBinder` передаёт ссылку на компонент в ViewModel (см. [[BindMode]] `OneWayToSource`).
- **Switcher / Enum / EnumGroup** — для Scrollbar их нет (в этой папке таких файлов нет); эти оси применимы к семействам, ориентированным на отображение, например [[Toggle Binders]] / [[Image Binders]].

## Особенности поведения

- **CanExecute -> интерактивность.** `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) отражает результат `CanExecute` команды, переключая `Target.interactable`, `gameObject.SetActive` или `ICanExecuteView`. Режим `Custom` требует перегрузки конструктора с `ICanExecuteView`.
- **TwoWay отклоняется.** Конструкторы Plain вызывают `mode.ThrowExceptionIfTwo()`.
- **Чистая деинициализация.** `OnUnbound` удаляет слушатель `onValueChanged` и обнуляет каждую команду, отписываясь от каждого `CanExecuteChanged`.

Это биндеры, написанные вручную. Потребляемый ими `IRelayCommand`, сгенерированный из `[RelayCommand]`, приходит со стороны ViewModel — см. [[Relay Commands]] и [[ViewModel to Generated Code]].

## Исходный код

`StarterKit/Unity/Runtime/Binders/Scrollbars/` — см. `source_paths`. Связанное: [[Slider Binders]], [[Scrollrect Binders]], [[Button Binders]], [[Binders Catalog]].
