using UnityEngine;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

namespace Aspid.UI.EmployeeSample.View
{
    [View]
    public sealed partial class EditEmployeeView1 : ReadOnlyEmployeeView1
    {
        [SerializeField] private ButtonCommandProvider[] _fireCommand;
        [SerializeField] private ButtonCommandProvider[] _levelUpCommand;
    }
}