using System;
using UnityEngine;

namespace Samples.UltimateUI.EmployeeSample.Model
{
    [CreateAssetMenu(fileName = "Employee Data", menuName = "Samples/UltimateUI/Other/Employee Data")]
    public sealed class EmployeeStaticData : ScriptableObject
    {
        [SerializeField] private string _id;
        
        [Header("Name")]
        [SerializeField] private string _name;
        [SerializeField] private string _family;

        [Header("Birth Day")]
        [SerializeField] private int _birthDay;
        [SerializeField] private int _monthData;
        [SerializeField] private int _yearData;

        [Header("Work")]
        [SerializeField] private int _salary;
        [SerializeField] private bool _isPartTime;
        
        public string Id => _id;

        public PersonData PersonData => new(_name, _family, new DateTime(_yearData, _monthData, _birthDay));
        
        public int Salary => _salary;
        
        public bool IsPartTime => _isPartTime;
    }
}