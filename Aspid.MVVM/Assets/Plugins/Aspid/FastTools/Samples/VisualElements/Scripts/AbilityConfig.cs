using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.VisualElements
{
    [CreateAssetMenu(fileName = "New Ability Config", menuName = "Aspid/Samples/FastTools/Ability Config")]
    public sealed class AbilityConfig : ScriptableObject
    {
        [SerializeField] private string _abilityName = "New Ability";
        [SerializeField] [TextArea] private string _description;
        
        [SerializeField] [Min(0)] private int _manaCost = 10;
        [SerializeField] [Min(0f)] private float _cooldown = 1f;

        public int ManaCost => _manaCost;
        
        public float Cooldown => _cooldown;
        
        public string AbilityName => _abilityName;
        
        public string Description => _description;
    }
}
