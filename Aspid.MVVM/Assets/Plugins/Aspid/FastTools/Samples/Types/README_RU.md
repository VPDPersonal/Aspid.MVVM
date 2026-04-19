# Пример Types

Маленькая система способностей, демонстрирующая полиморфный выбор типа в Unity Inspector с помощью `SerializableType<T>`, `TypeSelectorAttribute` и `ComponentTypeSelector`. Игрок выбирает наследника `Ability` и список наследников `AbilityModifier`; для врагов используется `ComponentTypeSelector`, чтобы конкретный скрипт врага можно было заменить «на лету» из Inspector.

Смотрите:

- `Scripts/AbilitySelector.cs:21` — поле `SerializableType<Ability>`, ограниченный выбор одного подтипа.
- `Scripts/AbilitySelector.cs:26` — `[TypeSelector(typeof(AbilityModifier), AllowAbstractTypes = false)]` на поле `string[]`.
- `Scripts/Enemies/EnemyBase.cs:18` — объявление `ComponentTypeSelector`, заменяющее прикреплённый скрипт по месту.

## Как запустить

Откройте `Scenes/Types.unity` — в сцене два prefab-инстанса:

- **Types** (`Prefabs/Types.prefab`) — `AbilitySelector` с предвыбранной способностью `Heal` и тремя заполненными модификаторами. Войдите в Play Mode, чтобы увидеть в Console лог активированной способности и каждого применённого модификатора.
- **Enemy** (`Prefabs/Enemy.prefab`) — `FastEnemy`, подключённый через `ComponentTypeSelector`. Выделите его в Hierarchy и используйте выпадающий список выбора типа в верхней части Inspector, чтобы переключиться между `FastEnemy` и `TankEnemy` по месту; значение `Health` сохраняется при замене.
