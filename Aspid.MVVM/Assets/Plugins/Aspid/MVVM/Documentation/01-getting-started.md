---
type: "note"
---
# Быстрый старт

Пошаговое руководство по началу работы с Aspid.MVVM: от установки до первого работающего примера.

## Содержание

* [Требования](#%D1%82%D1%80%D0%B5%D0%B1%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F)

* [Установка](#%D1%83%D1%81%D1%82%D0%B0%D0%BD%D0%BE%D0%B2%D0%BA%D0%B0)

* [Обучающий маршрут](#%D0%BE%D0%B1%D1%83%D1%87%D0%B0%D1%8E%D1%89%D0%B8%D0%B9-%D0%BC%D0%B0%D1%80%D1%88%D1%80%D1%83%D1%82)

* [Первый пример: Counter](#%D0%BF%D0%B5%D1%80%D0%B2%D1%8B%D0%B9-%D0%BF%D1%80%D0%B8%D0%BC%D0%B5%D1%80-counter)

* [Как это работает](#%D0%BA%D0%B0%D0%BA-%D1%8D%D1%82%D0%BE-%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%B0%D0%B5%D1%82)

* [Следующие шаги](#%D1%81%D0%BB%D0%B5%D0%B4%D1%83%D1%8E%D1%89%D0%B8%D0%B5-%D1%88%D0%B0%D0%B3%D0%B8)

***

## Требования

* **Unity 2022.3** или новее

* **.NET Standard 2.0** (целевая платформа фреймворка)

* Поддержка **Source Generators** в Unity (встроена с 2022.3+)

## Установка

### Из Unity Asset Store

1. Откройте [страницу Aspid.MVVM](https://assetstore.unity.com/packages/slug/298463) в Unity Asset Store

2. Импортируйте пакет в ваш проект

3. Убедитесь, что папка `Assets/Plugins/Aspid/MVVM/` создана

### Из исходного кода

```bash
git clone https://github.com/VPDPersonal/Aspid.MVVM.git
cd Aspid.MVVM
git submodule update --init --recursive
```

> **Важно:** Проект использует 5 git-подмодулей. Без `git submodule update --init --recursive` код не скомпилируется.

***

## Обучающий маршрут

Каждый шаг добавляет ровно одну новую концепцию:

| # | Пример       | Новое                                                 | Туториал |
| - | ------------ | ----------------------------------------------------- | -------- |
| 1 | **Counter**  | `[ViewModel]`, `[OneWayBind]`, `[RelayCommand]`       | Туториал |
| 2 | **Greeter**  | `[TwoWayBind]`, реактивность через `On*Changed`       | Туториал |
| 3 | **TodoItem** | Модель, `IDisposable`, отписка от событий             | Туториал |
| 4 | **TodoList** | `ObservableList`, `CreateSync`, коллекционные биндеры | Туториал |

***

## Первый пример: Counter

Кнопка увеличивает счётчик, число отображается в тексте. Это минимальный пример, демонстрирующий три ключевые концепции фреймворка.

### Шаг 1: ViewModel

ViewModel содержит данные и логику. Source Generator генерирует весь код привязки автоматически.

```csharp
using Aspid.MVVM;

// [ViewModel] — маркер для Source Generator.
// Класс обязательно должен быть partial.
[ViewModel]
public sealed partial class CounterViewModel
{
    // [OneWayBind] — данные идут только из ViewModel во View (только чтение для UI).
    // Source Generator создаст свойство Count, метод SetCount и событие CountChanged.
    [OneWayBind] private int _count;

    // [RelayCommand] — Source Generator создаст свойство IncrementCommand типа IRelayCommand.
    [RelayCommand]
    private void Increment() => SetCount(Count + 1);
}
```

**Что генерирует Source Generator из этого кода:**

| Исходный код                      | Генерируется                                                    |
| --------------------------------- | --------------------------------------------------------------- |
| `[ViewModel]` на классе           | Реализация `IViewModel`, метод `FindBindableMember`             |
| `[OneWayBind] int _count`         | Свойство `Count`, метод `SetCount(int)`, событие `CountChanged` |
| `[RelayCommand] void Increment()` | Свойство `IncrementCommand` типа `IRelayCommand`                |

### Шаг 2: View

View описывает, какие биндеры и к каким данным ViewModel подключить.

```csharp
using UnityEngine;
using Aspid.MVVM;

// [View] — маркер для Source Generator.
// Класс обязательно должен быть partial.
[View]
public sealed partial class CounterView : MonoView
{
    // Имя поля _count совпадает с именем поля в ViewModel.
    // Source Generator связывает их автоматически по имени.
    [SerializeField] private MonoBinder _count;

    // Массив биндеров — удобно когда несколько UI-элементов выполняют одно действие.
    [SerializeField] private MonoBinder[] _increment;
}
```

> **Правило именования:** Имя поля View (без префикса `_`, `m_`, `s_`) должно совпадать с именем поля ViewModel. `_count` → привязывается к `Count`.

### Шаг 3: Bootstrap

Bootstrap соединяет View и ViewModel:

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private CounterView _counterView;

    private void Awake()
    {
        var viewModel = new CounterViewModel();
        _counterView.Initialize(viewModel);
    }

    private void OnDestroy()
    {
        _counterView.DeinitializeView()?.DisposeViewModel();
    }
}
```

### Шаг 4: Настройка в Inspector

1. Создайте GameObject с компонентом `CounterView`

2. Добавьте дочерний объект с компонентом `TextMonoBinder` — перетащите в поле `_count`

3. Добавьте дочерний объект с `Button` и компонентом `ButtonCommandMonoBinder` — перетащите в массив `_increment`

4. На Bootstrap-объекте назначьте ссылку на `CounterView`

***

## Как это работает

После вызова `view.Initialize(viewModel)`:

1. **View** перебирает биндеры и вызывает `viewModel.FindBindableMember(id)` для каждого

2. **ViewModel** (сгенерированный код) находит нужный `BindableMember` по ID без рефлексии

3. **Binder** регистрируется и получает текущее значение

4. При изменении `Count` через `SetCount()` — биндер автоматически обновляет `Text`

5. При нажатии кнопки — `ButtonCommandMonoBinder` вызывает `IncrementCommand.Execute()`

```text
ViewModel ──► BindableMember ──► Binder ──► UI
               (без рефлексии, прямые вызовы)
```

> Source Generator создаёт прямые вызовы на этапе компиляции — без рефлексии и без аллокаций.

***

## Следующие шаги

* Counter — полный туториал с деталями по биндерам

* Greeter — двусторонний биндинг: InputField → Text в реальном времени

* TodoItem — отдельная Model, IDisposable, несколько типов биндинга

* [Архитектура](02-architecture.md) — детальное описание конвейера привязки

* [Режимы привязки](03-binding-modes.md) — OneWay, TwoWay, OneTime, OneWayToSource

* StarterKit — все готовые биндеры для Unity UI

⠀