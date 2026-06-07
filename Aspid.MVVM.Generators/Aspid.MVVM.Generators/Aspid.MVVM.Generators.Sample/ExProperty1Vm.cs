namespace Aspid.MVVM.Generators.Sample;

[ViewModel]
public partial class ExProperty1Vm
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    [Bind]
    public string FirstName
    {
        get => _firstName;
        private set
        {
            if (SetFirstNameField(ref _firstName, value))
                OnFullNamePropertyChanged();
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
            OnLastNamePropertyChanged();
        }
    }

    [Bind]
    public string FullName => $"{FirstName} {LastName}";
}
