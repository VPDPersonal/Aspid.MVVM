# VisualElements Sample

A custom UIToolkit Inspector for an `AbilityConfig` ScriptableObject that showcases the Aspid internal editor VisualElement components and the fluent `VisualElement` extension API.

Look at:

- `Scripts/Editor/AbilityConfigEditor.cs:18` — `AspidInspectorHeader` with a dynamic `StatusStyle` accent (Warning when the ability is free, Success otherwise).
- `Scripts/Editor/AbilityConfigEditor.cs:24` — `AspidBox` container built with fluent `.SetPadding(...)` and chained `.AddChild(...)` calls wrapping `PropertyField` editors.
- `Scripts/Editor/AbilityConfigEditor.cs:33` — conditional `AspidHelpBox` warning appended only when `manaCost == 0`.

To try it:

1. Select `ScriptableObjects/AbilityConfigAsset.asset` in the Project window — the custom inspector appears in the Inspector panel.
2. Edit the fields. Set `Mana Cost` to `0` to see the status accent flip to Warning and a help box appear inline.
3. Or create your own via `Assets > Create > Aspid > FastTools > Samples > Ability Config`.
