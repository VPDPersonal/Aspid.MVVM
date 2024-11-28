# Aspid.UI

## Content
* [Introduce](#introduce)
* [Features](#features)
* [MVVM Theory](#mvvm-theory)
* [Introduction to Aspid.UI.MVVM API](#introduction-to-aspiduimvvm-api)
  * [Hello World Example](#hello-world-example)
    * [Step 1 - Model](#step-1---model)
    * [Step 2 - View Model](#step-2---viewmodel)
    * [Step 3 - View](#step-3---view)
    * [Step 4 - Bootstrap](#step-4---bootstrap)
    * [Step 5 - Setup Scene](#step-5---setup-scene)
  * [ViewModel](#viewmodel)
    * [\[ViewModel\]](#viewmodel-1)
  * [Binding properties](#binding-properties)
    * [\[Bind\]](#bind)
    * [\[ReadOnlyBind\]](#readonlybind)
    * [\[BindAlso\]](#bindalso)
    * [\[Access\]](#access)
  * [Commands](#commands)
    * [\[RelayCommand\]](#relaycommand)
  * [View](#view)
    * [\[View\]](#view-1)
    * [ViewContent](#view-content)
    * [\[RequireBinder\]](#requirebinder)
  * [Binder](#binder)
    * [Reverse](#reverse)
    * [\[BinderLog\]](#binderlog)
  * [\[CreateFrom\]](#createfrom)
  * [Advanced](#advanced)
    * [Advanced ViewModel](#advanced-viewmodel)
* [StartedKit](#starterkit)
  * [Binders](#binders)
  * [ViewInitializer](#viewintializer)
* [Debug](#debug)
* [Roadmap](#roadmap)

## Introduce
***

**Aspid.UI** — это комплексный набор инструментов для разработки UI в Unity, который включает в себя MVVM Framework и
(в будущем) UI-компоненты (компоненты находятся в разработке).

**Aspid.UI.MVVM** — это MVVM (Model-View-ViewModel) фреймворк, созданный для упрощения разделения логики игры и
пользовательского интерфейса. Он помогает создавать архитектуру с чётким разграничением между моделями данных и UI,
что способствует гибкости, масштабируемости и улучшает тестируемость проекта.

**О текущей версии:** Версия 0.0.1 является ранней версией фреймворка, и его API может изменяться по мере развития до
выпуска стабильного релиза. Основные концепции фреймворка останутся неизменными, однако рекомендуется избегать ручной
привязки данных и использовать генерацию исходного кода, чтобы минимизировать влияние изменений в API.

## Features
***
* **MVVM Framework**
    * **Несколько режимов привязки**: Поддерживаются режимы привязки данных — [OneWay, TwoWay, OneTime, OneWayToSource](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.bindingmode?view=windowsdesktop-8.0#fields).
    * **Оптимизация**: Привязка данных без использования boxing/unboxing и Reflection, что улучшает производительность.
    * **[RelayCommand](#commands)**: Команды, поддерживающие до 4 параметров.
    * **[View](#view)**
      * **Базовые реализации**: Реализации View для MonoBehaviour и чистого C#.
      * **[\[View\]](#view-1)**: Автоматическая генерация шаблонного кода для View с использованием атрибута.
      * **[\[AsBinder\]](#asbinder)**: Преобразование компонента в указанный Binder.
      * **[\[RequireBinder\]](#requirebinder)**: Проверка соответствия Binder заданному типу.
      * **Отладка ViewModel**: Возможность отладки ViewModel через инспектор (для MonoView).
      * **Отладка**: Визуальные подсказки об ошибках в View (для MonoView).
      * **Отслеживание всех Binder’ов**: Просмотр всех связанных Binder’ов через инспектор (для MonoView).
    * **[ViewModel](#viewmodel)**
      * **Базовые реализации**: Реализации ViewModel для MonoBehaviour и чистого C#.
      * **[\[ViewModel\]](#viewmodel-1)** Автоматическая генерация шаблонного кода для ViewModel с использованием атрибута.
      * **[\[Bind\]](#bind)**: Автоматическая генерация свойства привязки для поля.
      * **[\[ReadOnlyBind\]](#readonlybind)**: Автоматическая генерация свойства привязки только для чтения для поля.
      * **[\[Access\]](#access)**: Настройка уровня доступа для сгенерированных свойств.
      * **[\[BindAlso\]](#bindalso)**: Связывание свойства только для чтения.
      * **[\[RelayCommand\]](#relaycommand)**: Генерация команд на основе методов с использованием атрибута
    * **[Binder](#binder)**
      * **Базовые реализации** Реализации Binder для MonoBehaviour и чистого C#.
      * **[\[BinderLog\]](#binderlog)** Генерирует код логирования на установку значения.
      * **Универсальные Binder’ы**: Возможность связывания разных типов данных через единый Binder.
      * **Возможность обратного связывания разных типов через одни Binder**.
      * **Отладка Binder’ов**: Визуальные подсказки об ошибках в Binder’ах (для MonoBinder).
      * **Просмотр логов**: Отображение логов привязок через инспектор.
    * **StarterKit**
        * **[ViewInitializer](#viewintializer)**: Упрощённая инициализация View через инспектор.
        * **[Набор Binder’ов](#binders)**: Стартовый комплект всех необходимых Binder’ов для проекта.
        * **[Набор MonoBinder’ов](#binders)**: Компоненты Unity для удобного использования Binder’ов.
        * **Конвертеры**: Преобразование типов данных или изменение текущего типа.
    * **Поддержка DI**: Интеграция с Dependency Injection-фреймворками VContainer и Zenject.
* **[\[CreateFrom\]](#createfrom)**: Генерация метода создания объекта на основе другого объекта
(в будущих версиях будет вынесен в отдельный пакет [UnityFastTools](https://github.com/VPDPersonal/UnityFastTools)).
* **UI компоненты** (в разработке)

## MVVM Theory
***

В шаблоне MVVM есть три основных компонента: Model, View и ModelView. Каждый служит отдельной целью. На схеме ниже
показаны связи между тремя компонентами.

```
┌───────────┐ Data Binding ┌───────────┐   Updates    ┌───────────┐
│           │ and Commands │           │  the Model   │           │
│   View    ├─────────────►│ ViewModel ├─────────────►│   Model   │
│           │              │           │              │           │
└───────────┘              └───────────┘              └───────────┘
          ▲                 |        ▲                 |
          └─ ─ ─ ─ ─ ─ ─ ─ ─┘        └─ ─ ─ ─ ─ ─ ─ ─ ─┘
          Send Notifications          Send Notifications
```

Помимо понимания обязанностей каждого компонента, важно также понять, как они взаимодействуют.
На высоком уровне View знает о ViewModel, и ViewModel знает о Model, но Model не знает о ViewModel,
и ViewModel не знает о View. Поэтому ViewModel изолирует View от Model и позволяет Model развиваться независимо от View.

Преимущества использования шаблона MVVM приведены ниже:
* Если существующая реализация Model инкапсулирует существующую бизнес-логику, это может быть трудно или рискованно
  изменить ее. В этом сценарии ViewModel выступает в качестве адаптера для классов Model и не позволяет вносить основные
  изменения в код Model.
* Разработчики могут создавать модульные тесты для ViewModel и Model без использования View.
  Модульные тесты для ViewModel могут выполнять точно те же функции, что и в View.
* UI приложения можно изменить без касания ViewModel и кода Model.
  Поэтому новая версия View должна работать с существующей ViewModel.
* Конструкторы и разработчики могут работать независимо и параллельно с их компонентами во время разработки.
  конструкторы могут сосредоточиться на View, в то время как разработчики могут работать над ViewModel и компонентами Model.

Информация взята с https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm.

## Introduction to Aspid.UI.MVVM API
***

В папке Assets/Samples/Aspid/UI находятся примеры, которые демонстрируют практическое использование фреймворка Aspid.UI.MVVM.
Эти примеры помогут лучше понять, как работать с API:
* **HelloWorld**: Пример базового использования MVVM-фреймворка. Демонстрирует создание простого взаимодействия между View, ViewModel и Model.
* **Stats**: Показывает, как можно использовать несколько представлений (View) для одной модели данных (Model). Включает управление состоянием View через ViewModel.
* **TodoList**: Демонстрирует привязку коллекции данных и взаимодействие между элементами UI и ViewModel.
* **ExampleScripts**: Пример, показывающий различные возможности API.

### Hello World Example
***

Пример HelloWorld — это простой способ продемонстрировать основной принцип работы Aspid.UI.MVVM,
используя ключевые компоненты: Model, ViewModel, View и Binder. В этом разделе показан упрощённый вариант примера,
доступного в Samples/Aspid/UI/HelloWorld.

#### Step 1 - Model
***

Начнем с создания простой модели, которая будет содержать текст, метод для его изменения и событие,
уведомляющее об изменении текста. Модель хранит данные и управляет их изменениями, уведомляя ViewModel через событие,
когда данные обновляются.

```csharp
using System;

public class Speaker
{
    public event Action TextChanged;
    
    private string _text;
    
    public string Text
    {
        get => _text;
        set 
        {
            _text = value;
            TextChanged?.Invoke();
        }
    }
    
    public void Say(string text) => Text = text;
}
```

**Объяснение:**
* Событие TextChanged: Это событие будет вызвано каждый раз, когда текст изменится.
Оно позволит ViewModel отслеживать изменения и обновлять интерфейс.
* Свойство Text: Позволяет получать и устанавливать текст. При изменении текста вызывается событие TextChanged.
* Метод Say: Это простой метод для изменения текста. Внутри него вызывается установка нового значения через свойство Text,
что автоматически уведомляет всех подписчиков об изменении.

#### Step 2 - ViewModel
***

Теперь создадим ViewModel, которая будет выступать посредником между Model и View.
ViewModel будет получать текст из модели для отображения и позволит вводить новый текст, который затем обновит модель.

```csharp
using Aspid.UI.MVVM.ViewModels.Generation;

[ViewModel]
public partial class SpeakerViewModel : IDisposable
{
    // Поля для привязки с View.
    // _text привязан к тексту из Model, а _inputText используется только в ViewModel для ввода текста.
    // Для полей можно использовать имена которые имеют префикс "m_" или "_", а так же которые не имеют префикса
    // и начинаются с маленькой буквы.
    // Пример: _text; m_text; text;
    [Bind] private string _text;
    [Bind] private string _inputText;
    
    private readonly Speaker _speaker;
    
    // Конструктор получает экземпляр модели и настраивает ViewModel.
    public SpeakerViewModel(Speaker speaker)
    {
        _speaker = speaker;
        
        // Инициализация текста из Model.
        _text = speaker.Text;
        speaker.TextChanged += OnTextChanged;
    }
    
    // Команда, которая обновляет текст в модели на основе значения из InputText (данные от View).
    [RelayCommand]
    private void Say() => _speaker.Say(InputText);
    
    // Метод, который вызывается, когда текст в модели меняется.
    // Синхронизирует данные между Model и ViewModel.
    private void OnTextChanged() => Text = _speaker.Text;
    
    public void Dispose() => _speaker.TextChanged -= OnTextChanged;
}
```

**Объяснение:**
* ViewModel как посредник: ViewModel выступает связующим звеном между Model и View, обеспечивая двухстороннее взаимодействие.
* Привязки (\[Bind\]):
* _text — это привязка к тексту из Model, который будет отображаться в View.
* _inputText — привязка для текста, который вводится в View и не используется в Model напрямую. Это данные только для ViewModel.
* Команда (\[RelayCommand\]): Команда Say вызывает метод модели Say,
передавая ей введённый текст из View через свойство InputText, которое генерируется автоматически.
* Событие TextChanged: Когда текст в модели меняется, вызывается метод OnTextChanged,
который обновляет свойство Text в ViewModel, синхронизируя данные между Model и ViewModel.

#### Step 3 - View
***

Создадим View, которое будет отображать данные из ViewModel и обеспечивать взаимодействие пользователя с приложением.
View в основном отвечает за привязку UI-компонентов (например, текстовых полей и кнопок) к свойствам и командам из ViewModel.

```csharp
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

[View]
public partial class SpeakerView : MonoView
{
    // Компоненты для отображения текста из ViewModel (связывается с полем _text).
    // MonoBinder связывает компонент с указанным типом, в данном случае это строка.
    // RequireBinder гарантирует, что компонент сможет корректно работать с типом string.
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _text;
    
    // Компонент для ввода текста (связывается с полем _inputText).
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _inputText;
    
    // Кнопка, связанная с командой Say, которая отправляет текст в модель.
    [SerializeField] private ButtonCommandBinder[] _sayCommand;
}
```

**Объяснение:**
* Привязка UI-компонентов:
  * _text: Этот массив содержит элементы UI (например, текстовые поля или другие визуальные компоненты),
  которые будут отображать текст, привязанный к ViewModel через свойство Text.
  * _inputText: Этот элемент UI (например, текстовое поле) принимает ввод от пользователя и связывается с полем InputText в ViewModel,
  позволяя пользователю вводить текст, который потом отправляется в модель.
  * _sayCommand: Эти кнопки привязаны к команде Say в ViewModel и позволяют пользователю инициировать действие
  (в данном случае, обновление текста в модели).
* RequireBinder: RequireBinder используется для того, чтобы гарантировать, что привязанные компоненты могут работать с
определённым типом данных (в данном случае, строкой). Это помогает предотвратить ошибки привязки на этапе выполнения.

**Как это работает:**
* Отображение данных: UI-компоненты, указанные в _text, привязаны к свойству Text из ViewModel,
и они будут автоматически обновляться при изменении текста в модели.
* Ввод данных: Компонент _inputText позволяет пользователю вводить новый текст,
который автоматически передаётся в ViewModel и сохраняется в поле InputText.
* Исполнение команды: Кнопка, привязанная к _sayCommand, вызывает команду Say,
которая обновляет текст в модели на основе значения, введённого пользователем.

**Обратите внимание:**
* Имена полей во View должны точно совпадать с именами привязанных полей в ViewModel.
Это необходимо для того, чтобы привязки корректно работали и данные синхронизировались между View и ViewModel.
* Для полей команд во View следует использовать имя, состоящее из названия метода в ViewModel и суффикса Command.
Например, если в ViewModel есть метод Say, команда в View должна называться SayCommand.

#### Step 4 - Bootstrap
***

Чтобы связать все три компонента вместе, мы воспользуемся ручным способом инициализации. 
Это даёт больше контроля над процессом.

```csharp
using UnityEngine;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

public sealed class Bootstrap : MonoBehaviour
{
    // Ссылка на View, которая будет инициализирована
    [SerializeField] private SpeakerView _speakerView;
    
    private Speaker _speaker;
    
    private void Awake()
    {
        _speaker = new Speaker();
        
        // Создаем ViewModel и передаем в неё модель
        var viewModel = new SpeakerViewModel(_speaker);
        
        // Инициализируем View, связывая её с ViewModel
        _speakerView.Initialize(viewModel);
    }
    
    // Упрощённый способ деинициализации View и освобождения ViewModel
    // с помощью методов расширения.
    // Простая реализация выглядит так:
    // var viewModel = _speakerView.ViewModel;
    // _speakerView.Deinitialize();
    // if (viewModel is IDisposable disposable) 
    //     disposable.Dispose();
    private void OnDestroy() =>
        _speakerView.DeinitializeView()?.DisposeViewModel();
}
```

**Объяснение:**
* **Инициализация:**
  * Мы создаём экземпляр Model (Speaker) и ViewModel (SpeakerViewModel), передавая модель в конструктор ViewModel.
  * Метод Initialize(viewModel) привязывает View к созданной ViewModel, что позволяет автоматически синхронизировать данные между ними.
* **Деинициализация:**
  * При уничтожении объекта (например, при выгрузке сцены) мы вызываем метод DeinitializeView() для безопасного освобождения ресурсов, связанных с View.
  * Метод DisposeViewModel() используется для освобождения ресурсов ViewModel, если она реализует интерфейс IDisposable.

#### Step 5 - Setup Scene
***

Теперь, когда весь код готов, осталось настроить сцену в Unity,
чтобы связать визуальные элементы с нашей Model, ViewModel и View.

* **Создайте новую сцену:**
  * В Unity создайте новую сцену для вашего проекта.
* **Создайте Canvas:**
  * На сцене создайте Canvas. Это будет контейнер для всех UI-элементов.
* **Создайте объект для View:**
  * Добавьте на Canvas пустой объект и назовите его Speaker View.
  * К объекту Speaker View добавьте компонент SpeakerView (который был создан ранее).
* **Добавьте UI-элементы:**
  * Создайте три дочерних объекта для Speaker View:
    * Text: Этот объект будет отображать текст из модели.
      * Добавьте компонент TextMeshProUGUI для отображения текста.
      * Добавьте компонент TextBinder и свяжите его с объектом Speaker View:
        * В поле View установите ссылку на Speaker View.
        * В поле ID введите "Text", чтобы привязать к тексту из ViewModel.
    * InputText: Поле ввода текста.
      * Добавьте компонент TMP_InputField для ввода текста пользователем.
      * Добавьте компонент InputFieldBinder и свяжите его с объектом Speaker View:
        * В поле View установите ссылку на Speaker View.
        * В поле ID введите "InputText", чтобы привязать к полю ввода текста в ViewModel.
    * SayButton: Кнопка для отправки текста.
      * Добавьте компонент Button для создания кнопки.
      * В поле SpeakerView добавьте кнопку в массив Say Command, чтобы связать её с командой Say из ViewModel.
* **Добавьте объект Bootstrap:**
  * Добавьте на сцену пустой объект и назовите его Bootstrap.
  * Прикрепите к нему компонент Bootstrap (который был написан ранее).
  * В поле Speaker View у Bootstrap перетащите объект Speaker View, чтобы связать его с ViewModel.
* **Запустите проект:**
  * Теперь можно запускать проект. Вы увидите, что:
    * В Text отображается текущий текст из Model.
    * При вводе текста в InputText и нажатии на кнопку SayButton, текст в Model обновляется на введённый.

### ViewModel
***

Каждая ViewModel должна реализовывать интерфейс "**IViewModel**", чтобы можно было связать её с View.
Рекомендуется использовать кодогенерацию для реализации этого интерфейса, так как текущий API может измениться в будущем.
Кодогенерация уменьшает вероятность ошибок и избавляет от необходимости вручную писать шаблонный код.

Пример реализации ViewModel без кодогенерации:

```csharp
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;

public class SomeViewModel : IViewModel
{
    // Метод для привязки binder к свойству по имени propertyName
    public IRemoveBinderFromViewModel AddBinder(IBinder binder, string propertyName) 
    {
        // Реализация связывания "binder" со свойством "propertyName" 
    }
}
```
***

Если вы предпочитаете не использовать кодогенерацию, то вы можете использовать абстрактный класс **ViewModel**, который
добавляет только "**ProfilerMarker**" в метод "**AddBinder**".

```csharp
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;

public class SomeViewModel : ViewModel
{
    // Внутренняя реализация привязки binder к свойству с именем propertyName
    protected override IRemoveBinderFromViewModel AddBinderInternal(IBinder binder, string propertyName)
    {
        // Реализация связывания "binder" со свойством "propertyName"
    }
}
```

Для тех, кто предпочитает использовать Unity-специфический подход, существует класс "**MonoViewModel**",
который имеет реализацию как у "**ViewModel**", но наследует "**MonoBehaviour**".
Это удобно, если ваша ViewModel должна быть MonoBehaviour-объектом и взаимодействовать с Unity-объектами напрямую.

***
Более подробно у ручной реализации **IViewModel** можно посмотреть в разделе [Advance ViewModel](#advanced-viewmodel).

#### \[ViewModel\]
***

Рекомендуемый способ реализации интерфейса "**IViewModel**" — это использование кодогенерации.
Она помогает упростить разработку, минимизируя шаблонный код и снижает вероятность ошибок, особенно при изменениях API.

**Для того чтобы включить кодогенерацию, необходимо:**
* Пометить класс атрибутом "**\[ViewModel\]**".
* Сделать класс "**partial**", чтобы кодогенерация могла добавлять необходимую логику.

```csharp
using Aspid.UI.MVVM.ViewModels.Generation;

[ViewModel]
public partial class SomeViewModel { }
```

"**SomeViewModel**" может наследовать любой другой класс, включая "**ViewModel**" или "**MonoViewModel**",
хотя это не является обязательным. Выбор зависит от ваших нужд в архитектуре приложения.

**Атрибуты для генерации:** Атрибут **"\[ViewModel\]"** также является необходимым для следующих атрибутов:
* [\[Bind\]](#bind);
* [\[ReadOnlyBind\]](#readonlybind);
* [\[BindAlso\]](#bindalso);
* [\[Access\]](#access);
* [\[RelayCommand\]](#relaycommand).

**Наследование с генерацией:**

Если ваш класс наследуется от другого класса, который использует кодогенерацию,
вам также нужно пометить наследника атрибутом "**\[ViewModel\]**", если вы хотите в нем тоже использовать кодогенерацию.
Это обеспечит корректное функционирование механизма кодогенерации на всех уровнях наследования.

```csharp
using Aspid.UI.MVVM.ViewModels.Generation;

[ViewModel]
public partial class SomeViewModelBase { }

[ViewModel]
public partial class SomeViewModelChild : SomeViewModelBase { }
```

### Binding properties
***

**Binding properties** — это свойства, которые связываются с элементами интерфейса в View.
Они позволяют ViewModel обмениваться данными с View и управлять поведением UI компонентов.
Для упрощённой реализации таких свойств используется кодогенерация,
которая автоматически создаёт необходимые геттеры и сеттеры для привязки.

**Чтобы создать Binding property, нужно:**
* Пометить поле атрибутом "**\[Bind\]**" для создания свойства с возможностью записи и чтения,
или "**\[ReadOnlyBind\]**", если свойство должно быть доступно только для чтения.
* Убедиться, что класс помечен атрибутом "**\[ViewModel\]**", чтобы кодогенерация сработала.

#### \[Bind\]
***

Атрибут "**\[Bind\]**" используется для генерации свойства, которое поддерживает различные режимы привязки:
OneWay, TwoWay, OneTime, OneWayToSource. Это свойство связывает поле в ViewModel с компонентом во View.

**Основные моменты:**
* Использование поля для первичной инициализации: Поле, помеченное атрибутом "**Bind**",
можно использовать только для первичной инициализации в конструкторе.
Во всех остальных случаях необходимо использовать сгенерированное свойство, чтобы привязка корректно обновляла View.
* Защита от ошибок: Неправильное использование полей вместо сгенерированных свойств может привести к ошибкам привязки.
Специальный анализатор кода предупреждает об этом и помогает избежать таких ошибок.

```csharp
[ViewModel]
public partial class SomeViewModel
{
    [Bind] private string _text;

    public SomeViewModel()
    {
        // Первичная инициализация в конструкторе
        _text = string.Empty;
    }

    private void UpdateText(string text)
    {
        // Использование сгенерированного свойства
        Text = text;
    }
}
```

**Пример сгенерированного кода (упрощённое представление):**

```csharp
 // <auto-generated>
public partial class SomeViewModel
{ 
    public event Action<string> TextChanged; 
    
    public string Text 
    { 
        get => _text; 
        set => SetText(value); 
    }
    
    private void SetText(string value)
    { 
        if (EqualityComparer<string>.Default.Equals(_text, value)) return;
        
        // Частичный метод, вызывается перед изменением значения
        OnTextChanging(_text, value);
        _text = value; 
        
        // Частичный метод, вызывается после изменения значения
        OnTextChanged(value);     
        
        // Уведомление о том, что свойство изменилось
        TextChanged?.Invoke(_text);
    }
    
    partial void OnTextChanging(string oldValue, string newValue);
    partial void OnTextChanged(string newValue);
}
```

**Кастомизация:** Сгенерированные частичные методы "**OnTextChanging**" и "**OnTextChanged**" можно использовать
для добавления дополнительной логики при изменении значения свойства (например, для валидации или логирования).

#### \[ReadOnlyBind\]
***

Атрибут "**\[ReadOnlyBind\]**" генерирует свойство, которое доступно только для чтения и поддерживает только режим
привязки OneTime. Это означает, что значение привязывается один раз и не изменяется в дальнейшем через привязку.

**Основные моменты:**
* Только для чтения: Свойство, сгенерированное с помощью ReadOnlyBind, не может быть изменено напрямую через привязку.
Оно используется для отображения неизменяемых данных в View.
* Использование "**\[ReadOnlyBind\]**" более производительное при связывании, чем "**\[Bind\]**".

```csharp
[ViewModel]
public partial class SomeViewModel
{
    [ReadOnlyBind] private string _text;

    public SomeViewModel()
    {
        // Первичная инициализация
        _text = "Initial Text";
    }
}
```

**Пример сгенерированного кода:**

```csharp
// <auto-generated>
public partial class SomeViewModel
{
    public string Text => _text;
}
```

**Применение:**
* Используйте "**\[ReadOnlyBind\]**" для свойств, которые не изменяются после установки их значений.
* Это полезно для отображения постоянных данных, таких как заголовки, описания или значения,
которые не должны изменяться во время работы приложения.

#### \[BindAlso\]
***

Иногда требуется, чтобы изменение одного свойства автоматически обновляло другое свойство,
которое зависит от нескольких значений. Атрибут "**\[BindAlso\]**" генерирует дополнительную логику для таких зависимых свойств,
автоматически вызывая обновление привязки и уведомление об изменениях, даже если эти свойства напрямую не изменяются.

```csharp
// <auto-generated>
public partial class SomeViewModel
{
    [BindAlos(nameof(FullName))]
    [Bind] private string _firstName;
    
    [BindAlso(nameof(FullName))]
    [Bind] private string _lastName;
    
    private string FullName => $"{FirstName} {LastName}";
}
```

**Пример сгенерированного кода (упрощённое представление):**

```csharp
// <auto-generated>
public partial class SomeViewModel
{
    public event Action<string> FirstNameChanged;
    public event Action<string> LastNameChanged;
    public event Action<string> FirstFull
        
    private string FirstName 
    { 
        get => _firstName; 
        set => SetFirstName(value); 
    }
    
    private string LastName 
    { 
        get => _lastName; 
        set => SetLastName(value); 
    }
    
    private void SetFirstName(string value)
    { 
        if (EqualityComparer<string>.Default.Equals(_firstName, value)) return;
        
        OnFirstNameChanging(_firstName, value); 
        _firstName = value; 
        OnFirstNameChanged(value); 
        
        FirstNameChanged?.Invoke(_firstName);
        FullNameChanged?.Invoke(FullName);
    }
    
    private void SetLastName(string value)
    { 
        if (EqualityComparer<string>.Default.Equals(_lastName, value)) return;
        
        OnLastNameChanging(_lastName, value); 
        _lastName = value; 
        OnLastNameChanged(value); 
        
        LastNameChanged?.Invoke(_lastName);
        FullNameChanged?.Invoke(FullName);
    }
    
    partial void OnFirstNameChanging(string oldValue, string newValue);
    partial void OnFirstNameChanged(string newValue);
    
    partial void OnLastNameChanging(string oldValue, string newValue);
    partial void OnLastNameChanged(string newValue);
}
```

**Применение:**
* Используйте BindAlso, когда нужно автоматически обновлять одно свойство при изменении других.
* Это особенно полезно для сложных вычисляемых свойств, таких как отображение полного имени на основе имени и фамилии.

#### \[Access\]
***

Иногда требуется изменить уровень доступа для сгенерированных свойств, чтобы контролировать,
как и где они могут быть использованы. Атрибут Access позволяет указать желаемый уровень доступа для сгенерированного свойства.
Это может быть полезно для предоставления публичного доступа к свойству только для чтения.

```csharp
public partial class SomeViewModel
{
    // Свойство с приватным доступом
    [Bind] private string _text1;
    
    // Публичный доступ
    [Access(Access.Public)]
    [Bind] private string _text2;
    
    // Доступ для наследников
    [Access(Access.Protected)]
    [Bind] private string _text3;
    
    // Публичный геттер, сеттер для наследников
    [Access(Get = Public, Set = Access.Protected)]
    [Bind] private string _text4;
}
```

**Пример сгенерированного кода (упрощённое представление):**

```csharp
// <auto-generated>
public partial class SomeViewModel
{
    private string Text1
    {
        get => _text1;
        set => SetText1(value);
    }
    
    public string Text2
    {
        get => _text2;
        set => SetText2(value);
    }
    
    protected string Text3
    {
        get => _text3;
        set => SetText3(value);
    }
    
    public string Text4
    {
        get => _text4;
        protected set => SetText4(value);
    }
}
```

**Применение:**
* Используйте Access, чтобы контролировать уровень доступа для чтения и записи свойства.
* Подходит для случаев, когда нужно предоставить доступ только для чтения (например, публичный геттер).

### Commands
***

Команды представляют собой механизм обработки пользовательского ввода и являются основным способом взаимодействия между
View и ViewModel в паттерне MVVM. В Aspid.UI команды реализованы через механизм RelayCommand,
который предлагает пять перегрузок для работы с разным количеством параметров. Более подробно смотрите тут:
https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/commanding-overview?view=netframeworkdesktop-4.8#what-are-commands

```csharp
[ViewModel]
public partial class MyViewModel
{
    // Команды, связанные с действиями во ViewModel. Используем ReadOnlyBind для лучшей производительности, 
    // так как команды обычно не меняются после инициализации.
    [ReadOnlyBind] private readonly IRelayCommand _command1;
    [ReadOnlyBind] private readonly IRelayCommand<int> _command2;
    [ReadOnlyBind] private readonly IRelayCommand<int, int> _command3;
    [ReadOnlyBind] private readonly IRelayCommand<int, int, int> _command4;
    [ReadOnlyBind] private readonly IRelayCommand<int, int, int, int> _command5;
    
    public MyViewModel()
    {
        // Инициализация команд. Второй параметр у RelayCommand отвечает за проверку
        // возможности выполнения команды и является необязательным.
        _command1 = new RelayCommand(Do1, CanDo1);
        _command2 = new RelayCommand(Do2, CanDo2);
        _command3 = new RelayCommand(Do3, CanDo3);
        _command4 = new RelayCommand(Do4, CanDo4);
        _command5 = new RelayCommand(Do5, CanDo5);
    }
    
    // Методы, выполняемые командами.
    private void Do1() { }
    
    private void Do2(int value1) { }
    
    private void Do3(int value1, int value2) { }
    
    private void Do4(int value1, int value2, int value3) { }
    
    private void Do5(int value1, int value2, int value3, int value4) { }
    
    // Методы, проверяющие возможность выполнения команд.
    private bool CanDo1() => true;
    
    private bool CanDo2(int value1) => true;
    
    private bool CanDo3(int value1, int value2) => true;
    
    private bool CanDo4(int value1, int value2, int value3) => true;
    
    private bool CanDo5(int value1, int value2, int value3, int value4) => true;
}
```

**RelayCommand поддерживает несколько перегрузок:**
* **RelayCommand:** используется для команд без параметров.
* **RelayCommand<T>:** поддерживает один параметр.
* **RelayCommand<T1, T2>:** поддерживает два параметра.
* **RelayCommand<T1, T2, T3>:** поддерживает три параметра.
* **RelayCommand<T1, T2, T3, T4>:** поддерживает четыре параметра.
  
* **Как работают команды:**
* Команды можно связывать с различными элементами интерфейса, такими как кнопки. Когда пользователь взаимодействует с View,
команда передаёт действие в ViewModel, где вызывается соответствующий метод.
* Встроенная поддержка условий выполнения команд через методы, возвращающие "bool" значение (например, CanDo1),
позволяет контролировать активность элементов управления.

**Преимущества:**
* Инкапсуляция логики: Команды позволяют инкапсулировать логику выполнения и проверки, что улучшает читаемость и поддержку кода.
* Гибкость: "**RelayCommand**" предоставляет несколько перегрузок, поддерживающих до четырёх параметров,
что позволяет эффективно использовать команды с различными сценариями и требованиями к данным.

#### \[RelayCommand\]
***

Атрибут "**\[RelayCommand\]**" используется для автоматической генерации команд на основе методов в ViewModel.
Этот подход позволяет избежать написания шаблонного кода вручную, что упрощает разработку и снижает вероятность ошибок.

```csharp
[ViewModel]
public partial class MyViewModel
{
    private bool CanDo2 => true;
    
    // Генерация команды для метода без условий выполнения
    [RelayCommand]
    private void Do1() { }
    
    // Генерация команды с условием выполнения (CanExecute)
    [RelayCommand(CanExecute = nameof(CanDo2))]
    private void Do2() { }
    
    [RelayCommand(CanExecute = nameof(CanDo3))]
    private void Do3() { }
    
    [RelayCommand(CanExecute = nameof(CanDo4))] 
    private void Do4(int value) { }
    
    [RelayCommand(CanExecute = nameof(CanDo5))] 
    private void Do5(int value) { }
    
    private bool CanDo3() => true;
    
    private bool CanDo4(int value) => true;
    
    private bool CanDo5() => true;
}
```
**Что сгенерирует кодогенератор:**
На основе указанного кода, генератор создаст команды, которые будут автоматически инициализированы и связаны с методами,
описанными в ViewModel. **Ниже приведён пример упрощённого сгенерированного кода:**

```csharp
public partial class MyViewModel
{
    private RelayCommand _do1Command;
    private RelayCommand Do1Command => _do1Command ??= new(Do1);
        
    private RelayCommand _do2Command;
    private RelayCommand Do2Command => _do2Command ??= new(Do2, () => CanDo2);
        
    private RelayCommand _do3Command;
    private RelayCommand Do3Command => _do3Command ??= new(Do3, CanDo3);
        
    private RelayCommand<int> _do4Command;
    private RelayCommand<int> Do4Command => _do4Command ??= new(Do4, CanDo4);
        
    private RelayCommand<int> _do5Command;
    private RelayCommand<int> Do5Command => _do5Command ??= new(Do5, (_) => CanDo5());
}
```

**Описание:**
* Автоматическая генерация команд: Атрибут "**\[RelayCommand\]**" автоматически генерирует приватные команды.
* Параметр CanExecute: Это опциональный параметр, который задаёт метод проверки, может ли команда быть выполнена в данный момент.
Если метод проверки указан, генератор добавляет соответствующую логику.
* Команды с параметрами: Поддерживается генерация команд с параметрами. Например, команда Do4Command принимает один параметр типа int.

**Преимущества использования:**
* Меньше шаблонного кода: Генерация команд значительно упрощает код, устраняя необходимость вручную создавать объекты команд и методы проверки их выполнения.
* Поддержка CanExecute: Генерация автоматически учитывает логику выполнения команды через указание метода для проверки условий.
* Чистый и поддерживаемый код: Использование атрибута упрощает структуру ViewModel, делая её более понятной и лёгкой для сопровождения.

### View
Каждая View должна реализовывать интерфейс "**IView**", чтобы View могла быть связана с ViewModel.
Рекомендуется использовать кодогенерацию для реализации этого интерфейса, так как текущий API может измениться в будущем.
Кодогенерация уменьшает вероятность ошибок и избавляет от необходимости вручную писать шаблонный код.
Пример реализации View без кодогенерации:

```csharp
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;

public class SomeView : IView
{
    protected override void Initialize(IViewModel viewModel)
    {
        // Реализация связывание Binder с ViewModel
    }
    
	protected override void Deinitialize()
    {
        // Реализация развязывания Binder от ViewModel
    }
}
```
***

Если вы предпочитаете не использовать кодогенерацию, то вы можете использовать абстрактный класс "**View**",
который добавляет только "ProfilerMarker" в методы "**Initialize**", "**Deinitialize**", а так же несколько проверок безопасности.

```csharp
using Aspid.UI.MVVM.View;

public partial class SomeView : View
{
    protected override void InitializeInternal(IViewModel viewModel)
    {
        // Реализация связывание Binder с ViewModel
    }

	protected override void DeinitializeInternal()
    {
        // Реализация развязывания Binder от ViewModel
    }
}
```

Для тех, кто предпочитает использовать Unity-специфический подход, существует класс "**MonoView**",
который имеет реализацию как у "**View**", но наследует "**MonoBehaviour**". Это удобно,
если ваша View должна быть MonoBehaviour-объектом и взаимодействовать с Unity-объектами напрямую.
Для Unity.UI рекомендуется использовать "**MonoView**" вместе с кодогенерацией, так как "**MonoView**" содержит
дополнительные средства [отладки](#monoview).

#### \[View\]
***
Рекомендуемый способ реализации интерфейса "**IView**" — использование кодогенерации.
Чтобы воспользоваться этим механизмом, необходимо пометить класс атрибутом "**\[View\]**" и сделать его "**partial**".
Так же рекомендуется использовать "**MonoView**" как родительский класс, если вы используете Unity.UI, так как он предлагает
дополнительные средства [отладки](#monoview).

```csharp
using Aspid.UI.MVVM.Mono.View;
using Aspid.UI.MVVM.View.Generation;

[View]
public partial class SomeView { }

// Рекомендуемый способ
[View]
public partial class SomeMonoView : MonoView { }
```

"**SomeView**" может быть унаследован от любого класса, в том числе от "**View**" и "**MonoView**". Рекомендуется
наследоваться от MonoView, если вы работаете с Unity.UI.

Атрибуты для генерации: Атрибут "**\[View\]**" также является необходимым для следующих атрибутов:
* [\[AsBinder\]](#asbinder);

#### View Content
***

View является контейнером, который хранит Binder'ы — объекты, обеспечивающие привязку данных между ViewModel и компонентами View.
Binder'ы связывают свойства из ViewModel с конкретными элементами интерфейса в Unity (например, текстовыми полями или кнопками).
Если используется кодогенерация, то любой тип, реализующий "**IBinder**", будет автоматически интегрирован в сгенерированный код.

```csharp
using Aspid.UI.MVVM.Mono.View;
using Aspid.UI.MVVM.View.Generation;

[View]
public partial class SomeMonoView : MonoView 
{
    [SerializeField] private MonoBinder _singleBinder;
    [SerializeField] private MonoBinder[] _arrayBinders;
    
    public MonoBinder SingleBinder
    {
        // Если при get возвращает _singleBinder,
        // то свойство не будет участвовать в генерации
        get => ...
    }
    
    public MonoBinder[] ArrayBinder
    {
        // Если при get возвращает _arrayBinders,
        // то свойство не будет участвовать в генерации
        get => ...
    }
}
```

Описание:
* MonoBinder: Это тип биндеров, которые связывают View с ViewModel.
В этом примере показаны как единичный биндер (singleBinder), так и массив биндера (arrayBinders), 
что позволяет связывать несколько компонентов интерфейса.

**На основе этого кода, генератор создаст необходимую логику для привязки значений ViewModel к компонентам View:**

```csharp
public partial class SomeMonoView
{
    private MonoBinder _singleBinderCachedPropertyBinder;
    private MonoBinder[] _arrayBinderCachedPropertyBinder;
    
    protected override void InitializeInternal(IViewModel viewModel)
    {
        InstantiateBinders();

        _singleBinder.BindSafely(viewModel, SingleBinderId);
        _arrayBinders.BindSafely(viewModel, ArrayBindersId);
        _singleBinderCachedPropertyBinder.BindSafely(viewModel, SingleBinderIdProperty);
        _arrayBinderCachedPropertyBinder.BindSafely(viewModel, ArrayBinderIdProperty);
    }
    
    protected override void DeinitializeInternal()
    {
        _singleBinder.UnbindSafely();
        _arrayBinders.UnbindSafely();
        _singleBinderCachedPropertyBinder.UnbindSafely();
        _arrayBinderCachedPropertyBinder.UnbindSafely();
    }
    
    private void InstantiateBinders()
    {
        if (!_singleBinderCachedPropertyBinder) 
            _singleBinderCachedPropertyBinder = SingleBinder;
        
        _arrayBinderCachedPropertyBinder ??= ArrayBinder;
    }
}
```

#### \[RequireBinder\]
***

Атрибут "**\[RequireBinder\]**" используется для гарантии того, что в Binder реализует необходимый "**IBinder<T>**",
обеспечивающий правильную привязку данных. Это позволяет повысить универсальность и гибкость кода, не нарушая его целостности,
и предотвращает возможные ошибки, связанные с отсутствием необходимой реализации. "**T**" в "**IBinder<T>**"
должен соответствовать типу свойству из ViewModel, которое будет привязано.

```csharp
using Aspid.UI.MVVM.Mono.View;
using Aspid.UI.MVVM.View.Generation;

[View]
public partial class SomeMonoView : MonoView 
{
    // Компонент который будет прикриплен, 
    // должен реализовывать IBinder<string>
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _singleBinder;
    
    // Компоненты которые будет прикриплены, 
    // должен реализовывать IBinder<int>
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _arrayBinders;
}
```

**Ограничения:**
* Атрибут "**\[RequireBinder\]**" работает только в контексте "**MonoView**".
Это ограничение необходимо учитывать, поскольку в других типах View данный атрибут не будет действовать.

**Преимущества:**
* Гарантия типовой безопасности: RequireBinder помогает избежать ошибок типов, гарантируя, что правильный тип биндеров используется для привязки.
* Универсальность: Этот атрибут позволяет View оставаться универсальной, при этом обеспечивая привязку конкретных типов данных.

#### \[AsBinder\]
***

Атрибут "**\[AsBinder\]**" позволяет использовать компоненты Unity (к примеру: TextMeshProUGUI), для привязки данных вместо стандартных Binder.
Это позволяет интегрировать сторонние компоненты UI с системой привязки данных без необходимости использовать специальные Binder'ы.
Важным моментом является то, что вы можете указать тип компонента
и указать соответствующий Binder для него.

```csharp
using Aspid.UI.MVVM.Mono.View;
using Aspid.UI.MVVM.View.Generation;

[View]
public partial class SomeMonoView : MonoView 
{
    [AsBinder(typeof(TextBinder))]
    [SerializeField] private TextMeshProUGUI _singleBinder;
    
    [AsBinder(typeof(TextBinder))]
    [SerializeField] private TextMeshProUGUI[] _arrayBinders;
    
    [AsBinder(typeof(TextBinder))]
    public TextMeshProUGUI SingleBinder
    {
        get => ...
    }
    
    [AsBinder(typeof(TextBinder))]
    public TextMeshProUGUI[] ArrayBinder
    {
        get => ...
    }
}
```

На основе примера с "**\[AsBinder\]**" генератор создаст код, который будет инициализировать привязку с нужным компонентом:

```csharp
public partial class SomeMonoView
{
    private TextBinder _singleBinderCachedBinder;
    private TextBinder[] _arrayBindersCachedBinder;
    private TextBinder _singleBinderCachedPropertyBinder;
    private TextBinder[] _arrayBinderCachedPropertyBinder;

	protected override void InitializeInternal(IViewModel viewModel)
    {
        InstantiateBinders();

        _singleBinderCachedBinder.BindSafely(viewModel, SingleBinderId);
        _arrayBindersCachedBinder.BindSafely(viewModel, ArrayBindersId);
        _singleBinderCachedPropertyBinder.BindSafely(viewModel, SingleBinderIdProperty);
        _arrayBinderCachedPropertyBinder.BindSafely(viewModel, ArrayBinderIdProperty);
    }

	protected override void DeinitializeInternal()
    {
        _singleBinderCachedBinder.UnbindSafely();
        _arrayBindersCachedBinder.UnbindSafely();
        _singleBinderCachedPropertyBinder.UnbindSafely();
        _arrayBinderCachedPropertyBinder.UnbindSafely();
    }

	private void InstantiateBinders()
    {
        _singleBinderCachedBinder ??= new TextBinder(_singleBinder);
        
		if (_arrayBindersCachedBinder == null)
		{
		    var local_arrayBinders = _arrayBinders;
		    _arrayBindersCachedBinder = new TextBinder[local_arrayBinders.Length];
		    
		    for (var i = 0; i < local_arrayBinders.Length; i++)
		        _arrayBindersCachedBinder[i] = new TextBinder(local_arrayBinders[i]);
		}
        
        _singleBinderCachedPropertyBinder ??= new TextBinder(SingleBinder);
        
		if (_arrayBinderCachedPropertyBinder == null)
		{
		    var localArrayBinder = ArrayBinder;
		    _arrayBinderCachedPropertyBinder = new TextBinder[localArrayBinder.Length];
		    
		    for (var i = 0; i < localArrayBinder.Length; i++)
		        _arrayBinderCachedPropertyBinder[i] = new TextBinder(localArrayBinder[i]);
		}
    }
}
```

Атрибут "**\[AsBinder\]**" позволяет легко связать обычные компоненты Unity (к примеру: TextMeshProUGUI), 
используя кастомные Binder. Это позволяет использовать привычные инструменты Unity в архитектуре MVVM,
при этом обеспечивая удобную привязку данных и их синхронизацию с ViewModel.

### Binder
***

В **Aspid.UI.MVVM Binder** отвечает за привязку данных между **свойствами ViewModel** и **элементами интерфейса (UI)**.
Binder реализует интерфейсы "**IBinder**" и "**IBinder<T>**", где "**T**" — тип данных,
который соответствует свойству **ViewModel**.

**Основные реализации Binder:**
* **Binder** — базовая реализация, которая не наследуется от "**MonoBehaviour**".
* **MonoBinder** — базовая реализация, наследующее "**MonoBehaviour**". Оно предоставляет дополнительные возможности,
включая средства [отладки](#monobinder). Этот класс чаще используется для компонентов Unity, так как он может быть привязан к объектам сцены.

***
**Пример простого Binder:**

```csharp
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.ViewModels;

public class ImageSpriteBinder : MonoBinder, IBinder<Sprite>
{
    [SerializeField] private Image _imaget;
    
    public void SetValue(Sprite value) =>
        _image.sprite = value;
}
```

В этом примере "**ImageSpriteBinder**" реализует "**IBinder<Sprite>**", чтобы связать свойство типа Sprite с изображением на экране.
Binder использует метод SetValue для обновления значения изображения, когда данные в ViewModel изменяются.

***
**Множественные реализации IBinder**
В одном Binder можно реализовать несколько интерфейсов "**IBinder<T>**" для поддержки разных типов данных. 
Это полезно, если один компонент интерфейса должен работать с несколькими типами данных:

```csharp
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.ViewModels;

public class SliderValueBinder : MonoBinder, IBinder<int>, IBinder<float>
{
    [SerializeField] private Slider _slider;
    
    public void SetValue(int value) =>
        _slider.value = value;
    
    public void SetValue(float value) =>
        _slider.value = value;
}
```

Здесь "**SliderValueBinder**" поддерживает как целочисленные значения, так и числа с плавающей точкой,
обновляя значение ползунка (Slider).

***
**Универсальный Binder:**

Если требуется привязка к свойствам любого типа, можно реализовать IBinder<object>,
что позволит Binder работать с любыми типами данных. Пример — привязка данных любого типа к текстовому полю:

```csharp
using TMPro;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.ViewModels;

public class TextBinder : MonoBinder, IBinder<object>
{
    [SerializeField] private TMP_Text _text;
    
    public void SetValue(object value) =>
        _text.text = value.ToString();
}
```

В этом случае "**TextBinder**" может работать с любым типом данных, преобразуя его в строку для отображения в "**TMP_Text**".

***
**Примечания:**
* "**Binder**" и "**MonoBinder**" реализуют только интерфейс "**IBinder**" по умолчанию. Для работы с конкретным типом данных необходимо
реализовать соответствующий интерфейс "**IBinder<T>**".
* Важно учитывать тип данных, который используется в привязке, чтобы избежать ошибок в процессе передачи данных между
**ViewModel** и **компонентами UI**.

#### Reverse
***

В **Aspid.UI.MVVM** существует возможность обратного связывания (two-way binding),
которая позволяет обновлять данные в **ViewModel** при изменении значения в **UI-компоненте**.
Чтобы реализовать обратное связывание, необходимо настроить **Binder** соответствующим образом.

**Основные шаги:**
* Реализовать свойство "**IsReverseEnabled**", которое включает возможность обратного связывания.
* Реализовать интерфейс "**IReverseBinder<T>**", где "**T**" — это тип данных для обратного связывания.
* Настроить события, которые уведомляют **ViewModel** об изменении значения в **UI-компоненте**.

**Пример реализации:**

```csharp
using TMPro;
using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

public class InputFieldMonoBinder : MonoBinder, IBinder<string>, IReverseBinder<string>
{
    // Событие, которое уведомляет об изменении значения в UI
    public event Action<string> ValueChanged;
    
    [SerializeField] private bool _isReverseEnabled;
    
    // UI-компонент
    [SerializeField] private TMP_InputField _inputField;  
    
    private bool _isNotifyValueChanged = true;

    // Реализация свойства для включения обратного связывания
    public bool IsReverseEnabled => _isReverseEnabled;
    
    // Подписка на событие при связывании с ViewModel
    protected override void OnBound(IViewModel viewModel, string id) =>
        Subscribe();

    // Отписка от события при отвязывании от ViewModel
    protected override void OnUnbound() =>
        Unsubscribe();
    
    private void Subscribe()
    { 
        if (!IsBound || !IsReverseEnabled) return;
        _inputField.onValueChanged.AddListener(OnValueChanged);
    }
    
    private void Unsubscribe()
    {
        _inputField.onValueChanged.RemoveListener(OnValueChanged);
    }
    
    public void SetValue(string value)
    {
        // Отписываемся от события, чтобы избежать рекурсии
        Unsubscribe();
        _inputField.text = value;
        
        // Подписываемся снова
        Subscribe();
    }
    
    private void OnValueChanged(string value)
    {
        // Уведомляем о новом значении
        ValueChanged?.Invoke(value);
    }
}
```

**Объяснение:**
* **IsReverseEnabled**: Это свойство определяет, разрешено ли обратное связывание для текущего биндинга.
Если оно установлено в true, **Binder** начинает передавать изменения из UI-компонента в ViewModel.
* **IReverseBinder<T>**: Этот интерфейс обязателен для обратного связывания. Его реализация требует использования события ValueChanged,
которое будет уведомлять ViewModel о том, что значение в UI-компоненте изменилось.
* Обратное связывание: Когда пользователь изменяет текст в поле ввода, метод OnValueChanged срабатывает,
вызывая событие ValueChanged, которое передает новое значение в ViewModel.

Этот подход обеспечивает двустороннее связывание данных между ViewModel и UI-компонентом,
что позволяет пользователю изменять данные напрямую в интерфейсе и синхронизировать их с моделью.

#### \[BinderLog\]
***

Атрибут "**\[BinderLog\]**" используется для добавления локального логирования в Binder, 
что помогает избежать перегрузки основной консоли Unity избыточными логами. 
Этот атрибут применяется непосредственно к методу "**SetValue**" интерфейса "**IBinder<T>**",
и при этом реализация метода должна быть неявной (implicit).
Класс, где используется "**\[BinderLog\]**", должен быть "**partial**".

**Основные моменты:**
* "**\[BinderLog\]**" добавляет опциональное логирование, которое можно включать и выключать через Unity Inspector для конкретного **Binder**.
* Это локальный способ контроля вывода сообщений для каждого Binder, что позволяет избежать избыточных логов в консоли при работе с множественными **Binder'ми** в больших проектах.

**Пример использования:**

```csharp
using TMPro;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Generation;

public partial class TextMonoBinder : MonoBinder, IBinder<object>
{
    [SerializeField] private TMP_Text _text;
    
    [BinderlLog]
    public void SetValue(object value) =>
        _text.text = value.ToString();
}

[Serializable]
public partial class TextBinder : Binder, IBinder<object>
{
    [SerializeField] private TMP_Text _text;
    
    [BinderlLog]
    public void SetValue(object value) =>
        _text.text = value.ToString();
}
```

**Логирование через Unity Inspector:**
Через Inspector в Unity для каждого Binder можно настроить включение или выключение логирования.
Это удобно, когда вы хотите отслеживать процессы привязки значений в UI во время отладки, но не хотите загромождать основной лог приложения.

### \[CreateFrom\]
***

Атрибут CreateFrom используется как метка для Source Generator, который генерирует методы расширения. 
Эти методы позволяют автоматически создавать объекты одного типа на основе другого,
что значительно упрощает и ускоряет работу с преобразованиями между объектами.

**Основные моменты:**
* Атрибут применяется к классу, который должен быть создан на основе другого класса.
* Генерируются методы расширения для создания объекта нового типа из объекта, массива, списка или перечисления другого типа.
* Методы расширения поддерживают преобразования как для отдельных объектов, так и для коллекций.

Атрибут "**\[CreateFrom\]**" является меткой для Source Generator, который генерирует методы расширения создания другого
объекта на основе помеченого. 

**Пример:**

```csharp
public class Model1 { }

[CreateFrom(typeof(Model1))]
public class Model2
{
    private readonly Model1 _model1;

    public Model2(Model1 model1)
    {
        _model1 = model1;
    }
}

public class Test
{
    public Test()
    {
        Model1 model1 = new Model1();
        Model1[] arrayModel1 = new Model1[10];
        List<Model1> listModel1 = new List<Model1>();
        IEnumerable<Model1> enumerableModel1 = new List<Model1>();
        
        // Использование методов расширения для преобразования Model1 в Model2
        Model2 model2 = model1.ToModel2();
        
        Model2[] arrayModel2 = arrayModel1.ToModel2();
        arrayModel2 = listModel1.ToModel2AsArray();

        List<Model2> listModel2 = listModel1.ToModel2();
        listModel2 = arrayModel1.ToModel2AsList();

        IEnumerable<Model2> enumerableModel2 = enumerableModel1.ToModel2();
    }
}
```

**На основе примера выше Source Generator автоматически сгенерирует методы расширения, такие как:**
* model1.ToModel2() — для преобразования объекта Model1 в Model2.
* arrayModel1.ToModel2() - для преобразования массива Model1[] в массив Model2[].
* listModel1.ToModel2AsArray() — для преобразования массива List<Model1> в массив Model2[].
* listModel1.ToModel2() — для преобразования списка List<Model1> в List<Model2>.
* arrayModel1.ToModel2AsList() — для преобразования списка Model1[] в List<Model2>.
* enumerableModel1.ToModel2() — для преобразования коллекций IEnumerable<Model1> в IEnumerable<Model2>.

**Преимущества:**
* Упрощает преобразование между типами объектов.
* Избавляет от необходимости вручную создавать методы для преобразования коллекций объектов.
* Улучшает читаемость и поддержку кода, поскольку использует понятные методы расширения.

### Advanced

В этом разделе рассмотрены примеры ручной реализации ViewModel.
В текущей версии фреймворка Aspid.UI.MVVM не рекомендуется использовать ручную реализацию, так как API может измениться,
что повлияет на совместимость ваших реализаций. Однако, если гибкость ручного подхода необходима,
приведенные примеры помогут разобраться в том, как это можно сделать.

#### Advanced ViewModel
***

Здесь представлен гибридный способ связывания, который комбинирует автоматическое и ручное связывание данных.
Этот подход может служить основой для полной ручной реализации, если это потребуется.

```csharp
[ViewModel]
public partial class SomeViewModel
{
    // Поле, которое будет связано автоматически через генерацию кода
    [Bind] private string _text;
    
    // Поле, которое будет связано вручную
    private string _manualText;
    
    // Свойство для получения и изменения значения manualText
    private string ManualText
    {
        get => _manualText;
        set => SetManualText(_manualText);
    }
    
    // Специальное событие для связывания
    private ViewModelEvent<string> _manualTextChangedEvent;
    
    private void SetManualText(string value)
    {
        // ViewModelUtility содержит методы для ручного связывания
        if (ViewModelUtility.SetProperty(ref _manualText, value))
        {
            // Вызов события, если значение изменилось
            _manualTextChangedEvent?.Invoke(text);
        }
    }
    
    // Частичный метод, который будет сгенерирован и использован для ручного связывания
    partial void AddBinderManual(IBinder binder, string propertyName, ref IRemoveBinderFromViewModel removeBinder)
    {
        removeBinder = propertyName switch
        {
            // Добавление биндинга для ManualText с использованием Utility
            nameof(ManualText) => ViewModelUtility.AddBinder(binder, ManualText, ref _manualTextChangedEvent, SetManualText), 
            _ => removeBinder
        };
    }
}
```

**Объяснение:**
* Автоматическое связывание: Поле _text связывается автоматически с помощью атрибута "**\[Bind\]**".
* Ручное связывание: Поле _"**manualText**" связывается вручную. Для этого создано свойство "**ManualText**" и событие _"**manualTextChangedEvent**".
* **ViewModelUtility**: Используется для упрощения ручного связывания данных и отслеживания изменений.
* **AddBinderManual**: Это частичный метод, который будет сгенерирован и вызван для настройки ручного связывания.

**Заключение:**

Этот гибридный подход позволяет вам использовать как автоматическое, так и ручное связывание в рамках одного **ViewModel**.
Это может быть полезно в ситуациях, когда автоматической генерации кода недостаточно или требуется тонкий контроль над процессом.

## StarterKit
***

**StarterKit** — это стартовый набор, включающий основные компоненты и биндеры для работы с **Aspid.UI.MVVM**,
предоставляя пользователям готовые решения для связывания данных, а также вспомогательные инструменты для ускорения разработки.

### Binders
***

В состав **StarterKit** входят разнообразные binder'ы, как основанные на "**Binder**", так и на "**MonoBinder**",
что позволяет покрывать разные потребности в привязке данных в **UI-компоненты**. Также в наборе есть биндеры,
которые работают с наблюдаемыми коллекциями из **Aspid.Collection**.

### ViewIntializer
***

**ViewInitializer** — это компонент, упрощающий инициализацию **View**. Поддерживает инициализацию через DI 
([VContainer](https://github.com/hadashiA/VContainer)/[Zenject](https://github.com/modesttree/Zenject)),

**StarterKit** содержит примеры использования "**ViewInitializer**" в связке с популярными DI-фреймворками [VContainer](https://github.com/hadashiA/VContainer) 
и [Zenject](https://github.com/modesttree/Zenject). Эти примеры можно найти в папке Assets/Samples/Aspid/UI,
где демонстрируются базовые сценарии настройки инициализации и внедрения зависимостей.


## Debug
***

### MonoView
***

Если ваше View наследуется от MonoView, вам предоставляются следующие полезные средства для отладки:
* Просмотр всех закрепленных и не закрепленных Binder’ов через инспектор Unity.
* Просмотр данных связанной ViewModel через инспектор. Это позволяет отслеживать текущее состояние данных ViewModel
напрямую через Unity Editor, что упрощает диагностику проблем при связывании данных.

### MonoBinder
***

Если Binder наследуется от MonoBinder, то вы предоставляет следующие средства отладки:
* Локальное логирование в инспекторе, если Binder реализует атрибут "**\[BinderLog\]**"
* Визуализация об ошибки, если binder не прикреплен к View

Если ваш **Binder** наследуется от "**MonoBinder**", вы получаете доступ к следующим функциям отладки:
* Локальное логирование через инспектор Unity, если в **Binder** используется атрибут "**\[BinderLog\]**". Это позволяет включать
и выключать логирование для каждого конкретного **Binder'а**, не засоряя основную консоль.
* Визуализация ошибок, если **Binder** не прикреплен к соответствующему **View**. Это помогает своевременно выявлять проблемы с некорректным связыванием компонентов.

## Roadmap
***

Планируется добавить следующие возможности в будущих версиях **Aspid.UI**:
* Оптимизация связывания при использовании массивов во View. Это улучшит производительность.
* Поддержка атрибута "**\[Access\]**" для "**\[ReadOnlyBind\]**", что добавит больше гибкости для работы с неизменяемыми данными.
* Дополнительные **компоненты UI**.
* Автоопределение Binder’ов для компонентов Unity.
* Асинхронные команды.
* Animation Binder
* Связывание по составному пути для поддержки более сложных сценариев привязки, где требуется доступ к вложенным данным **ViewModel**.
