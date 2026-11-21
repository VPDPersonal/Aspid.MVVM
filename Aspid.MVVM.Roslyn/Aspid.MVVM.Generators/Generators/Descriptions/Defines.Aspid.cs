namespace Aspid.MVVM.Generators.Generators.Descriptions;

// ReSharper disable InconsistentNaming
public static class Defines
{
    public const string ASPID_MVVM_BINDER_LOG_DISABLED = nameof(ASPID_MVVM_BINDER_LOG_DISABLED);
    
    // ENABLE_PROFILER keeps the marker fields out of a release player, as the package's own ASPID_MVVM_PROFILER alias does.
    public const string ASPID_MVVM_PROFILER = "ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED";
}