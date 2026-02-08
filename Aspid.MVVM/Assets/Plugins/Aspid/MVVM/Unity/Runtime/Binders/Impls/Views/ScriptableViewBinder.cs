// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ScriptableViewBinder : MonoViewBinder<ScriptableView>
    {
        public ScriptableViewBinder(ScriptableView target, BindMode mode = BindMode.OneWay) 
            : base(target, mode) { }
    }
}