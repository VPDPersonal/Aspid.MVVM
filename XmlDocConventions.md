# XML Documentation Conventions

Guidelines for writing XML documentation comments in Aspid.MVVM. Apply to all public and protected APIs.

---

## Class `<summary>` — Hierarchy Style

The summary always describes the class **in terms of its parent**, not in isolation.

**Abstract / base classes:**
```csharp
/// <summary>
/// Abstract base <see cref="ParentClass"/> that adds [what this layer contributes].
/// </summary>
```

**Sealed / concrete classes with an interface:**
```csharp
/// <summary>
/// <see cref="ParentClass"/> implementing <see cref="IInterface"/> that [main purpose].
/// </summary>
```

**Sealed non-generic specialization of a generic abstract parent:**
```csharp
/// <summary>
/// Concrete <see cref="ParentClass{T}"/> that also implements <see cref="IInterface"/>,
/// [what it additionally does].
/// </summary>
```
Use the `"Concrete"` prefix when the sealed class is a specific non-generic instantiation of a generic abstract parent and adds extra interface implementations or behavior on top.

**Rules:**
- The first word is always a `<see cref>` to the direct parent (not the root), or `"Concrete"` (which is then followed by the `<see cref>`).
- Never start with `"A"`, `"The"`, or `"This"`.
- Never describe what the parent already does — only what *this class adds*.
- Do not repeat what is visible from the class declaration (e.g., `": IBinder<T>"` is already there).

**Examples:**
```csharp
// Abstract binder
/// <summary>
/// Abstract base <see cref="Binder"/> that adds <see cref="IColorBinder"/> support,
/// accepting both <see cref="Color"/> values and HTML color strings.
/// </summary>

// Sealed concrete binder implementing an interface
/// <summary>
/// <see cref="Binder"/> implementing <see cref="IAnyBinder"/> that converts any bound value
/// to a <see cref="string"/> using a configurable converter before forwarding it to a target setter.
/// </summary>

// Sealed non-generic specialization of a generic abstract parent
/// <summary>
/// Concrete <see cref="ComponentToSourceMonoBinder{TComponent}"/> that also implements <see cref="IAnyReverseBinder"/>,
/// raising <see cref="ValueChanged"/> with the <see cref="Component"/> reference cast to <see langword="object"/>.
/// </summary>

// Sealed MonoBehaviour concrete binder (no extra interfaces)
/// <summary>
/// <see cref="ComponentMonoBinder{TComponent}"/> that binds a <see cref="Color"/> property
/// on a <typeparamref name="TComponent"/> component.
/// </summary>
```

---

## Properties

Use consistent opening verbs depending on access:

| Access | Format |
|---|---|
| Get-only | `Gets the X.` |
| Get + set | `Gets or sets the X.` |
| Boolean state | `Indicates whether X.` |

When a property has a non-obvious default value, add it as a second sentence:

```csharp
/// <summary>
/// Indicates whether binding is allowed.
/// The default value is <see langword="true"/>.
/// </summary>
public virtual bool IsBind => true;

/// <summary>
/// Indicates whether the binder is currently bound to a ViewModel.
/// </summary>
public bool IsBound { get; private set; }

/// <summary>
/// Gets the binding mode that determines the direction of data flow.
/// </summary>
public BindMode Mode => _mode;

/// <summary>
/// Gets or sets the component property that this binder reads from and writes to.
/// </summary>
protected abstract TProperty Property { get; set; }
```

**Note:** default values must use `<see langword="true"/>` / `<see langword="false"/>` — never `<c>true</c>` / `<c>false</c>`.

---

## Events

Use the pattern `"Raised when X."` or `"Raised with X when Y."`:

```csharp
/// <summary>
/// Raised when the bound value changes.
/// </summary>
public event Action<TProperty> ValueChanged;

/// <summary>
/// Raised with the cached <typeparamref name="TComponent"/> reference when binding is established.
/// </summary>
public event Action<TComponent> ValueChanged;

/// <summary>
/// Raised with the attached <see cref="Component"/> reference as <see langword="object"/> when binding is established.
/// </summary>
public new event Action<object> ValueChanged;
```

---

## Abstract Virtual Hook Methods

For Unity lifecycle overrides and framework hook methods, use:
`"Called [when / lifecycle moment]. [What it does / what to override for]."`

```csharp
/// <summary>
/// Called before binding is established. Override to add pre-binding logic.
/// </summary>
protected virtual void OnBinding() { }

/// <summary>
/// Called after binding is established. Override to add post-binding logic.
/// </summary>
protected virtual void OnBound() { }

/// <summary>
/// Called by Unity in the Editor when a serialized field value changes.
/// Automatically resolves and assigns the component if it is not yet set and the application is not playing.
/// </summary>
protected virtual void OnValidate() { }
```

When **overriding** a hook where the base call is mandatory, document it with `<remarks>`:

```csharp
/// <summary>
/// Called after binding is established.
/// Sends the initial property value to the ViewModel when in <see cref="BindMode.OneWayToSource"/> mode.
/// </summary>
/// <remarks>
/// When overriding this method, always call <c>base.OnBound()</c> to preserve
/// the <see cref="BindMode.OneWayToSource"/> initialization behavior.
/// </remarks>
protected override void OnBound() { }

/// <summary>
/// Called by Unity in the Editor when a serialized field value changes.
/// Automatically resolves and assigns the component if it is not yet set and the application is not playing.
/// </summary>
/// <remarks>
/// When overriding this method, always call <c>base.OnValidate()</c> to preserve
/// automatic component resolution in the Editor.
/// </remarks>
protected virtual void OnValidate() { }
```

---

## `<inheritdoc/>`

Use `<inheritdoc/>` **only** when the behavior is trivially identical to the base.

**Use `<inheritdoc/>`:**
- Interface explicit implementations with no added behavior.
- Overrides that do exactly what the base/interface describes with no side effects.
- `sealed override` methods whose only purpose is to delegate to an abstract method.

**Constructors:**
- Use `<inheritdoc/>` when the constructor's parameter list matches the parent's and the body only delegates (`base(...)`) or adds minor validation already implied by the parent docs (e.g., `ThrowExceptionIfMatches`).
- Write full `<summary>` + `<param>` + `<exception>` when the constructor changes the effective API: fewer parameters, hardcoded arguments, or behavior not covered by the parent doc.

**Do NOT use `<inheritdoc/>`:**
- Overrides that have **additional or different behavior** not described by the interface/base.
- Members where the inherited description would be misleading.

```csharp
// CORRECT — <inheritdoc/> on pass-through constructor (same params, direct delegation)
/// <inheritdoc/>
public AudioSourceVolumeSwitcherBinder(
    AudioSource target,
    float trueValue,
    float falseValue,
    IConverter<float, float>? converter = null,
    BindMode mode = BindMode.OneWay)
    : base(target, trueValue, falseValue, converter, mode) { }

// CORRECT — <inheritdoc/> with minor validation (ThrowExceptionIfMatches is implied by parent)
/// <inheritdoc/>
public AudioSourceVolumeBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
    : base(target, converter, mode)
{
    mode.ThrowExceptionIfMatches(BindMode.TwoWay);
}

// CORRECT — full doc because BindMode is hardcoded (API differs from parent)
/// <summary>
/// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{T}"/>.
/// </summary>
/// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
    : base(setValue, BindMode.OneTime) { }

// CORRECT — inheritdoc on trivial explicit implementation
/// <inheritdoc/>
void IDisposable.Dispose() => Unbind();

// CORRECT — inheritdoc on sealed override that only delegates
/// <inheritdoc/>
protected sealed override void SetDefaultValue(TElement element) =>
    SetValue(element, _defaultValue);

// CORRECT — full doc because behavior is non-trivial
/// <summary>
/// Called after binding is established.
/// In <see cref="BindMode.OneWayToSource"/> mode, broadcasts the current value
/// to all numeric event types.
/// </summary>
protected override void OnBound() { ... }
```

---

## `<remarks>`

Use `<remarks>` for implementation details that do not belong in `<summary>`.

**Put in `<remarks>`:**
- Conditional compilation notes (`UNITY_EDITOR`, `DEBUG`, `UNITY_2020_3_OR_NEWER`).
- Default values and why they exist.
- Alternative overloads or options that the user should know about.
- Availability constraints (`Only available in UNITY_EDITOR`).
- Mandatory `base.Method()` call requirement for virtual overrides (see **Abstract Virtual Hook Methods**).

**Do NOT put in `<remarks>`:**
- Information already documented in `<exception>`, `<param>`, or `<returns>`.
- What is visible from the class/method declaration (base class, implemented interfaces).
- The word "Note:" at the beginning — just state the fact directly.

```csharp
/// <remarks>
/// By default, uses <see cref="GenericToString{T}"/> for the conversion.
/// A custom <see cref="IConverter{TFrom,TTo}"/> can be supplied for specialized formatting.
/// </remarks>

/// <remarks>
/// Only available when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
/// </remarks>
```

---

## `<example>` on Non-MonoBinder Classes

Pure C# binder classes (non-`MonoBehaviour`) **must** include an `<example>` block. Examples live in a **dedicated XML file** alongside the `.cs` file; the `.cs` file references it via `<include>`.

### File layout

Each binder folder contains one `XmlExampleDoc-[Category]-[SubCategory]-1.1.0.xml` file placed in the same directory as the `.cs` files. Related binders in the same folder (e.g., a binder and its switcher variant) share a single XML file:

```
AudioSources/Volume/
    AudioSourceVolumeBinder.cs
    AudioSourceVolumeSwitcherBinder.cs
    XmlExampleDoc-AudioSource-Volume-1.1.0.xml
```

### Referencing from C# (`<include>`)

**Placement:** After `<remarks>`, before the class declaration.

Replace the inline `<example>` block with an `<include>` tag:

```csharp
/// <include file="XmlExampleDoc-AudioSource-Volume-1.1.0.xml" path="doc//member[@name='AudioSourceVolumeBinder']/*" />
public class AudioSourceVolumeBinder ...
```

For **generic classes**, the member name uses `{N}` where N is the number of type parameters:

```csharp
/// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneWayBinder{1}']/*" />
public class GenericOneWayBinder<T> ...

/// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneWayBinder{2}']/*" />
public class GenericOneWayBinder<TTarget, T> ...
```

### XML file structure

```xml
<?xml version="1.0" encoding="utf-8"?>
<doc>
    <member name="AudioSourceVolumeBinder">
        <example>
            Bind the AudioSource volume property using a serialized binder field — the target AudioSource is assigned in the Inspector.
            <code>
                [View]
                public partial class ExampleView
                {
                    [SerializeField]
                    private AudioSourceVolumeBinder _volume;
                }
                &#xD;
                [ViewModel]
                public partial class ExampleViewModel
                {
                    [Bind] public float _volume;
                }
            </code>
            &#xD;
            Bind the AudioSource volume property by constructing the binder in code.
            <code>
                [View]
                public partial class ExampleView
                {
                    [SerializeField] private AudioSource _audioSource;
                    &#xD;
                    private AudioSourceVolumeBinder Volume =&gt;
                        new(_audioSource);
                }
                &#xD;
                [ViewModel]
                public partial class ExampleViewModel
                {
                    [Bind] public float _volume;
                }
            </code>
        </example>
    </member>
</doc>
```

**XML encoding rules:**
- Blank lines between sections (fields/binder, View/ViewModel) → `&#xD;`
- Lambda arrows (`=>`) → `=&gt;`
- Generic angle brackets (`<`, `>`) → `&lt;` / `&gt;`
- Code inside `<code>` uses 4-space indentation relative to the tag (16 spaces for class body, 20 spaces for members)

### Content rules

- Description text goes **before** `<code>`, not inside it.
- The example class is always `[View] public partial class ExampleView`.
- Always end with a `[ViewModel]` block showing the matching `[Bind]` field.
- Use `&#xD;` for blank lines between all sections (fields/binder, View/ViewModel).
- **Closure-based binders** (e.g., `Generic*Binder`): one `<code>` block. Declare as a private property named after the VM field. Single-arg: `=&gt; new(...)`. Multi-arg: `= new\n(\n    ...\n)`. Show the target `[SerializeField]` field above the binder, separated by `&#xD;`.
- **Serialized field binders** (e.g., `*Value`): one `<code>` block. Declare as a private field with `= new()`. Add `[SerializeField]` only when the initial value is set in the Inspector. Use `OnInitializedInternal` / `OnDeinitializingInternal` (not `OnEnable` / `OnDisable`) for subscribe/unsubscribe.
- **Target-based binders** (e.g., `TargetBinder<TTarget, T>` subclasses): **two `<code>` blocks** within one `<example>`, separated by a description + `&#xD;`. The first shows the serialized-field form — `[SerializeField]` on its own line, then the field; description: `"Bind the [X] property using a serialized binder field — the target [T] is assigned in the Inspector."`. The second shows the constructor form — target `[SerializeField]` inline, `&#xD;`, then a property `=&gt; new(target[, ...])`.
- **Switcher binders**: same two-block structure. Description begins with `"Switch the [X] between two values"` instead of `"Bind the [X] property"`.

**Examples:**

```xml
<!-- CORRECT — closure-based, single-argument (GenericOneWayBinder{1}) -->
<member name="GenericOneWayBinder{1}">
    <example>
        Update a score label each time the ViewModel value changes
        <code>
            [View]
            public partial class ExampleView
            {
                [SerializeField] private TMP_Text _label;
                &#xD;
                private GenericOneWayBinder&lt;int&gt; Score =&gt; new(
                    value =&gt; _label.text = value.ToString());
            }
            &#xD;
            [ViewModel]
            public partial class ExampleViewModel
            {
                [Bind] public int _score;
            }
        </code>
    </example>
</member>

<!-- CORRECT — closure-based, multi-argument (GenericTwoWayBinder{1}) -->
<member name="GenericTwoWayBinder{1}">
    <example>
        Synchronize a slider with a ViewModel float property in both directions
        <code>
            [View]
            public partial class ExampleView
            {
                [SerializeField] private Slider _slider;
                &#xD;
                private GenericTwoWayBinder&lt;float&gt; Value = new(
                    onChanged =&gt; _slider.onValueChanged.AddListener(onChanged),
                    value =&gt; _slider.value = value);
            }
            &#xD;
            [ViewModel]
            public partial class ExampleViewModel
            {
                [Bind] public float _value;
            }
        </code>
    </example>
</member>

<!-- CORRECT — target-based binder: two <code> blocks -->
<member name="AudioSourceClipBinder">
    <example>
        Bind the AudioSource clip property using a serialized binder field — the target AudioSource is assigned in the Inspector.
        <code>
            [View]
            public partial class ExampleView
            {
                [SerializeField]
                private AudioSourceClipBinder _clip;
            }
            &#xD;
            [ViewModel]
            public partial class ExampleViewModel
            {
                [Bind] public AudioClip _clip;
            }
        </code>
        &#xD;
        Bind the AudioSource clip property by constructing the binder in code.
        <code>
            [View]
            public partial class ExampleView
            {
                [SerializeField] private AudioSource _audioSource;
                &#xD;
                private AudioSourceClipBinder Clip =&gt; new(_audioSource);
            }
            &#xD;
            [ViewModel]
            public partial class ExampleViewModel
            {
                [Bind] public AudioClip _clip;
            }
        </code>
    </example>
</member>
```

---

## Null References and Keywords

Always use `<see langword="..."/>` — never `<c>...</c>` for C# keywords.

```csharp
// CORRECT
/// <returns>The binder instance, or <see langword="null"/> if not found.</returns>
/// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
/// The default value is <see langword="true"/>.

// WRONG
/// <returns>The binder instance, or <c>null</c> if not found.</returns>
/// The default value is <c>true</c>.
```

Applies to: `null`, `true`, `false`, `this`, `void`, `default`, `new`.

---

## Enum Values

Always use `<see cref="..."/>` for enum members, never `<c>EnumValue</c>`.

```csharp
// CORRECT
/// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
/// Supports <see cref="BindMode.OneWay">OneWay</see> and <see cref="BindMode.OneTime">OneTime</see>.

// WRONG
/// <param name="mode">The binding mode. Must not be <c>BindMode.TwoWay</c>.</param>
```

Use the display-text form `<see cref="X">label</see>` when the enum member name alone is unclear in context.

---

## Method References in `<see cref>`

When referencing a specific overload, include the parameter types in the `<see cref>` signature:

```csharp
/// forwards it to <see cref="SetValue(T)"/>
/// delegates to <see cref="SetValue(TElement, TValue)"/>
/// resolves via <see cref="Component.GetComponent{T}"/>
```

Use this form when a class has multiple overloads of the same method and the reference must be unambiguous.

---

## `[Tooltip]` on Serialized Fields

Every `[SerializeField]` and `[SerializeReference]` field **must** have a `[Tooltip]` attribute placed directly above the field declaration.

```csharp
[Tooltip("The target component whose Color property will be driven by the binding.")]
[SerializeField] private Image _target;
```

Inside `#if UNITY_2022_1_OR_NEWER` preprocessor blocks, use the fully-qualified name to avoid ambiguity:

```csharp
#if UNITY_2022_1_OR_NEWER
[UnityEngine.Tooltip("The value applied when the bound boolean is true.")]
[SerializeField] private T _trueValue;
#endif
```

---

## Exception Docs — Unity Conditional Behavior

When a method has Unity-specific null-check behavior (skip + log instead of throw), document all three cases in the `<exception>` tag:

```csharp
/// <exception cref="BindSafelyNullReferenceException">
/// Thrown when <paramref name="binder"/> is <see langword="null"/>.
/// In Unity builds (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
/// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
/// </exception>
```

Pattern:
1. Base case — what is thrown and when.
2. Unity build override — skips instead of throwing.
3. Unity + DEBUG — additionally logs.

---

## `<typeparam>`

Keep descriptions short and concrete. State *what kind of type* is expected.

```csharp
// CORRECT
/// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
/// <typeparam name="TConverter">The converter type used to transform the bound value.</typeparam>
/// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
/// <typeparam name="T">The runtime type of the incoming value.</typeparam>

// WRONG — too vague
/// <typeparam name="TTarget">The type.</typeparam>
/// <typeparam name="TTarget">The target type (e.g., a Unity component).</typeparam>
```

---

## `<param>`

Use consistent phrasing:

| Parameter role | Format |
|---|---|
| Action/callback | `The action invoked with the converted <see cref="X"/> value.` |
| Binding mode | `The binding mode. [constraints, e.g., Must not be TwoWay.]` |
| Target object | `The X to bind.` |
| Boolean flag | `When <see langword="true"/>, [effect].` |
| Converter | `The converter used to transform X to Y.` |
| Nullable optional | `The X, or <see langword="null"/> to use the default.` |
| Enum value (binder) | `The bound enum value received from the ViewModel.` |
| Element | `The target element.` / `The element to reset to its default state.` |

---

## `<returns>`

Describe both the success case and the fallback:

```csharp
/// <returns>The converted <see cref="bool"/> value.</returns>
/// <returns>The binder instance if found; otherwise, <see langword="null"/>.</returns>
/// <returns><see langword="true"/> if the binding was established; otherwise, <see langword="false"/>.</returns>
/// <returns>The converted value.</returns>
```

---

## Static / Extension Method Classes

```csharp
// Extension method class
/// <summary>
/// Provides extension methods for <see cref="IBinder"/> instances that implement <see cref="IRebindableBinder"/>.
/// </summary>

// Utility/helper class (non-extension)
/// <summary>
/// Provides utility methods for <see cref="BinderFieldInfo"/>.
/// </summary>
```

Use `"Provides extension methods"` only when the class actually contains `this` parameter methods.
Use `"Provides utility methods"` for static helper classes without extension methods.

---

## Summary Format

- Always multiline: opening tag on its own line, closing tag on its own line.
- End single-sentence summaries with a period.
- For multi-sentence summaries: each sentence ends with a period.

```csharp
// CORRECT — multiline, period
/// <summary>
/// Rebinds the binder, re-establishing its connection to the current ViewModel.
/// </summary>

// CORRECT — multiline, two sentences
/// <summary>
/// Abstract base <see cref="Binder"/> that adds reverse binding support.
/// Raises <see cref="ValueChanged"/> whenever the target value changes.
/// </summary>

// WRONG — single line (avoid)
/// <summary>Rebinds the binder.</summary>
```

---

## Quick Reference Checklist

Before committing, verify each public/protected member:

- [ ] Class summary uses hierarchy style (starts with `<see cref="Parent"/>` or `"Concrete <see cref="..."/>"`)
- [ ] Non-MonoBehaviour binder classes: examples are in a `XmlExampleDoc-*.xml` file in the same folder, referenced via `<include file="..." path="doc//member[@name='...']/*" />` in the `.cs` file
- [ ] Constructors: `<inheritdoc/>` when the signature matches the parent (direct delegation ± minor validation); full `<summary>` + `<param>` + `<exception>` when the API changes (hardcoded args, fewer params, new behavior)
- [ ] No `<inheritdoc/>` where behavior differs from the base
- [ ] `<see langword="null"/>` / `<see langword="true"/>` / `<see langword="false"/>` — never `<c>null</c>` / `<c>true</c>` / `<c>false</c>`
- [ ] Enum members referenced with `<see cref="Enum.Member"/>`
- [ ] Every `[SerializeField]` / `[SerializeReference]` has `[Tooltip]`
- [ ] `<remarks>` does not duplicate `<exception>` or `<param>` content
- [ ] Exception doc covers Unity conditional behavior (skip / log / throw)
- [ ] `<typeparam>` is concrete, not just `"The type."`
- [ ] Properties use "Gets", "Gets or sets", or "Indicates whether"
- [ ] Events use "Raised when X." or "Raised with X when Y."
- [ ] Virtual hooks use "Called [when]. Override to add [purpose] logic."
- [ ] Overrides requiring `base.Method()` have a `<remarks>` note
- [ ] Summary ends with a period