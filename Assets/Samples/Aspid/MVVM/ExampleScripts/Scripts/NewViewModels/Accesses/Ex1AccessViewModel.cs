namespace Aspid.MVVM.ExampleScripts.NewViewModels.Accesses
{
    [ViewModel]
    public partial class Ex1AccessViewModel
    {
        // private string Text1
        // {
        //     get => _text1;
        //     set => SetText1(value);
        // }
        [Bind] private string _text1;
        
        // public string Text2
        // {
        //     get => _text2;
        //     set => SetText2(value);
        // }
        [Access(Access.Public)]
        [Bind] private string _text2;
        
        // protected string Text3
        // {
        //     get => _text3;
        //     set => SetText3(value);
        // }
        [Access(Access.Protected)]
        [Bind] private string _text3;
        
        // public string Text4
        // {
        //     get => _text4;
        //     private set => SetText4(value);
        // }
        [Access(Get = Access.Public)]
        [Bind] private string _text4;
        
        // protected string Text5
        // {
        //     get => _text5;
        //     private set => SetText5(value);
        // }
        [Access(Get = Access.Protected)]
        [Bind] private string _text5;
        
        // public string Text6
        // {
        //     private get => _text6;
        //     set => SetText6(value);
        // }
        [Access(Set = Access.Public)]
        [Bind] private string _text6;
        
        // protected string Text7
        // {
        //     private get => _text7;
        //     set => SetText7(value);
        // }
        [Access(Set = Access.Protected)]
        [Bind] private string _text7;
        
        // public string Text8
        // {
        //     get => _text8;
        //     protected set => SetText8(value);
        // }
        [Access(Get = Access.Public, Set = Access.Protected)]
        [Bind] private string _text8;
        
        // public string Text9
        // {
        //     protected get => _text9;
        //     set => SetText9(value);
        // }
        [Access(Get = Access.Protected, Set = Access.Public)]
        [Bind] private string _text9;
    }
}