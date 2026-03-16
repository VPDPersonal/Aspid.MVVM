# Binder Examples (`<example>` XML)

Pure C# binder classes (non-`MonoBehaviour`) must include an `<example>` block in a dedicated XML file.

---

## File Layout

One `XmlExampleDoc-[Category]-[SubCategory]-1.1.0.xml` per folder, shared by all binders in that folder:

```
AudioSources/Volume/
    AudioSourceVolumeBinder.cs
    XmlExampleDoc-AudioSource-Volume-1.1.0.xml
```

---

## Referencing from C# (`<include>`)

Place after `<remarks>`, before the class declaration:

```csharp
/// <include file="XmlExampleDoc-AudioSource-Volume-1.1.0.xml" path="doc//member[@name='AudioSourceVolumeBinder']/*" />
public class AudioSourceVolumeBinder ...
```

For generic classes, use `{N}` for the type parameter count:

```csharp
/// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneWayBinder{1}']/*" />
```

---

## XML File Structure

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

**Encoding:** blank lines → `&#xD;`, `=>` → `=&gt;`, generics → `&lt;` / `&gt;`. Indentation: 16 spaces for class body, 20 for members.

---

## Content Rules by Binder Type

| Type | Blocks | Notes |
|---|---|---|
| **Target-based** (e.g. `AudioSourceVolumeBinder`) | 2 | Serialized-field form first, constructor form second. Second description: `"Bind ... by constructing the binder in code."` |
| **Switcher** (e.g. `AudioSourceVolumeSwitcherBinder`) | 2 | Same structure as target-based. Description begins: `"Switch the [X] between two values"`. |
| **Closure-based** (e.g. `Generic*Binder`) | 1 | Property named after VM field. Single-arg: `=&gt; new(...)`. Multi-arg: `= new(\n    ...\n)`. |
| **Serialized field** (e.g. `*Value`) | 1 | Field declared with `= new()`. Use `OnInitializedInternal` / `OnDeinitializingInternal`, not `OnEnable` / `OnDisable`. |

**General rules:**
- Description text before `<code>`, never inside it.
- Example class: always `[View] public partial class ExampleView`.
- Always end with a `[ViewModel]` block showing the `[Bind]` field.
- Use `&#xD;` before `[ViewModel]` and between View sections.
