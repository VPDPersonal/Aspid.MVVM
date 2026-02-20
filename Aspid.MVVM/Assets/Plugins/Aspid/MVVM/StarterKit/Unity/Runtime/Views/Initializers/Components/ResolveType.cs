// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public enum ResolveType
    {
        Mono,
        References,
        ScriptableObject,
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
        Di,
#endif
    }
}