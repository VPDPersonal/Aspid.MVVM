using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Unity.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample
{
    [View]
    public partial class ReadOnlyStatsView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _cool;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _power;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _reflexes;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _intelligence;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _technicalAbility;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _skillPointsAvailable;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isDraft;
    }
}
