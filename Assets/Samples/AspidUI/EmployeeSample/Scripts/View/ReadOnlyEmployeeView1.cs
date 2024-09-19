using System;
using UnityEngine;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Views.Generation;
using MonoBinder = AspidUI.MVVM.Unity.MonoBinder;

namespace AspidUI.EmployeeSample.View
{
    [View]
    public partial class ReadOnlyEmployeeView1 : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _id;

        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _fullName;

        [RequireBinder(typeof(DateTime))]
        [SerializeField] private MonoBinder[] _birthDate;

        [RequireBinder(typeof(DateTime))]
        [SerializeField] private MonoBinder[] _startDate;

        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _level;

        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _salary;

        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _isPartTime;
    }
}