using Aspid.MVVM;
using MVVMGenerators.Sample.ViewModels;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class GenericView : IView<IPersonViewModel>, IView<ViewModelAccessTest>
{
    public Binder _a;
    public Binder _age;
    public Binder _name;
    public Binder _family;
}

[View]
public partial class ChildGenericView : GenericView, IView<WorkerViewModel1>
{
    public Binder _experience;
}
