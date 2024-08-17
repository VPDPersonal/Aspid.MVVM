using UltimateUI.MVVM.ViewModels.Generation;

namespace Samples.UltimateUI.Other.EmployeeSample.Scripts.ViewModel
{
    [ViewModel]
    public partial class EmployeeViewModel
    {
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _family;
        
        public string FullName => $"{Name} {Family}";
    }
}