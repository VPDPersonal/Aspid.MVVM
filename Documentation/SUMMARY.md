# Aspid.MVVM — Документация

Полное руководство по использованию MVVM-фреймворка Aspid.MVVM для Unity.

## Содержание

### Основы

1. [Быстрый старт](01-getting-started.md) — установка, первый ViewModel и View
2. [Архитектура](02-architecture.md) — MVVM-паттерн, Source Generation, конвейер привязки
3. [Режимы привязки](03-binding-modes.md) — OneWay, TwoWay, OneTime, OneWayToSource

### Ключевые концепции

4. [ViewModel](04-viewmodels.md) — создание ViewModel, атрибуты `[Bind]`, `[BindAlso]`, `[Access]`, обработчики изменений
5. [View](05-views.md) — создание View, MonoView, `[AsBinder]`, жизненный цикл
6. [Биндеры](06-binders.md) — IBinder, MonoBinder, создание кастомных биндеров
7. [Команды](07-commands.md) — IRelayCommand, `[RelayCommand]`, CanExecute, параметры
8. [Конвертеры](08-converters.md) — IConverter, встроенные конвертеры значений
9. [Коллекции](09-collections.md) — ObservableList, ObservableDictionary, FilteredList, синхронизация

### Продвинутые возможности

10. [DynamicViewModel](10-dynamic-viewmodel.md) — runtime-конструирование ViewModel без кодогенерации
11. [ViewInitializer](11-view-initializers.md) — автоматическая инициализация View из Inspector
12. [Интеграция с DI](12-di-integration.md) — Zenject и VContainer
13. [Анализаторы](13-analyzers.md) — Roslyn-анализаторы и автоматические исправления
14. [Лучшие практики](14-best-practices.md) — рекомендации и типичные ошибки

### StarterKit — готовые компоненты

- [Обзор StarterKit](StarterKit/README.md) — таблица всех компонентов
- [Text](StarterKit/text-binders.md) — TextBinder, TextSwitcherBinder, шрифты, размеры
- [InputField](StarterKit/input-field-binders.md) — InputFieldBinder, валидация, события
- [Image](StarterKit/image-binders.md) — ImageSpriteBinder, ImageFillBinder
- [Button / Command](StarterKit/button-command-binders.md) — ButtonCommandBinder, InteractableMode
- [Slider](StarterKit/slider-binders.md) — SliderValueBinder, SliderMinMaxBinder
- [Toggle](StarterKit/toggle-binders.md) — ToggleIsOnBinder
- [Dropdown](StarterKit/dropdown-binders.md) — DropdownValueBinder, DropdownOptionsBinder
- [GameObject](StarterKit/gameobject-binders.md) — GameObjectVisibleBinder
- [Transform](StarterKit/transform-binders.md) — позиция, вращение, масштаб, RectTransform
- [CanvasGroup](StarterKit/canvas-group-binders.md) — Alpha, BlocksRaycasts, Interactable
- [Animator](StarterKit/animator-binders.md) — SetBool, SetFloat, SetInt, SetTrigger
- [Graphic / Renderer](StarterKit/graphic-binders.md) — цвет, материалы
- [AudioSource](StarterKit/audio-source-binders.md) — громкость, клип и другие свойства
- [Collider](StarterKit/collider-binders.md) — Box, Capsule, Sphere, Mesh
- [UnityEvent](StarterKit/unity-event-binders.md) — UnityEvent-биндеры для всех типов
- [Коллекции](StarterKit/collection-binders.md) — Observable/Virtualized-биндеры списков
- [Switcher](StarterKit/switcher-binders.md) — паттерн true/false -> значение
- [Caster](StarterKit/caster-binders.md) — конвертирующие биндеры
- [Value](StarterKit/value-binders.md) — OneWayValue, TwoWayValue
- [Generic](StarterKit/generic-binders.md) — код-биндеры через делегаты
- [Debug](StarterKit/debug-binder.md) — DebugLogBinder для отладки
- [View Factories](StarterKit/view-factories.md) — PrefabViewFactory, PrefabViewPool
- [Прочие](StarterKit/misc-binders.md) — ObjectNameBinder, ComponentToSourceMonoBinder

### Туториалы

#### Обучающий маршрут

- [Counter](Tutorials/counter.md) — `[OneWayBind]`, `[RelayCommand]`: кнопка + счётчик
- [Greeter](Tutorials/greeter.md) — `[TwoWayBind]`, `On*Changed`: InputField → текст в реальном времени
- [TodoItem](Tutorials/todo-item.md) — Model, `IDisposable`, несколько типов биндинга
- [TodoList](Tutorials/todo-list.md) — `ObservableList`, `CreateSync`, коллекционные биндеры

#### Разбор Sample-проектов

- [Hello World](Tutorials/hello-world.md) — разбор Sample-проекта Speaker
- [Stats](Tutorials/stats.md) — CanExecute, параметризованные команды
- [VirtualizedList](Tutorials/virtualized-list.md) — виртуализация, FilteredList

## Ссылки

- [Unity Asset Store](https://assetstore.unity.com/packages/slug/298463)
- [GitBook документация](https://vpd-inc.gitbook.io/aspid.mvvm/)
