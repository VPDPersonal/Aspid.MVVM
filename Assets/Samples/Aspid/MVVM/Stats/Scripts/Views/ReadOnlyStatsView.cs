using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.Stats.ViewModels;

namespace Aspid.MVVM.Stats.Views
{
    [View]
    public partial class ReadOnlyStatsView : MonoView, IView<IReadOnlyStatsViewModel>
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
