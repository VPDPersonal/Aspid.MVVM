---
title: Биндеры Toggle
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/IsOn/ToggleIsOnBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/IsOn/Mono/ToggleIsOnMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/Command/ToggleCommandBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Toggles/OneWayToSource/Mono/ToggleToSourceMonoBinder.cs
tags: [binder, starterkit, ui, toggle, command]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Toggle Binders.md
translated_at: 2026-05-31
---

# Биндеры Toggle

> Биндеры StarterKit, которые связывают Unity UI `Toggle` с булевыми значениями и командами ViewModel, нацеливаясь на `Toggle.isOn` и `Toggle.onValueChanged`.

## Семейство

| Вариант | Связываемый тип | Режим по умолчанию | Цель |
|---|---|---|---|
| `ToggleIsOnBinder` / `ToggleIsOnMonoBinder` | `bool` | `TwoWay` | `Toggle.isOn` |
| `ToggleCommandBinder` (+ обобщённые арности) / `ToggleCommandMonoBinder` | `IRelayCommand`, `IRelayCommand<bool[, T...]>` | `OneWay` | `onValueChanged` -> `Execute` |
| `ToggleToSourceMonoBinder` | (сериализуемое свойство тоггла) | one-way-to-source | `Toggle.isOn` -> источник |

## Оси вариаций

**Plain против Mono.** Каждый биндер существует как сериализуемый обычный класс, наследуемый от [[Binder Base Classes|TargetBinder<Toggle>]] (создаётся в коде), и — для IsOn/Command — как `partial` MonoBehaviour, наследуемый от `ComponentMonoBinder<Toggle>` для связывания через инспектор. Mono-варианты несут `[AddComponentMenu]` / `[AddBinderContextMenu]` и являются `partial`-половиной, потребляемой [[Mono Binders]]; вторая половина генерируется. OneWayToSource поставляется **только** как Mono-биндер (`ComponentToSourceMonoBinder<Toggle>`).

**IsOn.** По умолчанию двусторонний: реализует `IBinder<bool>` и `IReverseBinder<bool>`, поэтому тоггл одновременно отражает и сообщает связанное значение. Флаг `_isInvert` инвертирует логическое значение в каждом направлении. Защита `_isNotifyValueChanged` подавляет обратный отклик, когда [[Data Binding|SetValue]] записывает `isOn`, предотвращая петлю обратной связи к источнику.

**Command.** Подписывается на `onValueChanged` и запускает связанную [[Relay Commands|команду]] при каждом изменении. Перегрузки принимают `IRelayCommand` (без параметров) или `IRelayCommand<bool>` (получает `isOn`); обобщённые `ToggleCommandBinder<T>`, `<T1,T2>`, `<T1,T2,T3>` передают дополнительные сериализуемые значения `Param`. `InteractableMode` (`Interactable` / `Visible` / `Custom` / `None`) отражает `CanExecute` на тоггл; `Custom` управляет `ICanExecuteView`.

## Примечательное поведение

- **Ограничения режима (выведены из защит).** IsOn отклоняет [[BindMode|None]]. Command отклоняет `TwoWay` (`ThrowExceptionIfTwo`). `[BindModeOverride(IsAll = true)]` на IsOn позволяет инспектору выбрать любой режим.
- **Инициализация OneWayToSource.** Когда IsOn связывается в режиме `OneWayToSource`, `OnBound` немедленно проталкивает текущее значение `isOn` в источник.
- **Очистка.** `OnUnbound` удаляет слушатель `onValueChanged`; Command дополнительно обнуляет каждую ссылку на команду, отсоединяя `CanExecuteChanged`.

> Примечание: эта папка содержит только IsOn, Command и OneWayToSource — здесь нет вариантов Switcher или Enum/EnumGroup для тогглов.

## Источник

`StarterKit/Unity/Runtime/Binders/Toggles/` — `IsOn/`, `Command/`, `OneWayToSource/` (каждая со вложенной подпапкой `Mono/`). См. [[Binders Catalog]], [[IBinder]], [[IRelayCommand]].
