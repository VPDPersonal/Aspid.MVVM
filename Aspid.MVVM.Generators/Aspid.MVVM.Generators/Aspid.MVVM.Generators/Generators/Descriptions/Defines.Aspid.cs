namespace Aspid.MVVM.Generators.Generators.Descriptions;

// ReSharper disable InconsistentNaming
public static class Defines
{
    public const string ASPID_MVVM_BINDER_LOG_DISABLED = nameof(ASPID_MVVM_BINDER_LOG_DISABLED);
    
    public const string ASPID_MVVM_UNITY_PROFILER_DISABLED =
#if DEBUG
        "!" + nameof(ASPID_MVVM_UNITY_PROFILER_DISABLED);
#else
        nameof(ASPID_MVVM_UNITY_PROFILER_DISABLED);
#endif
}