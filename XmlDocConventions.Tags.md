# Hook Methods, Tags & Exceptions

---

## Abstract / Virtual Hook Methods

Pattern: `"Called [when]. Override to [purpose]."`

```csharp
/// <summary>
/// Called before binding is established. Override to add pre-binding logic.
/// </summary>
protected virtual void OnBinding() { }
```

When an override requires a mandatory `base.Method()` call, add `<remarks>`:

```csharp
/// <remarks>
/// When overriding, always call <c>base.OnBound()</c> to preserve
/// the <see cref="BindMode.OneWayToSource"/> initialization behavior.
/// </remarks>
```

---

## `<inheritdoc/>`

**Use when:**
- Explicit interface implementations with no added behavior.
- Overrides that do exactly what the base describes.
- `sealed override` methods that only delegate to an abstract method.
- Pass-through constructors (same params, delegates via `base(...)`, ± minor validation).

**Do NOT use when:**
- Behavior is different or additional.
- Constructors that change the effective API (fewer params, hardcoded arguments).

```csharp
// Pass-through constructor — use inheritdoc
/// <inheritdoc/>
public AudioSourceVolumeBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
    : base(target, converter, mode) { }

// Hardcoded argument — write full doc
/// <summary>
/// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{T}"/>.
/// </summary>
/// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
    : base(setValue, BindMode.OneTime) { }
```

---

## `<remarks>`

**Use for:** conditional compilation (`UNITY_EDITOR`, `DEBUG`), non-obvious defaults, availability constraints, mandatory `base.Method()` calls.

**Do NOT use for:** info already in `<exception>`, `<param>`, or `<returns>`; what's visible from the declaration.

```csharp
/// <remarks>By default, uses <see cref="GenericToString{T}"/> for the conversion.</remarks>
/// <remarks>Only available when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.</remarks>
```

---

## `[Tooltip]` on Serialized Fields

Every `[SerializeField]` / `[SerializeReference]` must have `[Tooltip]` directly above it. Inside `#if UNITY_2022_1_OR_NEWER` blocks, use the fully-qualified `[UnityEngine.Tooltip]`.

---

## Exception Docs — Unity Conditional Behavior

Document all three cases:

```csharp
/// <exception cref="BindSafelyNullReferenceException">
/// Thrown when <paramref name="binder"/> is <see langword="null"/>.
/// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips instead of throwing.
/// When <c>DEBUG</c> is also defined, additionally logs via <c>UnityEngine.Debug.LogError</c>.
/// </exception>
```

1. Base case — what is thrown and when.
2. Unity build override — skips instead of throwing.
3. Unity + DEBUG — additionally logs.
