using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Greeter
{
    // MonoViewModel is a ViewModel that lives on a GameObject, so it can be assigned in the Inspector.
    [ViewModel]
    public sealed partial class GreeterMonoViewModel : MonoViewModel
    {
        // TwoWay: the input field writes back into Name.
        // BindAlso re-sends Greeting whenever Name changes.
        [BindAlso(nameof(Greeting))]
        [TwoWayBind]
        [SerializeField] private string _name;

        // BindAlso alone makes this computed property a bindable member; no attribute is needed here.
        private string Greeting =>
            string.IsNullOrEmpty(Name)
                ? string.Empty
                : $"Hi, {Name}!";

        [RelayCommand]
        private void Clear() =>
            Name = string.Empty;
    }
}
