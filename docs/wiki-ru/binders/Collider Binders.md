---
title: Binder-ы коллайдеров
type: binder-category
status: active
source_paths:
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/ColliderIsTriggerBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/Mono/ColliderIsTriggerMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/IsTrigger/Mono/ColliderIsTriggerEnumMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/Material/ColliderMaterialBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/Colliders/OneWayToSource/Mono/ColliderToSourceMonoBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/BoxColliders/Center/BoxColliderCenterBinder.cs
  - Aspid.MVVM/Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/BoxColliders/Center/BoxColliderCenterSwitcherBinder.cs
tags: [binder, starterkit, unity, physics, collider]
updated_at: 2026-05-31
lang: ru
translated_from: docs/wiki/binders/Collider Binders.md
translated_at: 2026-05-31
---

# Binder-ы коллайдеров

> Binder-ы из StarterKit, которые управляют свойствами Unity 3D `Collider` (и формовых коллайдеров вроде `BoxCollider`) на основе значений из ViewModel — переключают триггеры, заменяют физические материалы, изменяют размеры границ, и всё это без написанного вручную связующего кода.

## Семейство

| Папка | Цель | Привязываемое свойство |
|--------|--------|----------------|
| `Colliders/Enabled` | `Collider` | `enabled` (bool) |
| `Colliders/IsTrigger` | `Collider` | `isTrigger` (bool) |
| `Colliders/ProvidesContacts` | `Collider` | `providesContacts` (bool) |
| `Colliders/Material` | `Collider` | `material` (`PhysicsMaterial`) |
| `Colliders/OneWayToSource` | `Collider` | сама ссылка на компонент |
| `BoxColliders/Center`, `Size` | `BoxCollider` | `center`, `size` (Vector3) |
| `CapsuleColliders`, `SphereColliders`, `MeshColliders` | формовые коллайдеры | поля, специфичные для формы |

## Оси вариаций

Каждая папка свойства повторяет одну и ту же структуру StarterKit, поэтому, изучив одну, вы поймёте все:

- **Plain (`[Serializable]`)** — например, `ColliderIsTriggerBinder : TargetBoolBinder<Collider>`. Создаётся в коде, хранит явный `Target`, переопределяет get/set свойства `Property`. См. [[Binder Base Classes]].
- **MonoBinder** — например, `ColliderIsTriggerMonoBinder : ComponentBoolMonoBinder<Collider>`. MonoBehaviour, добавляемый на GameObject; разрешает свою цель через `CachedComponent`. Несёт `[AddComponentMenu]` + `[AddBinderContextMenu]` для настройки в инспекторе. См. [[Mono Binders]].
- **Switcher** — `...SwitcherBinder` / `...SwitcherMonoBinder` переключаются между двумя заранее заданными значениями (например, двумя размерами `Vector3`), управляемые *bool*-значением из VM.
- **Enum / EnumGroup** — `...EnumMonoBinder` сопоставляет вариант enum-а со значением; `...EnumGroupMonoBinder` выбирает из группы. Булевы цели используют `EnumMonoBinder<Collider, bool>`.
- **OneWayToSource** — `ColliderToSourceMonoBinder : ComponentToSourceMonoBinder<Collider>` проталкивает закэшированный `Collider` *наверх* в ViewModel при установке привязки (см. `OneWayToSource` в [[BindMode]]).

## Примечательное поведение

- **TwoWay отклоняется.** Большинство сеттеров вызывают `mode.ThrowExceptionIfMatches(BindMode.TwoWay)` в своём конструкторе — эти свойства являются приёмниками только для записи, а не редактируемыми источниками. Bool-MonoBinder-ы *поддерживают* `OneWayToSource`, отражая текущее значение обратно при привязке.
- **Material учитывает версию.** `ColliderMaterialBinder` использует `PhysicsMaterial` (Unity 2023.1+) либо более старый `PhysicMaterial`, с соответствующим типом конвертера для каждой ветки.
- Эти binder-ы привязывают конкретные свойства Unity, поэтому со стороны ViewModel это просто обычный [[Bindable Members|привязываемый член]]; здесь нет вывода генератора — генерация происходит на [[ViewModel]], а не на binder-е.

## Исходный код

`Assets/Aspid/MVVM/StarterKit/Unity/Runtime/Binders/Colliders/`. Базовые классы: [[Binder Base Classes]], [[Mono Binders]]. Каталог: [[Binders Catalog]]. Концепции: [[Data Binding]], [[IBinder]].
