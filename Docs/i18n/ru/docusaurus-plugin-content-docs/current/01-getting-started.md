# Быстрый старт

Пошаговое руководство по началу работы с Aspid.MVVM: от установки до первого работающего примера.

## Содержание

* [Требования](#требования)

* [Установка](#установка)

* [Обучающий маршрут](#обучающий-маршрут)

* [Первый пример: Counter](#первый-пример-counter)

* [Как это работает](#как-это-работает)

* [Следующие шаги](#следующие-шаги)

***

## Требования

* **Unity 2022.3** или новее

* **.NET Standard 2.0** (целевая платформа фреймворка)

* Поддержка **Source Generators** в Unity (встроена с 2022.3+)

## Установка

### Из Unity Asset Store

1. Откройте [страницу Aspid.MVVM](https://assetstore.unity.com/packages/slug/298463) в Unity Asset Store

2. Импортируйте пакет в ваш проект

3. Убедитесь, что папка `Assets/Aspid/MVVM/` создана

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

| # | Пример | Новое | Туториал |
| - | ------ | ----- | -------- |
| 1 | **Counter** | `[ViewModel]`, `[Bind]`, `[RelayCommand]`, `ViewInitializer` | [Counter](../../docusaurus-plugin-content-docs-tutorials/current/01.%20Counter/README.md) |
| 2 | **Greeter** | `MonoViewModel`, `[TwoWayBind]`, `[BindAlso]`, `On*Changed` | [Greeter](../../docusaurus-plugin-content-docs-tutorials/current/02.%20Greeter/README.md) |
| 3 | **Bind Modes** | четыре режима на одном экране, свой `ITwoWayConverter` | [Bind Modes](../../docusaurus-plugin-content-docs-tutorials/current/03.%20BindModes/README.md) |
| 4 | **Stats** | команды с параметром, `CanExecute`, черновик → модель | [Stats](../../docusaurus-plugin-content-docs-tutorials/current/04.%20Stats/README.md) |
| 5 | **Todo List** | модель, `ObservableList`, `CreateSync`, коллекционные биндеры | [Todo List](../../docusaurus-plugin-content-docs-tutorials/current/05.%20TodoList/README.md) |
| 6 | **Custom Binder** | биндер для своего компонента, `[GenerateSerializableBinder]` | [Custom Binder](../../docusaurus-plugin-content-docs-tutorials/current/06.%20CustomBinder/README.md) |

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