---
name: aspid-code-style
description: Formatting and layout rules for C# code in Aspid.MVVM — member ordering, `#nullable`, line breaks in expression-bodied members and ternaries, column-aligned parameters, named arguments for fallback/log helpers, float comparison, comments, `#if` guards. Use when writing or reviewing any `.cs` file in the repo (Source, StarterKit, Unity, Editor, tests), when deciding how to format something, or when the user asks to "tidy up", "align with our style", "do it like the rest of the code".
---

# Aspid.MVVM code style

These rules apply to all new and edited code without asking. Breaking public API is allowed, but members are never renamed for style alone: rename only when the name misrepresents behavior.

## File

- `#nullable enable` as the first line when the file has `?` annotations. Exception: files with `MonoBehaviour` classes (views, initializers, Mono binders) have **no** `#nullable enable` and no `?` annotations.
- File name = type name. One public type per file.
- `// ReSharper disable once CheckNamespace` above `namespace` when the folder differs from the namespace (the package default).
- No `#region`/`#endregion`, even with dozens of explicit interface implementations.
- The package targets Unity `6000.0`: no `#if UNITY_2023_1_OR_NEWER` or TMP guards. The only conditional defines are `ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION`, `ASPID_MVVM_ZENJECT_INTEGRATION`, `ASPID_MVVM_VCONTAINER_INTEGRATION`, `ASPID_MVVM_ADDRESSABLES_INTEGRATION`, `ASPID_MVVM_UNITASK_INTEGRATION`.
- No trailing whitespace, no whitespace-only lines. No em/en dashes (`—`, `–`) in code, comments or docs: use `:` or `,`.
- American spelling, including API names (`Center`, not `Centre`).

## Member ordering (StyleCop SA1201 with one deviation)

1. Serialized fields (`[SerializeField]`, `[SerializeReference]`)
2. Other fields
3. Auto-properties (including `protected abstract T X { get; set; }`)
4. Constructors
5. Events
6. Properties with bodies / expression-bodied
7. Methods

The single deviation from StyleCop: auto-properties sit above the constructor. Within fields, serialized ones come first.

## Constructors

- 2+ parameters: one parameter **per line**, opening parenthesis on the name line.
- Assignments in the body ordered by increasing line length, the longest (`?? throw`) last.
- Optional fallback: `_fallback = fallback ?? _fallback;` on one line (never `if (x is not null)`), so the field default survives omission.
- Default constructor access: `protected` on non-sealed generic types (a subclass needs a reachable base ctor for Unity serialization); `private` on sealed types when an empty instance fails at runtime; `public` with `<remarks>Default: …</remarks>` when an empty instance is valid.
- Invalid arguments throw (`ArgumentNullException`, `ArgumentOutOfRangeException` with the parameter name); never accept silently.

## Methods and expressions

- Method parameters go one per line only when the signature exceeds 120 columns.
- Short guard `if` with `return` on one line: `if (!IsMissing(value)) return value;`
- Brace-less `if` whose body is an action (assignment, call): body on the next line, indented. One line only for `return`.
- `if` whose body is a single multi-line `return …(` … `)` gets braces.
- Expression-bodied method: body on the **next line** after `=>`:
  ```csharp
  string IConverter<int, string>.Convert(int value) =>
      Convert(value);
  ```
- Ternary as an expression body: the condition stays on the `=>` line, branches on the following lines:
  ```csharp
  public string? Convert(string? value) => string.IsNullOrWhiteSpace(value)
      ? value
      : Wrap(value, _color, _includeAlpha);
  ```
- Ternary in a `var x = …` assignment: three lines (condition / `? a` / `: b`); inside a call argument or `return` it stays on one line.
- A chain of `if … return` over one value becomes a `switch` expression with relational patterns.
- Array initializers are not wrapped for line width.
- Multi-line string concatenation: `+` at the end of the line, not at the start of the next.
- Calls with 2+ arguments to fallback and log helpers (`UseFallback`, `Fallback`, `Fail`, `LogError`) use **named arguments, one per line**:
  ```csharp
  return this.UseFallback(
      fallback: _fallback,
      problem: value.Expected("an index"));
  ```

## Numbers

- Never `==`/`!=` between `float`/`double` (ReSharper CompareOfFloatsByEqualityOperator): zero via `value is 0d` / `is not 0f`, integrality via `value % 1d is 0d`, equality of two floats via `Mathf.Approximately`.
- A value outside the target type's range saturates at the bound (`NumericSaturation`); it is never dropped silently.

## Comments

- Delete `//` comments entirely when they restate the XML doc or the obvious. Shortening instead of deleting counts as half-done work.
- Keep only what the code cannot be understood without, as one short phrase.
- XML docs: skill `aspid-mvvm-xmldoc`.

## Generics and inheritance

- Generic classes are never `sealed`: they are extension points.
- `typeparam` is `T`, not `TFrom`, when there is a single parameter.
- Shared bases must not narrow the set of interfaces their subclasses implement.
- Duplicates and unused members left over from a refactor are deleted, not kept "for compatibility".
- Helpers and wrappers (loggers, `*Text`, `*Math`) stay thin: the operation and one message format, no profiler markers, caches or editor machinery. A logger exposes only logging methods; formatting helpers live in a separate type.
- Prefer one attribute with a `params` parameter over a family of marker attributes; the name states a fact (`[UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]`), not an editor behavior.

## Tests

- Assertion messages and comments in tests are in English.
