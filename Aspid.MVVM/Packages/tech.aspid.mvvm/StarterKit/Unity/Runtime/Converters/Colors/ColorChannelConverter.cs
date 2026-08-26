#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies one arithmetic operation to the chosen channels of a color.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Channel",
        Tooltip = "Applies one arithmetic operation to the chosen channels of a color")]
    public sealed class ColorChannelConverter : IConverter<Color, Color>
    {
        // Multiply with a white operand passes the color through; Set would emit the operand instead.
        [Tooltip("What the operand does to each chosen channel.")]
        [SerializeField] private ChannelOp _operation = ChannelOp.Multiply;

        [Tooltip("Supplies the operand for each channel — red operates on red, green on green.")]
        [SerializeField] private Color _operand = Color.white;

        [Tooltip("Which channels are written. The rest pass through untouched.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.Rgb;

        [Tooltip("Hold every written channel inside 0..1. Clear it for HDR colors, which live above one.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>
        /// Default: a clamped multiply by white over the color channels — an identity for every
        /// color that already sits inside 0..1.
        /// </remarks>
        public ColorChannelConverter() { }

        /// <param name="operation">What the operand does to each chosen channel.</param>
        /// <param name="operand">Supplies the operand for each channel.</param>
        /// <param name="channels">Which channels are written. The rest pass through untouched.</param>
        /// <param name="clamp">
        /// Whether to hold every written channel inside 0..1. Clear it for HDR colors, which live
        /// above one.
        /// </param>
        public ColorChannelConverter(
            ChannelOp operation,
            Color operand,
            ColorChannels channels = ColorChannels.Rgb,
            bool clamp = true)
        {
            _operation = operation;
            _operand = operand;
            _channels = channels;
            _clamp = clamp;
        }

        /// <summary>
        /// Applies the operation to the chosen channels of the specified color.
        /// </summary>
        /// <param name="value">The color to operate on.</param>
        /// <returns>
        /// The color, with the channels outside the mask unchanged. An operation that is not a
        /// declared <see cref="ChannelOp"/> value reports an error and leaves the written channels
        /// unchanged too.
        /// </returns>
        public Color Convert(Color value) => new(
            Apply(value.r, _operand.r, ColorChannels.R),
            Apply(value.g, _operand.g, ColorChannels.G),
            Apply(value.b, _operand.b, ColorChannels.B),
            Apply(value.a, _operand.a, ColorChannels.A));

        private float Apply(float channel, float operand, ColorChannels flag)
        {
            // Tested bitwise rather than with HasFlag, which boxes both sides on Mono and IL2CPP.
            if ((_channels & flag) == 0) return channel;

            float result;

            switch (_operation)
            {
                case ChannelOp.Set: result = operand; break;
                case ChannelOp.Multiply: result = channel * operand; break;
                case ChannelOp.Add: result = channel + operand; break;
                // Returns before the clamp below: the channel was never written, and holding an HDR
                // channel to 0..1 would change it.
                default: return Undeclared(channel);
            }

            return _clamp ? Mathf.Clamp01(result) : result;
        }

        private float Undeclared(float channel)
        {
            this.LogError($"the operation {_operation.Describe()} is not a declared {nameof(ChannelOp)}",
                "Leaving the channel unchanged.");

            return channel;
        }
    }
}
