using Aspid.MVVM;
using UnityEngine;
using System.Drawing;
using Aspid.MVVM.Unity;
using System.Collections.Generic;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class MonoViewTest1 : MonoView
{
    [SerializeField] private Binder _binder1;
    [SerializeField] private Binder _binder2;
    
    [BindId("Binder2")]
    [SerializeField] private Binder _binder3;
    
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _monoBinder1;
    
    [SerializeField] private MonoBinder _monoBinder2;

    [RequireBinder(typeof(Color))]
    [SerializeField] private MonoBinder[] _arrayMonoBinder1;

    [RequireBinder(typeof(IRelayCommand<Color>))]
    [SerializeField] private MonoBinder[] _arrayMonoBinder2;
    
    // TODO Fix
    // Don't work
    [SerializeField] private List<MonoBinder> _listMonoBinder1;
}
