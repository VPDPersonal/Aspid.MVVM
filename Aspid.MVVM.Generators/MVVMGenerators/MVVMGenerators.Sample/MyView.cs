using UltimateUI.MVVM;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Views;

namespace MVVMGenerators.Sample;

[View]
public partial class MyView
{
    private MonoBinder[] _name;
    private MonoBinder[] _color;
    private IBinder<string> _title;
}

[View]
public partial class MyView2 : MyView
{
    private MonoBinder[] _someBinders;
    private MonoBinder _singleBinders;
}

// [View]
// public partial class MyView3 : IView
// {
//     private MonoBinder[] _someBinders;
//     private MonoBinder _singleBinders;
//
//     public IReadOnlyBindersCollectionById GetBinders() =>
//         GetBindersIternal();
// }
//
// [View]
// public partial class MyView4 : MyView3
// {
//     private MonoBinder[] _someBinders;
//     private MonoBinder _someSingleBinders;
// }
//
// [View]
// public partial class MyView6 : MyView4
// {
//     private MonoBinder[] _lastBinder;
// }
//
// [View]
// public partial class MyMonoView1 : MonoView
// {
//     private MonoBinder[] _myBinder;
//     private MonoBinder _mySingleBinder;
// }
//
// [View]
// public partial class MyMonoView2 : MyMonoView1
// {
//     private MonoBinder[] _someBinders;
//     private MonoBinder _someSingleBinders;
// }
//
// [View]
// public partial class MyMonoView3 : MonoView
// {
//     private MonoBinder[] _myBinder;
//     private MonoBinder _mySingleBinder;
//
//     protected override IReadOnlyBindersCollectionById GetBinders() =>
//         GetBindersIternal();
// }
//
// [View]
// public partial class MyMonoView4 : MyMonoView3
// {
//     private MonoBinder[] _myBinder;
//     private MonoBinder _mySingleBinder;
// }