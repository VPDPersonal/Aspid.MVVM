using Aspid.MVVM.ExampleScripts.ViewModels;

namespace Aspid.MVVM.ExampleScripts.Views.Generic
{
    [View]
    public partial class Ex1GenericView : IView<Ex1BindViewModel>
    {
        private Binder _twoWayValue;
        private Binder _oneTimeValue;
    }
}