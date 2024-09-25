using System;
using UnityEngine;

namespace Aspid.UI.EmployeeSample.Model
{
    public sealed class Employee
    {
        public event Action Hired;
        public event Action Fired;
        public event Action LevelUpped;
        public event Action SalaryChanged;
        public event Action WorkTimeChanged;
        
        public const int MaxLevel = 10;
        
        public readonly string Id;
        public readonly PersonData PersonData;

        private float _salary;
        private bool _isPartTime;
        
        public int Level { get; private set; }

        public float Salary
        {
            get => _salary;
            private set
            {
                if (Mathf.Approximately(_salary, value)) return;

                _salary = value;
                SalaryChanged?.Invoke();
            }
        }

        public bool IsPartTime
        {
            get => _isPartTime;
            set
            {
                if (_isPartTime == value) return;
                
                _isPartTime = value;
                Salary = value ? Salary * 1.5f : Salary / 1.5f;
                
                WorkTimeChanged?.Invoke();
            }
        }
        
        public DateTime? StartWorkDate { get; private set; }

        public Employee(EmployeeStaticData data)
        {
            Id = data.Id;
            PersonData = data.PersonData;
            
            Level = 1;
            StartWorkDate = null;
            
            _salary = data.Salary;
            _isPartTime = data.IsPartTime;
        }
        
        public void Hire()
        {
            if (StartWorkDate != null) return;
            
            StartWorkDate = DateTime.Now;
            Hired?.Invoke();
        }

        public void Fire()
        {
            if (StartWorkDate == null) return;
            
            StartWorkDate = null;
            Fired?.Invoke();
        }
        
        public void LevelUp()
        {
            if (Level >= MaxLevel) return;
            
            Level++;
            Salary += (int)(Salary * 0.3f);
            LevelUpped?.Invoke();
        }
    }
}
