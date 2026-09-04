---
name: starterkit-binder-authoring
description: How to write a new Aspid.MVVM StarterKit binder correctly the first time — choosing a base (`ComponentMonoBinder`, `ComponentFloat/Int/ObjectMonoBinder`, `EnumMonoBinder`, `EnumGroupMonoBinder`, `SwitcherMonoBinder`, `CasterMonoBinder`, `*ToSourceMonoBinder`), the serializable twin via `[GenerateSerializableBinder]`, `AddComponentMenu`/`AddBinderContextMenu`, value validation through `BinderMath`/`BinderLogger`, `null` semantics, missing `IBinder<T>` implementations, the standard five-point binder review. Use when creating, reviewing or reworking any binder in `StarterKit/Runtime/Binders`, when the user asks to "add a binder", "make a family for property X", or sends a binder file.
---

# StarterKit binder

General style: skill `aspid-code-style`; placement: `starterkit-layout`; docs: `aspid-mvvm-xmldoc`. This skill covers only what is specific to binders.

## Binder review (standing routine for any binder the user sends)

1. Briefly: what the binder is for.
2. Bugs and shortcomings, including **missing `IBinder<T>` implementations** (a value binder may accept one more type).
3. `//` comments: delete the unnecessary ones entirely, shorten the rest.
4. `[Tooltip]`: short, on **all** serialized fields without exception.
5. XML docs: `<remarks>` only where it is needed.

Apply the fixes immediately; do not end with "shall I apply?".

## A family for a component property

For `Component.property` there are usually four Mono classes in one subfolder (`Binders/<Component>/<Property>/`):

| Class | Base | Purpose |
|---|---|---|
| `XxxPropertyMonoBinder` | `ComponentMonoBinder<TComponent, TValue>` or the typed `ComponentFloatMonoBinder<T>` / `ComponentIntMonoBinder<T>` / `ComponentObjectMonoBinder<T, TObject>` | direct binding, `[GenerateSerializableBinder]` |
| `XxxPropertySwitcherMonoBinder` | `SwitcherMonoBinder<TComponent, TValue>` | two values chosen by `bool` |
| `XxxPropertyEnumMonoBinder` | `EnumMonoBinder<TComponent, TValue>` | value by enum |
| `XxxPropertyEnumGroupMonoBinder` | `EnumGroupMonoBinder<TElement, TValue>` | group of elements, `SetValue(element, value)` |

Plus `XxxToSourceMonoBinder` (`ComponentToSourceMonoBinder`) at the root of the component folder when the property is read back into the ViewModel.

- A `UnityEngine.Object` value (`Material`, `AudioClip`, `Mesh`, `Sprite`) → `ComponentObjectMonoBinder`, not `ComponentMonoBinder<T, Material>`: the generator then gives the serializable twin a `TargetObjectBinder`.
- `float`/`int` → `ComponentFloatMonoBinder`/`ComponentIntMonoBinder`: they provide `INumberBinder` and the whole numeric family. A shared base must never narrow the interfaces of its subclasses.
- A Switcher accepts **`bool` only**: do not derive it from `TargetBinder<TTarget, T>` and do not add `IBinder<T>`. Reuse the selection logic through a closed `[SerializeField] BoolToValueConverter<T>`.
- Add `IColorBinder`, `IVectorBinder`, `IRotationBinder` (`General/Color`, `General/Vector`, `General/Rotation`) to color/vector/rotation binders so they accept strings, `Color32`, `Vector2/3/4`, `Quaternion`/Euler.

## Skeleton

```csharp
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="AudioSource.volume"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Volume")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Volume")]
    public class AudioSourceVolumeMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.volume;
            set => CachedComponent.volume = this.SafeClamp01(value);
        }
    }
}
```

- A `MonoBehaviour` file has no `#nullable enable` and no `?` annotations.
- Attribute order: `GenerateSerializableBinder` → `AddBinderContextMenu` → `AddComponentMenu`. Switcher/Enum/EnumGroup variants are not generated and set `SubPath = "Switcher"|"Enum"|"EnumGroup"` in `AddBinderContextMenu` instead.
- `serializePropertyNames` lists Unity's serialized property names (`m_Volume`, `m_Color`, `m_Sprite`) so the context menu appears on the right Inspector field.
- `AddComponentMenu`: `Aspid/MVVM/Binders/<Area>/<Component>/<Component> Binder – <Property>[ <Variant>]`. One path per component (`Audio/AudioSource/` for all of them, including `IsPlaying`/`PlayOneShot`).
- A class with `[GenerateSerializableBinder]` is not `sealed` and needs no `partial`; a non-generated class that is not a base is `sealed`.
- A concrete binder overrides only `Property` (or `SetValue`); checks and logging live inside the setter.

## Values and errors

- Validation is never silent: `BinderMath.SafeClamp`/`SafeClamp01`/`NonNegative`/`RequireFinite` log through `BinderLogger` and substitute the nearest valid value. No quiet `if (!IsFinite(value)) return;`.
- A helper that sanitizes a value takes the caller (`this IBinder` or `Type binderType`) instead of being a free static.
- A field with `[Range(0, 1)]` in the Inspector gets no duplicate runtime clamp for the same condition.
- `null` from the ViewModel means **reset the state** (clear the list, `default`, hide), not an early `return`. An early `return` is acceptable only when there is nothing to reset, and `<remarks>` says why.
- A value outside the target type's range saturates (`NumericSaturation`); the reverse channel (`*ToSource`) raises all numeric events the same way.
- Errors are logged on every occurrence, without "already logged" flags. Error texts: one or two short phrases.
- Switcher variants clamp in `SetValue`, not in `GetConvertedValue`.

## Converter inside a binder

- The slot is `[SerializeReference] private IConverter<TFrom, TTo> _converter;` without `[TypeSelector]` (`StarterKitTypePickerDrawer` supplies the picker). A required slot: `LogError` and skip when empty; default through `CreateDefaultConverter()` in `Reset`/`OnValidate` (see `CasterMonoBinder`).
- The type of a serialized factory or converter is not lifted into a class generic parameter: the field is typed by the interface directly (`IViewFactory<TView>`).
- Casters derive from `CasterMonoBinder<TFrom, TTo>`; `AnyToStringCasterMonoBinder` stays separate (generic `SetValue<T>` from `IAnyBinder`).

## Serializable (non-Mono) binders

- Generated from the Mono class via `[GenerateSerializableBinder]`; only bases (`Binder`, `TargetBinder`, `SwitcherBinder`, `DelegateXxxBinder`) and binders without a Mono twin are written by hand.
- A hand-written serializable binder: `[Serializable]`, a `protected` constructor for deserialization, multi-parameter constructors one parameter per line, `Object` parameters rather than `GameObject`, `CanBind` on an empty reference in the Mono version.
- Collection bases handle a multi-element `Replace` with a loop, not `NotImplementedException`; replacing a ViewModel = release + create, `Deinitialize` before `Release`.

## Serialized fields

- `[Tooltip]` on every one, short; the caveat is repeated in the `<param>` of the serializable twin's constructor.
- `[Min]`/`[Range]` wherever the value can become invalid; consistent with the constructor exception and the Tooltip.
- A field used only in some bind modes: `[UsedInModes(BindMode.…)]`.
- No `= default!`, no `[FormerlySerializedAs]`.

## Documentation

- `Documentation/StarterKit/README.md` and the family's `*-binders.md`: one row per binder, the modes column includes `OneWayToSource`, a ranges/clamps table where applicable.
- The binder `<summary>` is one line: `<see cref="Base{T}"/> that binds <see cref="Component.property"/>.`; cref generic bases by their parameter names, never `{Graphic, Color}`.
