namespace Aspid.MVVM.StarterKit.Unity
{
    public interface IViewFactoryRelease<in T>
        where T : IView
    {
        public void Release(T view);
    }
}