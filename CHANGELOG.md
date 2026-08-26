# Changelog

All notable changes to **Aspid.MVVM** are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

> 🌐 Русская версия: [CHANGELOG.ru.md](CHANGELOG.ru.md)

---

## [Unreleased]

### Added

- **~150 converters**, taking the catalogue from 14 to 148 and closing every empty `[SerializeReference]` picker in the package. Grouped in the type-picker dropdown as `Aspid/Bool` (9), `Aspid/Number` (15), `Aspid/String` (36), `Aspid/Time` (8), `Aspid/Color` (14), `Aspid/Vector` (23), `Aspid/Rotation` (9), `Aspid/Collection` (6), `Aspid/Enum` (5), `Aspid/Object` (3), `Aspid/Texture` (4), `Aspid/Layout` (3), `Aspid/Localization` (4), `Aspid/Asset` (1), `Aspid/Composition` (6). Highlights: `PercentStringConverter`, `AbbreviatedNumberConverter` (`1234` → `1.2K`), `RelativeTimeConverter`, `SecondsToTimeStringConverter`, `ThresholdColorConverter`, `RichTextNoParseConverter`, `RemapNumberConverter`, `CollectionCountConverter`.
- **`ITwoWayConverter<TFrom, TTo>`** — a converter that can undo itself. A binder in `BindMode.TwoWay` or `BindMode.OneWayToSource` now applies `ConvertBack` when the configured converter offers one, and warns in the console when it does not. Twenty-four shipped converters implement it.
- **Non-generic `IConverter`** as the root of the hierarchy. It declares nothing; it exists so validation, the picker and tests can recognise a converter without enumerating every closed generic.
- **A shared answer to data a converter cannot convert**: it reports the failure on the console and returns an authored fallback field, replacing three different ad-hoc answers. `ConverterFailureMode` (`ReturnFallback`, `ReturnInput`) adds the choice of passing the input through instead, and is carried only by `BoolLogicConverter` and `EnumMaskConverter` — the two converters whose input and output share a type, which is the only case where there is an input to return.
- **A pluggable plural grammar.** `PluralizeConverter` no longer hard-codes the languages it can word: a `PluralRule` picked in the Inspector carries both the grammar and the words it picks between — `SingleForm`, `English`, `French`, `EastSlavic`, `Polish`, `Czech`, `Arabic` — and each declares only the words its own language uses, so the Inspector never offers a field the chosen grammar cannot reach. A rule is an `IConverter<long, string>`, so a language the package does not ship is a subclass written in the project rather than an edit to the converter.
- **Composition primitives** under `Aspid/Composition`: `ComposeConverter`, `CachedConverter`, `SafeConverter`, `NullGuardConverter`, `ConditionalConverter`, `PassthroughConverter`. `CachedConverter` matters more than it looks — a binder pushes on every *notification*, not every *change*, so an allocating converter runs far more often than it appears to.
- **`ConverterAsset<TFrom, TTo>`** — a converter authored once as a `ScriptableObject` and referenced from any number of fields through `ConverterAssetReference`, instead of re-authored per prefab. 88 ready-made subclasses in **Create → Aspid → MVVM → Converters**.
- **`CultureInfoMode`** on every string and parsing converter. The decimal separator is a comma in half of Europe, so a number written by one culture and parsed by another loses its fraction rather than failing; `InvariantCulture` is now selectable for anything that round-trips.
- **A test suite where there was none**: 1048 EditMode tests, including contract tests that fail when a converter field has nothing to pick, when a serialized field has no `[Tooltip]`, when a pickable converter has no group, when a picker tooltip has a gap in it, or when a code-only converter is offered in the dropdown.

### Changed

- **The last three mirror pairs became one two-way converter each.** `Vector4ToVector3Converter` folded into `Vector3ToVector4Converter`, `Vector4ToQuaternionConverter` into `QuaternionToVector4Converter`, and `AngleToDirectionConverter` into `DirectionToAngleConverter`. Each survivor implements both pairs of interfaces, so a binder starting from either side still finds it. The angle pair gains something it did not have: the reverse pass now reads the same unit, offset and winding as the forward one, so an angle round-trips instead of coming back rotated.
- **Four more duplicated pairs became one converter each.** `Vector2ToFloatConverter` folded into `VectorToFloatConverter` (`Vector2`, `Vector3` and `Vector4`, with `VectorComponent` gaining `W` and `Vector2Component` gone); `FloatToVector2Converter` into `FloatToVectorConverter` (`AxisMask` gaining `W`); `Vector3ToVector3IntConverter` into `VectorToVectorIntConverter`; and `Vector3ToVector2Converter` into `Vector2ToVector3Converter`, which now converts both ways off the one mode the way `ColorToColor32Converter` does. A component the bound width does not carry is reported on every push instead of read as a zero measurement.
- **Every Unity wrapper that takes a 0..1 amount, an angle or a curve position now takes a `double` as well** — `AngleDifference`, `AngleWrap`, `AnimationCurve`, `AudioLinearToDecibel`, `DegreesToRadians`, `ColorLerp`, `GradientEvaluate`, `ThresholdColor`, `AngleToQuaternion`, `QuaternionSlerp`, `NormalizedToSprite`, `AngleToDirection`, and `DirectionToAngle` and `QuaternionToAngle` on the way out. The work underneath is Unity's own float math, so the double overload runs through it and carries a float's precision; a ViewModel that exposes a `double` no longer needs a cast converter in front.
- **The numeric helpers under `Converters/Core` are visible to the Unity half of the StarterKit.** They are shared by both assemblies but not API, so `Aspid.MVVM.StarterKit` names its sibling in an `InternalsVisibleTo` rather than making them public.
- **Three more converters serve the widths their job has.** `VectorDistanceConverter` measures a `Vector2` position too, ignoring the depth a 2D scene has no use for; `VectorLerpConverter` moves between `Vector2`, `Vector3` and `Vector4` from one pair of endpoints, widened to `Vector4` and read only as far as the bound vector goes; `ThresholdRichTextColorConverter` takes all four numeric widths like the rest of its group.
- **The last single-width number converters gained the widths their data actually has.** `IndexToValueConverter` takes a `long`, `float` or `double` index (a fraction drops toward zero, a NaN is reported); `PluralizeConverter` takes a `long`, which the `PluralRule` beneath it already spoke; `CurrencyConverter`, `RatioToStringConverter` and `LocalizedNumberConverter` take all four widths like every other formatter in their group; and `EasingConverter`, `SmoothStepConverter`, `InverseLerpConverter`, `NormalizedToPercentConverter` and `CountdownProgressConverter` gained a `double` width. The first two evaluate Unity's own float curves, so their double overload carries a float's precision; the other three compute in `double` throughout.
- **`EnumToIntConverter` is `EnumToNumberConverter` and `IntToEnumConverter` is `NumberToEnumConverter`,** and both serve `int`, `long`, `float` and `double`. The old pair read an enum through `Convert.ToInt32`, which **throws** on a `long`-backed enum whose member is past `int.MaxValue` — a binder push that took the whole scene's dispatch down. The underlying value is now read as a `long`; the int overload reports what it cannot hold and saturates instead of throwing, and a fractional float or double names no member and is refused like any other.
- **`CultureInfoMode` and `ToCultureStringExtensions` moved out of the converter folder** to `StarterKit/Runtime/Globalization`. The input-field and text binders carry the same serialized field, so the enum was a shared vocabulary living three levels deep inside one converter category. The namespace is unchanged, so no `using` moves with it.
- **Converters no longer keep a copy of their own settings to notice an Inspector edit.** The three format-string caches are gone entirely — the standard `F`/`N` formats come from a table — and `TrimStringConverter`, `ThousandsSeparatorConverter`, `StringToVector2Converter`, `StringToVector3Converter` and `EnumFlagsToStringConverter` drop their cache in `ISerializationCallbackReceiver.OnAfterDeserialize` instead, which is the one moment an authored field changes. Sixteen fields and their per-push comparisons leave the player build; behaviour is unchanged.
- **`SafeConverter` and `CachedConverter` are two-way.** Wrapping a two-way converter in either one used to drop its reverse path silently — a `TwoWay` binder warned that the converter could not undo itself while the converter inside it could. `Safe` catches the reverse pass the same way it catches the forward one and takes its own reverse fallback; `Cached` memoizes each direction separately, because the inner converter need not be a bijection. A one-way inner is reported on every reverse push instead of being ignored.
- **The collection converters accept any sequence.** `CollectionCountConverter`, `CollectionEmptyToBoolConverter` and `CollectionCountToStringConverter` now take an `IEnumerable<T>` as well as an `IReadOnlyCollection<T>`, so an iterator or a LINQ query can be counted or tested. A sequence that carries a count of its own is asked for it; only one that does not is walked, and emptiness pulls a single item. `CollectionCountConverter` also gained the `long`/`float`/`double` output family. `CollectionElementAtConverter` keeps its `IReadOnlyList<T>` — choosing an element from the end needs the length.
- **One notion of "blank" across the string converters.** `string.IsNullOrWhiteSpace` is the test wherever a converter asks "is there anything here?" — a lookup key, a value about to be parsed, a string about to be decorated. A whitespace-only string now takes the blank path instead of being parsed, masked or reported as a miss. Where spaces are the point — a separator, a search needle, a pad or trim set, a composite format — the empty test stays.
- **`FuncConverter` is `FuncConverter`** and lives under `Converters/Composition/`: it wraps a delegate or another converter and performs no conversion of its own.
- **`ColorToHtmlStringConverter` is two-way**, and the HTML colour mapping has one implementation per direction: writing lives on it, parsing on `ParseHtmlStringConverter`, and each calls the other's. A blank string takes the fallback silently in both directions.
- **`DateTimeToUnixTimestampConverter` and `UnixTimestampToDateTimeConverter` agree.** One `DateTimeKind` rule (an `Unspecified` moment reads as local, both directions), one out-of-range rule (clamp to the nearest bound and report), and the catalogue's fallback shape — `ConverterFallback<DateTime>` taken as one optional constructor parameter — in place of the loose tick field. The `DateTime` side gained the `int`/`long`/`double` output family.
- **The reverse fallbacks on `OffsetThenScaleConverter` and `PowerNumberConverter` widened to `double`,** so a `double` binding on `ReturnInput` gets its own value back instead of a float-narrowed one.
- **British spelling out of the API.** `BoxColliderCentreCombineConverter`, `CapsuleColliderCentreCombineConverter` and `SphereColliderCentreCombineConverter` became `…CenterCombineConverter`, matching Unity's own `center` property. All three carry `[MovedFrom]`.
- **Number converters are no longer single-type.** `LerpNumberConverter`, `RemapNumberConverter`, `OffsetThenScaleConverter`, `SnapToStepConverter`, `WrapNumberConverter`, `RoundNumberConverter`, `PowerNumberConverter`, `UnaryMathConverter`, `TimeSpanToNumberConverter` and the `Ordinal`, `PaddedNumber`, `ThousandsSeparator`, `AbbreviatedNumber`, `SignedNumberString` and `ByteSize` formatters now serve `int`, `long`, `float` and `double` through explicit interface members over one `double` core, the way `ArithmeticNumberConverter` already did. Existing public signatures are unchanged; the integer overloads truncate and saturate through `NumericSaturation`.
- **`Aspid.MVVM Settings`** window restyled to match the Aspid.FastTools **Welcome** window — animated dot background, animated logo (links to the Asset Store) and title, themed cards, gradient `Apply` / `Revert` buttons and a footer with version and links.
- Moved the settings window into the shared `Tools/Aspid 🐍` top-menu submenu, next to `Welcome FastTools`.
- Settings window version is now read from the package manifest instead of a hard-coded constant; `AspidToggle` colors aligned with the theme.
- `[SerializeReference]` fields in the `MonoView` / `MonoViewModel` / `MonoBinder` inspectors are now drawn with the FastTools type-picker dropdown instead of Unity's default managed-reference UI. The inspectors route them through `SerializeReferenceEditorGUI`, so no `[TypeSelector]` attribute is needed on any field and the candidate set is the field's own declared type. Nested managed references *inside* an assigned instance keep Unity's default UI — FastTools draws an instance's own children with a plain `PropertyField`.
- `Aspid.FastTools` is no longer embedded under `Packages/`. It is consumed as a UPM git dependency pinned to the immutable per-release tag `upm-preview/1.0.0-rc.7`, up from the embedded `1.0.0-rc.4`. rc.6 brought `[TypeSelector]` support for `[SerializeReference]` fields — the replacement for the removed `SerializeReferenceDropdown` integration — and rc.7 adds three type-picker fixes this project depends on: a candidate the field cannot close is no longer offered, generic arguments are inferred through the field's interfaces, and Unity's built-in types are accepted as generic arguments. Two upstream API renames were followed: the `Aspid.FastTools.Reflection` namespace collapsed into `Aspid.FastTools`, and `SerializedProperty.GetClassInstance()` became `GetDeclaringInstance()`.
- `GenericToString<TFrom>` exposes formatting as a virtual `Format` hook instead of hard-coding the decision in `Convert`. A format that is empty **or only whitespace** now falls back to `ToString()`; before, an empty one already did, but `"   "` was passed to `string.Format` and produced those spaces. `Format` still receives the typed value, so numeric and date specifiers (`{0:F2}`, `{0:hh\:mm}`) keep working. `formatEmptyValues` moved down to `StringFormatConverter`, the only converter with an opinion about blank input; `ObjectToStringConverter` and `TimeSpanToStringConverter` gained the format constructor they were missing.
- Inspector attributes (`[SerializeField]`, `[SerializeReference]`, `[Tooltip]`) in the Unity-independent layers are no longer wrapped in `#if UNITY_2022_1_OR_NEWER`. No-op stubs in `Source/Compatibility/UnityAttributesShim.cs` stand in for them outside Unity, so 22 preprocessor blocks across 14 files could be dropped. Directives guarding real Unity API (`Debug`, `Component`, `ProfilerMarker`) are unchanged.
- **Converter documentation.** All 40 marker interfaces, all 70 `ToConvert` / `ToConvertSpecific` wrappers, and the `Comparisons` / `CultureInfoMode` enums are documented; every serialized converter field gained a `[Tooltip]`, which is the only documentation visible where a converter is actually configured. `Documentation/08-converters.md` was rewritten around the shipped catalogue.
- **`ArithmeticNumberConverter`** exposes `Apply(double)` and `Undo(double)` publicly and is `sealed`. Its sixteen `Convert` overloads no longer reach the arithmetic by casting the object to one of its own interfaces.
- **The Greeter sample** uses the shipped `RichTextColorConverter` instead of a bespoke `PaintNameConverter`.
- **Two converter renames with no serialized footprint**: the nested enum type `Values` → `Mode` in the two vector dimension converters (the other six vector converters already called it `Mode`), and `Comparisons.Inequality` → `Comparisons.NotEqual` (a noun among five relations). An enum serializes as an ordinal and a nested type name is not serialized at all, so scenes are unaffected; only source naming these two changes.

  A wider rename wave was attempted and reverted. `[MovedFrom]` and `[FormerlySerializedAs]` cover an object's own serialized data but **not** a prefab-instance override, which is keyed by the stored type string and the property path. Renaming `SequenceConverters` emptied a converter in the shipped Hello World sample — 24 console errors, one binder that stopped converting, with `[MovedFrom]` present. `SequenceConverters`, `GenericToString`, `_preConvertor` / `_postConvertor` and `_values` therefore keep their names, and a test now says so with the reason attached.

- **`ParseHtmlStringConverter` now reports a colour it cannot parse** instead of returning transparent black in silence — a result indistinguishable from `"#00000000"` parsing correctly. The colour's serialized path moves from `_defaultColor` to `_fallback`, so **a fallback colour authored in an existing scene or prefab resets to transparent black** and has to be re-authored. Failures report in the shared message shape, and the constructor's first parameter is `fallback` rather than `defaultColor`.

- **A misconfigured converter now reports itself on every conversion, never once.** The shared failure handling always promised this; the catalogue did not deliver it evenly. A non-flags enum in `EnumMaskConverter`, an empty `_trueTokens` in `StringToBoolConverter`, a negative pad width, an inverted `min`/`max`, a missing inner converter in `CachedConverter` / `NullGuardConverter` / `SafeConverter` / `ConditionalConverter`, a duplicate key in a lookup table — each used to return a plausible value in silence. Each now writes to the console every time it happens and returns a documented fallback. The composition wrappers that change type — `SafeConverter`, `NullGuardConverter`, `CachedConverter`, `ComposeConverter` — all answer a missing inner converter with an authored fallback field rather than `default`, which for a colour or a number is indistinguishable from a successful conversion. Expect new console output from projects that were already misconfigured and did not know it.

- **The composition wrappers refuse a null link in their constructors.** `CachedConverter`, `SafeConverter`, `NullGuardConverter` and `ConverterAssetReference` throw `ArgumentNullException` on a missing inner converter or asset, as `ComposeConverter` already did, and `ConditionalConverter` throws on a missing predicate. A wrapper with nothing to wrap is a mistake in code; the half-filled state belongs to the Inspector, which still reports it on every conversion and returns the fallback. `then` / `else` and `SequenceConverter`'s links stay optional — there `null` means "skip this step".

- **`SafeConverter` lost its `_logErrors` field and always logs.** A switch that turns a caught exception into a silent fallback produces a result indistinguishable from success, which is the one outcome a containment boundary must never have. The caught exception is now logged in full — type, message and stack — instead of just its `Message`.

- **Four renames.** `ConverterExtensions.ToConvert` → **`ToConverter`** (the method hands back a converter; the old name read as an imperative). `ListToStringConverter` → **`CollectionJoinToStringConverter`** (it takes any `IEnumerable<T>`, and every sibling in the folder is named `Collection*`). `WrapMode` → **`NumberWrapMode`** (the old name collided with `UnityEngine.WrapMode`, so `using UnityEngine;` plus `using Aspid.MVVM.StarterKit;` made the reference ambiguous). `EnumMatch.Equals` → **`Equal`** (the member hid the inherited `object.Equals`). The two types carry `[MovedFrom]`, and the enum member is serialized as an integer, so authored data survives — but as `ConverterRenameCompatibilityTests` records, `[MovedFrom]` does not reach a **prefab-instance override**. Neither renamed type appears in any scene or prefab shipped with the package; a project that authored one as an override must run the repair tool that rewrites the stored type strings.

- **`DateTimeCompareConverter` and `DateTimeOffsetFormatConverter` replaced their bool pairs with an enum.** Three mutually exclusive sources were encoded as two booleans read in priority order — and in `DateTimeCompareConverter` the `UtcNow` source was unreachable from code entirely. They are now `ReferenceSource { FixedMoment, Now, UtcNow }` and `OffsetSource { AsGiven, Local, Override }`. **The old booleans do not migrate: an authored setting on either converter resets to its default and has to be re-authored.** `DateTimeCompareConverter` additionally stores the reference's `DateTimeKind`, so a UTC reference and a local one are no longer compared as raw ticks.

- **`EnumToValueConverter.Entry` and `LookupEntry` encapsulated their fields.** Public mutable fields on a public struct let any holder mutate an entry behind the converter's back. The fields are now private `[SerializeField]` with readonly properties and `[FormerlySerializedAs]`, so an object's own data migrates. The prefab-instance-override caveat above applies here too; neither type is authored in any scene or prefab in this repository.

- **An undeclared enum value now raises `InvalidOperationException`, not `ArgumentOutOfRangeException`.** The `default` arm of a `switch` over a *serialized field* was reporting corrupt object state through an exception that names an argument the caller never passed. A `switch` over a genuine method parameter — `EasingConverter.Evaluate`, `ToCultureStringExtensions.ToCultureInfo` — keeps `ArgumentOutOfRangeException`, which is correct there.

- **More converters convert both ways, and two signatures stopped lying.** `DateTimeToUnixTimestampConverter`, `StringToDateTimeConverter`, `StringToTimeSpanConverter` and `StringToBoolConverter` are now `ITwoWayConverter`; in a `TwoWay` binding they used to hand the value back untouched. `StringToBoolConverter` writes the first spelling authored for the answer, and the plain word when none is. `StringMatchToBoolConverter` declares `IConverter<string?, bool>`, which is what it always accepted, and `TimeSpanFormatConverter` declares `IConverter<TimeSpan, string>`, since `Convert` never returns null.

- **Constructors gained the settings only the Inspector could reach.** `DateTimeFormatConverter`, `DateTimeOffsetFormatConverter` and `TimeSpanFormatConverter` take a `culture`; `ByteSizeConverter` takes `binaryUnits` and `decimals`; `StringToDecimalConverter` gained the `_clamp` / `_min` / `_max` its four numeric siblings already had; `StringToBoolConverter` takes the `falseTokens` only the Inspector could fill. `ArithmeticNumberConverter`'s coefficient defaults to `1` instead of `0`, so picking `Division` or `Multiply` no longer produces an error before anything is typed, and `RepeatStringConverter`'s constructor default matches its serialized one.

- **The Unity-side converters got the same treatment as the core catalogue.** `StarterKit/Unity/Runtime/Converters` — vectors, quaternions, colours, textures, audio, localization and the converter assets — was reviewed against the same rules: misconfiguration reports every time, an undeclared enum value raises `InvalidOperationException`, and `ColorStop`, `SpriteMapEntry` and `EnumToDropdownOptionDataConverter.Entry` encapsulated their public mutable fields the way `LookupEntry` did. `Enum.HasFlag`, which boxes both operands on every call, was replaced with a bitwise test everywhere a converter uses a flag mask on the hot path.

- **Converters that could only be configured in the Inspector gained constructors.** `QuaternionToAngleConverter` takes a custom axis (the Custom mode was unreachable from code), `ColorBlockTintConverter` takes the blend amount, `ColorToHtmlStringConverter` takes `lowercase`, `RectOffsetScaleConverter` takes a rounding mode, and the localization converters gained the configuring constructors they had none of.

### Deprecated

- **The 40 named converter aliases** (`IConverterFloat`, `IConverterIntToLong`, `IConverterString`, …) and the **70 `ToConvert` / `ToConvertSpecific` wrappers** are marked `[Obsolete]` and will be removed in the next major. They existed only because Unity before 2023.1 could not serialize a `[SerializeReference]` field of an open generic type; the package requires Unity 6000.0. Use `IConverter<TFrom, TTo>` and the generic `ConverterExtensions.ToConverter<TFrom, TTo>`, which stays.

  The package's own converters keep implementing the aliases for one release, so a `[SerializeReference]` field a project declares as one still deserializes. Delete that base list and the field would resolve to `null` on load with no diagnostic at all — which is why this is a deprecation and not a removal.

### Removed

- **The two `*SubstitutionConverter` swizzle types**, folded into `VectorSwizzleConverter` (renamed from `Vector4SwizzleConverter`), which serves `Vector2`, `Vector3` and `Vector4` from four component slots instead of one enum per dimension. A slot naming a component the bound width does not carry is reported on every push and passes that component through unchanged.
- **`DefaultStringConverter._treatWhiteSpaceAsEmpty`.** A string of spaces is blank, with no opt-out — the same rule every string converter now follows.
- **Five duplicated `Vector2` maths converters**, each folded into the `Vector*` class of the same operation, which now serves `Vector2`, `Vector3` and `Vector4` from one picker entry: `Vector2ArithmeticConverter` (with `Vector3ArithmeticConverter`, renamed `VectorArithmeticConverter`), `Vector2ClampComponentsConverter`, `Vector2ClampMagnitudeConverter`, `Vector2NormalizeConverter` and `Vector2RoundConverter`. The vector settings widened to `Vector4` and only the components the bound vector carries are read, so an authored operand or clamp pair needs re-entering — see `MIGRATION.md` § 5.1.1.
- **`GenericToString<TFrom>.ToStringValue` is no longer `protected virtual`** — it became a private detail when formatting moved to the `Format` hook. A subclass that overrode it no longer compiles (CS0115); move the override to `protected virtual string? Format(TFrom value)`, which receives the typed value and is called for every non-blank format.
- The pre-2023.1 converter compatibility gate: 117 `using Converter = …` alias pairs, 12 inline `ToConvertSpecific()` branches, 4 conditional `[SerializeReference]` attributes, 10 `GetConverter` helpers the collapse reduced to identity functions, and 8 `UNITY_6000_0_OR_NEWER` branches naming the pre-Unity-6 `PhysicMaterial`. `package.json` has declared `"unity": "6000.0"` since 1.1.0, so none of it compiled in a supported configuration.
- Internal `FloatingBackgroundElement`, superseded by the FastTools animated dot background.
- `SerializeReferenceDropdown` integration: the `com.alexeytaranov.serializereferencedropdown` dependency, the `[SerializeReferenceDropdown]` attributes on `[SerializeReference]` fields, the assembly references and the `ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION` version defines. A replacement built into Aspid.FastTools will take its place.

### Fixed

- Reverse binding no longer re-applies the *forward* converter on the way back to the ViewModel. A binder in `TwoWay` / `OneWayToSource` now uses `ITwoWayConverter.ConvertBack` when available and sends the value unchanged otherwise.
- `SequenceConverter` no longer dereferences an empty Inspector slot. The type picker's `<None>` entry is a valid selection that serializes as a null element, so gaps are skipped instead of throwing.
- The `Vector3CombineConverter` family no longer throws a `NullReferenceException` when its scene reference is unassigned or destroyed. It reports itself once and returns the input unchanged.
- A `FormatException` from a format string no longer cuts the binder subscriber list. Dispatch is a bare multicast, so one bad format used to stop every binder queued behind it; `GenericToString` now contains the failure and falls back to `ToString()`.
- `StringFormatConverter` formats null and empty values again when `_formatEmptyValues` is set — the branch had become unreachable when `Convert` moved to the base class.
- The divide-by-zero diagnostic in `ArithmeticNumberConverter` reports on every conversion. It was briefly suppressed to once per instance to save the stack trace `Debug.LogError` captures, but a misconfiguration that reports once scrolls out of the console and reads as fixed; the cost belongs to the broken configuration, not to the diagnostic.
- `DropdownOptionsByEnumMonoBinder` no longer reflects over the enum on every push, nor resets the selected index of a `DropdownValueMonoBinder` on the same object.
- `DebugLogBinder` / `DebugLogMonoBinder` no longer throw on a null value: `?? value.ToString()` could not tell "the converter returned null" from "the value is null".
- Four `Double` converters that cannot be built in the Inspector are hidden from the picker; the region had been missed when its three siblings were marked.
- Six bare `ArgumentOutOfRangeException()` throws now name the argument and its value.
- Private `[SerializeReference]` fields no longer disappear from the `MonoView` / `MonoViewModel` / `MonoBinder` inspectors. The reflected field map admitted a private field only when it carried `[SerializeField]`, so a polymorphic field resolved to a null `FieldInfo` and was skipped for every property — a binder's `_converter` or `_customInteractable` was simply not drawn.
- Converters that can only be built in code are no longer offered by the type picker. `FuncConverter` and the private types behind `ToConvert` / `ToConvertSpecific` wrap a delegate no inspector can supply, so picking one produced an instance with a null delegate; they now carry `[TypeSelectorDisplay(Hidden = true)]`.
- `CachedConverter` no longer answers with another input's result. The cache key was stored *before* the inner converter ran, so an inner throw left the new key paired with the old value — and the next push of that same input was served the stale result with nothing in the console. The key is now written only after a conversion succeeds.
- Eight converters no longer throw on a value they are documented to accept. `Math.Abs` on `int.MinValue` / `long.MinValue` took down `ByteSizeConverter`, `PaddedNumberConverter`, `OrdinalConverter` and `PluralizeConverter`; `long.MinValue % -1` took down `ModuloNumberConverter`; `TimeSpan.FromSeconds` took down `SecondsToTimeSpanConverter` on `NaN` (which is what `0f / 0f` produces in a progress calculation) and on either infinity; `EnumMatchConverter` overflowed on a `[Flags]` enum backed by `ulong` with a bit above `long.MaxValue`; and `UnixTimestampToDateTimeConverter` threw on a timestamp outside the calendar. Each now reports and returns a documented fallback.
- A bad format string no longer cuts the binder subscriber list from four more places. `DateTimeFormatConverter` had no guard at all — a typo in the Inspector stopped every binder queued behind it — while `PluralizeConverter`, `CollectionJoinToStringConverter`, `NumberFormatConverter`, `DecimalFormatConverter` and `SignedNumberStringConverter` let the `FormatException` out. They now contain it, report on every push, and fall back to the general rendering.
- `StringToDateTimeConverter` no longer throws on every conversion, valid input included. The fallback `DateTime` was built from serialized ticks at the top of `Convert`, before any parsing, so a negative tick count entered in the Inspector made the converter unusable rather than merely misconfigured. The fallback is now built only on the failure path, and out-of-calendar ticks are clamped with a report.
- `ArithmeticNumberConverter` no longer collapses a two-way binding to zero. Forward `Division` by a zero coefficient reported and passed the value through, but the reverse path multiplied by that same zero, so every value the View sent back became `0` in silence. Reverse `Power` could likewise return `NaN`. Both now report and return the input, and the `int` / `long` overloads route their `double` result through `NumericSaturation` instead of an unchecked cast whose result is undefined outside the target's range — and therefore differed between Mono and .NET Core.
- `UnixTimestampToDateTimeConverter.ConvertBack` no longer drifts by the local offset. `ToUniversalTime()` treats a `DateTimeKind.Unspecified` input — what parsing a date string yields — as local time even when the converter is set to UTC, so a round trip moved the value every time. The kind is now specified from the converter's own setting.
- `StringToEnumConverter` accepts `[Flags]` combinations again. `Enum.IsDefined` rejects a combined value such as `Red, Blue` because the combination is not itself a declared member, so a string the converter had just parsed was thrown away in favour of the fallback — while `ConvertBack` happily wrote that same string. Membership is now tested against the declared-flag mask, which also removed the boxing that test performed on every push.
- `EnumFlagsToStringConverter` no longer shows a stale string after its separator changes. The cache key covered the value and the name source but not `_separator` or `_noneText`, both of which are editable while playing.
- `EnumMembers` no longer resolves a number to the wrong member. A negative member of a signed enum sign-extended into the flag mask, which made the mask claim every high bit, so a number such as `384` passed the validity check and `Enum.ToObject` silently truncated it to `-128` instead of falling back.
- `SecondsToTimeStringConverter` no longer flashes the negative label on the last frame of a countdown. The sign was tested before rounding, so `-0.3` with the default `Ceil` displayed the negative text instead of the `00:00` it rounds to.
- Text no longer breaks apart at an emoji. `MaskStringConverter` and `TruncateStringConverter` cut by UTF-16 unit, so a slice landing inside a surrogate pair produced a replacement character; both now move the cut to the pair boundary.
- `CurrencyConverter` writes `-$1,234` rather than `$-1,234` for a negative amount, and the `StringToFloat` / `StringToDouble` reverse path uses the round-trip format, so a value that survives `Convert` now survives `ConvertBack` unchanged.
- `NumericCastConverter` names itself in all three range messages, and the `long` → `int` path no longer falls through to a bare `checked` whose exception carries only the framework's generic text.
- A converter asset that leads back to itself no longer takes the Editor down. `ConverterAssetReference` can be picked inside a `ConverterAsset`, so an asset could be pointed at itself — an infinite `Convert` recursion whose `StackOverflowException` cannot be caught in .NET and kills the process. The cycle is now refused before it is entered, reported, and answered with the default; the asset keeps working afterwards.
- A `ConverterAsset` with nothing assigned no longer looks like a converter that does nothing. It returned `default` in silence, so an unfilled asset quietly fed transparent black to an `Image` or null to a padding. It now reports on every conversion, with the asset itself as the log's ping target.
- `Texture2DToSpriteConverter` no longer leaks a native object per push. The `Sprite` it created was never destroyed, so every texture change left the previous one behind. A non-positive or `NaN` pixels-per-unit is reported rather than silently replaced.
- A volume fader driven by `AudioLinearToDecibelConverter` no longer goes dead silently. With `_minDecibels >= _maxDecibels`, or a `NaN` bound, `Mathf.Clamp` collapses and every slider position produces the same number — the fader looks connected and does nothing. The range is now reported and falls back to −80..0 dB. The decibel-to-linear direction — now `isInvert` on the same converter — built its inverse once and cached it, so editing the range in play mode never reached the result.
- `VectorRoundConverter` no longer rounds the opposite way. A negative step mirrored `Floor` and `Ceil`, so an author asking to round down got up. `LookRotationConverter` with a zero `up` and `QuaternionToAngleConverter` with a zero custom axis — a degenerate pair and a constant zero indistinguishable from a real reading — now report and fall back.
- A `ColorBlock` fade duration that is negative or `NaN` no longer reaches `Selectable` as a tween that never ends, and `ColorAlphaConverter`'s `Set` mode clamps like its `Multiply` and `Add` siblings already did.
- Five more converters stopped answering a broken setup with a plausible value: a curve with no keys, a null gradient, an empty threshold-colour table, an empty sprite-frame array and a missing localization table.
- `RectOffset` conversions saturate instead of casting a float straight to `int`, whose result is undefined outside the target's range.
- Three documentation claims that disagreed with the code were corrected: `ColorBlend.Add` preserves alpha rather than adding it, `EulerToQuaternionConverter` round-trips in Unity's 0..360 convention rather than ±180, and `normalized` returns zero below Unity's length threshold rather than only for an exactly zero vector. The converter-asset section no longer promises an asset for every type pair — the numeric casts between `int`, `long`, `float` and `double` have none.
- `SmoothStepConverter`'s Inspector text described the opposite of what it does. The tooltips promised an inverse-lerp — "the value that maps to 0" — while `Mathf.SmoothStep` maps a 0..1 position *into* the range. The behaviour is unchanged and correct; the text now matches it, and says that the result is always clamped.

## [1.1.0-beta.1] — 2026-06-06

First preview cut of `1.1.0`, published to the `upm-preview` channel. The API is largely stabilised but may still change before the final `1.1.0` release.

### Highlights

- Editor inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` rewritten on top of UI Toolkit / `VisualElement`.
- Brand-new `DebugViewModelPanel` with tabs, persistent search, `RelayCommand` support and bindable / auto-property support.
- `Aspid.MVVM Settings` window prototype with `AspidToggle` and shared styling.
- Bindable Properties supported in the source generator; new `NotifyCanExecuteChangedAll()`.
- `MonoView` is now non-abstract — a single, self-contained base view.
- New `ValueViewModel`, `AnyReverseBinder`, OneWayToSource component binders (`…ToSourceMonoBinder` family), AudioSource / LayoutGroup / Dropdown / Selectable / Object-Name binders.
- `Aspid.FastTools` integrated, many editor visuals migrated to FastTools equivalents.
- All sub-projects extracted into git submodules (`Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`); `Aspid.Collections` consumed as a UPM git package (`tech.aspid.collections`).
- Minimum Unity raised to `6000.0`.

### Added

#### ViewModel & Generator
- **Bindable Properties** support in the source generator (PR #46) — usable in code, Debug panel and samples (Todo sample updated).
- **`NotifyCanExecuteChangedAll()`** generator method (PR #52, #54) — emits backing-field names with a null-conditional guard, skips commands without a `CanExecute`, and includes `IRelayCommand`-typed members.
- **`ValueViewModel`** — minimal ViewModel wrapper around a single value with full XML docs (PR #63).
- Keyword field support in the generator (PR #55).
- `EmptyExecution` static instance on `RelayCommand` (PR #36, #93) — an executable command that does nothing; `GetSelfOrEmptyExecution` falls back to it when the command is null. Plus try/catch in `RelayCommandField` (PR #43).
- Interface support for `ViewModel` (`IMyVm` can now be picked as a design ViewModel) (PR #53).
- Generic enum / struct bindable members now resolve their effective type kind from generic-parameter constraints instead of defaulting to the class member type (PR #44).
- **Virtual binder fields** — generator auto-emits `MonoBinder[]` slots for `IView<TViewModel>` bindable members that are not declared on the View. Opt out via `[View(AutoBinderFields = false)]`; `ScriptableObject`-derived views are always skipped (PR #74, generator PR `Aspid.MVVM.Generators#13`).

#### Views
- `MonoView` is now non-abstract and self-contained — the inspector-driven binder list, child validation and `[RequireBinder]` integration live directly on it (PR #48).
- `RelayCommand` support inside `View` / `MonoView`; `CommandsContainer` refactor; `CommandContainer in View` (PR #43).
- `ViewInitializer` overhaul (PR #41, #50) — hoisted view/container resolution into `ViewInitializerBase`, lazy edit-mode `Views` / `ViewModel`, container `Resolve` switched to `TryResolve`, and a new `InitializeStage.DiConstructor` injection stage.
- `DestroyView` mode in editor; `DestroyViewModel` extension fixes (PR #43, #53).
- `PrefabViewFactory` / `PrefabViewPool` upgraded.
- `ViewModelPickerWindow` with dropdown + improved navigation (PR #53).
- `[AddComponentMenu]` for `MonoView`; snake-style for settings menu (PR #47).
- `MonoView` editor refactor; fixed generated fields and base inspector display (PR #32).
- `DesignViewModel` upgrade (PR #53) including legacy Unity support.

#### Editor / Inspector
- New UI Toolkit inspectors for `MonoBinder`, `MonoView`, `MonoViewModel` (PR #31, #32, #35).
- `AspidInspectorHeader`, `AspidPropertyField`, `AspidDividingLine` shared visuals (PR #32, #40).
- USS-driven theme: `AspidToggle` (PR #47), IMGUI foldout drawer margin fix, IMGUIContainer wrapping in styled `AspidPropertyField`.
- `EnumMonoBinderEditor` (PR #57); `EnumValuesPropertyDrawer` fixes; `EnumValues` sample and `ComponentTypeSelector` documentation.
- Drag & Drop for unassigned and general binders (groups + Auto-Assign + Select / Restore buttons) (PR #43).
- `RequireBinder` and child View / Binder validation (alpha) (PR #43).
- `Aspid.MVVM Settings` window prototype (PR #47).
- **`HeaderGroup` foldout attributes** — `HeaderGroupAttribute` (single field), `HeaderGroupStartAttribute` / `HeaderGroupEndAttribute` (range) tag binder fields and VM members into named, collapsible inspector foldouts. New `HeaderGroupRouter` is consumed by `MonoViewVisualElement` / `AspidBaseInspectorVisualElement` instead of inline foldout layout. Stripped from non-`DEBUG` / non-`UNITY_EDITOR` builds (PR #74).

#### ViewModel Debug Panel (PR #45)
- Rewritten on UI Toolkit, with tabs (`DebugViewModelPanel`).
- Search with persistence and improved logic; type-based search.
- `RelayCommand` support (`RelayCommandField`, correct meta containers).
- Bindable property and auto-property support.
- New styles: `Debug field`, `DisableTextFields`, `DebugStringField`.

#### Binders — new
- LayoutGroup binders (PR #56).
- AudioSource binders (PR #59).
- OneWayToSource component binders (`…ToSourceMonoBinder` family) (PR #58).
- `AnyReverseBinder` with nullable support (PR #37) — reverse binders now forward `null` reference values via `OnValueChanged(default)` instead of throwing (PR #95).
- Object Name binders (PR #34).
- Additional InputField binders + large refactor (PR #51).
- Dropdown / Selectable binders (PR #61).
- `Addressable` binders gained an opt-in seamless swap mode, with a destroyed-object guard in the async completion callback (PR #86).
- `GameObjectInstantiateAddressableMonoBinder` for prefab spawning via Addressables.

#### Binders — improvements
- `OnReplace` / `OnMove` events forwarded to binder hooks; batch `Replace` unrolled into per-item `OnReplace` calls.
- Reactive collection binders: `CollectionBinderBase<T>` now subscribes to `CollectionChanged` and forwards granular `Add`, `Remove`, and `Reset` events to the new abstract hooks `OnAdded(T?)`, `OnAdded(IReadOnlyList<T?>)`, `OnRemoved(T?)`, `OnRemoved(IReadOnlyList<T?>)` (PR #94), and cleanly unsubscribes on `Unbind` and `Dispose` (PR #88, #91).
- General binder upgrade (PR #60).
- `BindSafely` / `UnbindSafely` enriched with View + bindable Id; new `owner` / `memberName` overloads.
- `EventTriggerCommandMonoBinder`, `ImageSpriteSwitcherBinder`, `MonoBinderPropertyField` — fixes.
- `VirtualizedListItemSourceBinder` Dispose / lifecycle fixes.
- `ViewModelObservableListBinder` fixes.
- `MonoBinderVisualElement` polish; binder-in-script visualizations and animation upgrade.
- `BindMode` support for `VisualElement` (PR #39).
- `IAnyBinder` BinderLog support.

#### Collections
- `Aspid.Collections` is now consumed as a UPM git package (`tech.aspid.collections`) instead of being shipped as source under the package (PR #79).
- `FilteredList` and `BindAlso` fixes.
- New collections tests.
- `Replace` / `Move` notifications surfaced to binders.

#### Project structure / infrastructure
- Submodules wired in (PR #38): `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.MVVM.Unity.Generators`.
- Unity project relocated from repo root into `Aspid.MVVM/`.
- MVVM package moved from `Plugins/Aspid/` to `Assets/Aspid/` (PR #77), then promoted to an embedded local UPM package under `Packages/tech.aspid.mvvm` (PR #117).
- `package.json` placed inside the package; `unity` field set to `6000.0`, `unityRelease` pinned; version `1.1.0-beta.1`.
- Samples shipped under `Samples~` and registered in `package.json`: HelloWorld, Stats, TodoList, VirtualizedList, plus the Counter / Greeter walkthroughs.
- Root `CLAUDE.md` describing structure and conventions.
- GitHub Actions: Claude PR Assistant + Code Review workflows (PR #64).
- GitHub Actions: Release workflow publishing stable (`upm`) and preview (`upm-preview`) UPM subtrees with immutable `upm/<version>` tags, generator-DLL drift verification and CHANGELOG-driven release notes (PR #78); the Readme gained matching Stable / Preview version badges.

#### Integrations / dependencies
- `Aspid.FastTools` integrated (PR #26) and later embedded as a local UPM package under `Packages/tech.aspid.fasttools`; many editor visuals migrated to FastTools equivalents.
- `Aspid.MVVM.Generators`, `Aspid.MVVM.Analyzers`, `Aspid.Collections`, `Aspid.FastTools` updated to current heads.
- `SerializeReferenceDropdown` updated to `1.2.7`.
- `Roboto-Bold SDF` font refreshed.
- Editor target lifted to `6000.4.0f1`; minimum supported Unity raised to `6000.0`.

#### Documentation
- Mass XML doc pass across all binder families: AudioSource, CanvasGroup, Collider, Animator, Behaviour, GameObject, Layout, UnityGeneric, Selectable, Graphic, Image, RawImage, Renderer, Transform, Slider, InputField, Toggle, Button, EventTrigger, ScrollBar, ScrollRect, Dropdown, Object, LineRenderer, Casters, LocalizeStringEvent, VirtualizedList plus base `MonoBinder` / Behaviour subfolders (PR #62).
- XML docs for converters.
- `ComponentTypeSelector` documentation and `EnumValues` sample.
- `Readme.md` relocated (PR #77) and tweaked (PR #71).

### Changed

- `MonoView` is no longer `abstract`; it is a concrete component with its own serialized binders list and `[RequireBinder]` validation. Existing subclasses still work (PR #48).
- `MonoView.Dispose()` no longer destroys the host GameObject — it only calls `Deinitialize()`. Call `Object.Destroy(gameObject)` explicitly if needed (PR #48).
- `MonoBinder.Bind()` no longer throws when called on an already-bound binder; it logs an error and returns instead (PR #62).
- `[AddComponentMenu]` paths reorganized — for example `Collections/Observable List Binder - ViewModel` → `Collection/Observable List Binder – ViewModel` (singular form, en-dash).

### Removed

- `AddComponentContextMenuAttribute` — replaced by `AddBinderContextMenuAttribute` / `AddBinderContextMenuByTypeAttribute` with a different signature (`Path = "..."` named property).
- `AddPropertyContextMenu` attribute — no replacement; the new editor pipeline handles property menus internally.
- Standalone `Aspid.Collections` source under the package — now consumed via a UPM git package (`tech.aspid.collections`).

### Renamed (StarterKit class names)

`.meta` GUIDs are preserved, so prefabs and scenes continue to bind to the correct script. **Game code referencing the old class names will not compile until updated.**

| 1.0 | 1.1 |
|-----|-----|
| `ViewModelObservableListMonoBinder` | `ObservableListViewModelMonoBinder` |
| `ViewModelObservableListBinder` | `ObservableListViewModelBinder` |
| `ViewModelObservableDictionaryBinder` | `ObservableDictionaryViewModelBinder` |
| `ViewModelCollectionMonoBinder` | `CollectionViewModelMonoBinder` |

### Fixed

<!-- Only fixes for bugs that actually shipped in a released version (1.0.0–1.0.5) are listed here. Fixes for code introduced during 1.1.0 development are intentionally folded into their corresponding feature bullets above, not listed as standalone fixes. -->

- `NumberToBoolConverter`: the `Inequality` comparison was inverted — it returned the same result as `Equal` instead of its negation. It now returns `true` when the values are not approximately equal (PR #81).
- `DynamicViewModel.Create<…>`: the factory overloads passed only `DynamicPropertyData.Value`, which forced every property to `BindMode.OneTime` and discarded the user-specified `Mode`. The full `DynamicPropertyData` is now passed so the configured `BindMode` is honoured (PR #83).
- `MonoBinder.Unbind()`: the `ProfilerMarker` block was guarded only by `!ASPID_MVVM_UNITY_PROFILER_DISABLED`, breaking compilation on Unity earlier than 2022.1. It now also requires `UNITY_2022_1_OR_NEWER`, matching `Bind()` (PR #84).
- `VirtualizedList`: `OnAdded` / `OnRemoved` bounds-checked the computed view-pool index against `ItemsSource.Count` with a too-loose `<=`. The check now compares `viewIndex < _views.Length` so it picks `Refresh` vs `ResizeContent` correctly (PR #89).
- `ObservableListBinder`: `InitializeList` subscribed to `CollectionChanged` on the original `list` argument while `DeinitializeList` unsubscribed on `List` (which may be a filtered wrapper), leaking the subscription. The subscribe switch now uses `List` (PR #90).
- Slider / Scrollbar command binders: `OnCanExecuteChanged` reinterpreted the 4-byte `float` `Target.value` as the command's generic type `T` via `Unsafe.As`, causing out-of-bounds reads and garbage `CanExecute` values for `long` / `double` commands. Typed overloads now perform proper numeric casts via `ApplyCanExecute` (PR #92).
- Source generator: bindable members whose type was a generic type parameter fell through to the default class case and ignored `enum` / `struct` constraints. The generator now resolves the effective type kind from the parameter's constraints and emits the correct bindable member type (PR #44).

### Migration

See [MIGRATION.md](MIGRATION.md) for a full upgrade checklist from 1.0 to 1.1.

---

## [1.0.5] — 2025-10-17

### Added
- New TextMeshPro text binders: `TextFontBinder`, `TextFontSwitcherBinder`, `TextAlignmentBinder`, `TextAlignmentSwitcherBinder`, plus Mono variants (`TextFontMonoBinder`, `TextFontEnumMonoBinder`, `TextFontEnumGroupMonoBinder`, `TextFontSwitcherMonoBinder`) — for binding TMP font and alignment (PR #30).
- New Unity Localization binders: `LocalizeStringEventVariableBinder` (+ Mono variant), `TextLocalizationEntryBinder`, `TextLocalizationEntrySwitcherBinder` and Mono variants, with `TextLocalizationExtensions` (PR #29).
- Profiler markers and improved logging across `BindableMember` types and `BindMode` (`BindModeExtensions.Throw`, `LoggerHelper`) (PR #15).

### Changed
- Editor project updated to Unity `6000.2.7f2` (PR #28).
- Vendored the `com.unity.asset-store-tools` package into the repository (packaging only, no framework code change).

### Fixed
- `RectTransformSetters.SetSizeDelta` wrote the computed value to `anchoredPosition` instead of `sizeDelta`, repositioning the `RectTransform` instead of resizing it (PR #27).

---

## [1.0.4] — 2025-09-19

### Fixed
- Component context-menu generation (the "Add Component" entries produced via `AddComponentContextMenuAttribute`) — fix shipped in the rebuilt `Aspid.MVVM.Unity.Generators.dll` (PR #14).

---

## [1.0.3] — 2025-09-15

### Changed
- Moved Unity-layer types (`MonoBinder`, `MonoViewModel`, `MonoView`, `ScriptableView`, editor classes) from the `Aspid.MVVM.Unity` namespace into the root `Aspid.MVVM` namespace, to satisfy Asset Store packaging requirements (PR #13).

### Removed
- `MonoBinderExtensions` (the `BindSafely<T>(...)` helper overloads) and the `OnBindingDebug` / `OnUnbindingDebug` partial debug hooks on `MonoBinder` (PR #13).

---

## [1.0.2] — 2025-09-11

### Fixed
- ViewModel source generator fix — shipped in the rebuilt `Aspid.MVVM.Generators.dll` (PR #12).

---

## [1.0.1] — 2025-09-10

### Changed
- Reverted the C# language version from C# 10 back to C# 9 (removed `-langversion:10` from the `csc.rsp` files), aligning with Unity's default compiler (PR #11).
- `AddressableMonoBinder<TAsset>` reworked from a UniTask/async model (`LoadAssetAsync` / `CancellationToken`) to a synchronous `Addressables.LoadAssetAsync(...).Completed` callback, dropping the UniTask dependency for Addressable binders.
- `OneTimeBindableMember<T>` (and Enum / Struct variants) turned into a pooled singleton via a static `Get(value)` factory instead of allocating per bind.

### Fixed
- `ViewModelCollectionBinder` / `ViewModelCollectionMonoBinder` now deactivate (`SetActive(false)`) leftover pooled views beyond the current item count, so stale views are no longer left visible when the bound collection shrinks.

---

## [1.0.0] — 2025-08-09

Initial public release. Subsequent entries describe changes relative to 1.0.0.
