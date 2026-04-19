using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    // Demonstrates two complementary ways to pick a System.Type from the Inspector:
    //
    //   1. SerializableType<T> — strongly typed wrapper, constrained via generic parameter.
    //                             Resolution is cached; implicit conversion to Type? is free.
    //   2. [TypeSelector] + string — annotate a raw string field to get the same picker window.
    //                                Useful when you want multiple base constraints
    //                                or want to opt out of the wrapper allocation.
    //
    // Both forms store the assembly-qualified name and resolve lazily at first access.
    public sealed class AbilitySelector : MonoBehaviour
    {
        // Picker is constrained to Ability subtypes by the generic argument.
        // Concrete types (Fireball, Dash, Heal) show up; the abstract base does not.
        [SerializeField] private SerializableType<Ability> _abilityType;

        // Array field + attribute: each element is its own picker constrained to AbilityModifier.
        // AllowAbstractTypes = false hides the base class from the dropdown (the default,
        // shown here for clarity).
        [TypeSelector(typeof(AbilityModifier), AllowAbstractTypes = false)]
        [SerializeField] private string[] _modifierTypes;

        private void Start()
        {
            // .Type performs the lazy GetType() lookup and caches the result.
            // Returns null if the stored assembly-qualified name no longer resolves
            // (e.g., the type was renamed).
            var abilityType = _abilityType.Type;
            if (abilityType is null) return;

            // Implicit conversion is guaranteed safe: the picker only offers Ability subtypes.
            var ability = (Ability)gameObject.AddComponent(abilityType);
            ability.Activate();

            // Raw-string form: resolve manually via Type.GetType.
            // Skip silently on unresolved names to keep the sample self-contained —
            // production code should log or surface the missing type.
            foreach (var qualifiedName in _modifierTypes)
            {
                var modifierType = Type.GetType(qualifiedName);
                if (modifierType is null) continue;

                // Modifiers are plain C# classes (not components) — Activator.CreateInstance
                // is appropriate here; AddComponent would fail for non-UnityEngine.Object types.
                var modifier = (AbilityModifier)Activator.CreateInstance(modifierType);
                modifier.Apply();
            }
        }
    }
}
