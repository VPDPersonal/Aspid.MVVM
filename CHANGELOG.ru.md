# История изменений

Все значимые изменения **Aspid.MVVM** фиксируются в этом файле.

Формат основан на [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
проект придерживается [семантического версионирования](https://semver.org/spec/v2.0.0.html).

> 🌐 English version: [CHANGELOG.md](CHANGELOG.md)

---

## [Unreleased]

### Добавлено

- **~150 конвертеров** — каталог вырос с 14 до 148, и в пакете не осталось ни одного пустого пикера `[SerializeReference]`. В выпадающем списке типов они разложены по группам: `Aspid/Bool` (9), `Aspid/Number` (15), `Aspid/String` (36), `Aspid/Time` (8), `Aspid/Colour` (14), `Aspid/Vector` (23), `Aspid/Rotation` (9), `Aspid/Collection` (6), `Aspid/Enum` (5), `Aspid/Object` (3), `Aspid/Texture` (4), `Aspid/Layout` (3), `Aspid/Localization` (4), `Aspid/Asset` (2), `Aspid/Composition` (6). Из заметного: `PercentStringConverter`, `AbbreviatedNumberConverter` (`1234` → `1.2K`), `RelativeTimeConverter`, `SecondsToTimeStringConverter`, `ThresholdColorConverter`, `RichTextNoParseConverter`, `RemapNumberConverter`, `CollectionCountConverter`.
- **`ITwoWayConverter<TFrom, TTo>`** — конвертер, умеющий отменить себя. Биндер в `BindMode.TwoWay` или `BindMode.OneWayToSource` теперь применяет `ConvertBack`, когда конвертер её предлагает, и пишет предупреждение в консоль, когда нет. Реализуют 23 конвертера из поставки.
- **Негенерический `IConverter`** в корне иерархии. Он ничего не объявляет; он нужен, чтобы валидация, пикер и тесты опознавали конвертер, не перебирая все закрытия генерика.
- **`ConverterFailureMode`** — единый словарь (`ReturnFallback`, `ReturnInput`, `Throw`) для того, что конвертер делает с данными, которые не может преобразовать; раньше на этот вопрос было три разных самодельных ответа.
- **Примитивы композиции** в группе `Aspid/Composition`: `ComposeConverter`, `CachedConverter`, `SafeConverter`, `NullGuardConverter`, `ConditionalConverter`, `PassthroughConverter`. `CachedConverter` важнее, чем кажется: биндер шлёт значение на каждое *уведомление*, а не на каждое *изменение*, поэтому аллоцирующий конвертер вызывается заметно чаще, чем выглядит.
- **`ConverterAsset<TFrom, TTo>`** — конвертер, настроенный один раз как `ScriptableObject` и подключаемый ссылкой через `ConverterAssetReference` из любого числа полей вместо перенастройки в каждом префабе. Восемь готовых подклассов в меню **Create → Aspid → MVVM → Converters**.
- **`CultureInfoMode`** у всех строковых конвертеров и конвертеров разбора. В половине Европы десятичный разделитель — запятая, поэтому число, записанное одной культурой и разобранное другой, теряет дробную часть, а не падает; теперь для всего, что ходит туда-обратно, можно выбрать `InvariantCulture`.
- **Тесты там, где их не было вовсе**: 1048 EditMode-тестов, включая контрактные — они падают, когда у поля конвертера нечего выбрать, когда у сериализуемого поля нет `[Tooltip]`, когда у конвертера в пикере нет группы, когда в тултипе пикера дыра или когда в списке предлагается конвертер, который можно создать только из кода.

### Изменено

- Окно **`Aspid.MVVM Settings`** переоформлено в стиле окна **Welcome** из Aspid.FastTools — анимированный фон из точек, анимированные логотип (ведёт в Asset Store) и заголовок, тематические карточки, градиентные кнопки `Apply` / `Revert` и футер с версией и ссылками.
- Окно настроек перенесено в общий раздел меню `Tools/Aspid 🐍`, рядом с `Welcome FastTools`.
- Версия в окне настроек теперь читается из манифеста пакета, а не из константы; цвета `AspidToggle` приведены в соответствие теме.
- Поля `[SerializeReference]` в инспекторах `MonoView` / `MonoViewModel` / `MonoBinder` теперь рисуются выпадающим списком типов из FastTools вместо стандартного managed-reference UI Unity. Инспекторы направляют их через `SerializeReferenceEditorGUI`, поэтому атрибут `[TypeSelector]` не нужен ни на одном поле, а набор кандидатов — объявленный тип самого поля. Вложенные managed-ссылки *внутри* назначенного экземпляра остаются со стандартным UI: FastTools рисует дочерние свойства экземпляра обычным `PropertyField`.
- `Aspid.FastTools` больше не лежит встроенным пакетом в `Packages/`. Он подключён как UPM-зависимость из git и закреплён на неизменяемом тег-релизе `upm-preview/1.0.0-rc.7` — вместо встроенного `1.0.0-rc.4`. В rc.6 появилась поддержка `[TypeSelector]` для полей `[SerializeReference]` — замена удалённой интеграции `SerializeReferenceDropdown`, — а rc.7 добавляет три исправления пикера типов, от которых зависит проект: кандидат, который поле не может закрыть, больше не предлагается; аргументы generic-типов выводятся через интерфейсы поля; встроенные типы Unity принимаются как аргументы generic-типов. Учтены два переименования API апстрима: namespace `Aspid.FastTools.Reflection` схлопнут в `Aspid.FastTools`, а `SerializedProperty.GetClassInstance()` стал `GetDeclaringInstance()`.
- `GenericToString<TFrom>` выносит форматирование в виртуальный метод `Format` вместо жёстко зашитого решения внутри `Convert`. Пустой формат теперь откатывается на `ToString()`, а не даёт пустую строку, и `Format` по-прежнему получает типизированное значение, поэтому числовые и датовые спецификаторы (`{0:F2}`, `{0:hh\:mm}`) продолжают работать. Флаг `formatEmptyValues` опущен в `StringFormatConverter` — единственный конвертер, у которого есть мнение о пустом вводе; у `ObjectToStringConverter` и `TimeSpanToStringConverter` появился отсутствовавший конструктор с форматом.
- Атрибуты инспектора (`[SerializeField]`, `[SerializeReference]`, `[Tooltip]`) в Unity-независимых слоях больше не обёрнуты в `#if UNITY_2022_1_OR_NEWER`. Вне Unity их заменяют пустые заглушки из `Source/Compatibility/UnityAttributesShim.cs`, благодаря чему удалось убрать 22 блока директив в 14 файлах. Директивы вокруг настоящего Unity API (`Debug`, `Component`, `ProfilerMarker`) не тронуты.
- **Документация конвертеров.** Задокументированы все 40 маркер-интерфейсов, все 70 обёрток `ToConvert` / `ToConvertSpecific` и енумы `Comparisons` / `CultureInfoMode`; каждое сериализуемое поле конвертера получило `[Tooltip]` — единственную документацию, видимую там, где конвертер настраивают. `Documentation/08-converters.md` переписан вокруг реального каталога.
- **`ArithmeticNumberConverter`** публикует `Apply(double)` и `Undo(double)` и помечен `sealed`. Его шестнадцать перегрузок `Convert` больше не добираются до арифметики приведением объекта к одному из его же интерфейсов.
- **Сэмпл Greeter** использует готовый `RichTextColorConverter` вместо самописного `PaintNameConverter`.
- **Переименования конвертеров** — одной волной, чтобы проект оплатил миграцию один раз. Оба переименования классов несут `[MovedFrom]`, все четыре переименования полей — `[FormerlySerializedAs]`, поэтому существующие сцены и префабы сохраняют настроенные значения; правки нужны только в исходном коде.

  | Было | Стало |
  |------|-------|
  | `SequenceConverters<T>` | `SequenceConverter<T>` |
  | `GenericToString<T>` | `GenericToStringConverter<T>` |
  | `Vector*CombineConverter._preConvertor` / `_postConvertor` | `_preConverter` / `_postConverter` |
  | `Vector2ToVector3Converter.Values` / `Vector3ToVector2Converter.Values` | `Mode` |
  | `Comparisons.Inequality` | `Comparisons.NotEqual` |

  У `Inequality` нет параллельного члена: в enum нельзя объявить один член устаревшим так, чтобы замена не появилась в выпадающем списке инспектора дважды. Сериализуется порядковый номер, который не изменился, так что сцены не затронуты.

### Устарело

- **40 именованных псевдонимов конвертеров** (`IConverterFloat`, `IConverterIntToLong`, `IConverterString`, …) и **70 обёрток `ToConvert` / `ToConvertSpecific`** помечены `[Obsolete]` и будут удалены в следующем мажоре. Они существовали только потому, что Unity до 2023.1 не умела сериализовать поле `[SerializeReference]` с типом-открытым генериком; пакет требует Unity 6000.0. Используйте `IConverter<TFrom, TTo>` и генерик-версию `ConverterExtensions.ToConvert<TFrom, TTo>`, которая остаётся.

  Конвертеры самого пакета продолжают реализовывать псевдонимы ещё один релиз, чтобы поле `[SerializeReference]`, объявленное проектом как один из них, по-прежнему десериализовалось. Убери базовый тип — и поле при загрузке стало бы `null` вообще без диагностики; поэтому это депрекация, а не удаление.

### Удалено

- Слой совместимости конвертеров с Unity до 2023.1: 117 пар-псевдонимов `using Converter = …`, 12 встроенных ветвлений с `ToConvertSpecific()`, 4 условных атрибута `[SerializeReference]`, 10 хелперов `GetConverter`, превратившихся после сворачивания в тождественные функции, и 8 веток `UNITY_6000_0_OR_NEWER`, называвших `PhysicMaterial` из времён до Unity 6. `package.json` объявляет `"unity": "6000.0"` начиная с 1.1.0, так что ничего из этого не компилировалось ни в одной поддерживаемой конфигурации.
- Внутренний `FloatingBackgroundElement` — заменён анимированным фоном из точек Aspid.FastTools.
- Интеграция `SerializeReferenceDropdown`: зависимость `com.alexeytaranov.serializereferencedropdown`, атрибуты `[SerializeReferenceDropdown]` над полями `[SerializeReference]`, ссылки в asmdef и version define `ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION`. На замену придёт встроенное решение из Aspid.FastTools.

### Исправлено

- Обратный биндинг больше не применяет *прямой* конвертер на пути во ViewModel. Биндер в `TwoWay` / `OneWayToSource` использует `ITwoWayConverter.ConvertBack`, когда она есть, и отправляет значение без изменений, когда нет.
- `SequenceConverter` больше не разыменовывает пустой слот в инспекторе. Пункт `<None>` в пикере типов — допустимый выбор, который сериализуется как null-элемент, поэтому пропуски пропускаются, а не приводят к исключению.
- Семейство `Vector3CombineConverter` больше не бросает `NullReferenceException`, когда ссылка на сцену не назначена или объект уничтожен. Оно сообщает о себе один раз и возвращает вход без изменений.
- `FormatException` из строки формата больше не обрывает список подписчиков биндера. Рассылка — голый multicast, поэтому один неверный формат раньше останавливал все биндеры, стоявшие за ним в очереди; `GenericToStringConverter` теперь удерживает сбой и откатывается на `ToString()`.
- `StringFormatConverter` снова форматирует null и пустые значения при включённом `_formatEmptyValues` — эта ветка стала недостижимой, когда `Convert` переехал в базовый класс.
- Диагностика деления на ноль в `ArithmeticNumberConverter` срабатывает один раз на экземпляр, а не на каждое преобразование. `Debug.LogError` захватывает стек вызовов, поэтому покадровый биндинг раньше стоил кадров.
- `DropdownOptionsByEnumMonoBinder` больше не обходит enum рефлексией на каждый push и не сбрасывает выбранный индекс у `DropdownValueMonoBinder` на том же объекте.
- `DebugLogBinder` / `DebugLogMonoBinder` больше не падают на null-значении: `?? value.ToString()` не различал «конвертер вернул null» и «значение null».
- Четыре конвертера `Double`, которые нельзя создать в инспекторе, скрыты из пикера; про этот регион забыли, когда размечали три соседних.
- Шесть голых `throw new ArgumentOutOfRangeException()` теперь называют аргумент и его значение.
- Приватные поля `[SerializeReference]` больше не пропадают из инспекторов `MonoView` / `MonoViewModel` / `MonoBinder`. Карта полей через рефлексию принимала приватное поле только с атрибутом `[SerializeField]`, поэтому полиморфное поле давало `FieldInfo` равный `null` и пропускалось на обоих проходах — `_converter` или `_customInteractable` у биндера просто не отрисовывался.
- Конвертеры, которые можно создать только из кода, больше не предлагаются пикером типов. `GenericFuncConverter` и приватные типы за `ToConvert` / `ToConvertSpecific` оборачивают делегат, который инспектор передать не может, поэтому их выбор давал экземпляр с `null` внутри; теперь они помечены `[TypeSelectorDisplay(Hidden = true)]`.

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
