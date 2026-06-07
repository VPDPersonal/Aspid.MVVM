# История изменений

Все значимые изменения **Aspid.MVVM** фиксируются в этом файле.

Формат основан на [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
проект придерживается [семантического версионирования](https://semver.org/spec/v2.0.0.html).

> 🌐 English version: [CHANGELOG.md](CHANGELOG.md)

---

## [Unreleased]

## [1.1.0-beta.1] — 2026-06-06

Первый preview-срез `1.1.0`, опубликованный в канал `upm-preview`. API в основном стабилизирован, но может ещё измениться до финального релиза `1.1.0`.

### Основное

- Инспекторы редактора для `MonoBinder`, `MonoView`, `MonoViewModel` переписаны на UI Toolkit / `VisualElement`.
- Совершенно новая `DebugViewModelPanel` со вкладками, сохраняемым поиском, поддержкой `RelayCommand`, bindable- и auto-свойств.
- Прототип окна `Aspid.MVVM Settings` с `AspidToggle` и общим стилем.
- Поддержка Bindable Properties в генераторе исходного кода; новый метод `NotifyCanExecuteChangedAll()`.
- `MonoView` больше не абстрактный — единый самодостаточный базовый View.
- Новые `ValueViewModel`, `AnyReverseBinder`, OneWayToSource-биндеры компонентов (семейство `…ToSourceMonoBinder`), биндеры AudioSource / LayoutGroup / Dropdown / Selectable / Object-Name.
- Интегрирован `Aspid.FastTools`, многие визуалы редактора переведены на аналоги из FastTools.
- Все подпроекты вынесены в git-подмодули (`Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`); `Aspid.Collections` подключается как UPM git-пакет (`tech.aspid.collections`).
- Минимальная версия Unity поднята до `6000.0`.

### Добавлено

#### ViewModel и генератор
- Поддержка **Bindable Properties** в генераторе исходного кода (PR #46) — доступны в коде, Debug-панели и сэмплах (обновлён сэмпл Todo).
- Метод генератора **`NotifyCanExecuteChangedAll()`** (PR #52, #54) — выводит имена backing-полей с null-conditional-проверкой, пропускает команды без `CanExecute` и учитывает члены типа `IRelayCommand`.
- **`ValueViewModel`** — минимальная обёртка ViewModel над единственным значением с полной XML-документацией (PR #63).
- Поддержка keyword-полей в генераторе (PR #55).
- Статический экземпляр `EmptyExecution` у `RelayCommand` (PR #36, #93) — исполняемая команда, которая ничего не делает; `GetSelfOrEmptyExecution` использует её как fallback, когда команда равна null. Плюс try/catch в `RelayCommandField` (PR #43).
- Поддержка интерфейсов для `ViewModel` (`IMyVm` теперь можно выбрать как design ViewModel) (PR #53).
- Bindable-члены обобщённых enum / struct теперь определяют свой эффективный вид типа из ограничений обобщённого параметра, а не по умолчанию по типу члена класса (PR #44).
- **Виртуальные поля биндеров** — генератор автоматически создаёт слоты `MonoBinder[]` для bindable-членов `IView<TViewModel>`, не объявленных на View. Отключается через `[View(AutoBinderFields = false)]`; View, унаследованные от `ScriptableObject`, всегда пропускаются (PR #74, PR генератора `Aspid.MVVM.Generators#13`).

#### Views
- `MonoView` теперь не абстрактный и самодостаточный — список биндеров для инспектора, валидация дочерних элементов и интеграция `[RequireBinder]` живут прямо в нём (PR #48).
- Поддержка `RelayCommand` внутри `View` / `MonoView`; рефакторинг `CommandsContainer`; `CommandContainer in View` (PR #43).
- Переработка `ViewInitializer` (PR #41, #50) — разрешение view/контейнера вынесено в `ViewInitializerBase`, ленивые `Views` / `ViewModel` в edit-режиме, `Resolve` контейнера заменён на `TryResolve`, добавлена новая стадия инъекции `InitializeStage.DiConstructor`.
- Режим `DestroyView` в редакторе; исправления расширения `DestroyViewModel` (PR #43, #53).
- Обновлены `PrefabViewFactory` / `PrefabViewPool`.
- `ViewModelPickerWindow` с выпадающим списком и улучшенной навигацией (PR #53).
- `[AddComponentMenu]` для `MonoView`; snake-стиль для меню настроек (PR #47).
- Рефакторинг редактора `MonoView`; исправлено отображение сгенерированных полей и базового инспектора (PR #32).
- Обновление `DesignViewModel` (PR #53), включая поддержку legacy-версий Unity.

#### Редактор / инспектор
- Новые инспекторы на UI Toolkit для `MonoBinder`, `MonoView`, `MonoViewModel` (PR #31, #32, #35).
- Общие визуалы `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine` (PR #32, #40).
- Тема на USS: `AspidToggle` (PR #47), исправление отступов IMGUI-foldout-драйвера, обёртка IMGUIContainer в стилизованный `AspidPropertyField`.
- `EnumMonoBinderEditor` (PR #57); исправления `EnumValuesPropertyDrawer`; сэмпл `EnumValues` и документация `ComponentTypeSelector`.
- Drag & Drop для неназначенных и общих биндеров (группы + Auto-Assign + кнопки Select / Restore) (PR #43).
- `RequireBinder` и валидация дочерних View / биндеров (alpha) (PR #43).
- Прототип окна `Aspid.MVVM Settings` (PR #47).
- **Foldout-атрибуты `HeaderGroup`** — `HeaderGroupAttribute` (одно поле), `HeaderGroupStartAttribute` / `HeaderGroupEndAttribute` (диапазон) собирают поля биндеров и члены VM в именованные сворачиваемые foldout-группы инспектора. Новый `HeaderGroupRouter` используется в `MonoViewVisualElement` / `AspidBaseInspectorVisualElement` вместо встроенной раскладки foldout. Вырезается из сборок без `DEBUG` / `UNITY_EDITOR` (PR #74).

#### ViewModel Debug Panel (PR #45)
- Переписана на UI Toolkit, со вкладками (`DebugViewModelPanel`).
- Поиск с сохранением состояния и улучшенной логикой; поиск по типу.
- Поддержка `RelayCommand` (`RelayCommandField`, корректные meta-контейнеры).
- Поддержка bindable- и auto-свойств.
- Новые стили: `Debug field`, `DisableTextFields`, `DebugStringField`.

#### Биндеры — новые
- Биндеры LayoutGroup (PR #56).
- Биндеры AudioSource (PR #59).
- OneWayToSource-биндеры компонентов (семейство `…ToSourceMonoBinder`) (PR #58).
- `AnyReverseBinder` с поддержкой nullable (PR #37) — reverse-биндеры теперь передают `null`-ссылочные значения через `OnValueChanged(default)`, а не выбрасывают исключение (PR #95).
- Биндеры Object Name (PR #34).
- Дополнительные биндеры InputField + крупный рефакторинг (PR #51).
- Биндеры Dropdown / Selectable (PR #61).
- Биндеры `Addressable` получили опциональный режим бесшовной замены (seamless swap) с защитой от обращения к уничтоженному объекту в async-колбэке завершения (PR #86).
- `GameObjectInstantiateAddressableMonoBinder` для спавна префабов через Addressables.

#### Биндеры — улучшения
- События `OnReplace` / `OnMove` пробрасываются в хуки биндеров; пакетный `Replace` разворачивается в поэлементные вызовы `OnReplace`.
- Реактивные collection-биндеры: `CollectionBinderBase<T>` теперь подписывается на `CollectionChanged` и пробрасывает гранулярные события `Add`, `Remove`, `Reset` в новые абстрактные хуки `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)` (PR #94), а также корректно отписывается при `Unbind` и `Dispose` (PR #88, #91).
- Обновление общего биндера (PR #60).
- `BindSafely` / `UnbindSafely` дополнены View и bindable Id; новые перегрузки с `owner` / `memberName`.
- Исправления `EventTriggerCommandMonoBinder`, `ImageSpriteSwitcherBinder`, `MonoBinderPropertyField`.
- Исправления Dispose / жизненного цикла `VirtualizedListItemSourceBinder`.
- Исправления `ViewModelObservableListBinder`.
- Полировка `MonoBinderVisualElement`; визуализация биндеров в скрипте и обновление анимации.
- Поддержка `BindMode` для `VisualElement` (PR #39).
- Поддержка BinderLog в `IAnyBinder`.

#### Коллекции
- `Aspid.Collections` теперь подключается как UPM git-пакет (`tech.aspid.collections`) вместо поставки исходниками внутри пакета (PR #79).
- Исправления `FilteredList` и `BindAlso`.
- Новые тесты коллекций.
- События `Replace` / `Move` доведены до биндеров.

#### Структура проекта / инфраструктура
- Подключены подмодули (PR #38): `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`.
- Проект Unity перенесён из корня репозитория в `Aspid.MVVM/`.
- Пакет MVVM перемещён из `Plugins/Aspid/` в `Assets/Aspid/` (PR #77), затем переведён во встроенный локальный UPM-пакет в `Packages/tech.aspid.mvvm` (PR #117).
- `package.json` размещён внутри пакета; поле `unity` установлено в `6000.0`, `unityRelease` зафиксирован; версия `1.1.0-beta.1`.
- Сэмплы поставляются в `Samples~` и зарегистрированы в `package.json`: HelloWorld, Stats, TodoList, VirtualizedList, а также пошаговые Counter / Greeter.
- Корневой `CLAUDE.md` с описанием структуры и конвенций.
- GitHub Actions: воркфлоу Claude PR Assistant + Code Review (PR #64).
- GitHub Actions: воркфлоу релиза публикует стабильный (`upm`) и preview (`upm-preview`) UPM-сабтри с неизменяемыми тегами `upm/<версия>`, проверкой дрейфа DLL генераторов и заметками о релизе из CHANGELOG (PR #78); в Readme добавлены соответствующие бейджи версий Stable / Preview.

#### Интеграции / зависимости
- `Aspid.FastTools` интегрирован (PR #26) и позже встроен как локальный UPM-пакет в `Packages/tech.aspid.fasttools`; многие визуалы редактора переведены на аналоги из FastTools.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.Collections`, `Aspid.FastTools` обновлены до актуальных HEAD.
- `SerializeReferenceDropdown` обновлён до `1.2.7`.
- Обновлён шрифт `Roboto-Bold SDF`.
- Целевая версия редактора поднята до `6000.4.0f1`; минимальная поддерживаемая версия Unity поднята до `6000.0`.

#### Документация
- Массовый проход XML-документации по всем семействам биндеров: AudioSource, CanvasGroup, Collider, Animator, Behaviour, GameObject, Layout, UnityGeneric, Selectable, Graphic, Image, RawImage, Renderer, Transform, Slider, InputField, Toggle, Button, EventTrigger, ScrollBar, ScrollRect, Dropdown, Object, LineRenderer, Casters, LocalizeStringEvent, VirtualizedList плюс базовые подпапки `MonoBinder` / Behaviour (PR #62).
- XML-документация для конвертеров.
- Документация `ComponentTypeSelector` и сэмпл `EnumValues`.
- `Readme.md` перемещён (PR #77) и доработан (PR #71).

### Изменено

- `MonoView` больше не `abstract`; это конкретный компонент с собственным сериализуемым списком биндеров и валидацией `[RequireBinder]`. Существующие подклассы продолжают работать (PR #48).
- `MonoView.Dispose()` больше не уничтожает GameObject-хост — он только вызывает `Deinitialize()`. При необходимости вызывайте `Object.Destroy(gameObject)` явно (PR #48).
- `MonoBinder.Bind()` больше не выбрасывает исключение при вызове на уже привязанном биндере; вместо этого логирует ошибку и возвращает управление (PR #62).
- Пути `[AddComponentMenu]` реорганизованы — например `Collections/Observable List Binder - ViewModel` → `Collection/Observable List Binder – ViewModel` (единственное число, длинное тире).

### Удалено

- `AddComponentContextMenuAttribute` — заменён на `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` с другой сигнатурой (именованное свойство `Path = "..."`).
- Атрибут `AddPropertyContextMenu` — без замены; новый конвейер редактора обрабатывает меню свойств внутренне.
- Исходники `Aspid.Collections` внутри пакета — теперь подключаются как UPM git-пакет (`tech.aspid.collections`).

### Переименовано (имена классов StarterKit)

GUID-ы `.meta` сохранены, поэтому префабы и сцены продолжают ссылаться на правильный скрипт. **Игровой код, ссылающийся на старые имена классов, не скомпилируется, пока не будет обновлён.**

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |

### Исправлено

<!-- Здесь перечислены только исправления багов, реально вышедших в релизах (1.0.0–1.0.5). Исправления кода, появившегося в ходе разработки 1.1.0, намеренно учтены в соответствующих пунктах о фичах выше, а не как отдельные исправления. -->

- `NumberToBoolConverter`: сравнение `Inequality` было инвертировано — возвращало тот же результат, что и `Equal`, вместо его отрицания. Теперь возвращает `true`, когда значения не приблизительно равны (PR #81).
- `DynamicViewModel.Create<…>`: перегрузки фабрики передавали только `DynamicPropertyData.Value`, из-за чего каждое свойство принудительно получало `BindMode.OneTime`, а заданный пользователем `Mode` отбрасывался. Теперь передаётся весь `DynamicPropertyData`, и настроенный `BindMode` учитывается (PR #83).
- `MonoBinder.Unbind()`: блок `ProfilerMarker` был защищён только `!ASPID_MVVM_UNITY_PROFILER_DISABLED`, что ломало компиляцию на Unity старше 2022.1. Теперь дополнительно требует `UNITY_2022_1_OR_NEWER`, как и `Bind()` (PR #84).
- `VirtualizedList`: `OnAdded` / `OnRemoved` проверяли вычисленный индекс пула view относительно `ItemsSource.Count` со слишком мягким `<=`. Теперь проверка сравнивает `viewIndex < _views.Length`, корректно выбирая `Refresh` или `ResizeContent` (PR #89).
- `ObservableListBinder`: `InitializeList` подписывался на `CollectionChanged` у исходного аргумента `list`, тогда как `DeinitializeList` отписывался у `List` (который может быть отфильтрованной обёрткой), что приводило к утечке подписки. Подписка теперь использует `List` (PR #90).
- Command-биндеры Slider / Scrollbar: `OnCanExecuteChanged` переинтерпретировал 4-байтовый `float` `Target.value` как обобщённый тип команды `T` через `Unsafe.As`, вызывая чтение за границами и мусорные значения `CanExecute` для команд `long` / `double`. Типизированные перегрузки теперь выполняют корректное числовое приведение через `ApplyCanExecute` (PR #92).
- Генератор исходного кода: bindable-члены, тип которых был обобщённым параметром, попадали в ветку по умолчанию (класс) и игнорировали ограничения `enum` / `struct`. Теперь генератор определяет эффективный вид типа из ограничений параметра и выводит корректный тип bindable-члена (PR #44).

### Миграция

Полный чек-лист обновления с 1.0 на 1.1 см. в [MIGRATION.ru.md](MIGRATION.ru.md).

---

## [1.0.5] — 2025-10-17

### Добавлено
- Новые биндеры текста TextMeshPro: `TextFontBinder`, `TextFontSwitcherBinder`, `TextAlignmentBinder`, `TextAlignmentSwitcherBinder` плюс Mono-варианты (`TextFontMonoBinder`, `TextFontEnumMonoBinder`, `TextFontEnumGroupMonoBinder`, `TextFontSwitcherMonoBinder`) — для привязки шрифта и выравнивания TMP (PR #30).
- Новые биндеры Unity Localization: `LocalizeStringEventVariableBinder` (+ Mono-вариант), `TextLocalizationEntryBinder`, `TextLocalizationEntrySwitcherBinder` и Mono-варианты, с `TextLocalizationExtensions` (PR #29).
- Profiler-маркеры и улучшенное логирование для типов `BindableMember` и `BindMode` (`BindModeExtensions.Throw`, `LoggerHelper`) (PR #15).

### Изменено
- Проект редактора обновлён до Unity `6000.2.7f2` (PR #28).
- В репозиторий вшит пакет `com.unity.asset-store-tools` (только упаковка, без изменений кода фреймворка).

### Исправлено
- `RectTransformSetters.SetSizeDelta` записывал вычисленное значение в `anchoredPosition` вместо `sizeDelta`, из-за чего SizeDelta-биндеры перемещали `RectTransform` вместо изменения размера (PR #27).

---

## [1.0.4] — 2025-09-19

### Исправлено
- Генерация контекстного меню компонентов (пункты «Add Component», создаваемые через `AddComponentContextMenuAttribute`) — исправление вошло в пересобранный `Aspid.MVVM.Unity.Generators.dll` (PR #14).

---

## [1.0.3] — 2025-09-15

### Изменено
- Типы Unity-слоя (`MonoBinder`, `MonoViewModel`, `MonoView`, `ScriptableView`, классы редактора) перенесены из пространства имён `Aspid.MVVM.Unity` в корневое `Aspid.MVVM` — для соответствия требованиям упаковки Asset Store (PR #13).

### Удалено
- `MonoBinderExtensions` (перегрузки-хелперы `BindSafely<T>(...)`) и partial-хуки отладки `OnBindingDebug` / `OnUnbindingDebug` у `MonoBinder` (PR #13).

---

## [1.0.2] — 2025-09-11

### Исправлено
- Исправление генератора исходного кода ViewModel — вошло в пересобранный `Aspid.MVVM.Generators.dll` (PR #12).

---

## [1.0.1] — 2025-09-10

### Изменено
- Версия языка C# возвращена с C# 10 на C# 9 (убран `-langversion:10` из файлов `csc.rsp`) для соответствия компилятору Unity по умолчанию (PR #11).
- `AddressableMonoBinder<TAsset>` переработан с модели UniTask/async (`LoadAssetAsync` / `CancellationToken`) на синхронный колбэк `Addressables.LoadAssetAsync(...).Completed`, что убирает зависимость от UniTask для Addressable-биндеров.
- `OneTimeBindableMember<T>` (и варианты Enum / Struct) превращён в пулящийся singleton через статическую фабрику `Get(value)` вместо аллокации на каждую привязку.

### Исправлено
- `ViewModelCollectionBinder` / `ViewModelCollectionMonoBinder` теперь деактивируют (`SetActive(false)`) оставшиеся пулящиеся view сверх текущего числа элементов, чтобы устаревшие view не оставались видимыми при сокращении привязанной коллекции.

---

## [1.0.0] — 2025-08-09

Первый публичный релиз. Последующие записи описывают изменения относительно 1.0.0.
