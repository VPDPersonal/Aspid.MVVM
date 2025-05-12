namespace Aspid.MVVM.ExampleScripts.Views.Other
{
    [View]
    public partial class Ex1BindIgnoreView
    {
        [IgnoreBind]
        private Binder _binder;
        
        [IgnoreBind]
        private Binder[] _binders;
        
        [IgnoreBind]
        private Binder Binder { get; }
        
        [IgnoreBind]
        private Binder[] Binders { get; }
    }
}