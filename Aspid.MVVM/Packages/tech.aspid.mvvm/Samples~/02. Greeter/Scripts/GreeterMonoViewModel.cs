using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Greeter
{
    [ViewModel]
    public sealed partial class GreeterMonoViewModel : MonoViewModel
    {
        [Bind] 
        [SerializeField] private string _name;
        
        [Bind]
        [SerializeField] private string _greeting;
        
        private void Start() =>
            OnNameChanged(Name);
        
        [RelayCommand]
        private void Clear() =>
            Name = string.Empty;

        // Partial method the Source Generator calls whenever Name changes; named On + {PropertyName} + Changed.
        partial void OnNameChanged(string newValue) =>
            Greeting = string.IsNullOrEmpty(newValue)
                ? string.Empty
                : $"Hi, {newValue}";
    }
}