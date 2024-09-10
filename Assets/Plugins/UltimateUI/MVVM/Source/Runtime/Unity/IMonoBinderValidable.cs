#if UNITY_EDITOR
using UltimateUI.MVVM.Views;

namespace UltimateUI.MVVM.Unity
{
    public interface IMonoBinderValidable
    {
        public IView? View { get; set; }
        
        public string? Id { get; set; }
    }
}
#endif