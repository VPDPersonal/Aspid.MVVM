using UltimateUI.MVVM.ViewModels.Generation;
using Samples.UltimateUI.EmployeeSample.Model;

namespace Samples.UltimateUI.Other.EmployeeSample.Scripts.ViewModel
{
    [ViewModel]
    public sealed partial class EditEmployeeViewModel1 : ReadOnlyEmployeeViewModel
    {
        public EditEmployeeViewModel1(Employee employee) : base(employee) { }

        [RelayCommand(CanExecute = nameof(CanHire))]
        private void Hire() => Employee.Hire();

        [RelayCommand(CanExecute = nameof(CanFire))]
        private void Fire() => Employee.Hire();
        
        private bool CanHire() => Employee.StartWorkDate == null;

        private bool CanFire() => Employee.StartWorkDate != null;
    }
}