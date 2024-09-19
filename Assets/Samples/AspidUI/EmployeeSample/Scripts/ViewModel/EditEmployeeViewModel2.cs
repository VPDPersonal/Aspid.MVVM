using AspidUI.MVVM.Commands;
using AspidUI.EmployeeSample.Model;
using AspidUI.MVVM.ViewModels.Generation;

namespace AspidUI.EmployeeSample.ViewModel
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