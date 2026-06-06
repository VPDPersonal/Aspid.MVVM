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

**Режимы:** OneWay, OneTime.

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
