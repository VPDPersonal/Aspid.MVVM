using System;
using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;

namespace Aspid.UI.EmployeeSample.View
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