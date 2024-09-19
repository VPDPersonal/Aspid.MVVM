using UnityEngine;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Views.Generation;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.StatsSample.Views
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
