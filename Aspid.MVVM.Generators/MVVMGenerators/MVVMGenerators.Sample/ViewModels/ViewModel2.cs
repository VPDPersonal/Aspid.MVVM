using System.Text;
using Aspid.UI.MVVM.Generation;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace MVVMGenerators.Sample.ViewModels
{
    [ViewModel]
// [CreateFrom(typeof(SomeType))]
    [CreateFrom(typeof(SomeType))]
    public partial class ViewModel2
    {
        [BindAlso(nameof(FullName))]
        [Bind] private string _name;

        [BindAlso(nameof(FullName))]
        [Bind] private string _family;

        public string FullName => Name + " " + _family;

        public ViewModel2(SomeType some)
        {
            SomeType[] somes = new SomeType[10];
        }

        public ViewModel2(Encoding encoding)
        {

        }

        public ViewModel2(SomeType some, SomeType someItem, int a)
        {
            SomeType[] somes = new SomeType[10];
            some.ToViewModel2(someItem, a);
            somes.ToViewModel2AsList(someItem, a);
        }
    }
    
    [ViewModel]
    public partial class ViewModel3 : ViewModel2
    {
        public ViewModel3(SomeType some) : base(some)
        {
        }

        public ViewModel3(Encoding encoding) : base(encoding)
        {
        }

        public ViewModel3(SomeType some, SomeType someItem, int a) : base(some, someItem, a)
        {
        }
    }
}

public partial class SomeType
{
    
}