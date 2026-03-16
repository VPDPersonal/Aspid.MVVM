# XML Documentation Conventions

Guidelines for XML documentation comments in Aspid.MVVM. Apply to all public and protected APIs.

**Sub-topics:**
- [Class Summary — Hierarchy Style](XmlDocConventions.ClassSummary.md)
- [Hook Methods, Tags & Exceptions](XmlDocConventions.Tags.md)
- [Binder Examples (`<example>` XML)](XmlDocConventions.Binders.md)

---

## Properties

| Access | Format |
|---|---|
| Get-only | `Gets the X.` |
| Get + set | `Gets or sets the X.` |
| Boolean state | `Indicates whether X.` |

Add a second sentence for non-obvious defaults. Use `<see langword="true"/>` / `<see langword="false"/>` — never `<c>true</c>`.

---

## Events

Pattern: `"Raised when X."` or `"Raised with X when Y."`

---

## `<param>`

| Role | Format |
|---|---|
| Action/callback | `The action invoked with the converted <see cref="X"/> value.` |
| Binding mode | `The binding mode. [constraints]` |
| Target object | `The X to bind.` |
| Boolean flag | `When <see langword="true"/>, [effect].` |
| Converter | `The converter used to transform X to Y.` |
| Nullable optional | `The X, or <see langword="null"/> to use the default.` |
| Enum value (binder) | `The bound enum value received from the ViewModel.` |
| Element | `The target element.` |

---

## `<typeparam>`

State *what kind of type* is expected — not just `"The type."`:

```csharp
/// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
/// <typeparam name="T">The runtime type of the incoming value.</typeparam>
```

---

## `<returns>`

Describe both success case and fallback:
- `The binder instance if found; otherwise, <see langword="null"/>.`
- `<see langword="true"/> if the binding was established; otherwise, <see langword="false"/>.`

---

## Null References and Keywords

Always `<see langword="..."/>` — never `<c>...</c>` for: `null`, `true`, `false`, `this`, `void`, `default`, `new`.

---

## Enum Values

Always `<see cref="..."/>` — never `<c>EnumValue</c>`. Use `<see cref="X">label</see>` when the member name alone is unclear.

---

## Method References in `<see cref>`

Include parameter types when referencing a specific overload: `<see cref="SetValue(T)"/>`, `<see cref="SetValue(TElement, TValue)"/>`.

---

## Static / Extension Method Classes

- Extension: `"Provides extension methods for <see cref="X"/> instances that implement <see cref="IFoo"/>."`
- Utility: `"Provides utility methods for <see cref="X"/>."`

---

## Summary Format

Always multiline; always end with a period. Never use single-line form.

---

## Quick Reference Checklist

- [ ] Class summary: hierarchy style — see [XmlDocConventions.ClassSummary.md](XmlDocConventions.ClassSummary.md)
- [ ] Non-MonoBehaviour binders: examples in `XmlExampleDoc-*.xml` via `<include>` — see [XmlDocConventions.Binders.md](XmlDocConventions.Binders.md)
- [ ] Constructors: `<inheritdoc/>` when matching parent; full doc when API differs — see [XmlDocConventions.Tags.md](XmlDocConventions.Tags.md)
- [ ] No `<inheritdoc/>` where behavior differs from base
- [ ] `<see langword="null"/>` / `<see langword="true"/>` — never `<c>null</c>` / `<c>true</c>`
- [ ] Enum members: `<see cref="Enum.Member"/>` — never `<c>EnumValue</c>`
- [ ] Every `[SerializeField]` / `[SerializeReference]` has `[Tooltip]`
- [ ] `<remarks>` does not duplicate `<exception>` or `<param>` content
- [ ] Exception doc covers Unity conditional behavior (skip / log / throw)
- [ ] `<typeparam>` is concrete — not just `"The type."`
- [ ] Properties: "Gets", "Gets or sets", "Indicates whether"
- [ ] Events: "Raised when X." or "Raised with X when Y."
- [ ] Virtual hooks: "Called [when]. Override to [purpose]." — see [XmlDocConventions.Tags.md](XmlDocConventions.Tags.md)
- [ ] Overrides requiring `base.Method()` have a `<remarks>` note
- [ ] Summary ends with a period
