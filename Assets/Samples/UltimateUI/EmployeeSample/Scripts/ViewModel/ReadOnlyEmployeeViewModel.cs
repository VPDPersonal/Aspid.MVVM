using System;
using UltimateUI.MVVM.ViewModels.Generation;
using Samples.UltimateUI.EmployeeSample.Model;

namespace Samples.UltimateUI.Other.EmployeeSample.Scripts.ViewModel
{
    [ViewModel]
    public partial class ReadOnlyEmployeeViewModel : IDisposable
    {
        [ReadOnlyBind] private readonly string _id;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;
        
        [BindAlso(nameof(FullName))]
        [Bind] private string _family;

        [Bind] private int _level;
        [Bind] private float _salary;
        [Bind] private bool _isPartTimer;
        
        [Bind] private DateTime _birthDay;
        
        [Access(Get = Access.Protected)]
        [Bind] private DateTime? _startWorkDate;

        protected readonly Employee Employee;
        
        public string FullName => $"{Name} {Family}";

        public ReadOnlyEmployeeViewModel(Employee employee)
        {
            Employee = employee;
            
            _id = employee.Id;
            
            var personData = employee.PersonData;
            _name = personData.Name;
            _family = personData.Family;
            _birthDay = personData.BirthDay;

            _level = employee.Level;
            _salary = employee.Salary;
            _isPartTimer = employee.IsPartTime;
            _startWorkDate = employee.StartWorkDate;

            Subscribe();
        }

        private void Subscribe()
        {
            Employee.Hired += OnWorkStatusChanged;
            Employee.Fired += OnWorkStatusChanged;
            
            Employee.LevelUpped += OnLevelUpped;
            Employee.SalaryChanged += OnSalaryChanged;
            Employee.WorkTimeChanged += OnWorkTimeChanged;
        }

        private void Unsubscribe()
        {
            Employee.Hired -= OnWorkStatusChanged;
            Employee.Fired -= OnWorkStatusChanged;
            
            Employee.LevelUpped -= OnLevelUpped;
            Employee.SalaryChanged -= OnSalaryChanged;
            Employee.WorkTimeChanged -= OnWorkTimeChanged;
        }
        
        private void OnLevelUpped() =>
            Level = Employee.Level;
        
        private void OnSalaryChanged() =>
            Salary = Employee.Salary;
        
        private void OnWorkTimeChanged() =>
            IsPartTimer = Employee.IsPartTime;
        
        private void OnWorkStatusChanged() =>
            _startWorkDate = Employee.StartWorkDate;

        partial void OnIsPartTimerChanged(bool newValue) =>
            Employee.IsPartTime = newValue;

        public virtual void Dispose()
        {
            if (Employee == null) return;
            Unsubscribe();
        }
    }
}