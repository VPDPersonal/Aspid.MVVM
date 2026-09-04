# Converters

A converter transforms a value on its way from the ViewModel to the View without touching the ViewModel.
It lets the ViewModel hold what describes the domain (a health `float` from 0 to 1) while the View gets
what the widget needs (`"75%"`, a red color, a bar width).

## Contents

- [Overview](#overview)
- [Contract](#contract)
- [Reverse conversion](#reverse-conversion)
- [Catalogue](#catalogue)
- [Composition](#composition)
- [Converter as an asset](#converter-as-an-asset)
- [Data failures](#data-failures)
- [Your own converter](#your-own-converter)
- [Using in the Inspector](#using-in-the-inspector)

---

## Overview

Typical cases:

| From | To | Converter |
|----|---|-----------|
| `float` 0..1 | `"75%"` | `NumberFormatConverter` with the `P0` format |
| `int` 1500 | `"Score: 1500"` | `StringFormatConverter` |
| health `float` | `Color` red → green | `ThresholdColorConverter` |
| `float` 0..1 | a `Vector3` scale | `FloatToVectorConverter` |
| `TimeSpan` | `"01:23"` | `TimeSpanFormatConverter` |
| `int` seconds | `"2 hours ago"` | `RelativeTimeConverter` |

A converter is assigned to a binder: in the Inspector through `[SerializeReference]`, or through the
constructor from code.

---

## Contract

```csharp
public interface IConverter { }                        // marker, no members

public interface IConverter<in TFrom, out TTo> : IConverter
{
    TTo Convert(TFrom value);
}
```

The non-generic `IConverter` declares nothing. It exists so validation, the picker and tests can
recognise a converter without enumerating every closed generic. You never implement it directly: it is
inherited automatically.

> [!NOTE]
> The marker stays empty on purpose: one type may implement `IConverter<,>` as many times as it likes,
> so a member naming the converted types would have to speak for all implementations at once.

Converters that can convert both ways also implement `ITwoWayConverter<TFrom, TTo>` with `ConvertBack`:
`BoolInvertConverter`, `EnumToNumberConverter<TEnum>`, `PassthroughConverter<T>`, `SequenceConverter<T>`.

---

## Reverse conversion

`IConverter` is one-way. A converter that can undo itself also implements `ITwoWayConverter`:

```csharp
public interface ITwoWayConverter<TFrom, TTo> : IConverter<TFrom, TTo>
{
    TFrom ConvertBack(TTo value);
}
```

A binder in `BindMode.TwoWay` or `BindMode.OneWayToSource` calls `ConvertBack` when the converter offers
it and passes the value unchanged when it does not. The forward converter is never applied on the way
back: it describes the presentation in the View, and running it towards the ViewModel would write the
presentation back into the model.

> [!NOTE]
> Only binders derived from `TargetBinder` / `ComponentMonoBinder` log a console warning about an assigned
> one-way converter. Binders with their own converter field (`InputField`, `Slider`, `RendererMaterials`,
> `ValueTwoWayBinder`) apply `ConvertBack` but stay silent: the value simply travels unconverted.

The implementation is expected to satisfy `ConvertBack(Convert(x)) == x`. A converter that cannot
guarantee this must not implement the interface, otherwise the value drifts on every round trip.

A converter that has a **reverse interface**, a second implementation taking the result and returning the
source (`IConverter<B, A>` or `ITwoWayConverter<B, A>` next to the forward pair, or `ITwoWayConverter<A, A>`
where both sides match), is named **without `To`**: `Vector2Vector3Converter`, `ColorColor32Converter`,
`DegreesRadiansConverter`. A single `ITwoWayConverter<A, B>` adds no direction (`ConvertBack` lives inside
the same pair), so `StringToIntConverter` and `BoolToValueConverter` keep `To`. The name thus tells whether
the converter can be attached from either side. The one exception is `SnapToStepConverter`: there "to" is
part of the operation, not a link between two types.

Two-way out of the box:

`AngleToQuaternionConverter`, `ArithmeticNumberConverter`,
`AudioLinearDecibelConverter`, `BoolInvertConverter`, `BoolLogicConverter`,
`BoolToValueConverter`, `CachedConverter`, `ColorColor32Converter`,
`ColorToHtmlStringConverter`, `ColorVector4Converter`, `DateTimeToUnixTimestampConverter`,
`DegreesRadiansConverter`, `EnumToNumberConverter`, `EulerToQuaternionConverter`,
`InverseConverter`, `InverseLerpConverter`, `LerpNumberConverter`,
`NormalizedPercentConverter`, `OffsetThenScaleConverter`, `PassthroughConverter`,
`PowerNumberConverter`, `QuaternionOffsetConverter`,
`RectVector4Converter`, `RemapNumberConverter`, `SafeConverter`,
`SecondsToTimeSpanConverter`, `SequenceConverter`, `StringToBoolConverter`,
`StringToDateTimeConverter`,
`StringToDecimalConverter`,
`StringToDoubleConverter`, `StringToEnumConverter`, `StringToFloatConverter`,
`StringToIntConverter`, `StringToLongConverter`, `StringToTimeSpanConverter`,
`StringToVector2Converter`, `StringToVector3Converter`, `UnixTimestampToDateTimeConverter`,
`Vector2Vector3Converter`, `VectorToVectorIntConverter`.

---

## Catalogue

The package ships 192 converters. One layout rule: **the group is the type of the value stored in the
ViewModel; the `To <type>` subgroup is what it becomes**. Looking for a converter, start from what you
have: a `float` is in `Aspid/Number`, a string in `Aspid/String`; need another output type, open the
`To ...` subgroup. Three exceptions: `Aspid/Composition` wraps other converters, `Aspid/Localization`
keeps all localization in one place, `Aspid/Asset` is the infrastructure of converter assets.

The same rule applies to the sources: the folder mirrors the group
(`Converters/Strings/ToNumber/` ↔ `Aspid/String/To Number`).

### Aspid/Bool (3)

`BoolInvertConverter`, `BoolLogicConverter`; **To Value**: `BoolToValueConverter`.

> [!NOTE]
> `BoolToValueConverter` is two-way: the reverse path compares the incoming value with the two configured
> ones and returns the matching bool. A value matching neither yields the fallback; equal values in both
> branches make the reverse path impossible and are reported as an error in the console.

### Aspid/Number (51)

Number → number: `AngleDifferenceConverter`, `AngleWrapConverter`, `AnimationCurveConverter`,
`ArithmeticNumberConverter`, `AudioLinearDecibelConverter`,
`ClampNumberConverter`, `CountdownProgressConverter`, `DegreesRadiansConverter`,
`EasingConverter`, `InverseLerpConverter`, `LerpNumberConverter`, `ModuloNumberConverter`,
`NormalizedPercentConverter`, `NumericCastConverter`,
`PowerNumberConverter`, `RemapNumberConverter`,
`RoundNumberConverter`, `SmoothStepConverter`, `SnapToStepConverter`,
`OffsetThenScaleConverter`, `UnaryMathConverter`, `WrapNumberConverter`

| Subgroup | Converters |
|-----------|-----------|
| To Bool | `NumberCompareConverter` |
| To Color | `ColorLerpConverter`, `GradientEvaluateConverter`, `ThresholdColorConverter` |
| To Enum | `NumberToEnumConverter` |
| To Quaternion | `AngleToQuaternionConverter`, `QuaternionSlerpConverter` |
| To Rect Offset | `IntToRectOffsetConverter` |
| To Sprite | `NormalizedToSpriteConverter` |
| To String | `AbbreviatedNumberConverter`, `ByteSizeConverter`, `CurrencyConverter`, `NumberFormatConverter`, `OrdinalConverter`, `PaddedNumberConverter`, `PluralizeConverter`, `RatioToStringConverter`, `RepeatStringConverter`, `RomanNumeralConverter`, `SecondsToTimeStringConverter`, `SignedNumberStringConverter`, `ThousandsSeparatorConverter`, `ThresholdRichTextColorConverter` |
| To Time | `SecondsToTimeSpanConverter`, `UnixTimestampToDateTimeConverter` |
| To Value | `IndexToValueConverter` |
| To Vector | `FloatToVectorConverter`, `VectorLerpConverter` |

> [!NOTE]
> `NumericCastConverter` is the only controlled way to narrow a number. Without it `long.MaxValue` landing
> in an int binder silently turns negative; `OverflowMode.Saturate` clamps to the bound, `Checked` throws.

> [!NOTE]
> `SecondsToTimeSpanConverter` accepts `int`, `long`, `float` and `double`. In the integer overloads the
> reverse path drops the fractional second, and a span that does not fit `int` or `long` is clamped to the
> type's bound.

### Aspid/String (33)

String → string: `ConcatStringConverter`, `DefaultStringConverter`, `MaskStringConverter`,
`PadStringConverter`, `ReplaceStringConverter`, `ReverseStringConverter`,
`SplitJoinStringConverter`, `StringFormatConverter`, `SubstringConverter`, `TextCaseConverter`,
`TrimStringConverter`, `TruncateStringConverter`

| Subgroup | Converters |
|-----------|-----------|
| Rich Text | `RichTextColorConverter`, `RichTextNoParseConverter`, `RichTextSizeConverter`, `RichTextStyleConverter`, `RichTextSanitizeConverter` |
| To Bool | `StringEmptyToBoolConverter`, `StringMatchToBoolConverter`, `StringToBoolConverter` |
| To Color | `HashToColorConverter`, `ParseHtmlStringConverter` |
| To Enum | `StringToEnumConverter` |
| To Number | `StringToDecimalConverter`, `StringToDoubleConverter`, `StringToFloatConverter`, `StringToIntConverter`, `StringToLongConverter` |
| To Sprite | `StringToSpriteConverter` |
| To Time | `StringToDateTimeConverter`, `StringToTimeSpanConverter` |
| To Vector | `StringToVector2Converter`, `StringToVector3Converter` |

The parsing converters (`String To *`) appear in the picker as `Parse *`: they parse culture-aware
(`CultureInfoMode`) and return the fallback for text that does not read.

> [!WARNING]
> Any text typed by a player needs `RichTextSanitizeConverter` or `RichTextNoParseConverter`. TMP executes
> markup in every string it receives: a nickname `<size=400%>` stretches every label it appears in, on every
> other player's screen. `RichTextNoParse` wraps everything in `<noparse>`; `SanitizeRichText` strips or
> escapes tags selectively, keeping an allow list.

> [!NOTE]
> `StringEmptyToBoolConverter` picks what counts as a missing string through its `StringEmptiness` field:
> `NullOrEmpty` (default); `Null`, where an empty string counts as filled; `NullOrWhiteSpace`, where a
> string of spaces counts as empty. The last one is what "did the user type anything?" means.

### Aspid/Time (9)

`TimeSpanArithmeticConverter`, `TimeUntilConverter`

| Subgroup | Converters |
|-----------|-----------|
| To Bool | `DateTimeCompareConverter` |
| To Number | `DateTimeToUnixTimestampConverter`, `TimeSpanToNumberConverter` |
| To String | `DateTimeFormatConverter`, `DateTimeOffsetFormatConverter`, `RelativeTimeConverter`, `TimeSpanFormatConverter` |

### Aspid/Enum (7)

`EnumMaskConverter`; **To Bool**: `EnumMatchConverter`; **To Collection**:
`EnumToDropdownOptionDataConverter`; **To Number**: `EnumToNumberConverter`; **To String**:
`EnumFlagsToStringConverter`, `EnumToStringConverter`; **To Value**: `EnumToValueConverter`.

### Aspid/Collection (11)

`CollectionTakeConverter`, `DictionaryLookupConverter`

| Subgroup | Converters |
|-----------|-----------|
| To Bool | `CollectionContainsToBoolConverter`, `CollectionEmptyToBoolConverter` |
| To Number | `CollectionAggregateConverter`, `CollectionCountConverter` |
| To String | `CollectionCountToStringConverter`, `CollectionJoinToStringConverter` |
| To Value | `CollectionElementAtConverter`, `CollectionFirstConverter`, `CollectionLastConverter` |

> [!NOTE]
> Every converter in the group except `CollectionElementAtConverter` accepts any `IEnumerable<T>`, an
> iterator or a LINQ query included. The counters use `Count` when it exists and enumerate only when it does
> not; `CollectionEmptyToBoolConverter` then pulls a single element. `CollectionElementAtConverter` stays on
> `IReadOnlyList<T>`: picking an element from the end needs the length.

### Aspid/Object (4)

`NullCoalesceConverter`; **To Bool**: `EqualityToBoolConverter`;
**To String**: `ValueToStringConverter`, `ObjectNameConverter`, `ObjectToStringConverter`.

> [!NOTE]
> `EqualityToBoolConverter` with an empty operand acts as an "is the object missing" check and treats a
> destroyed `UnityEngine.Object` as missing: the null side is compared through Unity's overloaded `==`,
> because `is null` returns `false` for a destroyed object. Reference equality compares instances as they
> are; a destroyed object does not equal the empty operand there.

### Aspid/Vector (28)

Vector → vector: `Vector2Vector3Converter`, `Vector3Vector4Converter`,
`VectorArithmeticConverter`, `VectorClampComponentsConverter`,
`VectorClampMagnitudeConverter`, `VectorNormalizeConverter`, `VectorRoundConverter`,
`VectorSwizzleConverter`, `VectorToVectorIntConverter`

> [!NOTE]
> `VectorArithmeticConverter`, `VectorClampComponentsConverter`, `VectorClampMagnitudeConverter`,
> `VectorNormalizeConverter`, `VectorRoundConverter`, `VectorSwizzleConverter`, `VectorToFloatConverter`
> and `FloatToVectorConverter` serve `Vector2`, `Vector3` and `Vector4` with one class,
> `VectorToVectorIntConverter` serves `Vector2` and `Vector3`, and `Vector2Vector3Converter` goes both ways.
> Vector settings (`_operand`, `_min`, `_max`) are stored as `Vector4`, and only the components the bound
> vector has are read.

| Subgroup | Converters |
|-----------|-----------|
| Combine | `BoxCollider2DOffsetCombineConverter`, `BoxCollider2DSizeCombineConverter`, `BoxColliderCenterCombineConverter`, `BoxColliderSizeCombineConverter`, `CapsuleColliderCenterCombineConverter`, `RectTransformAnchoredPosition2DCombineConverter`, `RectTransformAnchoredPositionCombineConverter`, `RectTransformSizeDeltaCombineConverter`, `SphereColliderCenterCombineConverter`, `TransformEulerAnglesCombineConverter`, `TransformPosition2DCombineConverter`, `TransformPositionCombineConverter`, `TransformScaleCombineConverter` |
| To Number | `DirectionAngleConverter`, `VectorDistanceConverter`, `VectorToFloatConverter` |
| To Quaternion | `EulerToQuaternionConverter`, `LookRotationConverter` |
| To Rect Offset | `Vector4ToRectOffsetConverter` |

> [!NOTE]
> The `Combine` subgroup takes some components from the bound vector and the rest from a scene component
> (`Transform`, `RectTransform`, a collider). The `*2D*` pairs are for 2D colliders and `Vector2` properties.

### Aspid/Color (14)

Color → color: `ColorAlphaConverter`, `ColorBlockAlphaConverter`,
`ColorBlockFadeDurationConverter`, `ColorBlockStateConverter`, `ColorBlockTintConverter`,
`ColorChannelConverter`, `ColorGrayscaleConverter`, `ColorHsvConverter`, `ColorTintConverter`,
`ColorColor32Converter`, `ColorToColorBlockConverter`, `HdrIntensityConverter`;
**To String**: `ColorToHtmlStringConverter`; **To Vector**: `ColorVector4Converter`.

### Other groups

| Group | Converters |
|--------|-----------|
| `Aspid/Quaternion` (4) | `QuaternionOffsetConverter`; **To Number**: `QuaternionToAngleConverter`; **To Vector**: `QuaternionToEulerConverter`, `QuaternionVector4Converter` |
| `Aspid/Bounds` (2) | **To Rect**: `BoundsToRectConverter`; **To Vector**: `BoundsToVectorConverter` |
| `Aspid/Rect` (1) | **To Vector**: `RectVector4Converter` |
| `Aspid/Rect Offset` (1) | `RectOffsetScaleConverter` |
| `Aspid/Texture` (3) | `SpriteToTextureConverter`, `Texture2DToSpriteConverter`; **To Rect**: `TextureToSpriteRectConverter` |
| `Aspid/Localization` (4) | `LocaleToStringConverter`, `LocalizedEnumConverter`, `LocalizedNumberConverter`, `LocalizedStringConverter` |
| `Aspid/Material` (1) | `MaterialInstanceConverter` |
| `Aspid/Asset` (1) | `ConverterAssetReference` |

The `Aspid/Composition` group (8) is described under [Composition](#composition).

---

## Shared enums

Three enums configure converters across groups, so they are worth knowing before the catalogue.

| Enum | Where | What it decides |
|------|-----------------|-----------|
| `ComparisonMode` | `NumberCompareConverter`, `DateTimeCompareConverter` | `Equal`, `NotEqual`, `LessThan`, `GreaterThan`, `LessThanOrEqual`, `GreaterThanOrEqual`. Read as `bound <op> configured`. In `NumberCompareConverter` the tolerance is shared by all six comparisons and depends on the type: `int`/`long` exact, `float` 1e-6 of the magnitude, `double` 1e-12 |
| `CultureInfoMode` | every string and parsing converter, plus input field and text binders | Which culture formats and parses. Text the player sees: `CurrentCulture`; text going to a save, the network or `PlayerPrefs`: `InvariantCulture` |
| `ConverterFailureMode` | `BoolLogicConverter`, `EnumMaskConverter` | What to do with a value that cannot be converted: `ReturnFallback` or `ReturnInput`. Only converters whose input and output types match have the field; elsewhere there is nothing to return |

> [!WARNING]
> The decimal separator is a comma in half of Europe. A number written under one culture and parsed under
> another loses its fraction instead of failing: `1,5` under `InvariantCulture` reads as `15`. Use
> `InvariantCulture` for everything that round-trips.

`CultureInfoMode` resolves to a `CultureInfo` through the `ToCultureInfo()` extension in
`ToCultureStringExtensions`, which also holds the `ToCultureString(number, mode)` overloads. Both types
live outside the converters folder, in `StarterKit/Runtime/Globalization`: input field and text binders
carry the same serialized field.

---

## Pluralization

`PluralizeConverter` (`Aspid/Number/To String`) writes a number followed by a word in the right form. It
holds no grammar: the converter keeps only the phrase format, while the words and the rule that picks them
live in a `PluralRule` chosen in the Inspector, group `Aspid/Plural Rule`.

`PluralRule` is an abstract class implementing `IConverter<long, string>`: number (absolute value) → word.
A subclass declares only the words its language needs, so the Inspector never shows a field the chosen
grammar cannot reach.

| Rule | Languages | Fields |
|---------|-------|------|
| `SingleFormPluralRule` | Chinese, Japanese, Korean, Thai, Vietnamese, Turkish | `word` |
| `EnglishPluralRule` | English, German, Dutch, Spanish, Italian, Swedish | `one`, `other` |
| `FrenchPluralRule` | French, Brazilian Portuguese, Hindi | `one` (0 and 1), `other` |
| `EastSlavicPluralRule` | Russian, Ukrainian, Belarusian | `one`, `few`, `many` |
| `PolishPluralRule` | Polish | `one` (exactly 1), `few`, `many` |
| `CzechPluralRule` | Czech, Slovak | `one`, `few`, `other` |
| `ArabicPluralRule` | Arabic | `one`, `two`, `few`, `many`, `other` |

Common to all is the `zero` field from the base class: an optional word that takes zero regardless of
grammar. English has no separate zero form, yet everyone needs "No items". A word the grammar reaches but
that is left empty is logged on every push; the converter will not silently substitute a neighbouring form.

Language not in the list: subclass `PluralRule` in the project, declare your fields and override
`Word(long)`. Zero handling and the empty-word report come from the base, and the picker places the rule in
the same group next to the built-in ones.

`CollectionCountToStringConverter` does not duplicate this logic: it counts the elements, hands the number
to `PluralizeConverter` and keeps only the text for an empty collection, the phrase written without a number
in front.

---

## Composition

The `Aspid/Composition` group holds no conversions, only wrappers around other converters.

| Converter | Purpose |
|-----------|-----------|
| `ComposeConverter<TFrom, TMid, TTo>` | Two converters in a row, with different types at the seam |
| `SequenceConverter<T>` | A chain of any length, every link `T → T` |
| `CachedConverter<TFrom, TTo>` | Repeats the last result while the input is unchanged; each direction caches separately |
| `SafeConverter<TFrom, TTo>` | Catches an exception from the inner converter and returns the fallback, in both directions |
| `NullGuardConverter<TFrom, TTo>` | Does not call the inner converter on `null` |
| `ConditionalConverter<T>` | Picks one of two converters by a predicate |
| `PassthroughConverter<T>` | Does nothing; a stub and the default element |
| `InverseConverter<TFrom, TTo>` | Runs a two-way converter backwards |

```csharp
// float → "1,500" with a cache, so the string is not rebuilt on every push
var converter = new CachedConverter<float, string>(
    new NumberFormatConverter());
```

Keep `CachedConverter` in mind: a binder sends the value on every **notification**, not on every
**change**, so an allocating converter is called far more often than it seems.

`SafeConverter` matters because binder dispatch is a bare multicast: an exception from one converter cuts
the subscriber list short and stops the neighbouring, innocent binders.

A wrapper without something to wrap is meaningless, so wrapper constructors throw `ArgumentNullException`
on an empty link: `inner` in `Cached`, `Safe` and `NullGuard`, both links in `Compose`, the converter in
`Inverse`, the predicate in `Conditional`, the asset in `ConverterAssetReference`. The half-empty state
belongs to the Inspector: there a wrapper is assembled one field at a time, so an empty link does not
throw; it reports an error on every conversion and returns the fallback. The `then` / `else` branches of
`Conditional` and the links of `SequenceConverter` may be empty: `null` there means "skip this step".

---

## Converter as an asset

`ConverterAsset<TFrom, TTo>` is a `ScriptableObject` wrapper around an ordinary `[SerializeReference]`
converter. Twelve gradient stops or a forty-entry enum map typed into a binder field belong to that one
field: they must be retyped in every prefab, and every fix repeated everywhere. An asset is configured once
and attached by reference.

Ready-made subclasses live in **Create → Aspid → MVVM → Converters**, grouped by input type (`Numbers`,
`String`, `Vector`, `Color`, `Time`, …): same-name ones for conversions "into itself" (`Float Converter`,
`Vector3 Converter`) and `X To Y Converter` for type changes (`Vector3 To Vector2 Converter`,
`String To Int Converter`). Not every pair of the catalogue is covered: numeric casts between `int`, `long`,
`float` and `double` have no assets and are configured through a `[SerializeReference]` field. A missing
pair is an empty sealed one-line subclass: Unity cannot create an asset of an open generic, so the types
must be closed. The enum family also ships open bases (`EnumConverterAsset<T>` and so on): close them over
your enum.

```csharp
[CreateAssetMenu(menuName = "Game/Converters/Health Color", fileName = "HealthColorConverter")]
public sealed class HealthColorConverterAsset : ConverterAsset<float, Color> { }
```

Such an asset is assigned to a binder through `ConverterAssetReference`, which sits in the regular
converter picker because a managed reference cannot hold a `ScriptableObject` directly.

---

## Data failures

A converter given a value it cannot convert (a color string that does not parse, a number out of range)
reports an error and returns the configured fallback, the `Fallback` field in the Inspector.

The error is reported on every failure, not once: a value that stops converting midway through a session
is exactly the case a "log once" rule hides.

The fallback answers **any** failure: data that does not convert, and configuration through which nothing
converts (both `BoolToValueConverter` branches equal, the list of true spellings empty).

`ConverterFailureMode` adds a second option to the fallback, returning the input unchanged
(`ReturnInput`), and therefore exists only on `BoolLogicConverter` and `EnumMaskConverter`: the input can
be returned only where input and output share a type. Other converters have nothing to return, so they have
no `On Failure` field.

Every message, about a data failure or a bad setting, goes through one helper, `ConverterLogger`: a data
failure is printed via `LogError` as `[Aspid.MVVM] Converter: expected X but got "Y". Using the fallback.`
(or `Returning the input unchanged.` under `ReturnInput`), everything else as
`[Aspid.MVVM] Converter: problem. What is returned instead of the result.` The `[Aspid.MVVM]` prefix
finds the package's errors in the console; `null` is printed as the word `null`, a string in quotes, other
values as they are; a generic converter name is printed closed (`BoolToValueConverter<Sprite>`). The
formatting of types and values itself lives in `LogMessageText`: `LogMessageText.GetTypeName` writes the
type name and the `value.Describe()` extension writes the value, interpolated straight into the message
text. Text inside a message therefore looks the same in a log and in an exception. For messages that are
not errors the helper has a plain `Log` in the same format. `Debug.Log`/`Debug.LogError` in converter code
live only inside `ConverterLogger`.

A converter logs about itself through extension methods on `IConverter`: `this.LogError(problem,
consequence)`, `this.LogError(exception, consequence)`, `this.Log(message)`. The type is taken from `this`,
and a converter that is itself a Unity object (`ConverterAsset`, for example) automatically becomes the
`context`, the object Unity highlights when the log is clicked; for the rest `context` is an optional
parameter. The `Type` overloads remain for helpers that report on someone else's behalf.

In code the fallback is returned with a single call, the extension
`this.UseFallback(_fallback, value.Expected("a whole number"))`: it logs the failure and returns
`_fallback`. The call brings the problem wording ready-made; the canonical one, "expected X but got Y", is
built by the `value.Expected("a whole number")` extension from `LogMessageText`, while a configuration
failure writes its own.

The two converters that have a mode keep it together with the value in one `ConverterFallback<T>` field
(two rows in the Inspector: the value and `On Failure`), and the call reads
`_fallback.Fail(this, value, problem)`: it also logs the failure but returns whatever the mode dictates, so
it takes the value on which the failure happened as well.

---

## Your own converter

```csharp
using System;
using Aspid.MVVM.StarterKit;
using Aspid.FastTools.Types;

[Serializable]
[TypeSelectorDisplay(Group = "Game/String", Name = "Percent", Tooltip = "0..1 as a percentage")]
public sealed class PercentConverter : IConverter<float, string>
{
    public string Convert(float value) => $"{value * 100:F0}%";
}
```

With Inspector parameters:

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Game/Number", Name = "Clamp", Tooltip = "Clamps the value to a range")]
public sealed class ClampFloatConverter : IConverter<float, float>
{
    [Tooltip("Lower bound.")]
    [SerializeField] private float _min;

    [Tooltip("Upper bound.")]
    [SerializeField] private float _max = 1f;

    public float Convert(float value) => Mathf.Clamp(value, _min, _max);
}
```

The class name follows one rule. `XToYConverter` is the reserved name of the canonical conversion of a
type pair, the single one expected by default (`Vector2Vector3Converter`, `StringToIntConverter` for
parsing). Any other converter of the same pair is named after the operation (`OrdinalConverter`,
`StringLengthConverter`), and a variant of the canonical conversion is a setting on the existing class,
not a new class. In the picker `Name` carries the operation; the types are already stated by the group
and subgroup.

Checklist:

- **`[Serializable]`**: without it the class does not appear in the `[SerializeReference]` list.
- **A parameterless constructor**: the picker creates the instance with it, through
  `Activator.CreateInstance(type, nonPublic: true)`, so it need not be public. Many converters hide it as
  `private`, keeping only the parameterized constructor public. If there is no such constructor at all, mark
  the class `[TypeSelectorDisplay(Hidden = true)]`, otherwise it shows up in the list and selecting it breaks.
- **`[Tooltip]` on every serialized field**: XML documentation is invisible in the Inspector; the tooltip is
  the only explanation that reaches whoever configures the value.
- **`Group` and `Tooltip` in `[TypeSelectorDisplay]`**: otherwise the converter lands in the flat common list.
- **No allocations without a cache**: see `CachedConverter` above.

> [!NOTE]
> `TypeSelectorDisplayAttribute` is marked `[Conditional("UNITY_EDITOR")]` and `Inherited = false`. Two
> consequences. Every annotation disappears from the metadata when the assembly is built outside Unity: a
> DLL built by a plain `dotnet build` reaches the picker without groups, names or hidden flags. And the
> attribute is not inherited: a subclass does not receive the base class markup and must repeat it.

---

## Using in the Inspector

1. On the binder (`TextBinder`, for example) find the **Converter** field.
2. Click the dropdown: the `[SerializeReference]` picker opens with groups.
3. Pick a converter and configure its fields.

From code:

```csharp
// a lambda as a converter
var converter = new FuncConverter<float, string>(value => $"{value:P0}");

// the same through ToConverter
IConverter<float, float> doubler = ((Func<float, float>)(x => x * 2f)).ToConverter();
```

---

## See also

- [Binders](06-binders.md), how a binder applies a converter
- [Binding Modes](03-binding-modes.md), when `ConvertBack` is called
- [StarterKit](StarterKit/README.md), ready-made binders with converter support
