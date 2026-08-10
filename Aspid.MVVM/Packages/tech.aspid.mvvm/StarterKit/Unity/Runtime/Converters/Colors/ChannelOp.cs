#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="ColorChannelConverter"/> does to each channel it writes.
    /// </summary>
    public enum ChannelOp
    {
        /// <summary>
        /// Replace the channel with the operand.
        /// </summary>
        Set,

        /// <summary>
        /// Scale the channel by the operand.
        /// </summary>
        Multiply,

        /// <summary>
        /// Add the operand to the channel.
        /// </summary>
        Add,
    }
}
