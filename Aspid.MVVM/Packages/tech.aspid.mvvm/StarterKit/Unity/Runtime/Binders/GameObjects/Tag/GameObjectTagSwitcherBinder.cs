#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{T1, T2}">SwitcherBinder&lt;GameObject, string&gt;</see> that switches the <see cref="GameObject.tag"/>
    /// property between two <see cref="string"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-GameObject-Tag-1.1.0.xml" path="doc//member[@name='GameObjectTagSwitcherBinder']/*" />
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<GameObject, string>
    {
        /// <inheritdoc/>
        public GameObjectTagSwitcherBinder(
            GameObject target,
            string trueValue,
            string falseValue, 
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            Target.tag = value;
    }
}