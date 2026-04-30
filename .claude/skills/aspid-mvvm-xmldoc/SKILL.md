---
name: aspid-mvvm-xmldoc
description: Conventions for writing XML documentation comments (`///`) in the Aspid.MVVM C# framework — `<summary>`, `<param>`, `<typeparam>`, `<returns>`, `<remarks>`, `<exception>`, `<inheritdoc/>`, hook methods, binder `<example>` includes, and class hierarchy-style summaries. Use this skill whenever writing, editing, or reviewing XML doc comments on classes, interfaces, methods, properties, events, binders, or attributes anywhere in the Aspid.MVVM repo, or when the user asks how to document something in this project, to add XML docs to a file, to write a summary for a binder, to format a `<param>` or `<returns>` block, or to fix inconsistent/missing documentation. Also triggers on phrases like "document this", "add XML docs", "write a summary for", "fix the docs on", "document the binder", or when editing `.cs` files that already contain `///` blocks.
---

# Aspid.MVVM XML Documentation Conventions

Rules for XML doc comments on all public and protected APIs in the Aspid.MVVM framework. The point of these conventions is consistency: every class summary reads in the same hierarchy style, every `<param>` uses the same phrasing, every nullable return and keyword is rendered with `<see langword="..."/>`. Readers (and IDE tooltips) rely on that consistency.

Skim the quick rules below first — they cover ~80% of cases. For deeper topics (class summaries, hook methods, binder examples), consult the matching reference file.

## When to use reference files

- **Writing a class/struct/interface `<summary>`** → read `references/class-summary.md` (hierarchy-style templates for abstract, sealed, concrete, and binder-targeting-specific-properties cases).
- **Documenting hook methods, `<inheritdoc/>`, `<remarks>`, `<exception>` for Unity conditional behavior, `[Tooltip]` rules** → read `references/tags.md`.
- **Writing `<example>` blocks for non-MonoBehaviour binder classes** (using `XmlExampleDoc-*.xml` files and `<include>`) → read `references/binders.md`. Note: `MonoBinder` classes (MonoBehaviour-based) do NOT need `<include>` or XML example blocks.

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

Describe both success and fallback cases:

- `The binder instance if found; otherwise, <see langword="null"/>.`
- `<see langword="true"/> if the binding was established; otherwise, <see langword="false"/>.`

---

## Null references and keywords

Always use `<see langword="..."/>` — never `<c>...</c>` — for: `null`, `true`, `false`, `this`, `void`, `default`, `new`.

The reason is semantic: `<see langword=>` tells the doc renderer these are language keywords, which enables proper styling and cross-language rendering. `<c>` is plain monospace formatting and loses that signal.

---

## Enum values

Always `<see cref="..."/>` — never `<c>EnumValue</c>`. Use `<see cref="X">label</see>` when the member name alone is unclear to the reader.

---

## Method references in `<see cref>`

Include parameter types when referencing a specific overload so the link resolves unambiguously:

```
<see cref="SetValue(T)"/>
<see cref="SetValue(TElement, TValue)"/>
```

---

## Static / extension method classes

- Extension methods: `"Provides extension methods for <see cref="X"/> instances that implement <see cref="IFoo"/>."`
- Utility: `"Provides utility methods for <see cref="X"/>."`

---

## Summary formatting

Always use multiline form; always end with a period. Never use the single-line `/// <summary>X.</summary>` form — it's harder to extend later and inconsistent with the rest of the codebase.

---

## Quick reference checklist

Before finishing a file with XML docs, verify:

- [ ] Class summary follows hierarchy style (see `references/class-summary.md`)
- [ ] Non-MonoBehaviour binders have `<example>` blocks via `<include>` from `XmlExampleDoc-*.xml` (see `references/binders.md`)
- [ ] MonoBinder (MonoBehaviour) classes do NOT have `<include>` / `<example>` blocks
- [ ] Constructors: `<inheritdoc/>` when matching parent; full doc when API differs (see `references/tags.md`)
- [ ] No `<inheritdoc/>` where behavior differs from base
- [ ] `<see langword="null"/>` / `<see langword="true"/>` — never `<c>null</c>` / `<c>true</c>`
- [ ] Enum members: `<see cref="Enum.Member"/>` — never `<c>EnumValue</c>`
- [ ] Every `[SerializeField]` / `[SerializeReference]` has `[Tooltip]`
- [ ] `<remarks>` does not duplicate `<exception>` / `<param>` content
- [ ] Exception docs cover Unity conditional behavior (skip / log / throw — see `references/tags.md`)
- [ ] `<typeparam>` is concrete — not just `"The type."`
- [ ] Properties: `"Gets"`, `"Gets or sets"`, `"Indicates whether"`
- [ ] Events: `"Raised when X."` or `"Raised with X when Y."`
- [ ] Virtual hooks: `"Called [when]. Override to [purpose]."` (see `references/tags.md`)
- [ ] Overrides requiring `base.Method()` have a `<remarks>` note
- [ ] Summary is multiline and ends with a period
