#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{GameObject, string}"/> that switches the <see cref="GameObject.tag"/>
    /// property between two <see cref="string"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <example>
    /// Switch the GameObject tag between two values based on a boolean ViewModel property.
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField]
    ///     private GameObjectTagSwitcherBinder _isPlayer;
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isPlayer;
    /// }
    /// </code>
    /// <code>
    /// [View]
    /// public partial class ExampleView
    /// {
    ///     [SerializeField] private GameObject _target;
    ///    
    ///     private GameObjectTagSwitcherBinder IsPlayer => new(
    ///         _target, trueValue: "Player", falseValue: "Untagged");
    /// }
    ///    
    /// [ViewModel]
    /// public partial class ExampleViewModel
    /// {
    ///     [Bind] public bool _isPlayer;
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<GameObject, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GameObjectTagSwitcherBinder"/> targeting the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="GameObject.tag"/> property is switched.</param>
        /// <param name="trueValue">The tag assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The tag assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode to use.</param>
        public GameObjectTagSwitcherBinder(
            GameObject target,
            string trueValue,
            string falseValue, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            Target.tag = value;
    }
}