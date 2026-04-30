using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Greeter
{
    [ViewModel]
    public sealed partial class GreeterMonoViewModel : MonoViewModel
    {
        // Когда InputField изменится, Name обновится автоматически.
        [Bind] 
        [SerializeField] private string _name;
        
        [Bind]
        [SerializeField] private string _greeting;
        
        private void Start() =>
            OnNameChanged(Name);
        
        [RelayCommand]
        private void Clear() =>
            Name = string.Empty;

        // Partial-метод, который Source Generator вызывает при каждом изменении Name.
        // Имя формируется по правилу: On + {PropertyName} + Changed.
        partial void OnNameChanged(string newValue) =>
            Greeting = string.IsNullOrEmpty(newValue)
                ? string.Empty
                : $"Hi, {newValue}";
    }
}