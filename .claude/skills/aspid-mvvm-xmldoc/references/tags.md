# Hook Methods, `<inheritdoc/>`, `<remarks>`, Exceptions, `[Tooltip]`

Rules for less-frequent XML tags and for Unity-specific edge cases (conditional compilation, `[SerializeField]` tooltips). Consult this file when documenting a `virtual`/`abstract` override point, deciding whether to use `<inheritdoc/>`, writing an exception that behaves differently in Unity builds, or adding tooltips to serialized fields.

---

## Abstract / virtual hook methods

Pattern: `"Called [when]. Override to [purpose]."` This phrasing tells a subclass author two things they actually need: the call site (so they know what state is valid) and the extension point (so they know what the override is *for*).

```csharp
/// <summary>
/// Called before binding is established. Override to add pre-binding logic.
/// </summary>
protected virtual void OnBinding() { }
```

When an override requires a mandatory `base.Method()` call to preserve invariants, add a `<remarks>` note so subclass authors don't silently break the base behavior:

```csharp
/// <remarks>
/// When overriding, always call <c>base.OnBound()</c> to preserve
/// the <see cref="BindMode.OneWayToSource"/> initialization behavior.
/// </remarks>
```

---

## `<inheritdoc/>`

Use `<inheritdoc/>` when there is literally nothing to add beyond what the base member says. Writing it anyway would just duplicate (and risk drifting from) the parent's doc.

**Use when:**

- Explicit interface implementations that add no behavior.
- Overrides that do exactly what the base describes.
- `sealed override` methods that only delegate to an abstract method.
- Pass-through constructors — same parameters, delegates via `base(...)`, with at most minor argument validation.

**Do NOT use when:**

- Behavior is different or additional to the base.
- Constructors change the effective API (fewer parameters, hardcoded arguments). Document the parameters so the new signature is readable in its own terms.

```csharp
// Pass-through constructor — use inheritdoc
/// <inheritdoc/>
public LightIntensityBinder(Light target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
    : base(target, converter, mode) { }

// Hardcoded argument — document the parameters, not the act of constructing
/// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
    : base(setValue, BindMode.OneTime) { }
```

---

## Constructors

Never write a `<summary>` of the form *"Initializes a new instance of the `<see cref="X"/>` class."* That a
constructor constructs is obvious from the declaration, and the block is pure noise in the tooltip.

A constructor carries:

| Tag | When |
|---|---|
| `<param>` | Always, one per parameter. |
| `<remarks>` | Only when there is something to say — most often the defaults a parameterless overload picks. Optional. |
| `<summary>` | Never. |

```csharp
/// <remarks>Default: writing every axis.</remarks>
public FloatToVector3Converter() { }

/// <param name="axes">Which axes the number is written into.</param>
/// <param name="base">The value used for the axes the number does not write.</param>
public FloatToVector3Converter(AxisMask axes, Vector3 @base = default) { }
```

---

## `<remarks>`

Default position: **no `<remarks>` at all.** The block earns its place only when it states a
non-obvious constraint or behavior of the type/member itself. Keep it to one or two sentences.

**Use for:**

- Conditional compilation notes (`UNITY_EDITOR`, `DEBUG`).
- Non-obvious defaults.
- Availability constraints (only under certain platforms / symbols).
- Mandatory `base.Method()` calls for overrides.
- Behavioral traps (e.g. a two-way converter whose reverse path becomes ambiguous for certain configs).

**Do NOT use for:**

- Info already covered by `<exception>`, `<param>`, or `<returns>` — duplication rots.
- Things visible from the declaration itself (attributes, modifiers, base type).
- History of how the type came to be, comparisons with other classes, or scenario-style
  motivation ("a ViewModel holding X could not feed Y..."). The doc describes the type, not its
  backstory.

```csharp
/// <remarks>By default, uses <see cref="GenericToStringConverter{TFrom}"/> for the conversion.</remarks>
/// <remarks>Only available when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.</remarks>
```

---

## `[Tooltip]` on serialized fields

Every `[SerializeField]` and `[SerializeReference]` must have a `[Tooltip]` directly above it so Inspector users see the field's purpose without jumping to source.

When a behavioral caveat applies to a field (clamping, ignored-when, fallback rules), it must appear **both** in the field's `[Tooltip]` and in the `<param>` of the constructor parameter that sets the same field — the Inspector user and the code user get the same warning.

Inside `#if UNITY_2022_1_OR_NEWER` blocks, use the fully-qualified `[UnityEngine.Tooltip]` to avoid `using` resolution issues across conditional branches.

---

## Exception docs — Unity conditional behavior

Aspid.MVVM's `BindSafely*` APIs throw in plain .NET but skip/log in Unity builds. Exception docs must document all three cases so callers aren't surprised when behavior flips:

```csharp
/// <exception cref="BindSafelyNullReferenceException">
/// Thrown when <paramref name="binder"/> is <see langword="null"/>.
/// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips instead of throwing.
/// When <c>DEBUG</c> is also defined, additionally logs via <c>UnityEngine.Debug.LogError</c>.
/// </exception>
```

Order:

1. Base case — what is thrown and under what condition.
2. Unity build override — skips instead of throwing.
3. Unity + `DEBUG` — additionally logs (does not change the skip behavior).
