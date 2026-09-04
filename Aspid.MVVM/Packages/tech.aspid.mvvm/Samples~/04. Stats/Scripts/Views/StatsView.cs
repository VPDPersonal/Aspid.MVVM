using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    [View]
    public sealed partial class StatsView : MonoView
    {
        [Header("Skills")]
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _strength;

        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _agility;

        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _intelligence;

        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _pointsAvailable;

        [Header("Commands")]
        [RequireBinder(typeof(IRelayCommand<Skill>))]
        [SerializeField] private MonoBinder[] _addCommand;

        [RequireBinder(typeof(IRelayCommand<Skill>))]
        [SerializeField] private MonoBinder[] _removeCommand;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _confirmCommand;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _resetCommand;
    }
}
