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
1.1:  <repo>/Aspid.MVVM/Assets/Aspid/MVVM/...
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
