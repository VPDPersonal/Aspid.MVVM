using UnityEngine;
using System.Drawing;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Extensions;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Views.Generation;

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
