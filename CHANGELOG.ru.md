# История изменений

Все значимые изменения **Aspid.MVVM** фиксируются в этом файле.

Формат основан на [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
проект придерживается [семантического версионирования](https://semver.org/spec/v2.0.0.html).

> 🌐 English version: [CHANGELOG.md](CHANGELOG.md)

---

## [1.1.0] — Не выпущено

Всё, что изменилось после `1.0.5`. Минимальная версия Unity — **`6000.0`**. Чеклист обновления: [MIGRATION.ru.md](MIGRATION.ru.md).
Превью `1.1.0-beta.1` опубликовано 2026-06-06 в канале `upm-preview`.

### Основное

- Генератор: bindable-свойства, `NotifyCanExecuteChangedAll()`, автогенерируемые поля биндеров, `[GenerateSerializableBinder]`.
- StarterKit: число типов биндеров выросло с ~360 до ~890 — uGUI, TextMeshPro, UI Toolkit, 2D, физика, аудио, анимация, свет и камеры.
- Конвертеры: каталог вырос с 17 до ~190, `ITwoWayConverter`, `ConverterAsset`, композиционные примитивы, форматирование с учётом культуры.
- Редактор: инспекторы на UI Toolkit, переписанный `DebugViewModelPanel`, окно настроек, type picker FastTools для `[SerializeReference]`.
- `MonoView` стал конкретным классом, `DynamicViewModel` — типизированный набор свойств, у `ViewInitializer` появилась DI-стадия конструирования.
- Пакет — встроенный UPM-пакет; `Aspid.Collections` и `Aspid.FastTools` подключаются как UPM git-зависимости; генераторы — в сабмодулях.
- Сайт документации на Docusaurus (`en` / `ru`), сэмплы перестроены в учебный путь, ~3000 EditMode-тестов.

### Добавлено

#### Ядро и генератор

- **Bindable-свойства** — `[Bind]` на свойствах, а не только на полях.
- **`NotifyCanExecuteChangedAll()`** — обновляет каждую команду с `CanExecute`, включая члены типа `IRelayCommand`.
- **Автогенерируемые поля биндеров** — слот `MonoBinder[]` для каждого bindable-члена `IView<TViewModel>`, не объявленного во View; отключается `[View(AutoBinderFields = false)]`.
- **`[GenerateSerializableBinder]`** — сериализуемый близнец `MonoBinder` генерируется; вручную пишется только MonoBehaviour-половина.
- `ValueViewModel` — ViewModel вокруг одного значения.
- `RelayCommand.EmptyExecution` и `GetSelfOrEmptyExecution()` — пустая команда для `null`-слота.
- В качестве design ViewModel можно выбрать интерфейс.
- Генератор принимает поля, названные ключевыми словами C#.
- `ITwoWayConverter<TFrom, TTo>` — биндер в `TwoWay` / `OneWayToSource` применяет `ConvertBack` и предупреждает, если у конвертера его нет.
- Необобщённый `IConverter` — корень иерархии конвертеров.
- Базы свойств `TargetBinder<TTarget, TProperty>` / `ComponentMonoBinder<TComponent, TProperty>` со слотом конвертера, плюс `Target{Int,Float,Object}Binder` / `Component{Int,Float,Object}MonoBinder`; уничтоженный объект передаётся как `null` в обе стороны.
- `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder` — дополнительные перегрузки `SetValue` как default-члены интерфейсов с насыщением на границах типа.
- `INumberReverseBinder` / `NumberReverseChannel` — один обратный канал для `int` / `long` / `float` / `double` с насыщением на границах типа.
- `BinderMath.SafeClamp` / `SafeClamp01` / `NonNegative` / `RequireFinite` и `BinderLogger` — неконечное значение ограничивается и репортится с именем биндера.
- `MonoBinder.DefaultMode` — режим, с которого стартует биндер, добавленный через инспектор; применяется из `Reset`.
- `ComponentMonoBinder.ResolveComponent` — сужает фолбэк `GetComponent`, чтобы биндер на базовом типе не находил самого себя.
- `ComponentIntMonoBinder<T>.RaiseNumberValueChanged` — `int`-биндер может писать во `float`-поле ViewModel.
- Расширение `Selectable.SetInteractable` — одна реализация `InteractableMode` для всех command-биндеров.
- `DebugLogBinder` / `DebugLogMonoBinder` — логируют привязанное значение; сообщение вырезается из релизной сборки.
- `IAnyReverseBinder` — обратный биндер для любого типа; `null` передаётся, а не бросается.
- Хуки `CollectionBinder<T>` `OnAdded`, `OnRemoved`, `OnReplaced`, `OnMoved` — гранулярные изменения с чистой отпиской в `Unbind` / `Dispose`.
- Перегрузки `BindSafely` / `UnbindSafely` с `owner` / `memberName`; ошибки репортятся с View, членом и индексом.
- `[HeaderGroup]`, `[HeaderGroupStart]` / `[HeaderGroupEnd]` — сворачиваемые группы в инспекторах биндера и ViewModel; вырезаются из плеерных сборок.
- `Source/Compatibility/UnityAttributesShim.cs` — `[SerializeField]`, `[SerializeReference]`, `[Tooltip]` компилируются вне Unity без `#if`.

#### Views

- Поддержка `RelayCommand` внутри `View` / `MonoView`; рефакторинг `CommandsContainer`.
- Переработка `ViewInitializer`: общий `ViewInitializerBase`, ленивые `Views` / `ViewModel` в edit mode, `TryResolve` для контейнеров, новая стадия `InitializeStage.DiConstructor`.
- `DestroyView()` уничтожает только компонент View; для старого поведения — `DestroyViewAndGameObject()`.
- `PrefabViewFactory` / `PrefabViewPool` обновлены; `PrefabViewPool` стал обобщённым.
- `ViewModelPickerWindow` с выпадающим списком и навигацией; обновлён `DesignViewModel`.
- `[AddComponentMenu]` для `MonoView`.

#### Редактор

- Инспекторы на UI Toolkit для `MonoBinder`, `MonoView`, `MonoViewModel`; общие `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine`, `AspidToggle`.
- `DebugViewModelPanel` переписан: вкладки, сохраняемый поиск по имени и типу, поддержка `RelayCommand`, bindable- и автосвойств.
- Окно `Aspid.MVVM Settings` в стиле FastTools Welcome, в меню `Tools/Aspid 🐍`; версия читается из манифеста пакета.
- Поля `[SerializeReference]` — конвертеры, фильтры, сортировки, обработчики, фабрики View, `PluralRule` — рисуются type picker'ом FastTools без атрибутов.
- Drag & Drop для неназначенных биндеров с группировкой, Auto-Assign и Select / Restore.
- `[RequireBinder]` и валидация дочерних View / биндеров.
- `EnumMonoBinderEditor`; исправления drawer'а `EnumValues`.
- Drawer `BindMode`: режим пишется безусловно, label передаётся, смешанное значение при мультивыделении, нет записей во время layout.
- Биндеры лежат в одной ветке меню `Aspid/MVVM/Binders` с единым именованием `Component – Property`; закреплено контрактными тестами.

#### Биндеры

Каждое семейство включает сериализуемый биндер и `MonoBinder`; большинство — ещё `Enum`, `EnumGroup` и `Switcher`.

- **UI Toolkit** — `VisualElementMonoBinder<TElement>` находит элемент в `UIDocument` по имени или USS-классу; `Label Text`, `Display`, `Enabled`, `Class`, `Button Command`, двусторонние `Slider Value` и `TextField Value`, `ListView.itemsSource` над observable-коллекцией.
- **Коллекции** — `ObservableCollectionMonoBinder<T>` и `ObservableCollectionViewModelMonoBinder` для set, queue и stack; `ObservableDictionaryMonoBinder<TKey, TValue>` и `ObservableDictionaryViewModelMonoBinder`; `CollectionCountMonoBinder<T>` отдаёт количество и пустоту.
- **Агрегаторы** — `AndBool`, `OrBool`, `FormatString` над `BoolAggregatorInput` / `StringAggregatorInput`; `ConditionalFloat`, `ConditionalString`, `ConditionalColor` выбирают одно из двух значений инспектора по привязанному `bool`.
- **Ограничение частоты** — кастеры `Debounce`, `Throttle`, `Delay` для `Float` и `String`, по умолчанию на unscaled-времени.
- **Твины** — `TweenFloat`, `TweenColor`, `TweenVector3` сглаживают каждое значение и перенацеливаются на лету; первое значение проходит мгновенно.
- **Кастеры** — `StringToInt`, `StringToFloat`, `StringToEnum<TEnum>` парсят привязанную строку: сначала культура пользователя, затем инвариантная; разделители групп, `NaN` и `Infinity` отклоняются.
- **Команды** — готовые `ButtonCommandInt` / `Float` / `String` / `Bool` / `Object`; `interactable` следует `CanExecute` для этого параметра.
- **ToSource** — семейство `…ToSourceMonoBinder` отдаёт ViewModel сам компонент; `SelectableToSource`, `GameObjectToSource`, `AudioSourceIsPlayingToSource`.
- **Глобальное состояние** — `Time.timeScale`, уровень `QualitySettings`, `Application.targetFrameRate`, `Screen.fullScreen`.
- **GameObject / Transform** — `layer`, `parent` (сохраняет локальную позицию), sibling index, `Object.name`.
- **RectTransform** — `anchorMin`, `anchorMax`, `pivot`, `offsetMin`, `offsetMax`; `sizeDelta` отдаёт ещё и `Vector2`.
- **Canvas и layout** — `Canvas.sortingOrder` / `overrideSorting`; `LayoutElement` preferred / flexible размеры и `ignoreLayout`; биндеры `LayoutGroup`; `CanvasScaler` режим, масштаб, разрешение, match; `GridLayoutGroup` размер ячейки, отступы, constraint; оси `ContentSizeFitter`; `AspectRatioFitter` режим и соотношение; `RectMask2D.padding`.
- **Graphic** — `Graphic.raycastTarget`, `MaskableGraphic.maskable`, `Mask.showMaskGraphic`, цвет и смещение `Shadow` / `Outline`, `GraphicMaterial` для любого `Graphic`.
- **Image / RawImage** — `type`, `preserveAspect`, `fillOrigin`, `fillClockwise`, `RawImage.uvRect`.
- **Selectable** — `transition`, `targetGraphic`, биндеры `Dropdown` и `Selectable`, `ToggleGroup.allowSwitchOff`.
- **Toggle** — `Enum` / `EnumGroup` для `isOn`, запись через `SetIsOnWithoutNotify`.
- **Slider / Scrollbar** — `Scrollbar.value` (`TwoWay`, `OneWayToSource`, все четыре числовых типа), `Scrollbar.size`.
- **ScrollRect** — вертикальная и горизонтальная нормализованная позиция, `normalizedPosition`, флаги включения осей.
- **Dropdown** — `TMP_Dropdown.value` двусторонний; биндеры опций обновляют подпись и сохраняют выбор.
- **Text / InputField** — `fontStyle`, `enableAutoSizing`, `characterSpacing`, `lineSpacing`, `margin`, `maxVisibleCharacters`, `richText`; `caretPosition`, `placeholder`, `characterLimit`, `readOnly`; дополнительные биндеры InputField.
- **Object** — биндеры `Object.name`.
- **SpriteRenderer** — `sprite`, `color`, `flipX`, `flipY`, `sortingOrder`, `size`.
- **Renderer** — `enabled`, `sortingOrder`, `sortingLayerName` (неизвестное имя репортится), `shadowCastingMode`; `RendererPropertyBlock` `Float` / `Color` / `Vector` / `Texture` пишут через `MaterialPropertyBlock`, не размножая материал.
- **LineRenderer** — `widthMultiplier`, `loop`.
- **Light** — `color`, `intensity`, `range`, `spotAngle`.
- **Camera** — `fieldOfView`, `orthographicSize`, `backgroundColor`, `orthographic`.
- **Rigidbody** — `mass`, `useGravity`, `isKinematic`, `constraints`; **Rigidbody2D** — `mass`, `gravityScale`, `simulated`, `bodyType`.
- **Collider** — `CapsuleCollider.height` / `direction`, `contactOffset`, `includeLayers`, `excludeLayers`, `MeshCollider.cookingOptions`; **Collider2D** — `isTrigger`, `offset`, `density`, `sharedMaterial`, `BoxCollider2D.size`, `CircleCollider2D.radius`, `CapsuleCollider2D.size`.
- **AudioSource** — биндеры свойств, `Play` / `Stop` / `Pause` / `UnPause` как `Action` или `IRelayCommand`, `PlayOneShot` на каждый опубликованный клип, `IsPlayingToSource`.
- **AudioMixer / AudioListener** — открытый float-параметр, снапшот по индексу или имени, `AudioListener.volume` / `pause`.
- **Animator** — `speed`, вес слоя, `runtimeAnimatorController`, проигрывание состояния по имени, сброс триггера; имена параметров проверяются один раз на контроллер.
- **ParticleSystem** — `Play` / `Stop` / `Pause` / `Clear`, включение эмиссии, множитель скорости эмиссии, начальный цвет.
- **VideoPlayer** — `clip`, `playbackSpeed` (0..10), `isLooping`; **NavMeshAgent** — `speed`, `isStopped`.
- **Addressables** — опциональная бесшовная замена с защитой от уничтоженного объекта; `GameObjectInstantiateAddressableMonoBinder`.

#### Конвертеры

- **Каталог** — ~190 конвертеров, сгруппированных в picker'е: `Aspid/Bool`, `Number`, `String`, `Time`, `Color`, `Vector`, `Rotation`, `Collection`, `Enum`, `Object`, `Texture`, `Layout`, `Localization`, `Asset`, `Composition`.
- **Двусторонние** — конвертер с `ConvertBack` работает в обе стороны в `TwoWay` / `OneWayToSource`; десятки конвертеров его реализуют.
- **Композиция** — `Compose`, `Cached`, `Safe`, `NullGuard`, `Conditional`, `Passthrough`, `Sequence`; `Safe` и `Cached` двусторонние.
- **`ConverterAsset<TFrom, TTo>`** — конвертер как `ScriptableObject`, подключаемый через `ConverterAssetReference`; готовые подклассы в **Create → Aspid → MVVM → Converters**.
- **Обработка ошибок** — значение, которое нельзя сконвертировать, репортится при каждой конвертации и заменяется настроенным фолбэком; `ConverterFailureMode` выбирает фолбэк или вход там, где типы совпадают.
- **`PluralRule`** — подключаемая грамматика множественного числа (`SingleForm`, `English`, `French`, `EastSlavic`, `Polish`, `Czech`, `Arabic`); новый язык добавляется подклассом.
- **`CultureInfoMode`** у каждого строкового и парсящего конвертера, с `InvariantCulture` для round-trip.
- **Числовая ширина** — числовые, векторные и Unity-конвертеры принимают `int`, `long`, `float` и `double`; целочисленные пути отбрасывают дробь и насыщаются.
- **Векторное семейство** — один конвертер на операцию обслуживает `Vector2`, `Vector3` и `Vector4`; `VectorSwizzleConverter` заменяет swizzle по размерностям.
- `BoolInvertConverter` — инверсия для bool-биндеров, применяется в обе стороны.

#### Сэмплы и документация

- Сэмплы перестроены в учебный путь: `01. Counter` → `06. CustomBinder`, плюс `VirtualizedList`, `DynamicViewModel`, `DiIntegration`, `ExampleScripts`; у каждого `README`, свой `.asmdef` и компактный шрифт `Sample SDF`.
- Сайт документации на Docusaurus (GitHub Pages): английский оригинал с русским переводом, туториалы в README сэмплов, API-справка DocFX.
- XML-документация на всех публичных биндерах, конвертерах и типах редактора; у каждого сериализуемого поля есть `[Tooltip]`; все четыре сборки собираются с `-doc` без ошибок.
- `Documentation/08-converters.md` переписан под актуальный каталог.
- ~3000 EditMode-тестов, включая контрактные: tooltip'ы, группы picker'а, пути меню, имена свойств в контекстном меню, разрешение `<include>`, документация guard'ов `BindMode`.

#### Проект и инфраструктура

- Пакет стал встроенным UPM-пакетом в `Packages/tech.aspid.mvvm`; Unity-проект перенесён в `Aspid.MVVM/`.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators` — git-сабмодули.
- `Aspid.Collections` (`tech.aspid.collections`) и `Aspid.FastTools` (`tech.aspid.fasttools`, тег `upm-preview/1.0.0-rc.7`) — UPM git-зависимости.
- Release workflow публикует поддеревья `upm` (stable) и `upm-preview` с неизменяемыми тегами `upm/<version>`, проверяет дрейф DLL генераторов и берёт release notes из этого файла.
- Workflow Claude PR Assistant и Code Review; автоматизация редактора через Unity CLI с сервером `.mcp.json`; корневой `CLAUDE.md`.
- Целевой редактор `6000.4.0f1`.

### Изменено

- **Ломающее:** минимальная версия Unity — `6000.0`.
- **Ломающее:** `MonoView` стал конкретным — список биндеров, валидация детей и `[RequireBinder]` живут в нём; наследники продолжают работать.
- **Ломающее:** `MonoView.Dispose()` только вызывает `Deinitialize()`; GameObject уничтожайте сами.
- **Ломающее:** `DestroyView()` больше не уничтожает GameObject.
- **Ломающее:** `MonoBinder.Bind()` на уже привязанном биндере логирует ошибку вместо исключения.
- **Ломающее:** числовые и векторные перегрузки `SetValue` — default-члены интерфейсов `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder`; доступ через интерфейс: `((IBinder<float>)binder).SetValue(5f)`.
- **Ломающее:** базы свойств — `TargetBinder<TTarget, TProperty>` и `ComponentMonoBinder<TComponent, TProperty>`; обе несут слот конвертера `[SerializeReference]`, передаваемый между target и mode.
- **Ломающее:** числовые биндеры поднимают одно `IReverseBinder<T>.ValueChanged` на каждую ширину через `INumberReverseBinder`; подписка — `((IReverseBinder<float>)binder).ValueChanged`.
- **Ломающее:** методы `BinderMath` — расширения `IBinder` (с перегрузкой для `Type`), чтобы санитизированное значение называло свой биндер.
- **Ломающее:** bool-биндеры несут слот `IConverter<bool, bool>` вместо `_isInvert`; сохранённая инверсия настраивается заново через `BoolInvertConverter`. Биндеры `*ByBind` сохраняют флаг.
- **Ломающее:** `DynamicViewModel` — типизированный набор свойств: `Add<T>` / `Get<T>` / `TryGet<T>` выдают `IDynamicProperty<T>` с `Value` и `ValueChanged`; все четыре `BindMode` на одном типе свойства.
- **Ломающее:** `Aspid.MVVM.StarterKit.Unity` и его Editor-сборка влиты в `Aspid.MVVM.StarterKit` / `Aspid.MVVM.StarterKit.Editor`; пространства имён не менялись.
- **Ломающее:** пути `[AddComponentMenu]` в единственном числе и с длинным тире: `Collection/Observable List Binder – ViewModel`.
- **Ломающее:** `ParseHtmlStringConverter` репортит нераспознанный цвет; фолбэк переехал из `_defaultColor` в `_fallback` и настраивается заново.
- **Ломающее:** у переименованных типов нет `[MovedFrom]`; слот `[SerializeReference]` со старым именем типа нужно прогнать через инструмент починки.
- `ValueToStringConverter<T>` (был `GenericToString<TFrom>`) выносит форматирование в виртуальный хук `Format(T value, string format)`; пустой или пробельный формат откатывается к `ToString()`.
- `ArithmeticNumberConverter` — `sealed`, открывает `Apply(double)` / `Undo(double)`; коэффициент по умолчанию `1`.
- `Vector2ToVector3Converter` и `Vector3ToVector2Converter` стали одним двусторонним `Vector2Vector3Converter`; `Vector2/3SubstitutionConverter` стал `VectorSwizzleConverter`.
- `NumberToBoolConverter` стал `NumberCompareConverter`, `Comparisons` — `ComparisonMode` (`Inequality` → `NotEqual`); порог расширен до `double` и настраивается заново.
- `ConverterExtensions.ToConvert` → `FuncConverterExtensions.ToConverter`.
- `CultureInfoMode` и `ToCultureStringExtensions` переехали в `StarterKit/Runtime/Globalization`; пространство имён прежнее.
- Строка из пробелов считается пустой во всех строковых конвертерах.
- Цветовые биндеры Renderer читают `sharedMaterial` и кэшируют массив `materials`; биндеры материала коллайдера читают `sharedMaterial` — чтение больше не создаёт копий.
- Диапазоны Slider и AudioSource: перевёрнутая пара `min` / `max` меняется местами с записью в лог, неконечная граница отклоняется.
- Биндеры Slider, Scrollbar и Dropdown отдают то значение, которое контрол реально принял после ограничения Unity.
- Сэмпл Greeter использует штатный `RichTextColorConverter`.
- Атрибуты инспектора в слоях без зависимости от Unity больше не обёрнуты в `#if UNITY_2022_1_OR_NEWER`.

#### Переименовано

GUID в `.meta` сохранены, сцены и префабы продолжают работать. Ссылки в исходниках нужно обновить.

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` / `…Binder` | `ObservableListViewModelMonoBinder` / `…Binder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` / `…Binder` | `CollectionViewModelMonoBinder` / `…Binder` |
| `CollectionBinderBase<T>` | `CollectionBinder<T>` |
| `OneWayValue<T>`, `OneTimeValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` | `ValueOneWayBinder<T>`, `ValueOneTimeBinder<T>`, `ValueTwoWayBinder<T>`, `ValueOneWayToSourceBinder<T>` |
| `Generic*Binder`, `UnityGeneric*Binder` | `Delegate*Binder` |
| `GenericCasterBinder`, `GenericToStringCasterBinder` | `CasterBinder`, `ValueToStringCasterBinder` |
| `MonoCommandBinder` | `CommandMonoBinder` |
| `ScrollBarCommandMonoBinder` | `ScrollbarCommandMonoBinder` |
| `RawImageMaterial*Binder` | `GraphicMaterial*Binder` |
| `RendererMaterialColorBinder` / `…SwitcherBinder` | `RendererMaterialsColorBinder` / `…SwitcherBinder` |
| `SliderValueMode` | `SliderRangeMode` |
| `ICanExecuteView`, `ColorInteractable`, `GameObjectVisibleInteractable`, `SequenceCanExecuteView` | `ICanExecuteHandler`, `ColorCanExecuteHandler`, `GameObjectVisibleCanExecuteHandler`, `SequenceCanExecuteHandler` |
| `RectTransformSetters`, `TransformSetters` | `RectTransformGettersAndSetters`, `TransformGettersAndSetters` |
| `Binder.IsBind` / `MonoBinder.IsBind` | `CanBind` |
| `ObservableListBinder.GetFilterList` | `GetFilteredList` |
| `IMonoBinderValidable.IsMonoExist`, `ValidableBindersById` | `IMonoBinderValidatable.IsMonoAlive`, `ValidatableBindersById` |
| `BinderFieldInfoExtensions.GetBinderId` | `BinderIdUtility.FromFieldName` (Editor-сборка) |
| `ViewModelDebugPanel` | `DebugViewModelPanel` |
| `GenericFuncConverter`, `GenericToString`, `SequenceConverters` | `FuncConverter`, `ValueToStringConverter`, `SequenceConverter` |
| `TimeSpanToStringConverter`, `ObjectToStringConverter`, `ObjectNullToBoolConverter` | `TimeSpanFormatConverter`, `ValueToStringConverter<object>`, `EqualityToBoolConverter<T>` |

### Удалено

- `AddComponentContextMenuAttribute` — используйте `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` (`Path = "..."`).
- `AddPropertyContextMenu` — меню свойств обрабатывает пайплайн редактора.
- Исходники `Aspid.Collections` внутри пакета — теперь пакет `tech.aspid.collections`; вместе с ними `IViewModelCollectionFilter`, составные фильтры и компараторы коллекций.
- Интеграция `SerializeReferenceDropdown` и её зависимость — заменены type picker'ом FastTools.
- `DynamicPropertyData<T>`, `DynamicPropertyFactory`, `OneWayDynamicProperty<T>`, `TwoWayDynamicProperty<T>`, `OneTimeDynamicProperty<T>`, `DynamicViewModel.Create<…>`.
- 40 алиасов `IConverterXToY` и 70 обёрток `ToConvert` / `ToConvertSpecific` — используйте `IConverter<TFrom, TTo>` и `ToConverter<TFrom, TTo>`; поле `[SerializeReference]`, объявленное алиасом, нужно перетипизировать.
- `IBindableValue<T>`, `IReadOnlyBindableValue<T>` — используйте `Value` у value-биндеров.
- `GenericToString<TFrom>.ToStringValue` — переопределяйте `Format(T value, string format)` у `ValueToStringConverter<T>`.
- Слой совместимости с Unity до 6: алиасы `using Converter = …`, ветки `ToConvertSpecific()`, ветки `UNITY_6000_0_OR_NEWER`, фолбэки `PhysicMaterial`.

### Исправлено

#### Ядро

- `TwoWayValue<T>` (теперь `ValueTwoWayBinder<T>`) возвращал каждое обновление ViewModel → View обратно во ViewModel уже сконвертированным.
- `OneWayToSource` не доходил до ViewModel для собственного типа числового биндера; остальные три ширины работали.
- Обратная привязка повторно применяла *прямой* конвертер.
- Числовые обратные каналы приводили значения вне диапазона через неопределённый каст; теперь насыщаются.
- `SetValue(Vector3)` на Vector3-биндере выбирал перегрузку `Vector2` и терял Z.
- `BindSafely` / `UnbindSafely` останавливались на первом бросающем или уничтоженном биндере; теперь он репортится и пропускается.
- Уничтоженный `MonoBinder` оставался подписанным — `OnDestroy` теперь отвязывает.
- `MonoBinder` без компонента привязывался и бросал из сеттера свойства; `CanBind` отвечает по найденному компоненту.
- `TargetBinder` с уничтоженным сериализованным target бросал из `OnBound`; проверка использует приведение Unity.
- `ComponentMonoBinder` пропускал фолбэк `GetComponent` для битой ссылки, и `OnValidate` её не лечил.
- Биндер, добавленный через инспектор, стартовал в `TwoWay`, даже если его `[BindMode]` это запрещал.
- Behaviour-биндеры находили самих себя в качестве target.
- `EnumGroup`-биндеры бросали на неназначенной записи и оставляли остаток группы несогласованным.
- Echo-guard у Toggle, Slider и InputField не восстанавливался после исключения, навсегда глуша обратный канал.
- Биндеры InputField подписывались из `OnValidate` без привязки, накапливая дубликаты слушателей.
- Целочисленный канал InputField молчал для числа, не влезающего в `int`; неудачный целочисленный парсинг глушил и float-каналы.
- `NaN` и бесконечности проходили через каждый `Mathf.Clamp` в `alpha`, `fillAmount`, `pitch` и ещё 45 мест.
- Коллайдеры принимали отрицательные размеры; `AudioSource.time` / `timeSamples` перематывали за конец клипа с ошибкой каждый кадр.
- `SliderCommandBinder` принимал только `IRelayCommand<float>`; команды `int`, `long` и `double` бросали.
- Command-биндеры Slider / Scrollbar переинтерпретировали `float` как `T` через `Unsafe.As` — мусорный `CanExecute` для `long` / `double`.
- `UnityEventStringMonoBinder` бросал на `null`.
- Генератор игнорировал ограничения `enum` / `struct` на обобщённых bindable-членах.
- Guard `ProfilerMarker` в `MonoBinder.Unbind()` ломал компиляцию до Unity 2022.1.

#### Коллекции

- `CollectionMonoBinder<T>` применял коллекцию один раз и не следил за `CollectionChanged`.
- `CollectionViewModelMonoBinder<T>` выходил за массив View, когда коллекция его перерастала.
- View, вставленный в середину списка, становился последним в иерархии; теперь занимает соответствующий sibling index.
- `ObservableListBinder` отписывался не от того списка, на который подписывался, — утечка подписки.
- Проверка границ `VirtualizedList` ошибалась на единицу; исправления dispose в `VirtualizedListItemSourceBinder`; исправления `FilteredList`.

#### Биндеры

- Значения `Slider`, ограниченные Unity, не доходили до ViewModel — echo-guard глотал коррекцию.
- `InteractableMode.Visible` у `*CommandMonoBinder` скрывал GameObject самого биндера, а не target.
- `InteractableMode.Custom` без `ICanExecuteView` бросал из `CanExecuteChanged`, обрывая остальных подписчиков.
- Десять Transform-биндеров писали в собственный `transform`, а не в назначенный компонент.
- Выключенное «disabled when null» принудительно включало компонент на каждом значении в 14 биндерах спрайтов и текстур.
- `int`-параметры Animator сравнивались через `Mathf.Approximately`, и изменение на единицу за миллионом пропускалось.
- Биндеры Animator сбрасывали параметр в ноль из `OnEnable` до прихода значения.
- Биндеры Animator писали в отсутствующий параметр с ошибкой каждый кадр; имя и тип проверяются один раз.
- Биндеры материалов `Renderer` бросали на пустом наборе; цветовые биндеры `LineRenderer` бросали на режиме по умолчанию.
- `TextFontSwitcherBinder` не был `[Serializable]` и не появлялся в инспекторе.
- Биндеры записей локализации молча отдавали `null` для записи по id; теперь биндер объясняет причину.
- `RectTransformSetters.SetSizeDelta` отдавал `Vector3(w, h, 0)` в `OneWayToSource`; добавлен канал `Vector2`.
- `min` / `max` у `AudioSource` и `Slider` могли остаться перевёрнутыми.

#### Конвертеры

- `Inequality` в `NumberToBoolConverter` (теперь `NumberCompareConverter`) возвращал тот же результат, что `Equal`.
- `SequenceConverter` разыменовывал пустой слот инспектора.
- Семейство `Vector3CombineConverter` бросало при неназначенной или уничтоженной ссылке на сцену.
- `FormatException` в `GenericToString` обрывал список подписчиков биндера; теперь откат к `ToString()`.

#### Редактор

- `MonoBinderEditor.OnDisable` бросал для View, не являющегося `MonoView`.
- Выпадающий список `BindMode` сохранял выбор только для rebindable-владельцев и делил один кэш на всё мультивыделение.
- Контекстное меню Add Binder передавало в `AddComponent` абстрактный тип target для полей TextMeshPro.
- Пути сэмплов в `package.json` указывали на несуществующую папку, и импорт давал пустой результат.

---

## [1.0.5] — 2025-10-17

### Добавлено
- Биндеры TextMeshPro: `TextFontBinder`, `TextFontSwitcherBinder`, `TextAlignmentBinder`, `TextAlignmentSwitcherBinder` и Mono-варианты (PR #30).
- Биндеры Unity Localization: `LocalizeStringEventVariableBinder`, `TextLocalizationEntryBinder`, `TextLocalizationEntrySwitcherBinder`, Mono-варианты, `TextLocalizationExtensions` (PR #29).
- Маркеры профилировщика и улучшенное логирование в типах `BindableMember` и `BindMode` (PR #15).

### Изменено
- Проект редактора обновлён до Unity `6000.2.7f2` (PR #28).
- В репозиторий добавлен `com.unity.asset-store-tools` (только упаковка).

### Исправлено
- `RectTransformSetters.SetSizeDelta` писал в `anchoredPosition` вместо `sizeDelta` (PR #27).

## [1.0.4] — 2025-09-19

### Исправлено
- Генерация контекстного меню компонентов через `AddComponentContextMenuAttribute`; в пересобранной `Aspid.MVVM.Unity.Generators.dll` (PR #14).

## [1.0.3] — 2025-09-15

### Изменено
- Типы Unity-слоя (`MonoBinder`, `MonoViewModel`, `MonoView`, `ScriptableView`, классы редактора) перенесены из `Aspid.MVVM.Unity` в корневое пространство `Aspid.MVVM` для упаковки в Asset Store (PR #13).

### Удалено
- `MonoBinderExtensions` (перегрузки `BindSafely<T>`) и хуки `OnBindingDebug` / `OnUnbindingDebug` у `MonoBinder` (PR #13).

## [1.0.2] — 2025-09-11

### Исправлено
- Исправление генератора ViewModel; в пересобранной `Aspid.MVVM.Generators.dll` (PR #12).

## [1.0.1] — 2025-09-10

### Изменено
- Версия языка C# откачена с 10 на 9 под компилятор Unity по умолчанию (PR #11).
- `AddressableMonoBinder<TAsset>` переведён с UniTask на callback `Addressables.LoadAssetAsync(...).Completed`; зависимость от UniTask убрана.
- `OneTimeBindableMember<T>` (и Enum / Struct-варианты) пулятся через статическую фабрику `Get(value)`.

### Исправлено
- `ViewModelCollectionBinder` / `ViewModelCollectionMonoBinder` деактивируют лишние View из пула при уменьшении коллекции.

## [1.0.0] — 2025-08-09

Первый публичный релиз.

[1.1.0]: https://github.com/VPDPersonal/Aspid.MVVM/compare/v1.0.5...HEAD
[1.0.5]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.4...v1.0.5
[1.0.4]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.3...1.0.4
[1.0.3]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.2...1.0.3
[1.0.2]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.1...1.0.2
[1.0.1]: https://github.com/VPDPersonal/Aspid.MVVM/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/VPDPersonal/Aspid.MVVM/releases/tag/1.0.0
