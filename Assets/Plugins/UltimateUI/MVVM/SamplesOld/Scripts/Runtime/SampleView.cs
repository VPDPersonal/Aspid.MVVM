using UnityEngine;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public partial class SampleView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _age;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _icon;
    }
    
    public partial class SampleView
    {
        public override IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders() => new Dictionary<string, IReadOnlyList<IBinder>>
        {
            { nameof(_age), _age },
            { nameof(_name), _name },
            { nameof(_icon), _icon }
        };
    }
}