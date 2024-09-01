using Samples.UltimateUI.EmployeeSample.Model;
using Samples.UltimateUI.Other.EmployeeSample.Scripts.ViewModel;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels.Generation;

namespace Samples.UltimateUI.EmployeeSample.Scripts.ViewModel
{
    [ViewModel]
    public sealed partial class EditEmployeeViewModel2 : ReadOnlyEmployeeViewModel
    {
        [ReadOnlyBind] private readonly IRelayCommand _hireCommand;
        [ReadOnlyBind] private readonly IRelayCommand _fireCommand;

        public EditEmployeeViewModel2(Employee employee) : base(employee)
        {
            _hireCommand = new RelayCommand(Hire, CanHire);
            _fireCommand = new RelayCommand(Fire, CanFire);
        }
        
        private void Hire() => Employee.Hire();

        private void Fire() => Employee.Hire();
        
        private bool CanHire() => StartWorkDate == null;

        private bool CanFire() => StartWorkDate != null;
    }
}