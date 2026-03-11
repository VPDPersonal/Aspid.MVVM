#nullable enable
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{T}"/> that accepts a <see cref="UnityAction{T}"/>.
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <example>
    /// Apply the player name from the ViewModel exactly once at binding time
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private TMP_Text _label;
    ///     
    ///     private UnityGenericOneTimeBinder&lt;string&gt; Name =>
    ///         new(value => _label.text = value);
    /// }
    ///     
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public string _name;
    /// }
    /// </code>
    /// </example>
    public class UnityGenericOneTimeBinder<T> : UnityGenericOneWayBinder<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{T}"/>.
        /// </summary>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
        public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }

    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{TTarget,T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{TTarget,T}"/> that accepts a <see cref="UnityAction{T0,T1}"/>.
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <example>
    /// Target-scoped variant — no closure over the label
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private TMP_Text _label;
    ///
    /// 
    ///     private UnityGenericOneTimeBinder&lt;TMP_Text, string&gt; Name => new
    ///     (
    ///         _label,
    ///         (label, value) => label.text = value
    ///     );
    /// }
    ///
    /// 
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public string _name;
    /// }
    /// </code>
    /// </example>
    public class UnityGenericOneTimeBinder<TTarget, T> : UnityGenericOneWayBinder<TTarget, T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{TTarget,T}"/>.
        /// </summary>
        /// <param name="target">The target object whose property is updated.</param>
        /// <param name="setValue">The <see cref="UnityAction{T0,T1}"/> invoked once with the target and the bound value.</param>
        public UnityGenericOneTimeBinder(TTarget target, UnityAction<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}