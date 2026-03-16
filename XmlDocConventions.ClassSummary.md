# Class `<summary>` — Hierarchy Style

Describe the class **in terms of its parent**. First word: `<see cref="DirectParent"/>` or `"Concrete"` — never `"A"`, `"The"`, or `"This"`. Only describe what *this* class adds; don't repeat what's visible in the declaration.

---

## Templates

**Abstract / base:**
`Abstract base <see cref="Parent"/> that adds [what this layer contributes].`

**Sealed with interface:**
`<see cref="Parent"/> implementing <see cref="IInterface"/> that [main purpose].`

**Concrete non-generic** (non-generic instantiation of a generic parent that also adds interface/behavior):
`Concrete <see cref="Parent{T}"/> that also implements <see cref="IInterface"/>, [extra behavior].`

**Binder targeting specific component properties** — use `<see cref="Component.Property"/>`, not just the class:
`<see cref="ParentBinder{TComponent}"/> that sets the <see cref="Component.Prop1"/> and <see cref="Component.Prop2"/> depending on [condition].`

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
