# Text Binders

Биндеры для управления текстовыми компонентами TextMeshPro.

---

## TextBinder

Основной биндер для `TMP_Text.text`. Принимает `string`, а также числовые типы через `INumberBinder`.

| Свойство | Описание |
|----------|----------|
| Target | `TMP_Text` компонент |
| Converter | `IConverter<string?, string?>` (опционально) |
| CultureInfoMode | Режим форматирования чисел (`Invariant`, `Current`, `Custom`) |

**Типы данных:** `string`, `int`, `float`, `long`, `double` (числа конвертируются через `ToCultureString`).

**Режимы:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class ScoreViewModel
{
    [OneWayBind] private int _score;       // TextBinder покажет "1500"
    [OneWayBind] private string _label;    // TextBinder покажет текст напрямую
}
```

---

## TextSwitcherBinder

`bool` → выбор между двумя строками.

| Свойство | Описание |
|----------|----------|
| True Value | Текст при `true` |
| False Value | Текст при `false` |

**Режимы:** OneWay, OneTime.

---

## TextFontBinder / TextFontSwitcherBinder

Привязка шрифта `TMP_FontAsset`:

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `TextFontBinder` | `TMP_FontAsset` | Устанавливает шрифт |
| `TextFontSwitcherBinder` | `bool` → `TMP_FontAsset` | Выбор шрифта по условию |

---

## TextFontSizeBinder / TextFontSizeSwitcherBinder

Привязка размера шрифта:

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `TextFontSizeBinder` | `float` | Устанавливает `fontSize` |
| `TextFontSizeSwitcherBinder` | `bool` → `float` | Выбор размера по условию |

---

## TextAlignmentBinder / TextAlignmentSwitcherBinder

Привязка выравнивания текста:

| Биндер | Тип данных | Описание |
|--------|-----------|----------|
| `TextAlignmentBinder` | `TextAlignmentOptions` | Устанавливает выравнивание |
| `TextAlignmentSwitcherBinder` | `bool` → `TextAlignmentOptions` | Выбор по условию |

---

## Прочие свойства TMP_Text

| Биндер | Тип данных | Свойство |
|--------|-----------|----------|
| `TextFontStyleBinder` | `FontStyles` | `fontStyle` |
| `TextAutoSizeBinder` | `bool` | `enableAutoSizing` |
| `TextRichTextBinder` | `bool` | `richText` |
| `TextCharacterSpacingBinder` | `float` | `characterSpacing` |
| `TextLineSpacingBinder` | `float` | `lineSpacing` |
| `TextMarginBinder` | `Vector4` | `margin` (left, top, right, bottom) |
| `TextMaxVisibleCharactersBinder` | `int` | `maxVisibleCharacters`; `0` скрывает текст, не очищая его |

Числовые биндеры (`FontSize`, `CharacterSpacing`, `LineSpacing`, `Margin`) пишут только конечные значения: NaN логируется и пропускается.

---

## Пример: отображение статистики

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private int _health;    // TextBinder → "100"
    [OneWayBind] private bool _isAlive;  // TextSwitcherBinder → "Жив" / "Мёртв"
}
```

---

## См. также

- [Конвертеры](../08-converters.md) — StringFormatConverter для шаблонов
- [Switcher](switcher-binders.md) — паттерн Switcher
- [Обзор StarterKit](README.md)
