using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Data
{
    [CreateAssetMenu(fileName = "New Creature Description Data", menuName = "UltimateUI/Samples/Heroes/Heroes/Creature Description Data", order = 0)]
    public sealed class DescriptionData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite RaceIcon { get; private set; }
        
        [field: Multiline]
        [field: SerializeField]
        public string Description { get; private set; }
    }
}