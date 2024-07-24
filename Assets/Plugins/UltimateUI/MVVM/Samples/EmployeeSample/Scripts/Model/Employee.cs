// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.EmployeeSample.Model
{
    public struct Employee
    {
        public string Name;
        public string Email;
        public bool IsPartTimer;
    
        public readonly string Id;

        public Employee(string id, string name, string email, bool isPartTimer)
        {
            Id = id;
            Name = name;
            Email = email;
            IsPartTimer = isPartTimer;
        }
    }
}
