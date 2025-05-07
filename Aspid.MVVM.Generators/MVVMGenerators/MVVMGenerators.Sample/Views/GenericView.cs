using Aspid.MVVM;
using MVVMGenerators.Sample.ViewModels;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class GenericView1 : IView<PersonViewModel>, IView<ViewModelAccessTest>
{
    public Binder _a;
    public Binder _age;
    public Binder _name;
    
    [RequireBinder(typeof(string))]
    public Binder _family;
}

[View]
public partial class ChildGenericView : GenericView1, IView<WorkerViewModel1>
{
    public Binder _experience;
}
