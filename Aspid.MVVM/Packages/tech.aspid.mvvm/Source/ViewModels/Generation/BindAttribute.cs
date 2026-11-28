using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Makes a field or property bindable in a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Fields get a generated property. Mutable fields default to <see cref="BindMode.TwoWay"/>,
    /// and <see langword="readonly"/> fields use <see cref="BindMode.OneTime"/>.
    /// Properties default to <see cref="BindMode.OneWay"/> for a getter, <see cref="BindMode.OneWayToSource"/>
    /// for a setter, or <see cref="BindMode.TwoWay"/> for both.
    /// </para>
    /// <para>
    /// The generator warns and skips members with an incompatible mode. A get-only property also supports
    /// <see cref="BindMode.OneTime"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [Bind] private int _health;
    /// </code>
    /// <code>
    /// [Bind(BindMode.OneWay)] private string _title;
    /// </code>
    /// </example>
    public sealed class BindAttribute : BaseBindAttribute
    {
        /// <summary>
        /// Gets the explicit binding mode; <see langword="null"/> to infer it from the member.
        /// </summary>
        public BindMode? Mode { get; }

        /// <summary>
        /// Creates an attribute whose binding mode follows from the member.
        /// </summary>
        public BindAttribute() { }

        /// <summary>
        /// Creates an attribute with an explicit binding mode.
        /// </summary>
        /// <param name="mode">The binding mode. A mode the member cannot support skips the member.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is <see cref="BindMode.None"/> or undefined.</exception>
        public BindAttribute(BindMode mode)
        {
            mode.EnsureValid();
            Mode = mode;
        }
    }
}
