---
title: Начало работы
type: overview
status: active
source_paths:
  - Readme.md
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/CounterViewModel.cs"
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/CounterView.cs"
  - "Aspid.MVVM/Assets/Aspid/MVVM/Samples/01. Counter/Scripts/Bootstrap.cs"
tags: [overview, onboarding, install, sample]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/overview/Getting Started.md
translated_at: 2026-05-31
---

# Начало работы

> Установите Aspid.MVVM, затем свяжите свой первый сгенерированный исходным генератором ViewModel с View и binder'ами — без рефлексии и без шаблонного кода.

## Установка

Aspid.MVVM поставляется как UPM-пакет для Unity 2022.3+. Генератор и анализатор закоммичены [[Committed DLLs]] и потребляются Unity напрямую, но сам *репозиторий* зависит от трёх git-[[Submodule Init|сабмодулей]]. Клонируйте так:

```bash
git submodule update --init --recurse
```

Без сабмодулей Unity не скомпилируется. Observable-коллекции берутся из [[External Dependencies|внешнего UPM-пакета]] (`tech.aspid.collections`), а не из этого репозитория.

## Ваш первый ViewModel

ViewModel — это обычный `partial`-класс без базового класса. Атрибут `[ViewModel]` запускает [[Source Generation Pipeline]] (см. [[ViewModel to Generated Code]]):

```csharp
[ViewModel]
[Serializable]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;

    [RelayCommand] private void Increment() => Count++;
    [RelayCommand] private void Reset()     => Count = 0;
}
```

Генератор создаёт то, что вы никогда не пишете вручную: публичное свойство `Count` поверх `_count` (см. [[Bindable Members]]), реализацию [[IViewModel]], а также `IncrementCommand` / `ResetCommand` типа [[IRelayCommand]] (см. [[Relay Commands]]). Атрибут `[Bind]` может принимать [[BindMode]], чтобы ограничить допустимые направления [[Data Binding]].

> Класс **должен быть `partial`** — генератор создаёт вторую partial-половину. См. [[Must Be Partial]].

## Ваш первый View

View — это `partial`-[[View|MonoView]], помеченный атрибутом `[View]`. Он предоставляет поля `MonoBinder`, которые заполняются в инспекторе; `[RequireBinder]` ограничивает связываемый тип:

```csharp
[View]
public sealed partial class CounterView : MonoView
{
    [RequireBinder(typeof(int))]            [SerializeField] private MonoBinder[] _count;
    [RequireBinder(typeof(IRelayCommand))]  [SerializeField] private MonoBinder _incrementCommand;
}
```

Каждый [[IBinder]] (из [[Binders Catalog]]) связывает bindable-член с Unity-компонентом (Text, Button и т. д.).

## Связываем всё вместе

Создайте ViewModel и передайте его View. Инициализация разрешает привязки во время выполнения ([[Runtime Binding Resolution]], [[View Initialization]]):

```csharp
private void Awake() => _counterView.Initialize(new CounterViewModel());
private void OnDestroy() => _counterView.DeinitializeView()?.DisposeViewModel();
```

О связывании через контейнер вместо ручного `new` см. [[DI Integration]].

## Дальнейшие шаги

- [[Samples|01. Counter]] — полный минимальный пример выше (ViewModel + View + Bootstrap).
- HelloWorld и другие [[Samples]] — постепенно усложняющиеся сцены.
- [[Architecture]] — как View, ViewModel и binder'ы складываются воедино.

## Источники

- `Samples/01. Counter/Scripts/` — `CounterViewModel.cs`, `CounterView.cs`, `Bootstrap.cs`
- `Readme.md` — обзор возможностей и ссылки на установку
