#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies one arithmetic operation to the chosen channels of a colour.
    /// </summary>
    /// <remarks>
    /// The general case the other colour converters are special cases of — boosting only red for a
    /// damage flash, zeroing blue for a sepia pass, halving green on a colour-blind palette. Each of
    /// those otherwise needs its own converter or a hand-written one.
    /// <para>
    /// Channels outside the mask pass through untouched, so a mask of <see cref="ColorChannels.A"/>
    /// with <see cref="ChannelOp.Set"/> is <see cref="ColorAlphaConverter"/>, and a mask of
    /// <see cref="ColorChannels.All"/> with <see cref="ChannelOp.Multiply"/> is a
    /// <see cref="ColorTintConverter"/> multiply. Reach for those when they say what you mean; reach
    /// for this when they do not.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Channel", Tooltip = "Applies one arithmetic operation to the chosen channels of a colour")]
    public sealed class ColorChannelConverter : IConverterColor
    {
        // Multiply against the white operand below, so a freshly picked converter passes the bound
        // colour through: Set would emit the operand and read as a binding that stopped working.
        [Tooltip("What the operand does to each chosen channel.")]
        [SerializeField] private ChannelOp _operation = ChannelOp.Multiply;

        [Tooltip("Supplies the operand for each channel — red operates on red, green on green.")]
        [SerializeField] private Color _operand = Color.white;

        [Tooltip("Which channels are written. The rest pass through untouched.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.Rgb;

        [Tooltip("Hold every written channel inside 0..1. Clear it for HDR colours, which live above one.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChannelConverter"/> class that changes nothing.
        /// </summary>
        public ColorChannelConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChannelConverter"/> class.
        /// </summary>
        /// <param name="operation">What the operand does to each chosen channel.</param>
        /// <param name="operand">Supplies the operand for each channel.</param>
        /// <param name="channels">Which channels are written.</param>
        /// <param name="clamp">Whether to hold every written channel inside 0..1.</param>
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
        /// Applies the operation to the chosen channels of the specified colour.
        /// </summary>
        /// <param name="value">The colour to operate on.</param>
        /// <returns>The colour, with the channels outside the mask unchanged.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public Color Convert(Color value) => new(
            Apply(value.r, _operand.r, ColorChannels.R),
            Apply(value.g, _operand.g, ColorChannels.G),
            Apply(value.b, _operand.b, ColorChannels.B),
            Apply(value.a, _operand.a, ColorChannels.A));

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        private float Apply(float channel, float operand, ColorChannels flag)
        {
            if (!_channels.HasFlag(flag)) return channel;

            var result = _operation switch
            {
                ChannelOp.Set => operand,
                ChannelOp.Multiply => channel * operand,
                ChannelOp.Add => channel + operand,
                _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
            };

            return _clamp ? Mathf.Clamp01(result) : result;
        }
    }
}
