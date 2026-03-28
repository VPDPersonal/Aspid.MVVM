#nullable enable
// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.XmlDoc
{
    /// <summary>
    /// Represents a <c>&lt;see cref="..."&gt;</c> or <c>&lt;seealso cref="..."&gt;</c> reference.
    /// </summary>
    public sealed class SeeReference
    {
        /// <summary>
        /// The <c>cref</c> attribute value, e.g. <c>"T:System.String"</c> or <c>"AudioSource.pitch"</c>.
        /// </summary>
        public string? Cref;
    }
}