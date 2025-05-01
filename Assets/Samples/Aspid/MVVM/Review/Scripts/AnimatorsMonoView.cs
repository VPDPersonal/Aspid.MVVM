using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.Review
{
    [View]
    public partial class AnimatorsMonoView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _intParameter;
        
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _boolParameter;
        
        [RequireBinder(typeof(float))]
        [SerializeField] private MonoBinder[] _floatParameter;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _command;
        
        [RequireBinder(typeof(IRelayCommand<int>))]
        [SerializeField] private MonoBinder[] _intCommand;
        
        [RequireBinder(typeof(IRelayCommand<bool>))]
        [SerializeField] private MonoBinder[] _boolCommand;
        
        [RequireBinder(typeof(IRelayCommand<float>))]
        [SerializeField] private MonoBinder[] _floatCommand;
        
        // Alternative:
        // [RequireBinder(typeof(int))]
        // [SerializeField] private MonoBinder _intParameter;
        // 
        // [RequireBinder(typeof(bool))]
        // [SerializeField] private MonoBinder _boolParameter;
        // 
        // [RequireBinder(typeof(float))]
        // [SerializeField] private MonoBinder _floatParameter;
        //
        // [RequireBinder(typeof(IRelayCommand))]
        // [SerializeField] private MonoBinder _command;
        // 
        // [RequireBinder(typeof(IRelayCommand<int>))]
        // [SerializeField] private MonoBinder _intCommand;
        // 
        // [RequireBinder(typeof(IRelayCommand<bool>))]
        // [SerializeField] private MonoBinder _boolCommand;
        // 
        // [RequireBinder(typeof(IRelayCommand<float>))]
        // [SerializeField] private MonoBinder _floatCommand;
    }
}
