// using Aspid.UI.MVVM;
// using Aspid.UI.MVVM.Extensions;
// using Aspid.UI.MVVM.Mono.Views;
// using Aspid.UI.MVVM.ViewModels;
// using Aspid.UI.MVVM.Views;
// using Aspid.UI.MVVM.Views.Generation;
//
// namespace MVVMGenerators.Sample.Views;
//
// [View]
// public partial class ViewTest1 : IView
// {
//     private INumberBinder _numberBinder;
//     
//     private IBinder<string> StringBinder { get; set; }
//
//     private INumberBinder NumberBinder => _numberBinder;
//     public IViewModel? ViewModel
//     {
//         get;
//     }
//     
//     public void Initialize(IViewModel viewModel)
//     {
//         throw new System.NotImplementedException();
//     }
//
//     public void Deinitialize()
//     {
//         throw new System.NotImplementedException();
//     }
// }
//
// [View]
// public partial class ViewTest2 : ViewTest1
// {
//     private INumberBinder _numberBinder2;
// }