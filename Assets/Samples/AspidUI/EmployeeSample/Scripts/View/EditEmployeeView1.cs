using UnityEngine;
using AspidUI.MVVM.Views.Generation;
using AspidUI.MVVM.StarterKit.Binders.Commands;

namespace AspidUI.EmployeeSample.View
{
    [View]
    public sealed partial class EditEmployeeView1 : ReadOnlyEmployeeView1
    {
        [SerializeField] private ButtonCommandProvider[] _fireCommand;
        [SerializeField] private ButtonCommandProvider[] _levelUpCommand;
    }
}