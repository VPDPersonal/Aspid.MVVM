using UnityEngine;
using System.Drawing;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.ViewModels;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class MonoViewTest1 : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _name;

    [SerializeField] private Binder _bind;

    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder _age;

    [RequireBinder(typeof(Color))]
    [SerializeField] private MonoBinder[] _hairColor;

    [RequireBinder(typeof(IRelayCommand<Color>))]
    [SerializeField] private MonoBinder[] _changeHairColorCommand;
}
