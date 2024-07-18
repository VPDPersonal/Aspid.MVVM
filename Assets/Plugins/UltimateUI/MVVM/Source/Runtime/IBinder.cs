// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface IBinder
    {
        public void Bind<T>(in BindData<T> bindData);
        
        public void Unbind();
    }
}