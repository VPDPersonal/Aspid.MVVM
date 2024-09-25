using System;

namespace Aspid.UI.EmployeeSample.Model
{
    public struct PersonData
    {
        public string Name;
        public string Family;
        public readonly DateTime BirthDay;

        public PersonData(string name, string family, DateTime birthDay)
        {
            Name = name;
            Family = family;
            BirthDay = birthDay;
        }
    }
}