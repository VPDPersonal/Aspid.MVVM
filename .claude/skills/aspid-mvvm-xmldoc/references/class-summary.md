# Class `<summary>` — Hierarchy Style

Describe the class **in terms of its parent**, not in isolation. The first word of the summary is either `<see cref="DirectParent"/>` or the literal `"Concrete"` — never `"A"`, `"The"`, or `"This"`. Only describe what *this* class adds on top of its parent; don't repeat information that's already visible in the class declaration (base class, implemented interfaces, generic parameters).

The reason for this style is that Aspid.MVVM has deep binder hierarchies. A reader looking at `AudioSourceVolumeBinder` cares about what's special about it — not about what every `ComponentBinder` does. Framing each class relative to its parent makes the inheritance chain readable in IDE tooltips.

---

## Templates

**Abstract / base class:**

```
Abstract base <see cref="Parent"/> that adds [what this layer contributes].
```

**Sealed class implementing an interface:**

```
<see cref="Parent"/> implementing <see cref="IInterface"/> that [main purpose].
```

**Concrete non-generic** — a non-generic instantiation of a generic parent that also adds interface or behavior:

```
Concrete <see cref="Parent{T}"/> that also implements <see cref="IInterface"/>, [extra behavior].
```

**Binder targeting specific component properties** — reference the actual `<see cref="Component.Property"/>`, not just the component class, so readers can jump to the exact property:

```
<see cref="ParentBinder{TComponent}"/> that sets the <see cref="Component.Prop1"/> and <see cref="Component.Prop2"/> depending on [condition].
```

---

## Examples

```csharp
// Abstract base
/// <summary>
/// Abstract base <see cref="Binder"/> that adds <see cref="IColorBinder"/> support,
/// accepting both <see cref="Color"/> values and HTML color strings.
/// </summary>

// Sealed with interface
/// <summary>
/// <see cref="Binder"/> implementing <see cref="IAnyBinder"/> that converts any bound value
/// to a <see cref="string"/> using a configurable converter before forwarding it to a target setter.
/// </summary>

// Concrete non-generic
/// <summary>
/// Concrete <see cref="ComponentToSourceMonoBinder{TComponent}"/> that also implements <see cref="IAnyReverseBinder"/>,
/// raising <see cref="ValueChanged"/> with the <see cref="Component"/> reference cast to <see langword="object"/>.
/// </summary>

// Specific component properties
/// <summary>
/// <see cref="ComponentColorMonoBinder{LineRenderer}"/> that sets the <see cref="LineRenderer.startColor"/>
/// and <see cref="LineRenderer.endColor"/> depending on the configured <see cref="LineRendererColorMode"/>.
/// </summary>
```
