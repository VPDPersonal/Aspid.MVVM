namespace Aspid.MVVM.Generators.Sample;

[ViewModel]
public partial class ExProperty2Vm
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    [Bind]
    public string FirstName
    {
        get => _firstName;
        private set
        {
            if (SetField(ref _firstName, value))
                OnPropertyChanged(nameof(FullName));
        }
    }

    [Bind]
    [BindAlso(nameof(FullName))]
    public string LastName
    {
        get => _lastName;
        private set
        {
            if (_lastName == value) return;
            
            _lastName = value;
            OnPropertyChanged();
        }
    }
    
    [Bind]
    public string FullName => $"{_firstName} {_lastName}";
}
