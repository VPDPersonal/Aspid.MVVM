using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Views.Generation;

namespace UltimateUI.Samples.StatsSample.Views
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
    }
}
