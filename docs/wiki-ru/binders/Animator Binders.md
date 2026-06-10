---
title: Биндеры Animator
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetParameterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetBoolBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/AnimatorSetTriggerBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/Mono/AnimatorSetBoolMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Animators/Mono/AnimatorToSourceMonoBinder.cs
tags:
  - binder
  - animator
  - starterkit
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Animator Binders.md
translated_at: 2026-05-31
---

# Биндеры Animator

> Управляйте параметрами Unity `Animator` (bool/float/int) и триггерами на основе состояния ViewModel либо передавайте ссылку на `Animator` обратно во ViewModel.

## Зачем это нужно

Распространённая задача в UI — передать состояние ViewModel в `Animator`: переключить bool «isOpen», задать float «speed» или сработать триггером «play» — без написания связующего кода в MonoBehaviour. Это семейство превращает вызовы `Animator.SetBool/SetFloat/SetInteger/SetTrigger` в декларативные [[Bindable Members|привязки]].

## Семейство

| Биндер | Вызов Animator | Примечания |
|---|---|---|
| `AnimatorSetBoolBinder` | `SetBool` | опциональный `_isInvert` |
| `AnimatorSetFloatBinder` | `SetFloat` | |
| `AnimatorSetIntBinder` | `SetInteger` | |
| `AnimatorSetParameterBinder<T>` | (абстрактная база) | общая логика для трёх перечисленных выше |
| `AnimatorSetTriggerBinder` | `SetTrigger` | только `OneWayToSource` |
| `AnimatorToSourceMonoBinder` | — | передаёт `Animator` во ViewModel |

## Как это работает

`AnimatorSetParameterBinder<T>` — это общая база, [[Binder Base Classes|TargetBinder]]`<Animator>`, несущий сериализованное `ParameterName`. Конкретные подклассы переопределяют `SetParameter(T)`, чтобы выполнить фактический вызов `Animator` (вариант для bool сначала сравнивает значение с `GetBool`, чтобы пропустить избыточные установки). `CanExecute` по умолчанию равен `Target.gameObject.activeInHierarchy`, ограничивая каждую установку.

`AnimatorSetTriggerBinder` не наследуется от базы параметров — триггеры не несут значения, поэтому это обычный `TargetBinder<Animator>`, предоставляющий `SetTrigger` без параметров.

## Оси вариантов

- **Plain против Mono.** Plain-биндеры (`AnimatorSetBoolBinder`) — это `[Serializable]`-поля, размещённые внутри представления; родственные `Mono`-варианты (`AnimatorSetBoolMonoBinder`) — это MonoBehaviour с `[AddComponentMenu]`/`[AddBinderContextMenu]`, которые читают компонент через `CachedComponent` вместо переданного через конструктор `Animator`. См. [[Mono Binders]].
- **OneWayToSource.** Через `[BindModeOverride]` база параметров допускает `OneWay`, `OneTime` и `OneWayToSource`; `TwoWay` отклоняется при создании. В режиме `OneWayToSource` биндер передаёт `SetParameter` (или `SetTrigger`) во ViewModel либо как обычный `Action<T>`/`Action`, либо как [[IRelayCommand]]`<T>`, чей `CanExecute` отражает ограничение биндера (это [[Data Binding|IReverseBinder]]). Биндер триггера работает только в режиме `OneWayToSource`.
- **ToSource-компонент.** `AnimatorToSourceMonoBinder` — это `ComponentToSourceMonoBinder<Animator>`: он передаёт саму ссылку на `Animator`, а не параметр.

Вывод: в этом семействе нет варианта Switcher или Enum/EnumGroup (в отличие от некоторых других категорий биндеров); существуют только четыре вида параметров плюс source-биндер.

## Исходный код

`StarterKit/Unity/Runtime/Binders/Animators/` (plain) и его подпапка `Mono/`. Часть [[Binders Catalog]]; см. [[BindMode]] для семантики режимов.
