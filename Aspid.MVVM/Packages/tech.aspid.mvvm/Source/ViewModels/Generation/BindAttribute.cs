using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a bindable member for the field or property of a <see cref="ViewModelAttribute">[ViewModel]</see> class, and a property for a field.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Without a mode, a mutable field binds <see cref="BindMode.TwoWay"/> and a <see langword="readonly"/> field
    /// <see cref="BindMode.OneTime"/>; a property binds <see cref="BindMode.OneWay"/> when it is get-only,
    /// <see cref="BindMode.OneWayToSource"/> when it is set-only and <see cref="BindMode.TwoWay"/> otherwise.
    /// </para>
    /// <para>
    /// A mode the member cannot support skips the member with a generator warning: a <see langword="readonly"/> field takes only
    /// <see cref="BindMode.OneTime"/>, a get-only property <see cref="BindMode.OneTime"/> or <see cref="BindMode.OneWay"/>, and
    /// a set-only property <see cref="BindMode.OneWayToSource"/>.
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
