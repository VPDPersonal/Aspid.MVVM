# Text Binders

Binders for TextMeshPro text components.

---

## TextBinder

The main binder for `TMP_Text.text`. Accepts `string` and, through `INumberBinder`, numeric types.

| Property | Description |
|----------|----------|
| Target | The `TMP_Text` component |
| Converter | `IConverter<string?, string?>` (optional) |
| CultureInfoMode | Number formatting mode (`Invariant`, `Current`, `Custom`) |

**Data types:** `string`, `int`, `float`, `long`, `double` (numbers go through `ToCultureString`).

**Modes:** OneWay, OneTime, OneWayToSource.

```csharp
[ViewModel]
public partial class ScoreViewModel
{
    [OneWayBind] private int _score;       // TextBinder shows "1500"
    [OneWayBind] private string _label;    // TextBinder shows the text as is
}
```

---

## TextSwitcherBinder

`bool` → one of two strings.

| Property | Description |
|----------|----------|
| True Value | Text for `true` |
| False Value | Text for `false` |

**Modes:** OneWay, OneTime.

---

## TextFontBinder / TextFontSwitcherBinder

Binds the `TMP_FontAsset`:

| Binder | Data type | Description |
|--------|-----------|----------|
| `TextFontBinder` | `TMP_FontAsset` | Sets the font |
| `TextFontSwitcherBinder` | `bool` → `TMP_FontAsset` | Picks a font by condition |

---

## TextFontSizeBinder / TextFontSizeSwitcherBinder

Binds the font size:

| Binder | Data type | Description |
|--------|-----------|----------|
| `TextFontSizeBinder` | `float` | Sets `fontSize` |
| `TextFontSizeSwitcherBinder` | `bool` → `float` | Picks a size by condition |

---

## TextAlignmentBinder / TextAlignmentSwitcherBinder

Binds the text alignment:

| Binder | Data type | Description |
|--------|-----------|----------|
| `TextAlignmentBinder` | `TextAlignmentOptions` | Sets the alignment |
| `TextAlignmentSwitcherBinder` | `bool` → `TextAlignmentOptions` | Picks by condition |

---

## Other TMP_Text properties

| Binder | Data type | Property |
|--------|-----------|----------|
| `TextFontStyleBinder` | `FontStyles` | `fontStyle` |
| `TextAutoSizeBinder` | `bool` | `enableAutoSizing` |
| `TextRichTextBinder` | `bool` | `richText` |
| `TextCharacterSpacingBinder` | `float` | `characterSpacing` |
| `TextLineSpacingBinder` | `float` | `lineSpacing` |
| `TextMarginBinder` | `Vector4` | `margin` (left, top, right, bottom) |
| `TextMaxVisibleCharactersBinder` | `int` | `maxVisibleCharacters`; `0` hides the text without clearing it |

The numeric binders (`FontSize`, `CharacterSpacing`, `LineSpacing`, `Margin`) write finite values only: NaN is logged and skipped.

---

## Example: showing stats

```csharp
[ViewModel]
public partial class StatsViewModel
{
    [OneWayBind] private int _health;    // TextBinder → "100"
    [OneWayBind] private bool _isAlive;  // TextSwitcherBinder → "Alive" / "Dead"
}
```

---

## See also

- [Converters](../08-converters.md), StringFormatConverter for templates
- [Switcher](switcher-binders.md), the Switcher pattern
- [StarterKit overview](README.md)
