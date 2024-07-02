using UltimateUI.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace MVVMGenerators.Sample;

[ViewModel]
public partial class MyViewModel
{
    [Bind] private int _age;
    [Bind] private string? _name;
    [Bind] private ObservableCollection<string>? _collection;

    partial void OnNameChanged(string? newValue)
    {
    }

    partial void OnNameChanging(string? oldValue, string? newValue)
    {
    }
}