---
name: starterkit-migration
description: Чеклист переноса скриптов StarterKit из `StarterKit/Unity/Runtime` в объединённый `StarterKit/Runtime` (этап 1, Runtime → New → Runtime, завершён) с одновременным ревью — раскладка папок (Core/General/Helpers), проверка имени против поведения, баги, нулабельность, Tooltip, XML-доки, форматирование. Использовать, когда пользователь присылает путь к папке или файлу внутри `StarterKit/Unity/Runtime` (или `StarterKit/Runtime` на ревью), говорит «переносим», «следующая папка», «продолжаем миграцию StarterKit», или правит перенесённый файл и просит «делай за мной».
---

# Миграция StarterKit → New

Цель: собрать весь StarterKit в одну сборку. Этап 1 (готово, коммит `d02142514` в `fix/generator-compiler-hang`): старый `Runtime` перенесён в `New`, `New` переименован обратно в `Runtime`. Этап 2 (текущий): `StarterKit/Unity/Runtime` → `StarterKit/Runtime`, по той же таксономии. Каждый файл при переносе проходит полный разбор. Публичное API ломать **можно** (у пользователя есть инструмент починки сериализованных ссылок). Правки вносятся сразу, без вопроса «внести?».

## Порядок работы над папкой

1. Прочитать все `.cs` в папке, найти использования вне неё (`grep` по StarterKit, Tests, Documentation, Samples~).
2. Перенести через `git mv` **вместе с `.meta`** (файлы и папки). Для новых папок создать `.meta` с `folderAsset: yes` и свежим guid.
3. Разложить по таксономии (ниже).
4. Пройти чеклист файла (ниже) по каждому файлу.
5. Обновить ссылки: другие скрипты, тесты, `Documentation/*.md`, `ConverterRenameCompatibilityTests` (замороженные имена).
6. Отчёт: таблица «файл → правки», отдельно «что тронуто вне папки» и «что не стал делать и почему».

## Таксономия папок внутри семейства (`Binders/`, `Converters/`)

| Папка | Содержимое | Примеры |
|---|---|---|
| `Core/` | Инфраструктурная основа семейства, на которой держится всё: базовые интерфейсы и декларативные атрибуты генерации | `IConverter`, `ITwoWayConverter`; `GenerateSerializableBinderAttribute`. **Не** `INumberBinder`/`IColorBinder`: они сокращают дублирование у части биндеров и остаются в `General/` |
| `General/` | Типы с логикой, общие для семейства | `Binder`, `TargetBinder`; `Fallback/ConverterFallback` + `ConverterFailureMode` |
| `Helpers/` | «Просто помощники»: enum-ы `*Mode`, логгеры | `ComparisonMode`, `ConverterLogger`, `BinderLogger` |
| Тематические (`Objects/`, `Bools/`, `Strings/`…) | Только конкретные реализации; абстрактные базы (`SwitcherMonoBinder`, `EnumMonoBinder`, `AddressableMonoBinder`) идут в `General/`, с подпапками `ToBool/`, `ToString/`, `ToValue/` по целевому типу | `Objects/ToBool/EqualityToBoolConverter` |

- **Без подпапки `Mono/` в тематических папках биндеров**: `Binder` и `MonoBinder` одного семейства лежат рядом (`Object/ObjectNameBinder`, `Object/ObjectNameMonoBinder`). Иначе в папках, где есть только Mono-версии, ради единообразия появлялся бы лишний уровень вложенности. `General/Mono/` остаётся, там подпапка отделяет абстрактные Mono-базы от сериализуемых.
- **Папки биндеров именуются в единственном числе**: `Binders/Command`, `Caster`, `Collection`, `Delegate`, `Value`, `General/Number`, `General/Mono/Enum`, `General/Mono/Addressable`. Исключение — `General` и `Helpers`. У конвертеров папки остались во множественном (`Converters/Numbers`, `Strings`).
- **Расширения лежат рядом с расширяемым типом**, не в `Helpers`: `ConverterFallbackExtensions` в `General/Fallback/`, `FuncConverterExtensions` рядом с `FuncConverter`.
- **Общее для всей сборки** (используют и биндеры, и конвертеры) — в корневой `New/Helpers/<Тема>/`: `Globalization/CultureInfoMode`, `Logging/LogMessageText`, `Numeric/NumericFormat`, `Numeric/NumericSaturation`, `Collections/EnumerableCountExtensions`.
- Критерий «общего»: **потенциальная** применимость, а не текущие вызовы. `EnumerableCountExtensions` (подсчёт `IEnumerable`) пока нужен только конвертерам, но это общий хелпер → `New/Helpers/Collections/`. `CultureInfoMode` нужен биндерам → корневой `Helpers`; `ComparisonMode` только конвертерам → `Converters/Helpers`.

## Чеклист файла

### Имя и назначение
- Имя типа должно само говорить, что он делает. Префикс `Generic` убирается: `GenericCasterBinder` → `CasterBinder`, `GenericToStringConverter` → `ValueToStringConverter`.
- Имя не должно врать об области применения: `ConverterMessageText`, который используют биндеры, стал `LogMessageText`.
- Enum-ы настроек именуются с суффиксом по роли: `*Mode` (`ComparisonMode`, `EnumMatchMode`), `*Operation` (`LogicOperation`, `AggregateOperation`); члены согласованы между собой (`Equal`/`NotEqual`, не `Equal`/`NotEquals`).
- Имя файла = имя типа. Display name в `TypeSelectorDisplay` согласован с именем класса.
- `typeparam` — `T`, не `TFrom`, если параметр один.

### Баги и API
- Тип возврата и параметры совпадают с интерфейсом по нулабельности (`Convert` возвращает `T?`, если поле `T?`).
- Если реализация уже принимает `null`, интерфейс объявляется как `IConverter<T?, …>`.
- Семейство конвертеров с одинаковым набором из 16 числовых интерфейсов и делегированием в `double` наследуется от `NumberConverter` / `TwoWayNumberConverter` (`Converters/General/Numbers`), переопределяя только `Apply` / `Undo`. Публичные `Convert(int/long/float/double)` даёт база.
- Парсеры чисел из строки наследуются от `StringToNumberConverter<T>` (`Strings/ToNumber`), переопределяя `TryParse`/`Clamp`/`ConvertBack`/`Expected`; `StringToDecimalConverter` отдельный (поля-строки).
- Повторяющийся try/catch форматирования (числа, DateTime, DateTimeOffset, TimeSpan) вынесен в `Helpers/Globalization/FormatExtensions.FormatOrGeneral<T>`; конвертация Unix-времени в обе стороны — в `Helpers/Time/UnixTime`; выбор часов по `DateTimeKind` — в `Helpers/Time/CurrentTime`.
- Дубликат целого типа сводить к наследнику базового (`EnumToValueConverter : DictionaryLookupConverter`), а не держать копию логики.
- Неиспользуемые члены и дубликаты после рефакторинга — **удалять**, не хранить «ради совместимости» (пример: `internal static Fail(mode, …)` в `ConverterFallback`).
- Недостающие реализации `IBinder<T>` / перегрузки конструктора добавлять (например, параметр `CultureInfoMode culture`).
- Конструктор по умолчанию: `protected` у не-sealed generic-типов (наследнику нужен доступный базовый ctor для Unity-сериализации), `private` у sealed, если пустой экземпляр = ошибка в runtime; `public` с `<remarks>Default: …</remarks>`, если пустой экземпляр валиден.
- Общие `New/Helpers/…` и `Converters/Helpers/…` из этого документа читать как `Runtime/Helpers/…` и `Runtime/Converters/Helpers/…`.
- Пакет требует Unity `6000.0`: гварды `#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION` и define TMP удалены из всего пакета, новые файлы их не получают; условными остаются только `ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION`, `ASPID_MVVM_ZENJECT_INTEGRATION`, `ASPID_MVVM_VCONTAINER_INTEGRATION`, `ASPID_MVVM_ADDRESSABLES_INTEGRATION`, `ASPID_MVVM_UNITASK_INTEGRATION`.
- Ошибки не глушатся: логировать при каждом появлении, `null` в биндере = сброс состояния.

### Код и форматирование
- Никаких `==`/`!=` между `float`/`double` (ReSharper CompareOfFloatsByEqualityOperator): сравнение с нулём через паттерн `value is 0d` / `is not 0f`, целость через `value % 1d is 0d`, равенство двух float через `Mathf.Approximately`.
- `#nullable enable` первой строкой, если в файле есть `?`-аннотации. Исключение: файлы с `MonoBehaviour`-классами (вью, инициализаторы, Mono-биндеры) пишутся без `#nullable enable` и без `?`-аннотаций.
- Порядок членов: сериализованные поля → обычные поля → автосвойства → конструкторы → события → свойства → методы.
- Конструктор с 2+ параметрами — параметры **в столбик**, скобка на строке имени. У методов — в столбик только когда сигнатура вылезает за правую границу (120 символов).
- Короткий guard-`if` с `return` — в одну строку: `if (!IsMissing(value)) return value;`
- `if` без фигурных скобок, тело которого не `return`, а действие (присваивание, вызов) — тело на следующей строке с отступом: `if (this.RequireFinite(value))` / `    CachedComponent.center = value;`. В одну строку только `return`.
- Expression-bodied метод: тело на **следующей строке** после `=>` (явные реализации, публичные `Convert`, приватные однострочники):
  ```csharp
  string IConverter<int, string>.Convert(int value) =>
      Convert(value);
  ```
- Тернарий в присваивании `var x = …` — в три строки (условие / `? a` / `: b`); внутри аргумента вызова и в `return` остаётся одной строкой.
- Тернарий как тело expression-bodied члена: условие остаётся на строке с `=>`, ветки на следующих строках с одним отступом (правило «тело на новой строке» здесь не действует):
  ```csharp
  public string? Convert(string? value) => string.IsNullOrWhiteSpace(value)
      ? value
      : Wrap(value, _color, _includeAlpha);
  ```
- Присваивания в теле конструктора — по возрастанию длины строки, самое длинное (`?? throw`) внизу.
- Цепочка `if … return` над одним значением → `switch`-выражение с реляционными паттернами.
- Инициализаторы массивов не переносятся ради ширины строки.
- Вызовы с 2+ аргументами у фолбэк- и лог-хелперов (`UseFallback`, `NumberText.Fallback`, `ConverterFallback.Fail`, `LogError`) пишутся **именованными аргументами в столбик**, а не позиционно в одну строку:
  ```csharp
  return this.UseFallback(
      fallback: _fallback,
      problem: value.Expected("an index"));
  ```
- `if` с телом из одного многострочного `return …(`…`)` берётся в фигурные скобки, даже если это единственный оператор.
- Многострочная конкатенация: `+` в конце строки.
- Без хвостовых пробелов, без строк из пробелов.

### Комментарии
- Комментарии в коде — **удалять целиком**, если объясняют «почему так» уже сказанное в XML-доке или очевидное. Оставлять только то, без чего код нельзя понять, и максимально коротко.

### Атрибуты-ограничения на сериализованных полях
- Там, где невалидное значение поля возможно, ставить `[Min]`, `[Range]` и подобные: количество элементов и индексы `[Min(0)]`, доли `[Range(0f, 1f)]`, знаменатели без нуля и т.д.
- Если атрибут исключает невалидное значение из Inspector, runtime-проверка того же условия удаляется, а конструктор бросает `ArgumentOutOfRangeException` (пример: `CountdownProgressConverter`, `RoundNumberConverter`).
- Ограничение атрибута согласовано с проверкой в конструкторе (`[Min(0)]` ↔ `ArgumentOutOfRangeException` для отрицательного) и с Tooltip.

### Tooltip
- На **всех** `[SerializeField]`/`[SerializeReference]` без исключений (в структурах — `[field: Tooltip]`).
- Одна короткая фраза, максимум две. Не пересказывать механику: «Culture for numbers and dates.», а не «The culture numbers and dates are formatted with. Defaults to the device locale.»
- Оговорка о поведении, если есть, стоит и в Tooltip поля, и в `<param>` парного параметра конструктора.

### XML-доки (см. также скилл `aspid-mvvm-xmldoc`)
- `<summary>` — одно предложение. На конструкторах `<summary>` не пишется, только `<param>` (+ `<remarks>` при необходимости).
- Каждый параметр задокументирован, включая добавленные в переопределения (`Format(T value, string format)` → есть `<param name="format">`).
- `<remarks>` только где действительно нужен, коротко, про сам тип — без истории и сравнений.
- `<exception>` — одна фраза: когда бросается, без объяснений про Inspector.
- Не сочетать `<inheritdoc cref>` с добавленными `<param>` (CS1573) — писать полный докблок.
- Без em-dash (`—`) в доках, использовать `:` или `,`.
- `#region`/`#endregion` не используются, даже при десятках явных реализаций интерфейсов.

## Что сделано (состояние)

Все пути ниже теперь под `StarterKit/Runtime/` (бывший `New`). Перенесено: `Binders/*` (Caster, Collection, Delegate, General, Helpers, Value), `Collections/*`, `Commands/*`, `Converters/{Core, General, Helpers, Bools, Objects, Composition, Collections, Enums, Numbers, Strings, Times}` целиком; базы `Converters/General/Numbers/{NumberConverter, TwoWayNumberConverter}` (в `Binders/General` — `Number/`), `Helpers/{Globalization (+FormatExtensions), Logging, Numeric, Collections, Enums, Time (UnixTime, CurrentTime)}`, `ViewModels/*`.

Этап 1 закрыт коммитом `d02142514`. Дальше: `StarterKit/Unity/Runtime` (~27 .cs в Converters + биндеры Mono и пр.) переезжает в `StarterKit/Runtime`; при переносе `Unity/Runtime/Converters/*` смотреть, нет ли дубликатов уже перенесённых типов (например `AnyToStringCasterMonoBinder` ↔ `AnyToStringCasterBinder`). Остальное в `Runtime/` и весь `Unity/Runtime` — смотреть `find StarterKit/Runtime StarterKit/Unity/Runtime -name '*.cs'`.

Этап 2, перенесено: остаток `Converters/{Localizations, Materials, RectOffsets, Rects}` → `Runtime/Converters/…` (папка `Unity/Runtime/Converters` пуста); `Converters/Bounds/*` → `Runtime/Converters/Bounds/{ToRect, ToVector}`; `Converters/Textures/*` → `Runtime/Converters/Textures/{корень, ToRect}`; `Converters/Quaternions/*` → `Runtime/Converters/Quaternions/{корень, ToNumber, ToVector}`; `Converters/Assets/*` (90 файлов, структура сохранена) → `Runtime/Converters/Assets/`; `Converters/Colors/*` (18 файлов) → `Runtime/Converters/Colors/{корень, ToVector}` (enum `ChannelOp` → `ChannelOperation`, `ColorBlend` → `ColorBlendMode`); `Converters/Vectors/*` (33 файла) → `Runtime/Converters/Vectors/{корень, Combine, ToNumber, ToQuaternion, ToRectOffset}`; вместе с ними `TransformGettersAndSetters` и anchored-часть `RectTransformGettersAndSetters` → `Runtime/Helpers/Transforms/`, остаток (`SetSizeDelta`) остался в Unity как `RectTransformSizeDeltaExtensions`; переименования: `Vector2Vector3Converter._values` → `_mode`, `_preConvertor/_postConvertor` → `_preConverter/_postConverter`; `Converters/Enums/*` → `Runtime/Converters/Enums/{ToCollection, ToString}` (asmdef: `Unity.TextMeshPro`; `EnumToDropdownOptionDataConverter.Entry` → `OptionEntry`); `Converters/Numbers/*` (19 файлов) → `Runtime/Converters/Numbers/{корень, ToColor, ToQuaternion, ToRectOffset, ToSprite, ToString, ToVector}` и вместе с ними `RectOffsets/RectSides` → `Runtime/Converters/RectOffsets/`, `Quaternions/RotationAxis` → `Runtime/Converters/Quaternions/` (папки под остальные конвертеры уже созданы); `Converters/Strings/*` → `Runtime/Converters/Strings/{LocalizedStringConverter, RichText, ToColor, ToSprite}` и вместе с ним `Colors/ToString/ColorToHtmlStringConverter` → `Runtime/Converters/Colors/ToString/` (взаимные internal-вызовы `Parse`/`Write`); asmdef получил `Unity.Localization` + versionDefine; `Converters/Objects/ToString/ObjectNameConverter` → `Runtime/Converters/Objects/ToString/`; `Components/UI/VirtualizedList` → `Runtime/Components/UI/`; `Utilities/ShaderPropertyId` → `Runtime/Binders/Helpers/`; `Commands/*` (`ColorCanExecuteHandler`, `GameObjectVisibleCanExecuteHandler`, `InteractableMode`) → `Runtime/Commands/{Core/ICanExecuteHandler, Helpers/InteractableMode, CanExecuteHandlers/*}`; `Views/*` → `Runtime/Views/{EventMonoView, Factories/{Core, Prefabs}, Initializers/{Core, Components}}`; asmdef `Aspid.MVVM.StarterKit` получил ссылки `Zenject`, `VContainer`, `Aspid.MVVM.Unity` и versionDefine VContainer (define Zenject задан в ProjectSettings). Editor-скрипты инициализаторов (`Unity/Editor/Views/Initializers`) ещё в старой сборке; Переименовано вместе с редактором: `InitializeComponent.GetComponent()` → `Resolve()`, свойство `Resolve` → `ResolveType`, поле `_resolve` → `_resolveType`, `_mono` → `_component`, enum `ResolveType.{Mono, References}` → `{Component, Reference}`, `ViewInitializerBase.GetFromInitializeComponent` → `Resolve`; `ViewInitializerEditor` сравнивает стадию по `enumNames`, а не по индексам. Редактор по-прежнему читает поля `_resolveType`, `_component`, `_reference`, `_scriptableObject`, `_typeName` по имени.

Перенесено из `Unity/Runtime/Binders`: `Mono/*` → `Runtime/Binders/General/Mono/{MonoBinder, FloatMonoBinder, IntMonoBinder, ObjectMonoBinder, SwitcherMonoBinder, Component/{ComponentMonoBinder, ComponentFloatMonoBinder, ComponentIntMonoBinder, ComponentObjectMonoBinder, ComponentToSourceMonoBinder}}`, `General/Mono/Enum/{EnumMonoBinder, EnumGroupMonoBinder}`, `Runtime/Binders/Command/{CommandMonoBinder, CommandBinderExtensions}` (последний из `Extensions/`), `General/Mono/Addressable/{AddressableMonoBinder, AddressableAssetLoader}`; asmdef получил `Unity.Addressables` + versionDefine. Переименовано: `MonoCommandBinder<T…>` → `CommandMonoBinder<T…>`. `RaiseNumberValueChanged` добавлен и во Float-варианты. Дублирующаяся логика загрузки Addressables вынесена в internal `AddressableAssetLoader<TAsset>`; неуспешная загрузка теперь логируется, а не молча ставит `default`.

`Binders/Objects/*` → `Runtime/Binders/Object/{ObjectNameBinder, ObjectNameMonoBinder}`; конструкторы `ObjectNameBinder` принимают `Object`, а не `GameObject`; у Mono-версии добавлен `CanBind` на пустую ссылку; в README режимы дополнены `OneWayToSource`.

`Binders/General/*` и `Binders/Vector/*` → `Runtime/Binders/General/{ObjectBinder, Target/TargetObjectBinder, Color/IColorBinder, Rotation/{IRotationBinder, IRotationReverseBinder}, Vector/{IVectorBinder, IVector2Binder, IVector3Binder, IVectorReverseBinder}}`; `IColorBinder` логирует нераспарсенную строку вместо молчаливого чёрного, пустая строка даёт `default`; `TargetObjectBinder` получил `[Serializable]`.

`Binders/Generation/GenerateSerializableBinderAttribute` → `Runtime/Binders/Core/` (генератор ищет атрибут по полному имени, сборка не важна). Leaf-генератор `[assembly: GenerateBinders]` удалён целиком по решению пользователя: атрибут, фикстура `Tests/EditMode/Fixtures/GeneratedBinderDeclarations`, `GeneratedBinderTests`, в сабмодуле `Generators/LeafBinders/LeafBinderGenerator` и `LeafBinderGeneratorTests`; DLL пересобран (Release). Сериализуемые двойники Mono-биндеров делаются только через `[GenerateSerializableBinder]`.

`Binders/Casters/*` → `Runtime/Binders/Caster/{ToString, ToBool, ToNumber, ToEnum, ToVector}`; девять копий «конвертер + UnityEvent + проверка на null» сведены к новой абстрактной базе `Caster/CasterMonoBinder<TFrom, TTo>` с хуком `CreateDefaultConverter()` (вызывается из `Reset` и `OnValidate`); `ToStringCasterMonoBinder<T>` → `ValueToStringCasterMonoBinder<T>` (по имени `ValueToStringCasterBinder<T>`), получил дефолтный `ValueToStringConverter<T>`; `AnyToStringCasterMonoBinder` остался отдельным (generic `SetValue<T>` из `IAnyBinder`); `ParsingCasterTests.Field` ищет поле по всей иерархии; README дополнен строками Int/Float/Enum.

`Binders/Collections/*` → `Runtime/Binders/Collection/`: базы `CollectionMonoBinder`, `ObservableListMonoBinder`, `ObservableDictionaryMonoBinder`, `ObservableCollectionMonoBinder` рядом с сериализуемыми, `Count/{CollectionCountMonoBinder, ObjectCollectionCountMonoBinder}`, `ViewModel/{Collection, ObservableCollection, ObservableDictionary, ObservableList}ViewModel(Mono)Binder` + `ObservableListViewModelBinderHelper`. Пользователь убрал generic-параметр `TViewFactory` у ViewModel-биндеров: поле фабрики типизируется интерфейсом `IViewFactory<TView>` / `IViewFactoryWithKey<TView>` напрямую, промежуточные классы `…<TView, IViewFactory<TView>>` удалены. Правки: Mono-базы списка и словаря обрабатывают многоэлементный Replace циклом, а не `NotImplementedException` (как Runtime-базы); сериализуемые ViewModel-биндеры получили `protected` конструкторы для десериализации; `ObservableDictionaryViewModelBinder` выровнен с Mono-версией (`TryGetValue`, `Deinitialize` перед `Release`, замена = release + create); `ObservableListViewModelMonoBinder` проверяет фабрику через `HasFactory()`; снят лишний `[BindModeOverride]` с `ObjectCollectionCountMonoBinder`; в документации `ViewModelObservableListMonoBinder` → `ObservableListViewModelMonoBinder`.

`Binders/Delegates/*` (`UnityCasterBinder`, `UnityDelegate{OneTime,OneWay,OneWayToSource,TwoWay}Binder`) удалены целиком: все пять были помечены `[Obsolete]` как копии `Runtime/Binders/{Delegate, Caster}` с `UnityAction` вместо `Action`, использований не было; таблица «Generic» в `Documentation/StarterKit/README.md` переведена на `Delegate*Binder<T>` и `CasterBinder<TFrom, TTo>`. Их тесты `Tests/EditMode/Binders/Delegates/*` удалены: `Delegate*Binder` уже покрыты `Tests/EditMode/Binders/Bases/Delegate{OneWayToSource,TwoWay}BinderTests`.

`Binders/Extensions/BinderMath` → `Runtime/Binders/Helpers/BinderMath` (добавлен `#nullable enable`, убраны em-dash); вместе с ним `Transforms/RectTransforms/SizeDeltaMode` → `Runtime/Helpers/Transforms/`, а `RectTransformSizeDeltaExtensions.SetSizeDelta` влит обратно в `Runtime/Helpers/Transforms/RectTransformGettersAndSetters` (класс-обёртка удалён). `Unity/Runtime/Binders/Extensions` пуст и удалён.

`Binders/Graphics/*` (21 файл) → `Runtime/Binders/Graphic/{Color, ColorComponent, Maskable, Material, RaycastTarget}` + `GraphicToSourceMonoBinder` в корне (папка `OneWayToSource` схлопнута); `ColorComponent` enum и `GraphicExtensions` положены в `ColorComponent/` рядом с единственными потребителями. `GraphicMaterialMonoBinder` переведён с `ComponentMonoBinder<Graphic, Material>` на `ComponentObjectMonoBinder<Graphic, Material>` (Material это UnityEngine.Object; генератор сериализуемого двойника подставит `TargetObjectBinder`). Доки сокращены, битые cref вида `{T1, T2}`/`{Graphic, Color}` приведены к именам параметров баз.

По предложению пользователя enum `ColorComponent` (один канал) слит с флагами `ColorChannels`: enum переехал в `Runtime/Helpers/Colors/`, рядом новый `ColorChannelsExtensions` (`SelectsAny`, `Color.With(channels, value)`, `Color.Get(channels)` читает первый выбранный канал); биндеры `GraphicColorComponent*` → `GraphicColorChannel*` в папке `Graphic/ColorChannel/`, поле `_colorComponent` → `_channels : ColorChannels`, пустая маска логируется через `GraphicExtensions`; README и `graphic-binders.md` обновлены.

`Binders/AudioSources/*` (78 файлов) → `Runtime/Binders/AudioSource/` (единственное число): папка `AudioClip` → `Clip`, `OneWayToSource/` схлопнута (`AudioSourceToSourceMonoBinder` в корне), два класса расширений `AudioSourceSetters` + `AudioSourceTimeSetters` слиты в `AudioSourceExtensions` в корне семейства (`SetTime`, `SetTimeSamples`, `SetMinMaxDistance`). `AudioSourceClipMonoBinder` и `AudioSourceOutputAudioMixerGroupMonoBinder` переведены на `ComponentObjectMonoBinder`; Switcher-варианты клампят в `SetValue`, а не в `GetConvertedValue`; `AudioSourceIsPlayingToSourceMonoBinder` стал `sealed`, лишний `partial` снят; у `PlayOneShot` убран runtime-`SafeClamp01` поля с `[Range(0,1)]`; `SetMinMaxDistance` проверяет конечность через `BinderMath.RequireFinite`, `ArgumentOutOfRangeException` получил имя параметра; добавлены `serializePropertyNames` (`m_audioClip`, `m_Volume`, `m_Pitch`, `Loop`, `Mute`, `DopplerLevel`, `Bypass*`, `OutputAudioMixerGroup`, `Pan2D`, `MinDistance`/`MaxDistance`); пути `AddComponentMenu` у IsPlaying/PlayOneShot выровнены на `Audio/AudioSource/`. Доки: README получил `OneWayToSource` в режимах, `audio-source-binders.md` — таблицу диапазонов.

`Binders/Colliders/*` (55 файлов) → `Runtime/Binders/Collider/`: общие биндеры `Colliders/Colliders/*` подняты в корень семейства, `BoxColliders`/`CapsuleColliders`/`MeshColliders`/`SphereColliders` → `Box`/`Capsule`/`Mesh`/`Sphere`, папки `OneWayToSource/` схлопнуты (ToSource в корне своей подпапки). `ColliderMaterialMonoBinder` и `MeshColliderMeshMonoBinder` переведены на `ComponentObjectMonoBinder`; `CapsuleColliderHeight` с `SafeClamp(0, MaxValue)` на `NonNegative`; все `*Center*` пишут только конечный вектор (`RequireFinite`); порядок атрибутов выровнен (Generate → ContextMenu → ComponentMenu), доки сведены к одной строке, битые cref `{BoxCollider, Vector3}`/`{T1, T2}` заменены именами параметров баз, en/em-dash убраны. Доки: README и `collider-binders.md` получили `OneWayToSource`, `PhysicsMaterial` и недостающие строки (ContactOffset, Include/ExcludeLayers, Height, Direction, CookingOptions).

`Binders/Colliders2D/*` (7 файлов) → `Runtime/Binders/Collider2D/{Density, IsTrigger, Material, Offset, Box/Size, Capsule/Size, Circle/Radius}`: `SafeClamp(value, 0f, float.MaxValue)` и покомпонентный `new Vector2(SafeClamp…)` заменены на `NonNegative`; `Offset` пишет через `if (RequireFinite) …` в две строки; cref `{BoxCollider2D, Vector2}`/`{T1, T2}` → имена параметров баз; remarks сокращены. В README и `collider-binders.md` добавлен раздел Collider2D (его раньше не было).

`Binders/Transforms/*` (33 файла) → два семейства `Runtime/Binders/Transform/{Position, Rotation, EulerAngles, Scale, Parent, SiblingIndex}` и `Runtime/Binders/RectTransform/{AnchoredPosition, AnchorMin, AnchorMax, OffsetMin, OffsetMax, Pivot, SizeDelta}`; опечатка папки `EulerAngels` исправлена, `OneWayToSource/` схлопнуты. Снят лишний `partial` с `TransformRotationMonoBinder`/`TransformEulerAnglesMonoBinder`; `Scale` и Vector2-свойства RectTransform пишут только конечные значения; в хелперы `SetPosition`/`SetEulerAngles`/`SetAnchoredPosition` (`Runtime/Helpers/Transforms`) добавлен `RequireFinite`, как в `SetSizeDelta`; `if (!RequireFinite) return; …` заменён на `if (RequireFinite) …` в две строки; Tooltip `_space` унифицирован; `serializePropertyNames: "m_AnchoredPosition"`/`"m_SizeDelta"` добавлены семействам AnchoredPosition/SizeDelta. Добавлены (новые файлы + .meta, не в индексе): Switcher/Enum/EnumGroup для `RectTransform{AnchorMin, AnchorMax, Pivot}` (9 файлов), `IFloatBinder` у `TransformScaleMonoBinder` (равномерный масштаб). Доки: README и `transform-binders.md`: `AnchoredPosition`/`SizeDelta` это `Vector3`, а не `Vector2`; добавлены строки AnchorMin/Max, OffsetMin/Max, Pivot, Parent, SiblingIndex.

`Binders/Renderers/*` (23 файла) → `Runtime/Binders/Renderer/{Enabled, Materials, MaterialsColor, PropertyBlock, ShadowCasting, SortingLayerName, SortingOrder}` + `RendererToSourceMonoBinder` и `RendererExtensions` (бывший `Extensions/RendererSetters`, оставлен один `SetMaterials(IReadOnlyCollection)`, params-перегрузка и ветка на один элемент удалены) в корне; `MaterialsColor/Mono/` схлопнута. Переименованы `RendererMaterialColorBinder` → `RendererMaterialsColorBinder`, `RendererMaterialColorSwitcherBinder` → `RendererMaterialsColorSwitcherBinder` (по имени Mono-версий; тесты их не используют, доки обновлены). Баги: `RendererMaterialsMonoBinder.OnBound` читал `material`/`materials` (инстанцировал копии и аллоцировал массив на каждой итерации) → `sharedMaterial`/`sharedMaterials`; `MaterialsColor` getter падал на пустом `sharedMaterial` → `default`; `SortingLayerName` молча игнорировал пустую строку → пишет `Default`; `serializePropertyNames` добавлены для `m_SortingOrder`/`m_SortingLayerID`. Доки: README получил недостающие строки (Enabled, ShadowCasting, SortingOrder/LayerName, PropertyBlock), `graphic-binders.md` раздел PropertyBlock.

`Binders/Texts/*` (30 файлов) → `Runtime/Binders/Text/{Alignment, AutoSize, CharacterSpacing, Font, FontSize, FontStyle, LineSpacing, Localization, Margin, MaxVisibleCharacters, RichText, Text}` + `TextToSourceMonoBinder` в корне; `Localizations/{Mono, Extensions}` схлопнуты в `Localization/` (все файлы под `#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION`). `TextFontMonoBinder` → `ComponentObjectMonoBinder`; `FontSize` Switcher/Enum/EnumGroup получили `RequireFinite`, как Mono; у `TextMonoBinder` снят дублирующий базу `[BindModeOverride]`, четыре числовых `SetValue` на `<inheritdoc/>`; в локализационных биндерах приватные `Subscribe`/`Unsubscribe` влиты в `OnEnable`/`OnDisable`/`OnBinding`/`OnUnbound`, `OnValidate` с `<inheritdoc/>`, доки сокращены; путь меню EnumGroup у Alignment/Font выровнен (лишний сегмент `/EnumGroup/`). Доки: README и `text-binders.md` получили строки FontStyle, AutoSize, RichText, CharacterSpacing, LineSpacing, Margin, MaxVisibleCharacters; режим `TextBinder` дополнен OneWayToSource.

Хелпер `Unity/Runtime/Binders/LocalizeStringEvents/Extensions/TableEntryReferences` (нужен и Text, и LocalizeStringEvents) → `Runtime/Helpers/Localization/TableEntryReferenceExtensions` (новая папка, .meta не в индексе); `Unity/Runtime/Binders/LocalizeStringEvents/Extensions` пуста и удалена.

`Binders/Dropdowns/*` (18 файлов) → `Runtime/Binders/Dropdown/{AlphaFadeSpeed, Command, Options, Value}` + `DropdownToSourceMonoBinder`; подпапки `Mono/` схлопнуты. `AlphaFadeSpeed`: `Mathf.Max(value, 0)` → `NonNegative` (Mono без `GetConvertedValue`); `DropdownCommandBinder` (4 класса): `ArgumentOutOfRangeException(nameof(mode))` → `nameof(interactableMode)`, конструктор `(target, mode)` получил `<param>`, доки конструкторов/SetValue сведены к `<inheritdoc/>` и одной строке; `DropdownOptionsBinder.SetValue(IEnumerable)` выровнен с Mono (`ClearOptions` вместо ручного `options ??= new; Clear()`); `DropdownOptionsMonoBinder` получил доки и `[BinderLog]`; `DropdownValueBinder` конструктор в столбик с `<param>`; `serializePropertyNames` `m_AlphaFadeSpeed`/`m_Value`/`m_Options` добавлены. Добавлен `Dropdown/DropdownExtensions.SetOptions` (новый файл + .meta, не в индексе): Options Switcher/Enum/EnumGroup раньше отдавали дропдауну свой сериализованный список по ссылке (`options = value`), теперь копируют его с сохранением выбора. Оба файла Command сгенерированы скриптом (`scratchpad/gen_dd.py`), 0..3 параметров. Доки: README (типы Options, OneWayToSource, строка OptionsByEnum), `dropdown-binders.md` (раздел OptionsByEnum).

`Binders/InputFields/*` (26 файлов) → `Runtime/Binders/InputField/{CaretPosition, CharacterLimit, CharacterValidation, Command, ContentType (бывш. Content), InputType, LineType, Placeholder, ReadOnly, Text}` + `InputFieldToSourceMonoBinder`, `UpdateInputFieldEvent` и новый `InputFieldExtensions` (+ .meta, не в индексе) в корне; `Mono/` схлопнуты. `InputFieldExtensions`: `GetEvent(UpdateInputFieldEvent)` заменил 12 копий `switch` по событиям, `RemoveListenerFromAll` заменил ручные пять `RemoveListener` в `OnValidate`, `RaiseNumber(ref NumberReverseChannel, text, culture)` заменил дублированный парсинг числа в `InputFieldBinder`/`InputFieldMonoBinder`. Баги: `ArgumentOutOfRangeException(nameof(mode))` → `nameof(interactableMode)` в 8 конструкторах Command; `_updateEvent =  updateEvent` (двойной пробел); `string?` в Mono-файле убран. Доки Command/Text сведены к `<inheritdoc/>`, конструкторы получили `<param>`/`<exception>`. Оба файла Command сгенерированы `scratchpad/gen_if.py`. Доки: README (строки CharacterLimit, CaretPosition, ReadOnly, Placeholder; Command принимает и `IRelayCommand`), `input-field-binders.md` (таблица прочих биндеров, раздел Command).

`Binders/LocalizeStringEvents/*` (6 файлов) → `Runtime/Binders/LocalizeStringEvent/{Entry, Variable}` + `LocalizeStringEventToSourceMonoBinder` (стал `sealed`, путь меню выровнен на `UI/`). `LocalizeStringEventVariableMonoBinder`: 13 одинаковых `SetValue` сведены к приватному `Set<TVariable, T>(T)` через `Variable<T>`; вместо `throw new InvalidCastException()` при переменной другого типа и вместо молчаливой работы с пустым именем теперь `LogError` и пропуск записи. Доки Entry-семейства ссылаются на `LocalizedString.TableEntryReference`. README: типы Entry/Variable уточнены, OneWayToSource.

Следующий шаг этапа 2: остаток `StarterKit/Unity/Runtime/Binders` (тематические биндеры, `Extensions/BinderMath`, asmdef, `AssemblyInfo`). Порядок работы тот же; при переносе биндеров учитывать:
- `ThresholdRichTextColorConverter` уже в `Runtime` и зовёт `RichTextColorConverter.Wrap` внутри одной сборки; `InternalsVisibleTo("Aspid.MVVM.StarterKit.Unity")` в `Runtime/AssemblyInfo.cs` можно будет снять, когда Unity-сборка исчезнет.
- Биндеры коллекций (`Binders/Collections/*`) используют `IViewFactory`/`IViewFactoryWithKey` и `PrefabViewFactory` из `Runtime/Views`; `VirtualizedListItemSourceMonoBinder` и `VirtualizedListToSourceMonoBinder` ссылаются на `VirtualizedList` из `Runtime/Components/UI`.
- Editor-скрипты StarterKit (`StarterKit/Unity/Editor`) остаются в своей сборке и ссылаются на `Aspid.MVVM.StarterKit`; после исчезновения `Aspid.MVVM.StarterKit.Unity` убрать её из references обоих Editor-asmdef и Tests-asmdef.
- MonoBehaviour-файлы переносить без `#nullable enable` и `?`-аннотаций.

Открытые хвосты:
- `PrefabViewPool` теперь реально прогревает `InitialCount` экземпляров при первом `Create`; проверить в PlayMode.
- `ViewInitializeComponent` и `ViewModelInitializeComponent` остаются копиями: `[TypeSelector(typeof(T))]` не выразить в generic-базе.
- `ConverterLogger` и `BinderLogger` — дословные копии; решить, сводить ли к общему писателю.
- Проверить в Editor, что Unity вызывает `OnAfterDeserialize` у `[SerializeReference]`-конвертера после правки в Inspector (на это полагаются кеши `TrimStringConverter`, `ThousandsSeparatorConverter`, `StringToVector2/3Converter`, `EnumFlagsToStringConverter`); иначе инвалидировать из `OnValidate` биндера.
- После переименований прогнать инструмент починки сериализованных ссылок (`GenericToStringConverter` → `ValueToStringConverter`, enum `Aggregate` → `AggregateOperation`, `NumberOperation.{Plus,Minus,Division}` → `{Add,Subtract,Divide}`, `EnumMatch` → `EnumMatchMode` с членом `NotEquals` → `NotEqual`, `EnumToValueConverter.Entry` → `LookupEntry`, `IndexMode` → `IndexOutOfRangeMode`, `StringMatch` → `StringMatchMode`, параметр `TimeSpanArithmeticConverter(operandSeconds)` → `operand` (поле `_operandSeconds` → `_operand`); удалён `DecimalFormatConverter`, его роль у `NumberFormatConverter` через `IConverter<decimal, string>`).
- `CollectionElementAtConverter` принимает только `IReadOnlyList`; для сетов/очередей без индексатора реализации нет.
- `CollectionCountToStringConverter` зависит от `PluralizeConverter`/`EnglishPluralRule` из `Runtime/Strings` (ещё не перенесены).
- `GameObjectNameMonoBinder` отказывается писать `null`, а `ObjectName*` пишут пустую строку; при переносе `GameObjects/Name` выровнять на пустую строку.
- `IVectorReverseBinder` и `IRotationReverseBinder` никем не реализуются; решить, оставлять ли их как API-заготовку.
- У `ObservableCollectionMonoBinder` нет сериализуемого двойника `ObservableCollectionBinder` (сет/очередь/стек) в `Runtime/Binders/Collection`.
- `RaiseNumberValueChanged` есть только у Mono-числовых баз; у `IntBinder`/`FloatBinder`/`TargetIntBinder`/`TargetFloatBinder` в `Runtime/Binders/General` его нет, стоит добавить для симметрии.
