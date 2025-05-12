using Aspid.MVVM.ExampleScripts.ViewModels.Bind;

namespace Aspid.MVVM.ExampleScripts.Views.Generic
{
    [View]
    public partial class Ex1GenericView : IView<Ex1BindViewModel>
    {
        private Binder _twoWayValue;
        private Binder _oneTimeValue;
    }
}

//  Generated Code:
//  public partial class Ex1GenericView : IView<Ex1BindViewModel.IBindableMembers>
//  {
//      public void Initialize(Ex1BindViewModel viewModel)
//      {
//          if (ViewModel is null) return;
//          InitializeInternal(Unsafe.As<Ex1BindViewModel, Ex1BindViewModel.IBindableMembers>(ref viewModel));
//      }
//          
//      public void Initialize(Ex1BindViewModel.IBindableMembers viewModel)
//      {
//          if (ViewModel is null) return;
//          InitializeInternal(viewModel);
//      }
//          
//      protected void InitializeInternal(Ex1BindViewModel.IBindableMembers viewModel)
//      {
//          OnInitializingInternal(viewModel);
//                  
//          _twoWayValue.BindSafely(viewModel.TwoWayValue);
//          _oneTimeValue.BindSafely(viewModel.OneTimeValue);
//                  
//          OnInitializedInternal(viewModel);
//      }
//  }