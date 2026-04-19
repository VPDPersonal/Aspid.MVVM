using UnityEngine;
using Aspid.FastTools.Enums;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.EnumValues
{
    public sealed class DamageDealer : MonoBehaviour
    {
        [SerializeField] private EnumValues<float> _damageMultipliers;
        [SerializeField] private EnumValues<Color> _damageColors;

        // Flag combinations (e.g. Burning | Slowed) match via HasFlag and first-hit wins, so list
        // composite entries BEFORE their constituent flags — otherwise the single-flag entry matches first.
        [SerializeField] private EnumValues<float> _speedMultipliersByStatus;

        [SerializeField] private DamageType _currentDamageType = DamageType.Physical;
        [SerializeField] private StatusEffect _activeEffects = StatusEffect.None;
        [SerializeField] private float _baseDamage = 10f;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            DealDamage();
        }

        private void DealDamage()
        {
            var multiplier = _damageMultipliers.GetValue(_currentDamageType);
            var color = _damageColors.GetValue(_currentDamageType);
            var speedMod = _speedMultipliersByStatus.GetValue(_activeEffects);
            var finalDamage = _baseDamage * multiplier;
            var colorHex = ColorUtility.ToHtmlStringRGB(color);

            Debug.Log($"<color=#{colorHex}>{_currentDamageType} hit: {finalDamage} dmg (speed mod: {speedMod:F2})</color>");
        }
    }
}
