using UnityEngine;
using UltimateUI.MVVM.Views.Generation;
using UltimateUI.MVVM.StarterKit.Binders.Commands;

[View]
public sealed partial class EditEmployeeView1 : ReadOnlyEmployeeView1
{
    [SerializeField] private ButtonCommandProvider[] _fireCommand;
    [SerializeField] private ButtonCommandProvider[] _levelUpCommand;
}