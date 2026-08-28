# Руководство по миграции

Заметки по переходу существующего проекта с **Aspid.MVVM 1.0** на **Aspid.MVVM 1.1**.

Полный список изменений см. в [CHANGELOG.ru.md](CHANGELOG.ru.md).

> 🌐 English version: [MIGRATION.md](MIGRATION.md)

> Ссылки на Unity-ассеты (префабы, сцены, ScriptableObject) переживают обновление, потому что каждый перемещённый скрипт сохранил свой исходный GUID в `.meta`. Ссылки в исходном коде на переименованные классы **не** переживают — требуется поиск-и-замена.

> **Минимальная версия Unity теперь `6000.0`**.

---

## TL;DR

1. Добавьте необходимые git-пакеты `tech.aspid.collections` и `tech.aspid.fasttools` в `manifest.json` — они не разрешаются автоматически (см. § 3.1).
2. Переименуйте `ViewModelObservableList*` → `ObservableList*ViewModel`, и так же для Dictionary / Collection (см. § 1.1).
3. Замените каждый `[AddComponentContextMenu(typeof(X), "path")]` на `[AddBinderContextMenu(typeof(X), Path = "path")]` и перенесите любой `[AddPropertyContextMenu(typeof(X), "m_Field")]` в аргумент `serializePropertyNames` того же атрибута (см. § 1.2).
4. Проверьте каждый вызов `view.Dispose()`: GameObject больше не уничтожается автоматически (см. § 2.2).
5. Проверьте каждый вызов `view.DestroyView()`: теперь он уничтожает только компонент View, а не GameObject — используйте `view.DestroyViewAndGameObject()` для прежнего поведения (см. § 2.3).

---

## 1. Несовместимости компиляции

### 1.1 Переименованные классы биндеров StarterKit

GUID-ы `.meta` сохранены, поэтому существующие префабы / сцены продолжают работать. Обновить нужно только ваш собственный исходный код.

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` (включая обобщённые `<T>`, `<T, TViewFactory>`) | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |

Рекомендуемый подход: одна глобальная замена на строку (regex / рефакторинг в IDE). Пространство имён `Aspid.MVVM.StarterKit` не менялось.

### 1.2 Удалён `AddComponentContextMenuAttribute`

`AddComponentContextMenuAttribute` и `AddPropertyContextMenuAttribute` оба удалены и объединены в единый `AddBinderContextMenuAttribute` (плюс вариант по типу `AddBinderContextMenuByTypeAttribute`, регистрирующий биндер только по типу целевого компонента). Путь меню переходит в именованное свойство `Path`; имена сериализуемых свойств, которые задавал `[AddPropertyContextMenu]`, переходят в параметр конструктора `serializePropertyNames` (`params string[]`, можно передать несколько).

```csharp
// БЫЛО — Aspid.MVVM 1.0
[AddPropertyContextMenu(typeof(CanvasGroup), "m_Alpha")]        // необязательный
[AddComponentContextMenu(typeof(CanvasGroup), "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }

// СТАЛО — Aspid.MVVM 1.1 (один атрибут; оба аргумента переносятся, Path необязателен)
[AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", Path = "Add CanvasGroup Binder/Alpha")]
public partial class MyAlphaBinder : MonoBinder { }
```

Если у биндера был только `[AddComponentContextMenu(typeof(X), "path")]`, механическая замена — `[AddBinderContextMenu(typeof(X), Path = "path")]`.

---

### 1.3 Типизированные базы биндеров заменены интерфейсами биндеров

Базы, существовавшие только ради повторного объявления перегрузок `SetValue`, удалены. Преобразования переехали в
интерфейсы биндеров как реализации по умолчанию, поэтому биндер называет интерфейс, а не наследует базу.

| 1.0 / ранняя 1.1 | Сейчас |
|-----|-----|
| `TargetVector3Binder<T>` | `TargetBinder<T, Vector3>, IVector3Binder` |
| `TargetVector2Binder<T>` | `TargetBinder<T, Vector2>, IVector2Binder` |
| `ComponentVector3MonoBinder<T>` | `ComponentMonoBinder<T, Vector3>, IVector3Binder` |
| `ComponentVector2MonoBinder<T>` | `ComponentMonoBinder<T, Vector2>, IVector2Binder` |
| `ComponentQuaternionMonoBinder<T>` | `ComponentMonoBinder<T, Quaternion>, IRotationBinder` |
| `Vector3Binder` / `Vector2Binder` | `Binder<Vector3>, IVector3Binder` / `Binder<Vector2>, IVector2Binder` |
| `Vector3MonoBinder` / `Vector2MonoBinder` | `MonoBinder<Vector3>, IVector3Binder` / `MonoBinder<Vector2>, IVector2Binder` |
| `TargetQuaternionBinder<T>` | `TargetBinder<T, Quaternion>, IRotationBinder` |
| `ComponentColorMonoBinder<T>` | `ComponentMonoBinder<T, Color>, IColorBinder` |
| `TargetColorBinder<T>` | `TargetBinder<T, Color>, IColorBinder` |
| `ComponentBoolMonoBinder<T>` / `ComponentStringMonoBinder<T>` | `ComponentMonoBinder<T, bool>` / `ComponentMonoBinder<T, string>` |
| `ColorMonoBinder` / `QuaternionMonoBinder` | `MonoBinder<Color>, IColorBinder` / `MonoBinder<Quaternion>, IRotationBinder` |
| `BoolMonoBinder` / `StringMonoBinder` | `MonoBinder<bool>` / `MonoBinder<string>` |
| `TargetBoolBinder<T>` / `TargetStringBinder<T>` | `TargetBinder<T, bool>` / `TargetBinder<T, string>` |

`.meta` GUID конкретных биндеров не тронуты, поэтому префабы и сцены продолжают работать.

`TargetQuaternionBinder<T>` отвергал `BindMode.TwoWay` в конструкторе — свойство вращения не поднимает событие
изменения. Теперь эту проверку несёт сам биндер вращения; своему биндеру на удалённой базе нужно добавить
`mode.ThrowExceptionIfMatches(BindMode.TwoWay);` в собственный конструктор.

Реализация по умолчанию в интерфейсе не является членом класса, поэтому дополнительные точки входа `SetValue`
доступны только через интерфейс. Местам вызова нужен каст:

```csharp
// БЫЛО
vector2Binder.SetValue(5f);
vector3Binder.SetValue(new Vector2(1f, 2f));

// СТАЛО
((IBinder<float>)vector2Binder).SetValue(5f);
((IBinder<Vector2>)vector3Binder).SetValue(new Vector2(1f, 2f));
```

То же касается числовых баз: `SetValue(int)`, `SetValue(long)`, `SetValue(float)` и `SetValue(double)` теперь приходят
из `IIntBinder` / `ILongBinder` / `IFloatBinder` / `IDoubleBinder`, а значения вне диапазона насыщаются на границах
целевого типа, а не переполняются.

---

### 1.4 `TargetBinderWithConverter<T, TProperty>` слит с `TargetBinder<T, TProperty>`

Двухаргументный `TargetBinder` теперь держит конвертер сам — так же, как всегда делали
`ComponentMonoBinder<T, TProperty>` и `MonoBinder<TProperty>`. `TargetBinderWithConverter<T, TProperty>` и
`TargetObjectBinderWithConverter<T, TObject>` удалены — переименуйте их в `TargetBinder<T, TProperty>` и
`TargetObjectBinder<T, TObject>`; больше в этих биндерах ничего не меняется.

Конструктор принимает конвертер между целью и режимом, поэтому биндер, построенный прямо на двухаргументной базе,
передаёт на один аргумент больше:

```csharp
// БЫЛО
public MyBinder(Image target, BindMode mode = BindMode.OneWay)
    : base(target, mode) { }

// СТАЛО
public MyBinder(Image target, IConverter<Image.Type, Image.Type>? converter = null, BindMode mode = BindMode.OneWay)
    : base(target, converter, mode) { }
```

Вызовы, передававшие режим позиционно вторым аргументом, теперь должны называть его: `new MyBinder(target,
mode: BindMode.OneWayToSource)`. Биндеры, у которых конвертера не было, получают сериализуемый слот — он пуст и
ничего не меняет, пока его не заполнили.

---

### 1.5 Методы `BinderMath` называют биндер, за который санируют

`SafeClamp`, `SafeClamp01` и `NonNegative` подменяли нефинитное значение молча — ровно наоборот тому, как
конвертеры поступают со значением, которое не могут преобразовать. Теперь подмена сообщается, а для этого методу
нужно знать вызывающего: каждый стал расширением на `IBinder`, с перегрузкой по `Type` для хелпера, который
сообщает за другой биндер, — той же парой, что даёт `BinderLogger`.

```csharp
// БЫЛО
Target.pitch = BinderMath.SafeClamp(value, -3f, 3f);

// СТАЛО — внутри биндера; сериализуемый биндер передаёт свою цель как объект для пинга
Target.pitch = this.SafeClamp(value, -3f, 3f, Target);

// СТАЛО — внутри статического хелпера
audioSource.time = BinderMath.SafeClamp(typeof(AudioSourceTimeSetters), value, 0f, end, audioSource);
```

`BinderMath.IsFinite(float)` остаётся чистым предикатом. Новый `RequireFinite` — его сообщающая форма: возвращает
`false` и пишет ошибку, заменяя охранник `if (!BinderMath.IsFinite(value)) return;`, который молча гасил запись.
Перегрузки покрывают `float`, `Vector2`, `Vector3`, `Vector4` и `Rect`, причём вектор сообщается один раз, а не по
разу на компоненту.

Сообщается только нефинитный путь. Конечное значение вне диапазона по-прежнему насыщается на границе молча — это
документированный контракт, иначе ползунок, который двигают каждый кадр, забил бы консоль.

---

## 2. Изменения времени выполнения / поведения

### 2.1 `MonoView` больше не абстрактный

```csharp
// 1.0
public abstract partial class MonoView : MonoBehaviour, IDisposable

// 1.1
public partial class MonoView : MonoBehaviour, IDisposable
```

Существующие подклассы продолжают работать — недавно добавленные сериализуемые поля (`_bindersList`, `_designViewModel`, `_designViewModelAssemblyQualifiedNames`) будут пустыми. Либо заполните их в инспекторе, либо сохраните прежний стиль с переопределениями — поддерживаются оба варианта.

### 2.2 `MonoView.Dispose()` больше не уничтожает GameObject

```csharp
// 1.0
public virtual void Dispose() {
    Deinitialize();
    if (this) Destroy(gameObject); // <-- удалено
}

// 1.1
public virtual void Dispose() => Deinitialize();
```

Если ваш код полагался на `view.Dispose()` для освобождения объекта-хоста, перейдите на:

```csharp
view.Dispose();
Object.Destroy(view.gameObject);
```

(или переопределите `Dispose` в своём подклассе, чтобы вернуть прежнее поведение).

### 2.3 `DestroyView()` больше не уничтожает GameObject

По аналогии с § 2.2 изменился метод-расширение `DestroyView`. В 1.0 `view.DestroyView()` уничтожал весь GameObject; в 1.1 он деинициализирует View (или вызывает `Dispose()`, если View реализует `IDisposable`) и уничтожает только **компонент** View, оставляя GameObject живым. Новый `DestroyViewAndGameObject()` возвращает прежнее поведение.

```csharp
// 1.0 — уничтожал GameObject
view.DestroyView();

// 1.1 — уничтожает только компонент View; чтобы уничтожить и GameObject:
view.DestroyViewAndGameObject();
```

Оба метода теперь безопасны к null/уничтоженным объектам (возвращают `null` вместо исключения) и в редакторе вне play-режима используют `DestroyImmediate`. Та же пара есть для обобщённых перегрузок `DestroyView<T>()` / `DestroyViewAndGameObject<T>()`.

### 2.4 `CollectionBinderBase<T>` пробрасывает гранулярные события изменений

В 1.0 `CollectionBinderBase<T>` имел только `OnAdded(IReadOnlyCollection<T>)` и `OnReset()` и не подписывался на `CollectionChanged`. В 1.1 он подписывается на `CollectionChanged` и добавляет шесть новых абстрактных хуков:

- `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`
- `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)`
- `OnReplace(T? oldItem, T? newItem, int newStartingIndex)`
- `OnMove(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex)`

Пакетные события `Replace` разворачиваются в поэлементные вызовы `OnReplace`.

**Влияние на компиляцию:** любой класс-наследник `CollectionBinderBase<T>` обязан реализовать все шесть новых абстрактных методов, иначе он не скомпилируется. Пустые тела сохраняют поведение 1.0. Сам `CollectionMonoBinder<T>` не изменился (по-прежнему только `OnAdded` / `OnReset`).

### 2.5 Переработка `ViewInitializer`

Семейство `ViewInitializer` переработано: разрешение view/контейнера перенесено в `ViewInitializerBase`, `Views` / `ViewModel` в edit-режиме разрешаются лениво, `Resolve` контейнера стал `TryResolve` (несработавшее DI-разрешение больше не бросает исключение). Добавлена новая стадия `InitializeStage.DiConstructor` (компилируется только при заданном define интеграции Zenject или VContainer). Стадия инициализации по умолчанию **не изменилась** — это по-прежнему `Awake`.

Сериализуемые данные разрешения также реструктурированы: записи разрешения по целям теперь представлены элементами `ViewInitializeComponent` (целевой тип хранится строкой-именем типа) вместо прежних встроенных полей `InitializeComponent<IView>`. После обновления перепроверьте настройки разрешения на существующих компонентах `ViewInitializer` / `ViewInitializerManual` в инспекторе.

### 2.6 Бесшовная замена Addressable

`AddressableMonoBinder<TAsset>` / `AddressableMonoBinder<TAsset, TComponent>` получают сериализуемый флаг `_seamlessSwap` (по умолчанию `false`, то есть опционально). Когда он выключен, новая привязка по-прежнему сбрасывается на дефолтный ассет перед загрузкой, как в 1.0; когда включён — ранее загруженный ассет остаётся на экране до завершения новой загрузки. Жизненный цикл загрузки переписан даже на дефолтном пути (один внутренний handle заменён на отдельные current/pending), поэтому если вы наследуете Addressable-биндер и переопределяете поток установки/освобождения ассета, перепроверьте его с учётом нового флага и жизненного цикла handle.

### 2.7 Пути `[AddComponentMenu]`

Ряд путей меню нормализован:

- "Collections/…" → "Collection/…" (единственное число).
- ASCII-дефис `-` между словами → длинное тире `–`.

Инструменты, ищущие в диалоге Add Component или по путям меню по точной строке, нужно обновить.

### 2.8 Исправления поведения, меняющие результат во время выполнения

Исправлены две ошибки 1.0, поэтому тот же код теперь ведёт себя иначе во время выполнения — без перекомпиляции:

- **`NumberToBoolConverter` со `Comparisons.Inequality`** в 1.0 возвращал тот же результат, что и `Comparisons.Equal` (сравнение было инвертировано). Теперь он корректно возвращает `true`, когда значения *не* приблизительно равны. Проверьте биндеры с `Inequality` и уберите компенсирующую инверсию ниже по потоку, если добавляли.
- **`DynamicViewModel.Create<…>`** в 1.0 принудительно делал каждое свойство `BindMode.OneTime`, игнорируя заданный режим. Теперь учитывается `BindMode` каждого `DynamicPropertyData`, поэтому свойства без явного режима обновляются вживую. Передавайте `BindMode.OneTime` явно, если полагались на разовую привязку.

---

## 3. Проект / инфраструктура

### 3.1 Необходимые пакеты

1.1 распространяется как UPM-пакет (`tech.aspid.mvvm`). Его сборки зависят от двух внешних git-пакетов, которые `package.json` не объявляет, поэтому добавьте их в `Packages/manifest.json` до импорта 1.1:

```json
"tech.aspid.collections": "https://github.com/VPDPersonal/Aspid.Collections.git#upm",
"tech.aspid.fasttools": "https://github.com/VPDPersonal/Aspid.FastTools.git#upm"
```

Исходники `Aspid.Collections`, ранее поставлявшиеся внутри пакета, удалены — теперь это отдельный пакет `tech.aspid.collections`. Имя сборки (`Aspid.Collections.Observable`) и пространства имён не изменились, поэтому директивы `using` и ссылки на типы править не нужно при наличии пакета.

### 3.2 Перемещён проект Unity

Дерево проекта Unity перенесено из корня репозитория в `Aspid.MVVM/`, а сам фреймворк также вынесен из слоя `Plugins/`:

```
1.0:  <repo>/Assets/Plugins/Aspid/MVVM/...
1.1:  <repo>/Aspid.MVVM/Packages/tech.aspid.mvvm/...
```

(Сторонние плагины, например Zenject, остаются под `Assets/Plugins/`.) GUID-ы `.meta` сохранены, поэтому ссылки из префабов / сцен / ScriptableObject переживают переход — обновить нужно только текстовые строки путей (CI/CD-скрипты, рабочие пространства IDE, сборочные конвейеры, константы путей).

### 3.3 Версии редактора Unity

`package.json` теперь объявляет `"unity": "6000.0"`, формально задавая минимальную поддерживаемую версию Unity `6000.0`. В 1.0 не было UPM-`package.json`, поэтому минимальная версия не объявлялась (файл проекта в репозитории уже был на Unity `6000.2.7f2`). Проекты, оставшиеся на Unity 2022 / 2023, должны обновить редактор перед переходом на 1.1.

---

## 4. Архитектурные заметки

### 4.1 `BindSafely` / `UnbindSafely`

К существующим методам `BindSafely` / `UnbindSafely` добавлены опциональные параметры `owner` и `memberName` (по умолчанию `null`), чтобы диагностика null-биндера называла владеющий View (его тип и имя GameObject), поле с биндерами и индекс элемента. Существующий исходный код вызовов компилируется без изменений.

### 4.2 Bindable Properties

Существующие поля `[Bind]` продолжают работать. Bindable Properties (PR #46) — аддитивная возможность; подключается **на уровне свойства**, применяя `[Bind]` (или `[OneWayBind]` / `[TwoWayBind]` / `[OneTimeBind]` / `[OneWayToSourceBind]`) прямо к свойству вместо поля. В 1.0 эти атрибуты применялись только к полям; в 1.1 они принимают и свойства. Изменения на уровне ViewModel не требуются.

### 4.3 `RelayCommand`

`RelayCommand.Empty` сохранён (по-прежнему невыполнимая). Новый `RelayCommand.EmptyExecution` возвращает команду, которую можно выполнить, но которая ничего не делает; оба члена есть на всех арностях (`RelayCommand`, `RelayCommand<T>`, … вплоть до четырёх параметров типа). Внутренний пустой конструктор изменён с безпараметрического `RelayCommand()` на `RelayCommand(bool value = false)` — через публичный API это незаметно, но рефлексию, ищущую приватный безпараметрический конструктор по сигнатуре, нужно обновить.

---

## 5. Конвертеры

Подсистема конвертеров перестроена: 14 конвертеров стали 148, у контракта появилось обратное направление, а слой совместимости с Unity до 2023.1 объявлен устаревшим. Почти всё это — работа в исходном коде: имена, попадающие в сериализованные данные, намеренно оставлены как были, а устаревшие типы по-прежнему реализуются. Трём исключениям нужно время в редакторе: `DateTimeCompareConverter` и `DateTimeOffsetFormatConverter` теряют настройки из-за перехода на енумы, а `NumberCompareConverter` — порог из-за расширенного поля (§ 5.4); а override на инстансе префаба для переименованного или закрытого типа требует ремонтного инструмента (§ 5.1).

### 5.1 Переименования

Изменились девять имён. У четырёх нет следа в сериализации вовсе; остальные — типы, собственные данные которых мигрирует `[MovedFrom]`:

| Было | Стало | Примечания |
|------|-------|-----------|
| `Vector2ToVector3Converter.Values`, `Vector3ToVector2Converter.Values` | `Mode` | Вложенный тип enum; имя вложенного типа не сериализуется |
| `Comparisons.Inequality` | `ComparisonMode.NotEqual` | Enum сериализуется порядковым номером, он не изменился |
| `EnumMatch.Equals` | `EnumMatch.Equal` | То же — порядковый номер. Член скрывал унаследованный `object.Equals` |
| `ConverterExtensions.ToConvert` | `ToConverter` | Метод-расширение, только код. Метод возвращает конвертер, а старое имя читалось как повеление |
| `WrapMode` | `NumberWrapMode` | Переименование **типа** enum; значение остаётся порядковым номером, поэтому авторские данные не затронуты. Старое имя было неоднозначно с `UnityEngine.WrapMode` |
| `ListToStringConverter` | `CollectionJoinToStringConverter` | Несёт `[MovedFrom]`. Принимает любой `IEnumerable<T>`, а все соседи названы `Collection*` |
| `NumberToBoolConverter` | `NumberCompareConverter` | Переименование класса, а вместе с ним `Comparisons` → `ComparisonMode` и порог, расширенный с `float` до `double` — вот это стоит времени в редакторе (§ 5.4) |
| `BoxColliderCentreCombineConverter`, `CapsuleColliderCentreCombineConverter`, `SphereColliderCentreCombineConverter` | `…CenterCombineConverter` | Несут `[MovedFrom]`. Американское написание, совпадающее со свойством `center` самого Unity |

Первые пять замените поиском по коду — и всё. По всему, что несёт `[MovedFrom]`, прочитайте предупреждение ниже, прежде чем считать сцены нетронутыми.

> **Более широкая волна переименований была предпринята и откатана, и причину стоит знать, если вы
> сопровождаете собственные типы с `[SerializeReference]`.** `[MovedFrom]` и `[FormerlySerializedAs]`
> покрывают собственные сериализованные данные объекта. Они **не** покрывают override на инстансе
> префаба, который ключуется хранимой строкой типа и путём свойства. Переименование
> `SequenceConverters` обнулило конвертер в собственном сэмпле Hello World этого пакета — 24 ошибки
> в консоли и биндер, переставший конвертировать, при том что `[MovedFrom]` стоял и был верным.
> Поэтому `SequenceConverters`, `GenericToString`, `_preConvertor` / `_postConvertor` и `_values`
> сохраняют имена — вместе с опечаткой.
>
> Та же оговорка действует для `ListToStringConverter` → `CollectionJoinToStringConverter` и для
> `EnumToValueConverter.Entry` / `LookupEntry`, публичные поля которых стали приватными
> `[SerializeField]` с `[FormerlySerializedAs]`. Ни один из трёх не настроен ни в одной сцене или
> префабе, поставляемых с пакетом, поэтому внутри него мигрировать было нечего. Если **ваш** проект
> настроил такой override на инстансе префаба, прогоните ремонтный инструмент, переписывающий
> хранимые строки типов и пути свойств, по всем сценам и префабам — иначе override будет отброшен
> при загрузке без единого сообщения.

### 5.2 Устарели именованные псевдонимы конвертеров

40 интерфейсов `IConverterXToY` и 70 обёрток `ToConvert` / `ToConvertSpecific` помечены `[Obsolete]`. Они существовали потому, что Unity до 2023.1 не умела сериализовать поле `[SerializeReference]` с типом-открытым генериком; 1.1 требует Unity 6000.0.

```csharp
// было
[SerializeReference] private IConverterFloat _converter;
IConverterFloat c = ((Func<float, float>)(x => x * 2f)).ToConvert();

// стало
[SerializeReference] private IConverter<float, float> _converter;
IConverter<float, float> c = ((Func<float, float>)(x => x * 2f)).ToConverter();
```

Генерик-версия `ConverterExtensions.ToConverter<TFrom, TTo>` — это замена, и она не устарела. До этого релиза метод назывался `ToConvert`: имя говорило «сконвертировать», хотя метод возвращает конвертер.

**На переход у вас один релиз.** Конвертеры самого пакета пока реализуют псевдонимы, поэтому поле, объявленное как `IConverterFloat`, сегодня десериализуется. Когда псевдонимы удалят, такое поле при загрузке станет `null` — молча, без исключения и без записи в консоли. Предупреждение компилятора — единственное уведомление, которое вы получите.

### 5.3 Обратный биндинг теперь конвертирует

Биндер в `BindMode.TwoWay` или `BindMode.OneWayToSource` раньше отправлял значение из View во ViewModel без преобразования. Теперь он вызывает `ITwoWayConverter.ConvertBack`, если назначенный конвертер её реализует, и пишет предупреждение в консоль, если нет.

Это меняет поведение там, где к двустороннему биндингу был подключён конвертер. Если вы компенсировали отсутствие обратного преобразования во ViewModel — сеттером, отменявшим работу конвертера, — эту компенсацию нужно убрать.

### 5.4 Исправления, меняющие поведение во время выполнения

- **`StringFormatConverter` с включённым `_formatEmptyValues`** снова форматирует null и пустой вход. В промежуточных сборках 1.1 он возвращал `null` вместо отформатированной пустой строки.
- **`FormatException` в конвертере больше не останавливает посторонние биндеры.** Если в сцене была битая строка формата, биндеры, стоявшие за ней в порядке рассылки, молча не обновлялись; теперь будут.
- **Семейство `Vector3CombineConverter` возвращает вход без изменений** вместо исключения, когда ссылка на сцену отсутствует, и сообщает об этом на каждом пуше.
- **Неправильно настроенный конвертер теперь сообщает о себе на каждом преобразовании.** Ждите новых сообщений в консоли от конвертеров, которые уже были настроены неправильно и молча возвращали фолбэк: пустой список токенов, перевёрнутые `min`/`max`, отсутствующий внутренний конвертер, дубликат ключа в таблице. Сообщения называют конвертер и то, что он сделал вместо; они указывают на настройку, которая была сломана и раньше, а не на новый сбой.
- **`SafeConverter` лишился поля `_logErrors`.** Пойманное исключение теперь логируется всегда и целиком. Если вы держали этим переключателем сцену в тишине, спрятанный им шум станет виден.
- **`NumberCompareConverter` нужно заново задать порог.** Бывший `NumberToBoolConverter` сохранил имя поля `_value`, но расширил его с `float` до `double`, а Unity не переносит float в поле double: каждый заданный порог читается как `0`. Само сравнение выживает — у `ComparisonMode` те же члены в том же порядке, что были у `Comparisons`.
- **`DateTimeCompareConverter` и `DateTimeOffsetFormatConverter` нужно настроить заново.** Их пары bool стали енумами `ReferenceSource` и `OffsetSource`, и старые булевы значения **не** мигрируют: каждый экземпляр возвращается к значению по умолчанию, и нужный источник придётся выбрать в инспекторе заново. Оба конвертера стоит поискать по сценам перед обновлением.
- **Двусторонние биндинги получили обратное направление, которого у них не было.** `DateTimeToUnixTimestampConverter`, `StringToDateTimeConverter` и `StringToTimeSpanConverter` раньше отдавали значение назад нетронутым в `TwoWay`-биндинге; теперь они конвертируют. Если ваша ViewModel это компенсировала, компенсацию нужно убрать.
- **Необъявленное значение enum поднимает `InvalidOperationException` вместо `ArgumentOutOfRangeException`.** Важно, только если вы его ловите: эта ветка сообщает о порче сериализованного состояния, а не о плохом аргументе.

### 5.5 `GenericToString.ToStringValue` удалён

`protected virtual string ToStringValue(TFrom)` стал приватной деталью, когда форматирование
переехало в хук `Format`. Подкласс, который его переопределял, больше не компилируется:

```csharp
// было
protected override string ToStringValue(float value) => value.ToString("F2");

// стало — Format получает типизированное значение и вызывается для любого непустого формата
protected override string? Format(float value) => value.ToString("F2");
```

Вызываются эти хуки не в одинаковых случаях. `ToStringValue` срабатывал только при пустом формате;
`Format` срабатывает, когда формат **не** пуст, а пустой откатывается на `ToString()`. Если
переопределение существовало ради вывода без формата, его место теперь в собственном `Convert`
подкласса.

---

## Чек-лист обновления

- [ ] Добавить git-пакеты `tech.aspid.collections` и `tech.aspid.fasttools` в `manifest.json` (обязательно; не разрешаются автоматически)
- [ ] Обновить редактор до Unity `6000.0` или новее
- [ ] Обновить CI / сборочные скрипты и константы путей: `Assets/Plugins/Aspid/...` → `Aspid.MVVM/Assets/Aspid/...` (корневой `Assets/` → `Aspid.MVVM/Assets/`)
- [ ] Глобально переименовать классы биндеров StarterKit (см. § 1.1)
- [ ] Заменить `[AddComponentContextMenu(...)]` на `[AddBinderContextMenu(..., Path = ...)]`
- [ ] Перенести аргументы `[AddPropertyContextMenu(..., "m_Field")]` в `[AddBinderContextMenu(..., serializePropertyNames: "m_Field")]`
- [ ] Добавить явный `Object.Destroy(view.gameObject)` там, где `view.Dispose()` использовался для освобождения объектов
- [ ] Заменить `view.DestroyView()` на `view.DestroyViewAndGameObject()` там, где он использовался для уничтожения GameObject-хоста
- [ ] Реализовать шесть новых абстрактных хуков в любом кастомном наследнике `CollectionBinderBase<T>` (`OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)`, `OnReplace`, `OnMove`)
- [ ] Пересмотреть настройки `ViewInitializer`: разрешение перенесено в `ViewInitializerBase`, `Resolve` контейнера стал `TryResolve`, добавлена стадия `InitializeStage.DiConstructor` (стадия по умолчанию не изменилась — `Awake`)
- [ ] Перепроверить данные инспектора `ViewInitializer` / `ViewInitializerManual` — сериализуемые компоненты разрешения сменили тип, поэтому существующие настройки разрешения view/viewModel могут не перенестись- [ ] Проверить использования `NumberToBoolConverter` (`Inequality`) и `DynamicViewModel.Create` на исправленное поведение во время выполнения
- [ ] Прогнать сцены, использующие `ImageSpriteSwitcherBinder`, Addressable-биндеры и `VirtualizedList*`
- [ ] Обновить тесты / инструменты, ищущие компоненты по пути `AddComponentMenu`
- [ ] Переименовать `Values` → `Mode`, `Comparisons.Inequality` → `NotEqual`, `EnumMatch.Equals` → `Equal`, `ToConvert` → `ToConverter`, `WrapMode` → `NumberWrapMode` и `ListToStringConverter` → `CollectionJoinToStringConverter` в своём коде (см. § 5.1)
- [ ] Заново задать пороги `NumberCompareConverter` (см. § 5.4)
- [ ] Настроить заново `DateTimeCompareConverter` и `DateTimeOffsetFormatConverter` во всех сценах и префабах — их булевы настройки не мигрируют в новые енумы (см. § 5.4)
- [ ] Прогнать инструмент починки сериализованных ссылок, если в сценах или префабах есть override на инстансе префаба для `CollectionJoinToStringConverter`, `EnumToValueConverter.Entry` или `LookupEntry` (см. § 5.1)
- [ ] Ожидать и разобрать новые ошибки в консоли от конвертеров, которые уже были настроены неправильно, — они больше не сообщают о себе однократно (см. § 5.4)
- [ ] Перевести поля `[SerializeReference]` и код с `[Obsolete]`-псевдонимов `IConverterXToY` на `IConverter<TFrom, TTo>` — на это есть один релиз, а дальше отказ будет молчаливым (см. § 5.2)
- [ ] Проверить каждый двусторонний биндинг с конвертером: обратное направление теперь конвертирует (см. § 5.3)
- [ ] Перенести переопределение `ToStringValue` в `Format` (см. § 5.5)
