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

* **Unity 6000.0.53f1** или новее.
* Git для установки UPM-пакетов по Git URL.

## Установка

### Минимальная установка из исходников

Описанная ниже схема необязательных зависимостей относится к ещё не выпущенным исходникам 1.1. Старые preview-теги сохраняют свои требования к зависимостям.

1. Создайте чистый проект Unity 6000.0.53f1 или новее.
2. Откройте **Window → Package Manager → + → Install package from disk**.
3. Выберите `Aspid.MVVM/Packages/tech.aspid.mvvm/package.json` в рабочей копии исходников.

Пакет появится в `Packages/tech.aspid.mvvm/`. Aspid.FastTools обязателен. До его установки runtime, Editor, samples и tests MVVM исключены из компиляции. После установки FastTools базовые сборки работают без Collections и UI-пакетов. Сохраняйте нужные вашему проекту модули Unity: биндер удалённого модуля недоступен до его повторной установки.

Сначала установите FastTools: `https://github.com/VPDPersonal/Aspid.FastTools.git#upm-preview/1.0.0-rc.9`. Текущая Git-поставка не устанавливает его автоматически.

### Необязательные интеграции

| Возможность | Что установить в Package Manager |
| --- | --- |
| Counter и остальные примеры uGUI/TMP | `com.unity.ugui` 2.0.0 или совместимую с редактором более новую версию |
| Биндеры UI Toolkit | `com.unity.modules.uielements` |
| Инструменты Inspector | FastTools обязателен; инструменты Editor также используют UI Toolkit и IMGUI |
| Observable-коллекции | `https://github.com/VPDPersonal/Aspid.Collections.git#upm/1.1.1` |
| VirtualizedList | Collections и uGUI |
| AI, animation, audio, particles, physics, physics2d, video | Соответствующий модуль `com.unity.modules.*` |

Localization, Addressables, VContainer и Zenject остаются необязательными. Collections 1.1.1 не устанавливает Test Framework и NUnit в runtime-проект. В старых версиях Collections такая зависимость есть: сначала обновите пакет, затем удаляйте ненужные тестовые зависимости.

### Counter

Для Counter дополнительно установите `com.unity.inputsystem` 1.20.0 и включите **Active Input Handling → Input System Package (New)** или **Both**. Импортируйте **Path 1. Counter** из вкладки **Samples** пакета и **TMP Essential Resources**, если появится запрос. Откройте импортированную сцену Counter и включите Play mode. Два способа инициализации разобраны в [туториале Counter](../../docusaurus-plugin-content-docs-tutorials/current/01.%20Counter/README.md).

### Опубликованные версии

Preview-канал: `https://github.com/VPDPersonal/Aspid.MVVM.git#upm-preview`. Используйте инструкцию именно установленного релиза: опубликованный тег не содержит ещё не выпущенные изменения исходников. Версии Asset Store импортируются через **Package Manager → My Assets**; старый путь `Assets/Aspid/MVVM/` не является обязательным.

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
        _counterView.DeinitializeViewSafely()?.DisposeViewModel();
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