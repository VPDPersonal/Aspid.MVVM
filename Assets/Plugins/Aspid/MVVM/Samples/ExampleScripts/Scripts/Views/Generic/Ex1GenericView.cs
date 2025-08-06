using Aspid.MVVM.Samples.ExampleScripts.ViewModels.Bind;

namespace Aspid.MVVM.Samples.ExampleScripts.Views.Generic
{
    [View]
    public partial class Ex1GenericView : IView<Ex1BindViewModel>
    {
        private Binder _twoWayValue;
        private Binder _oneTimeValue;
    }
}