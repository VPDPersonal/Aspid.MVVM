---
name: starterkit-converter-authoring
description: How to write a new `IConverter` for Aspid.MVVM StarterKit correctly the first time — choosing a base (`NumberConverter`, `StringToNumberConverter`, `DictionaryLookupConverter`), the numeric and Vector families, `ConvertBack` and the "To" naming rule, `ConverterFallback<T>` and when not to use it, nested `[SerializeReference]` slots, `[UsedInModes]`, `[TypeSelectorDisplay]`, logging through `ConverterLogger`, contract tests. Use when creating, reviewing or reworking any converter in `StarterKit/Runtime/Converters`, when the user asks to "add a converter", "make it like the other converters", or shows a converter file.
---

# StarterKit converter

General style: skill `aspid-code-style`; placement: `starterkit-layout`; docs: `aspid-mvvm-xmldoc`. This skill covers only what is specific to converters.

## Skeleton

```csharp
#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>Clamps a number to a range.</summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Numbers",
        Name = "Clamp",
        Tooltip = "Clamps a number to a range")]
    public class ClampNumberConverter : TwoWayNumberConverter
    {
        [Tooltip("Lower bound.")]
        [SerializeField] private double _min;

        [Tooltip("Upper bound.")]
        [SerializeField] private double _max = 1d;

        /// <remarks>Default: 0..1.</remarks>
        public ClampNumberConverter() { }

        /// <param name="min">Lower bound.</param>
        /// <param name="max">Upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">Max is below min.</exception>
        public ClampNumberConverter(double min, double max)
        {
            if (max < min) throw new ArgumentOutOfRangeException(nameof(max));

            _min = min;
            _max = max;
        }

        protected override double Apply(double value) =>
            Math.Clamp(value, _min, _max);
    }
}
```

- The class is `[Serializable]` and not `sealed` when generic. The display name matches the class name; `Group` follows the family.
- `[SerializeReference]` fields typed `IConverter<,>`, `ICollectionFilter<>`, `ICollectionOrder<>`, `ICanExecuteHandler`, `IViewFactory<>`, `PluralRule` are **not** marked `[TypeSelector]`: `StarterKitTypePickerDrawer` supplies the picker by field type. `[TypeSelector(...)]` with arguments remains only where the candidate set must be narrowed.

## Which base to use

| Situation | Base |
|---|---|
| Number → number, one formula, the 16 numeric interfaces | `NumberConverter` / `TwoWayNumberConverter` (`Converters/General/Numbers`); override only `Apply` / `Undo` |
| String → number | `StringToNumberConverter<T>` (`Strings/ToNumber`): `TryParse`/`Clamp`/`ConvertBack`/`Expected`; `decimal` stays separate (string-backed fields) |
| Key → value by table | subclass `DictionaryLookupConverter` (see `EnumToValueConverter`), never a copy of the logic |
| Formatting via `ToString(format, culture)` | `Helpers/Globalization/FormatExtensions.FormatOrGeneral<T>` instead of a private try/catch |
| Unix time, "now" by `DateTimeKind` | `Helpers/Time/UnixTime`, `Helpers/Time/CurrentTime` |
| Range of a narrow type | `Helpers/Numeric/NumericSaturation`: saturate, never drop |

## Which types to accept

- A converter that takes a number implements the whole family `int`, `long`, `float`, `double` when integer input is meaningful (`Lerp`, `Remap`, `Round`, formatters). Normalized `0..1` inputs (`Easing`, `SmoothStep`, `InverseLerp`) and wrappers over Unity float APIs stay `float` **plus** `double → double`; when `Mathf`/`AnimationCurve` sits underneath, double goes through the float path and one `<remarks>` phrase says so.
- One vector operation lives in one class implementing `IConverter<Vector2, Vector2>`, `<Vector3, Vector3>`, `<Vector4, Vector4>`, named without a digit (`VectorNormalizeConverter`). Exception: the `Vector2CombineConverter`/`Vector3CombineConverter` bases.
- If the implementation already accepts `null`, declare the interface as `IConverter<T?, …>`; return-type nullability matches the interface.
- Public `Convert(...)` overloads plus explicit implementations: the explicit one delegates to the public one on the line after `=>`.

## `ConvertBack` and the name

- Add `ConvertBack` where the reverse is unambiguous; do not force it when it is lossy or already covered by a paired converter.
- Drop "To" from the name only when a **reverse interface** exists: `IConverter<B, A>`/`ITwoWayConverter<B, A>` beside the forward one, or `ITwoWayConverter<A, A>` (`DegreesRadiansConverter`, `ColorColor32Converter`). A single `ITwoWayConverter<A, B>` keeps "To" (`BoolToValueConverter`, `StringToIntConverter`). `ConverterAsset` wrappers and caster binders are outside the rule.
- A field used only in `ConvertBack`: name `_convertBackFallback` + `[UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]`. A parser field used only in `Convert`: `[UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]`.

## Errors and fallback

- The constructor throws on invalid arguments. Misconfigured serialized fields are detected in `Convert` and logged **on every call** through `ConverterLogger`; no "already logged" flags.
- `ConverterFallback<T>` is for **runtime** failures that depend on the value (unparseable string, division by zero, a root with no real result), or when runtime and configuration failures mix in one converter. Pure configuration errors (an empty required reference) get no fallback field: `LogError` plus a sensible return.
- The constructor takes the fallback as one optional `ConverterFallback<T>? fallback = null`, assigned as `_fallback = fallback ?? _fallback;`. The internal representation (ticks, a decimal string) never leaks into the signature.
- `UseFallback`/`Fail` calls use named arguments, one per line; the problem text comes from `value.Expected("…")`.
- When `[Min]`/`[Range]` on a field rules the invalid value out of the Inspector, the runtime check for the same condition is removed and the constructor throws `ArgumentOutOfRangeException`. Attribute, exception and Tooltip agree.

## Nested slots

- A parameter that duplicates another converter (a format string, a comparison value) becomes `[SerializeReference] private IConverter<…>? _slot` with a **default implementation in the initializer** (`= new NumberFormatConverter("0.##")`).
- Empty slot: either documented pass-through (optional) or an error on every call plus `ArgumentNullException` in the constructor (required).
- A convenience constructor with the plain parameter may stay, delegating into the slot.
- Do not apply to a field that is the converter's essence (the format in `NumberFormatConverter`).

## Serialized fields

- `[Tooltip]` on every one, one short phrase. A behavioral caveat is repeated in the constructor `<param>`.
- `[Min]`, `[Range]` and similar wherever the field can become invalid: indices and counts `[Min(0)]`, fractions `[Range(0f, 1f)]`.
- No `= default!`, no `[FormerlySerializedAs]`.

## Tests

- The catalogue is covered by contract tests in `Tests/EditMode/Converters/Contracts/*` (`TypeSelectorDisplay`, field coverage, picker, frozen names in `ConverterRenameCompatibilityTests`). Update the frozen list after a rename; make sure the contracts pass after an addition.
- Assertions and comments in English.
