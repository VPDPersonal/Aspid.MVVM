#if UNITY_EDITOR
using AspidUI.MVVM.Views;

namespace AspidUI.MVVM.Unity
{
    public interface IMonoBinderValidable
    {
        public IView? View { get; set; }
        
        public string? Id { get; set; }
    }
}
#endif