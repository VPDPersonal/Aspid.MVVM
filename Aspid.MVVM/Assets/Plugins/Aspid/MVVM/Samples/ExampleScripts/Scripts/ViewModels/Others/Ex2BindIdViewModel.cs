// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.ExampleScripts.ViewModels.Others
{
    [ViewModel]
    public partial class Ex2BindIdViewModel
    {
        // Generated ID: Text2
        [BindId("Text2")]
        [Bind] private string _text1;
        
        // Generated ID: OtherDoCommand 
        [RelayCommand]
        [BindId("OtherDoCommand")]
        private void Do() { }
    }
}