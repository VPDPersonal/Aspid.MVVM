# Пример EnumValues

Маленькая система боевого урона, которая сопоставляет члены enum типизированным значениям через `EnumValues<TValue>`. `DamageDealer` выбирает `DamageType` и `StatusEffect` в Inspector, а по нажатию `Space` наносит урон — извлекая множитель урона, цвет лога и модификатор скорости из трёх полей `EnumValues`.

Смотрите:
- `Scripts/DamageDealer.cs:9` — `EnumValues<float>`, сопоставляющий `DamageType` множителю урона.
- `Scripts/DamageDealer.cs:10` — `EnumValues<Color>`, сопоставляющий `DamageType` цвету отладочного лога.
- `Scripts/DamageDealer.cs:14` — `EnumValues<float>` по `[Flags]` enum `StatusEffect`; композитные записи вроде `Burning | Slowed` должны идти до одиночных флагов (см. комментарий рядом).
- `Scripts/DamageDealer.cs:30` — вызов `GetValue` на Flags-поле.
- `Scripts/StatusEffect.cs` — `[Flags]` enum, используемый в третьем сопоставлении.

## Как запустить

Откройте `Scenes/EnumValues.unity` и войдите в Play Mode. В сцене есть `DamageDealer`, подключённый из `Prefabs/EnumValues.prefab`, который предзаполнен:

- `_damageMultipliers`: `Physical = 1.0`, `Fire = 1.5`, `Ice = 0.8`, `Poison = 0.6`.
- `_damageColors`: серый / оранжевый / голубой / ядовито-зелёный по `DamageType`.
- `_speedMultipliersByStatus`: `Burning | Slowed = 0.4` **первой**, затем `Burning = 1.0`, `Frozen = 0.2`, `Slowed = 0.5` — порядок с композитом впереди именно и гарантирует, что комбинация флагов разрешается в `0.4`, а не проваливается на первое совпадение одиночного флага.
- `_currentDamageType = Fire`, `_activeEffects = Burning | Slowed`, `_baseDamage = 10`.

Нажмите `Space` — в Console появится оранжевое `Fire hit: 15 dmg (speed mod: 0.40)`. Меняйте значения enum в Inspector (или переключайте флаги в `_activeEffects`), чтобы увидеть другие варианты поиска.
