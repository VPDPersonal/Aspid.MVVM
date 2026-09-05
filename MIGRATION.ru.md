# Руководство по миграции: 1.0 → 1.1

Что поменять в проекте при переходе с **Aspid.MVVM 1.0.x** на **1.1.0**. Полный список изменений — в [CHANGELOG.ru.md](CHANGELOG.ru.md).

> 🌐 English version: [MIGRATION.md](MIGRATION.md)

Каждый перемещённый скрипт сохранил GUID в `.meta`, поэтому префабы, сцены и ScriptableObject не теряют ссылок. **Исходный код — теряет**: переименованные типы правятся поиском и заменой. У переименованных типов нет `[MovedFrom]`; слот `[SerializeReference]`, настроенный со старым именем типа, нужно прогнать через инструмент починки сериализованных ссылок или настроить заново.

---

## Кратко

1. Обновите редактор до Unity `6000.0` или новее.
2. Добавьте `tech.aspid.collections` и `tech.aspid.fasttools` в `manifest.json` (§ 1).
3. Переименуйте типы по таблице § 2; остальное найдёт компилятор.
4. `view.Dispose()` и `view.DestroyView()` больше не уничтожают GameObject (§ 4.2).
5. Свои наследники `CollectionBinder<T>` реализуют шесть новых хуков (§ 4.4).
6. Заново настройте пороги `NumberCompareConverter` и фолбэки `ParseHtmlStringConverter` (§ 4.7).

---

## 1. Проект и пакеты

- Минимум — **Unity `6000.0`**; проект редактора на `6000.4.0f1`.
- Два git-пакета обязательны и не подтягиваются автоматически:

```json
"tech.aspid.collections": "https://github.com/VPDPersonal/Aspid.Collections.git#upm",
"tech.aspid.fasttools": "https://github.com/VPDPersonal/Aspid.FastTools.git#upm"
```

- `Aspid.Collections` больше не лежит внутри пакета. Сборка (`Aspid.Collections.Observable`) и пространства имён прежние.
- Фреймворк переехал из `Assets/Plugins/Aspid/MVVM/` в UPM-пакет `Packages/tech.aspid.mvvm/`. Обновите константы путей и CI-скрипты; ссылки на ассеты сохранятся.
- Сборки `Aspid.MVVM.StarterKit.Unity` и `Aspid.MVVM.StarterKit.Unity.Editor` влиты в `Aspid.MVVM.StarterKit` и `Aspid.MVVM.StarterKit.Editor`. Поправьте ссылки в `.asmdef`; пространство имён `Aspid.MVVM.StarterKit` не менялось.
- `SerializeReferenceDropdown` больше не зависимость; поля `[SerializeReference]` рисует type picker FastTools.

---

## 2. Переименованные типы и члены

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` / `…Binder` | `ObservableListViewModelMonoBinder` / `…Binder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` / `…Binder` | `CollectionViewModelMonoBinder` / `…Binder` |
| `CollectionBinderBase<T>` | `CollectionBinder<T>` |
| `OneWayValue<T>`, `OneTimeValue<T>`, `TwoWayValue<T>`, `OneWayToSourceValue<T>` | `ValueOneWayBinder<T>`, `ValueOneTimeBinder<T>`, `ValueTwoWayBinder<T>`, `ValueOneWayToSourceBinder<T>` |
| `GenericOneWayBinder`, `GenericOneTimeBinder`, `GenericTwoWayBinder`, `GenericOneWayToSourceBinder` (и `UnityGeneric*`) | `DelegateOneWayBinder`, `DelegateOneTimeBinder`, `DelegateTwoWayBinder`, `DelegateOneWayToSourceBinder` |
| `GenericCasterBinder` (и `UnityGenericCasterBinder`) | `CasterBinder` |
| `GenericToStringCasterBinder` / `…MonoBinder` | `ValueToStringCasterBinder` / `…MonoBinder` |
| `MonoCommandBinder` | `CommandMonoBinder` |
| `ScrollBarCommandMonoBinder` | `ScrollbarCommandMonoBinder` |
| `RawImageMaterial*Binder` | `GraphicMaterial*Binder` (любой `Graphic`) |
| `RendererMaterialColorBinder` / `…SwitcherBinder` | `RendererMaterialsColorBinder` / `…SwitcherBinder` |
| `SliderValueMode` | `SliderRangeMode` (те же члены) |
| `ICanExecuteView`, `ColorInteractable`, `GameObjectVisibleInteractable`, `SequenceCanExecuteView` | `ICanExecuteHandler`, `ColorCanExecuteHandler`, `GameObjectVisibleCanExecuteHandler`, `SequenceCanExecuteHandler` |
| `RectTransformSetters`, `TransformSetters` | `RectTransformGettersAndSetters`, `TransformGettersAndSetters` |
| `IMonoBinderValidable.IsMonoExist`, `ValidableBindersById` | `IMonoBinderValidatable.IsMonoAlive`, `ValidatableBindersById` |
| `Binder.IsBind`, `MonoBinder.IsBind` | `CanBind` |
| `ObservableListBinder.GetFilterList` | `GetFilteredList` |
| `BinderFieldInfoExtensions.GetBinderId` | `BinderIdUtility.FromFieldName` (Editor-сборка) |
| `ViewModelDebugPanel` | `DebugViewModelPanel` |
| `GenericFuncConverter<TFrom, TTo>` | `FuncConverter<TFrom, TTo>` |
| `ConverterExtensions.ToConvert` | `FuncConverterExtensions.ToConverter` |
| `GenericToString<TFrom>` | `ValueToStringConverter<T>` — переопределяйте `Format(T value, string format)` вместо `ToStringValue` |
| `SequenceConverters<T>` | `SequenceConverter<T>` |
| `NumberToBoolConverter`, `Comparisons` (`Inequality`) | `NumberCompareConverter`, `ComparisonMode` (`NotEqual`) — порог расширен до `double`, см. § 4.7 |
| `Vector2ToVector3Converter`, `Vector3ToVector2Converter` (`Values`) | `Vector2Vector3Converter` (`Mode`), двусторонний |
| `Vector2SubstitutionConverter`, `Vector3SubstitutionConverter` | `VectorSwizzleConverter` |
| `TimeSpanToStringConverter` | `TimeSpanFormatConverter` |
| `ObjectToStringConverter` | `ValueToStringConverter<object>` |
| `ObjectNullToBoolConverter` | `EqualityToBoolConverter<T>` со сравнением с `null` |

---

## 3. Удалено

| Удалено | Замена |
|---------|--------|
| `AddComponentContextMenuAttribute`, `AddPropertyContextMenu` | `[AddBinderContextMenu(typeof(X), serializePropertyNames: "m_Field", Path = "path")]`; `AddBinderContextMenuByTypeAttribute` регистрирует только по типу target |
| `DynamicViewModel.Create<…>`, `DynamicPropertyData<T>`, `DynamicPropertyFactory`, `OneWay/TwoWay/OneTimeDynamicProperty<T>` | `DynamicViewModel.Add<T>` / `Get<T>` → `IDynamicProperty<T>` (§ 4.8) |
| Алиасы `IConverterXToY` (`IConverterFloat`, `IConverterIntToLong`, …), обёртки `ToConvert` / `ToConvertSpecific` | `IConverter<TFrom, TTo>` и `ToConverter<TFrom, TTo>`. Поле `[SerializeReference]`, объявленное алиасом, при загрузке станет `null` — смените тип поля |
| `IBindableValue<T>`, `IReadOnlyBindableValue<T>` | `ValueTwoWayBinder<T>.Value` / `ValueOneWayBinder<T>.Value` |
| `IViewModelCollectionFilter`, `And/OrCompositeCollectionFilter`, `And/OrViewModelCompositeCollectionFilter`, `ICollectionComparer`, `NumberCollectionComparer` | Фильтрация и сортировка из `tech.aspid.collections` |
| Ветки совместимости `UNITY_2022_1_OR_NEWER` / `UNITY_6000_0_OR_NEWER`, фолбэки `PhysicMaterial` | Только Unity 6 |

```csharp
// 1.0
[AddPropertyContextMenu(typeof(CanvasGroup), "m_Alpha")]
[AddComponentContextMenu(typeof(CanvasGroup), "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }

// 1.1
[AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", Path = "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }
```

---

## 4. Изменения поведения

### 4.1 `MonoView` стал конкретным

`MonoView` больше не `abstract`: список биндеров, валидация детей и `[RequireBinder]` живут в нём. Наследники продолжают работать; новые сериализуемые поля стартуют пустыми.

### 4.2 `Dispose()` и `DestroyView()` сохраняют GameObject

```csharp
// 1.0 — оба уничтожали GameObject
view.Dispose();
view.DestroyView();

// 1.1
view.Dispose();                   // только Deinitialize()
Object.Destroy(view.gameObject);  // если объект всё же нужно убрать

view.DestroyView();               // уничтожает только компонент View
view.DestroyViewAndGameObject();  // поведение 1.0
```

Оба метода-расширения возвращают `null` вместо исключения на уничтоженном View и вне play mode используют `DestroyImmediate`.

### 4.3 `MonoBinder.Bind()` на уже привязанном биндере

Логирует ошибку и выходит вместо исключения.

### 4.4 Хуки `CollectionBinder<T>`

`CollectionBinder<T>` подписывается на `CollectionChanged` и добавляет шесть абстрактных хуков. Свой наследник обязан их реализовать; пустые тела дают поведение 1.0.

```csharp
protected abstract void OnAdded(T? newItem);
protected abstract void OnAdded(IReadOnlyList<T?>? newItems);
protected abstract void OnRemoved(T? oldItem);
protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems);
protected abstract void OnReplaced(T? oldItem, T? newItem, int index);
protected abstract void OnMoved(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);
```

`CollectionMonoBinder<T>` сохранил `OnAdded(IReadOnlyCollection<T>)` / `OnReset()`, но теперь следит за коллекцией и перестраивается на каждом изменении.

### 4.5 `ViewInitializer`

Резолв перенесён в `ViewInitializerBase`, `Resolve` контейнера стал `TryResolve` (неудачный резолв больше не бросает), добавлена стадия `InitializeStage.DiConstructor`. Стадия по умолчанию по-прежнему `Awake`. Сериализованные записи резолва сменили тип — перепроверьте `ViewInitializer` / `ViewInitializerManual` в инспекторе.

### 4.6 Биндеры

- **Пути `[AddComponentMenu]`**: `Collections/…` → `Collection/…`, дефис → длинное тире (`Binder – ViewModel`). Обновите инструменты, сравнивающие пути меню.
- **Addressable-биндеры** получили опциональный `_seamlessSwap`; жизненный цикл загрузки использует раздельные current/pending handle. Перепроверьте наследников, переопределяющих установку ассета или освобождение.
- **Санитизация стала громкой**: `NaN` или бесконечность, записанные в биндер, ограничиваются и репортятся через `BinderLogger`. Конечное значение вне диапазона по-прежнему насыщается молча.
- **Обратная привязка конвертирует**: в `TwoWay` / `OneWayToSource` применяется `ConvertBack` конвертера. Уберите компенсацию отсутствующей обратной конверсии из ViewModel.
- **Bool-биндеры** несут слот `IConverter<bool, bool>` вместо `_isInvert`. Инверсию настройте заново через `BoolInvertConverter`; биндеры `*ByBind` сохранили флаг.
- **`MonoBinder`, добавленный через инспектор**, стартует в режиме из `DefaultMode`, а не всегда в `TwoWay`.
- **Уничтоженный `MonoBinder`** отвязывается из `OnDestroy`; в переопределениях вызывайте `base.OnDestroy()`.

### 4.7 Конвертеры

- **Неверная конфигурация репортится при каждой конвертации** и возвращает настроенный фолбэк. Ожидайте новых ошибок в консоли от настроек, которые были молча сломаны.
- **`NumberCompareConverter`** (был `NumberToBoolConverter`): `Inequality` в 1.0 был инвертирован и теперь работает; порог `_value` расширен с `float` до `double`, поэтому каждый настроенный порог читается как `0` — настройте заново.
- **`ParseHtmlStringConverter`**: фолбэк переехал из `_defaultColor` в `_fallback` и настраивается заново; нераспознанный цвет теперь репортится.
- **`ValueToStringConverter<T>`** (был `GenericToString<TFrom>`): форматирование — виртуальный хук `Format(T value, string format)`; пустой или пробельный формат откатывается к `ToString()`. `FormatException` сдерживается, а не обрывает список подписчиков биндера.
- **Пробелы — это пусто**: каждый строковый конвертер считает строку из пробелов пустой.

### 4.8 `DynamicViewModel`

```csharp
var viewModel = new DynamicViewModel
{
    { "Title", "Hello" },
    { "Volume", 0.5f, BindMode.TwoWay }
};

IDynamicProperty<float> volume = viewModel.Get<float>("Volume");
volume.Value = 0.8f;
volume.ValueChanged += v => Debug.Log(v);
```

`Add<T>` возвращает тот же handle `IDynamicProperty<T>`. Все четыре `BindMode` живут на одном типе свойства; число свойств не ограничено.

### 4.9 `RelayCommand`

`RelayCommand.Empty` остался невыполнимым; `RelayCommand.EmptyExecution` выполним и ничего не делает. Приватный пустой конструктор теперь `RelayCommand(bool value = false)` — важно только для рефлексии.

---

## 5. Свои биндеры в 1.1

- **Сериализуемый близнец генерируется.** Пишите `MonoBinder`-половину, помечайте её `[GenerateSerializableBinder]` — `{Name}Binder` генерируется над соответствующим `Target*Binder`. Близнец, уже существующий по имени, не трогается.
- **Базы свойств несут конвертер.** `TargetBinder<TTarget, TProperty>` и `ComponentMonoBinder<TComponent, TProperty>` держат слот `[SerializeReference] IConverter<TProperty, TProperty>`; поверх них — типизированные `Target{Int,Float,Object}Binder<TTarget>` и `Component{Int,Float,Object}MonoBinder<TComponent>`.
- **Числовые и векторные перегрузки приходят из интерфейсов.** `IIntBinder`, `ILongBinder`, `IFloatBinder`, `IDoubleBinder`, `IVector2Binder`, `IVector3Binder`, `IColorBinder`, `IRotationBinder` дают дополнительные `SetValue` как default-члены интерфейса; значения вне диапазона насыщаются. Доступны только через интерфейс: `((IBinder<float>)binder).SetValue(5f)`.
- **Один обратный канал для чисел.** `INumberReverseBinder` пробрасывает `IReverseBinder<int/long/float/double>.ValueChanged` в `NumberReverseChannel`, который держит биндер; подписка через `((IReverseBinder<float>)binder).ValueChanged`.
- **`BinderMath` называет вызывающего.**

```csharp
Target.pitch = this.SafeClamp(value, -3f, 3f, Target);            // внутри биндера
if (!this.RequireFinite(value, Target)) return;                   // вместо молчаливого IsFinite
value = BinderMath.SafeClamp(typeof(MyHelper), value, 0f, 1f);    // внутри статического хелпера
```

---

## Чеклист

- [ ] Редактор на Unity `6000.0`+; `tech.aspid.collections` и `tech.aspid.fasttools` в `manifest.json`
- [ ] Константы путей и CI: `Assets/Plugins/Aspid/MVVM/` → `Packages/tech.aspid.mvvm/`; ссылки `.asmdef` на `Aspid.MVVM.StarterKit.Unity*` → `Aspid.MVVM.StarterKit*`
- [ ] Переименовать типы и члены по § 2
- [ ] Заменить `[AddComponentContextMenu]` / `[AddPropertyContextMenu]` на `[AddBinderContextMenu]`
- [ ] Сменить тип полей `[SerializeReference]`, объявленных алиасами `IConverterXToY`, на `IConverter<TFrom, TTo>`
- [ ] Добавить `Object.Destroy(view.gameObject)` после `Dispose()` или заменить `DestroyView()` → `DestroyViewAndGameObject()`, где объект должен исчезать
- [ ] Реализовать шесть хуков в своих наследниках `CollectionBinder<T>`
- [ ] Перепроверить данные `ViewInitializer` в инспекторе
- [ ] Заменить `DynamicViewModel.Create` на `Add<T>` / инициализатор коллекции
- [ ] Заново настроить пороги `NumberCompareConverter`, фолбэки `ParseHtmlStringConverter` и инверсию bool-биндеров (`BoolInvertConverter`)
- [ ] Перенести переопределения `ToStringValue` в `Format(T, string)`
- [ ] Убрать из ViewModel компенсацию отсутствующей обратной конверсии в двусторонних привязках
- [ ] Прогнать инструмент починки сериализованных ссылок по сценам и префабам с переименованными конвертерами или биндерами в слотах `[SerializeReference]`
- [ ] Обновить инструменты, сравнивающие пути `AddComponentMenu`
- [ ] Разобрать новые ошибки в консоли от конвертеров и биндеров, которые уже были неверно настроены
