---
title: Биндеры кнопок
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/Command/ButtonCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/Command/Mono/ButtonCommandMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Buttons/OneWayToSource/Mono/ButtonToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Commands/CanExecuteView/InteractableMode.cs
tags: [binder, starterkit, button, command, ui]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Button Binders.md
translated_at: 2026-05-31
---

# Биндеры кнопок

> Связывают Unity UI `Button` с [[IRelayCommand]], чтобы клик выполнял команду ViewModel — а состояние доступности/видимости кнопки следовало за `CanExecute` команды.

В отличие от большинства биндеров, которые передают цели *значение*, семейство Button связывает *поведение*: оно перенаправляет `Button.onClick` в команду и отражает возможность её выполнения обратно на кнопку. Это канонический способ вызывать [[Relay Commands]] из UI.

## Семейство

| Вариант | Базовый класс | Что связывает | Особенности |
|---|---|---|---|
| `ButtonCommandBinder` / `ButtonCommandBinder<T..T4>` | [[Binder Base Classes\|TargetBinder<Button>]] | `IRelayCommand`, `IRelayCommand<T..T4>` | Обычный сериализуемый биндер; конструктор принимает `Button` напрямую |
| `ButtonCommandMonoBinder` | [[Mono Binders\|ComponentMonoBinder<Button>]] | `IRelayCommand` **и** `IRelayCommand<bool>` | Компонент на том же GameObject; перегрузка с bool получает `true` при клике |
| `ButtonCommandMonoBinder<T..T4>` | `ComponentMonoBinder<Button>` | `IRelayCommand<T..T4>` | **abstract** — для использования нужен конкретный sealed-подкласс |
| `ButtonToSourceMonoBinder` | [[Mono Binders\|ComponentToSourceMonoBinder<Button>]] | (отправляет сам `Button`) | OneWayToSource: передаёт ссылку на компонент наверх во ViewModel |

## Оси вариаций

- **Plain против Mono.** Обычный `ButtonCommandBinder` — это `[Serializable]`-поле, которому вы передаёте `Button` (используется внутри [[View]] или собирается в коде). `…MonoBinder` — это MonoBehaviour, который вы помещаете на GameObject кнопки; он автоматически получает свой `CachedComponent` и поставляется с записями `[AddComponentMenu]`.
- **Арность команды (0–4 параметра).** У каждого варианта есть неgeneric-форма плюс `<T>`, `<T1,T2>`, `<T1,T2,T3>`, `<T1,T2,T4>`. Generic-формы предоставляют сериализованные поля `Param1…Param4`, которые передаются в `Execute(...)` при клике.
- **OneWayToSource.** `ButtonToSourceMonoBinder` работает в обратном направлении: вместо получения команды он передаёт ссылку на `Button` во ViewModel через `ComponentToSourceMonoBinder<Button>` (см. [[BindMode]] `OneWayToSource`).

> Варианты Switcher и Enum / EnumGroup существуют для других семейств Selectable (см. [[Toggle Binders]] / [[Dropdown Binders]]); сама папка Button поставляет только перечисленные выше варианты Command и OneWayToSource.

## Примечательное поведение

- **`CanExecute` → interactable.** Значение `InteractableMode` (`None`, `Visible`, `Interactable`, `Custom`) определяет, как отражается `CanExecuteChanged`: переключение `Button.interactable`, переключение `gameObject.SetActive` или делегирование в `[SerializeReference] ICanExecuteView` (`Custom`). По умолчанию — `Interactable`.
- **Нет TwoWay.** Конструкторы вызывают `mode.ThrowExceptionIfTwo()` — `BindMode.TwoWay` отвергается.
- **Жизненный цикл.** `OnBound` подписывается на `onClick`; `OnUnbound` удаляет слушателя и передаёт `null` в `SetValue`, чтобы отсоединить команду и отписаться от `CanExecuteChanged` (через `CommandBinderExtensions.UpdateCommand`). Generic-варианты Mono являются абстрактными, поэтому сгенерированная исходным кодом обвязка [[View]] нацеливается на ваш конкретный подкласс.

## Исходный код

`StarterKit/Unity/Runtime/Binders/Buttons/` — `Command/ButtonCommandBinder.cs`, `Command/Mono/ButtonCommandMonoBinder.cs`, `OneWayToSource/Mono/ButtonToSourceMonoBinder.cs`. Связанное: [[IRelayCommand]], [[IBinder]], [[Binder Base Classes]], [[Selectable Binders]], [[Binders Catalog]].
