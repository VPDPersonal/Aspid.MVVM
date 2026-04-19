# EnumValues Sample

A tiny combat damage system that maps enum members to typed values through `EnumValues<TValue>`. `DamageDealer` picks a `DamageType` and `StatusEffect` in the Inspector, then on `Space` applies damage — pulling the damage multiplier, log color, and speed modifier from three `EnumValues` fields.

Look at:
- `Scripts/DamageDealer.cs:9` — `EnumValues<float>` mapping `DamageType` to damage multiplier.
- `Scripts/DamageDealer.cs:10` — `EnumValues<Color>` mapping `DamageType` to debug log color.
- `Scripts/DamageDealer.cs:14` — `EnumValues<float>` keyed on `[Flags]` enum `StatusEffect`; composite entries like `Burning | Slowed` must come before single-flag entries (see the inline comment).
- `Scripts/DamageDealer.cs:30` — `GetValue` call on the Flags-keyed field.
- `Scripts/StatusEffect.cs` — `[Flags]` enum used by the third mapping.

## How to run

Open `Scenes/EnumValues.unity` and enter Play Mode. The scene hosts a `DamageDealer` wired up from `Prefabs/EnumValues.prefab`, which is pre-seeded with:

- `_damageMultipliers`: `Physical = 1.0`, `Fire = 1.5`, `Ice = 0.8`, `Poison = 0.6`.
- `_damageColors`: grey / orange / cyan / acid-green per `DamageType`.
- `_speedMultipliersByStatus`: `Burning | Slowed = 0.4` **first**, then `Burning = 1.0`, `Frozen = 0.2`, `Slowed = 0.5` — composite-first ordering is what makes the combined flag resolve to `0.4` instead of falling through to the first single-flag match.
- `_currentDamageType = Fire`, `_activeEffects = Burning | Slowed`, `_baseDamage = 10`.

Press `Space` and the Console prints `Fire hit: 15 dmg (speed mod: 0.40)` in orange. Change the enums in the Inspector (or toggle flags in `_activeEffects`) to see different lookups.
