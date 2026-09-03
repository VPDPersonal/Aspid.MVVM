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
| `Core/` | Только базовые интерфейсы семейства | `IConverter`, `ITwoWayConverter` |
| `General/` | Типы с логикой, общие для семейства | `Binder`, `TargetBinder`; `Fallback/ConverterFallback` + `ConverterFailureMode` |
| `Helpers/` | «Просто помощники»: enum-ы `*Mode`, логгеры | `ComparisonMode`, `ConverterLogger`, `BinderLogger` |
| Тематические (`Objects/`, `Bools/`, `Strings/`…) | Реализации, с подпапками `ToBool/`, `ToString/`, `ToValue/` по целевому типу | `Objects/ToBool/EqualityToBoolConverter` |

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
- Ошибки не глушатся: логировать при каждом появлении, `null` в биндере = сброс состояния.

### Код и форматирование
- Никаких `==`/`!=` между `float`/`double` (ReSharper CompareOfFloatsByEqualityOperator): сравнение с нулём через паттерн `value is 0d` / `is not 0f`, целость через `value % 1d is 0d`, равенство двух float через `Mathf.Approximately`.
- `#nullable enable` первой строкой, если в файле есть `?`-аннотации.
- Порядок членов: сериализованные поля → обычные поля → автосвойства → конструкторы → события → свойства → методы.
- Конструктор с 2+ параметрами — параметры **в столбик**, скобка на строке имени. У методов — в столбик только когда сигнатура вылезает за правую границу (120 символов).
- Короткий guard-`if` с `return` — в одну строку: `if (!IsMissing(value)) return value;`
- Expression-bodied метод: тело на **следующей строке** после `=>` (явные реализации, публичные `Convert`, приватные однострочники):
  ```csharp
  string IConverter<int, string>.Convert(int value) =>
      Convert(value);
  ```
- Тернарий в присваивании `var x = …` или в expression body — в три строки (условие / `? a` / `: b`); внутри аргумента вызова и в `return` остаётся одной строкой.
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

Все пути ниже теперь под `StarterKit/Runtime/` (бывший `New`). Перенесено: `Binders/*` (Casters, Collections, Delegates, General, Helpers, Values), `Collections/*`, `Commands/*`, `Converters/{Core, General, Helpers, Bools, Objects, Composition, Collections, Enums, Numbers, Strings, Times}` целиком; базы `Converters/General/Numbers/{NumberConverter, TwoWayNumberConverter}`, `Helpers/{Globalization (+FormatExtensions), Logging, Numeric, Collections, Enums, Time (UnixTime, CurrentTime)}`, `ViewModels/*`.

Этап 1 закрыт коммитом `d02142514`. Дальше: `StarterKit/Unity/Runtime` (~27 .cs в Converters + биндеры Mono и пр.) переезжает в `StarterKit/Runtime`; при переносе `Unity/Runtime/Converters/*` смотреть, нет ли дубликатов уже перенесённых типов (например `AnyToStringCasterMonoBinder` ↔ `AnyToStringCasterBinder`). Остальное в `Runtime/` и весь `Unity/Runtime` — смотреть `find StarterKit/Runtime StarterKit/Unity/Runtime -name '*.cs'`.

Открытые хвосты:
- `ConverterLogger` и `BinderLogger` — дословные копии; решить, сводить ли к общему писателю.
- Проверить в Editor, что Unity вызывает `OnAfterDeserialize` у `[SerializeReference]`-конвертера после правки в Inspector (на это полагаются кеши `TrimStringConverter`, `ThousandsSeparatorConverter`, `StringToVector2/3Converter`, `EnumFlagsToStringConverter`); иначе инвалидировать из `OnValidate` биндера.
- После переименований прогнать инструмент починки сериализованных ссылок (`GenericToStringConverter` → `ValueToStringConverter`, enum `Aggregate` → `AggregateOperation`, `NumberOperation.{Plus,Minus,Division}` → `{Add,Subtract,Divide}`, `EnumMatch` → `EnumMatchMode` с членом `NotEquals` → `NotEqual`, `EnumToValueConverter.Entry` → `LookupEntry`, `IndexMode` → `IndexOutOfRangeMode`, `StringMatch` → `StringMatchMode`, параметр `TimeSpanArithmeticConverter(operandSeconds)` → `operand` (поле `_operandSeconds` → `_operand`); удалён `DecimalFormatConverter`, его роль у `NumberFormatConverter` через `IConverter<decimal, string>`).
- `CollectionElementAtConverter` принимает только `IReadOnlyList`; для сетов/очередей без индексатора реализации нет.
- `CollectionCountToStringConverter` зависит от `PluralizeConverter`/`EnglishPluralRule` из `Runtime/Strings` (ещё не перенесены).
