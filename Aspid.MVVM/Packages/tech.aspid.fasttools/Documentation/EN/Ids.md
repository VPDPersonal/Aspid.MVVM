# ID System

Reference for the ID System internals: generator diagnostics and the `IdRegistry` runtime API. For the setup walkthrough with examples, see the [README](README.md#id-system-beta).

> **Beta:** the ID System is currently in beta. The public API, generated code layout and editor workflow may change in future releases.

## Generator diagnostics

The generator reports `AFID001` if the struct is missing `partial`, and `AFID002` if your code already declares `_id`, `Id`, or `__stringId` (the generator skips emission so you get a clear error pointing at the struct rather than a CS compile error inside generated source). Generic targets (`EnemyId<T>`) and generic containing types are supported.

## IdRegistry

`ScriptableObject` in `Aspid.FastTools.Ids` that stores `(int, string)` entries and keeps the lookup tables available at runtime. Each name is assigned a stable, auto-incrementing ID that never changes when other entries are added or removed.

| Member | Description |
|--------|-------------|
| `bool TryGetId(string name, out int id)` | Returns `true` and the ID when found; otherwise `false` |
| `bool TryGetName(int id, out string name)` | Returns `true` and the name when found; otherwise `false` and `string.Empty` |
| `bool Contains(int id)` | Whether an ID is registered |
| `bool Contains(string name)` | Whether a name is registered |
| `int Count` | Number of entries |
| `IReadOnlyList<int> Ids` · `IReadOnlyList<string> IdNames` | Registered IDs / names, in registration order |
| `IEnumerator<KeyValuePair<int, string>> GetEnumerator()` | Iterate `(id, name)` pairs |

The registry derives from `ScriptableObject` directly and exposes a generic counterpart `IdRegistry<T>` (with `T : struct, IId`) that adds typed `Contains(T)` and `TryGetName(T, out string)` overloads. Edits — adding, renaming, removing entries — happen through the registry inspector and `RegistryEditorCore`, not via a public runtime API.
