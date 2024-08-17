using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Views.Generation;

[View]
public partial class EmployeeView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _id;
    
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _name;
    
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _email;
    
    [RequireBinder(typeof(bool))]
    [SerializeField] private MonoBinder _isPartTimer;
}
