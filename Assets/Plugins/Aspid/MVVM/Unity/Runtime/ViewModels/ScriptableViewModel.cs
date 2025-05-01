namespace Aspid.MVVM.Unity
{
    [ViewModel]
    public abstract partial class ScriptableViewModel : ScriptableView
    {
        protected virtual void OnValidate() =>
            this.InvokeAllChangedEventsDebug();
    }
}