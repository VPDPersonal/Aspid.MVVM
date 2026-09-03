// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A View or ViewModel that needs a setup call after a <see cref="ViewInitializerBase"/> resolves it.
    /// </summary>
    public interface IComponentInitializable
    {
        /// <summary>
        /// Runs the setup once the component is resolved.
        /// </summary>
        public void Initialize();
    }
}
