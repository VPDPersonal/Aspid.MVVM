#if UNITY_EDITOR
using Aspid.UI.MVVM.Views;

namespace Aspid.UI.MVVM.Mono
{
    public interface IMonoBinderValidable
    {
        public IView? View { get; set; }
        
        public string? Id { get; set; }

        public void Reset()
        {
            Id = null;
            View = null;
        }
    }
}
#endif