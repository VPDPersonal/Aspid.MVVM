using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.Review
{
    [ViewModel]
    public partial class AnimatorMonoViewModel : MonoViewModel
    {
        [OneWayBind]
        [SerializeField] private int _intParameter;
        
        [OneWayBind]
        [SerializeField] private bool _boolParameter;
        
        [OneWayBind]
        [SerializeField] private float _floatParameter;
    }
}