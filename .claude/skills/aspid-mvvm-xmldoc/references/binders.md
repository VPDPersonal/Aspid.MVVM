# Binder `<example>` Blocks — `XmlExampleDoc-*.xml`

Pure C# binder classes (non-`MonoBehaviour`) must include an `<example>` block showing how to wire them up in a `[View]`/`[ViewModel]` pair. These examples live in a dedicated XML file next to the binder source and are pulled into the doc via `<include>`.

**Exception:** `MonoBinder` classes (MonoBehaviour-based binders) do NOT need `<include>` or XML example blocks. They're configured via the Inspector and their docs stay inline.

The reason for the external XML file is that example code with angle brackets, arrows, and multi-line blocks is painful to keep readable inside `///` comments. Putting them in a sibling XML file keeps the example rendered correctly in IDE tooltips and lets multiple binders in a folder share the same example file.

---

## File layout

One `XmlExampleDoc-[Category]-[SubCategory]-1.1.0.xml` per folder, shared across every binder in that folder:

```
AudioSources/Volume/
    AudioSourceVolumeBinder.cs
    XmlExampleDoc-AudioSource-Volume-1.1.0.xml
```

---

## Referencing from C# (`<include>`)

Place the `<include>` after `<remarks>`, immediately before the class declaration:

```csharp
/// <include file="XmlExampleDoc-AudioSource-Volume-1.1.0.xml" path="doc//member[@name='AudioSourceVolumeBinder']/*" />
public class AudioSourceVolumeBinder ...
```

For generic classes, use `{N}` for the type parameter count — the XML doc ID format expects arity, not names:

```csharp
/// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneWayBinder{1}']/*" />
```

---

## XML file structure

```xml
<?xml version="1.0" encoding="utf-8"?>
<doc>
    <member name="AudioSourceVolumeBinder">
        <example>
            Bind the AudioSource volume property using a serialized binder field — the target is assigned in the Inspector.
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
        </example>
    </member>
</doc>
```

### Encoding rules

- Blank lines inside `<code>` → `&#xD;`.
- `=>` (lambda) → `=&gt;`.
- Generics `<T>` → `&lt;T&gt;`.
- Indentation: 16 spaces for class body, 20 spaces for class members.

---

## Content rules by binder type

| Type | Blocks | Notes |
|---|---|---|
| **Target-based** (e.g. `AudioSourceVolumeBinder`) | 2 | Serialized-field form first, constructor form second. Second block's description: `"Bind ... by constructing the binder in code."` |
| **Switcher** (e.g. `AudioSourceVolumeSwitcherBinder`) | 2 | Same structure as target-based. Description begins: `"Switch the [X] between two values"`. |
| **Closure-based** (e.g. `Generic*Binder`) | 1 | Property named after the VM field. Single-arg form: `=&gt; new(...)`. Multi-arg form: `= new(\n    ...\n)`. |
| **Serialized field** (e.g. `*Value`) | 1 | Field declared with `= new()`. Use `OnInitializedInternal` / `OnDeinitializingInternal` (not `OnEnable` / `OnDisable`). |

---

## General rules

- Description text lives **before** `<code>`, never inside it.
- The example class is always `[View] public partial class ExampleView`.
- Always end with a `[ViewModel]` block showing the matching `[Bind]` field.
- Use `&#xD;` as a separator before the `[ViewModel]` block and between multiple `[View]` sections in the same `<code>` block.
