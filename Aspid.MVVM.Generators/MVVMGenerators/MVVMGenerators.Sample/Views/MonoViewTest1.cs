using UnityEngine;
using AspidUI.MVVM;
using System.Drawing;
using AspidUI.MVVM.Unity;
using AspidUI.MVVM.Commands;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Views.Generation;

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
