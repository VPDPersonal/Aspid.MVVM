// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Build/CI gate severity for missing or unset-required managed references.
    /// </summary>
    internal enum GateSeverity
    {
        Off,
        Warn,
        Fail,
    }
}
