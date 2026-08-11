using Object = UnityEngine.Object;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;TComponent, TObject&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> in both binding directions.
    /// </summary>
    /// <remarks>
    /// A destroyed Unity object is not a <see langword="null"/> reference: the managed wrapper survives and compares
    /// equal to <see langword="null"/> only through <see cref="Object"/>'s own operators. Without this layer a
    /// ViewModel could hand over a destroyed asset — which the property would accept and the Inspector would show as
    /// <c>Missing</c> — or receive one back in <see cref="BindMode.OneWayToSource"/> and store it as a live value.
    /// </remarks>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    public abstract class ComponentObjectMonoBinder<TComponent, TObject> : ComponentMonoBinder<TComponent, TObject>
        where TComponent : Component
        where TObject : Object
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when <paramref name="value"/> refers to a destroyed object.
        /// </remarks>
        protected override TObject GetConvertedValue(TObject value) =>
            // Приведение к Object обязательно: пользовательский operator bool не применяется
            // к значению параметра типа, поэтому `value ? …` без каста не скомпилируется,
            // а `value != null` собралось бы в ссылочное сравнение и пропустило уничтоженный объект.
            (Object)value ? value : null;
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;TComponent, TObject, TConverter&gt;</see> that binds a
    /// property holding a reference to a <see cref="Object">UnityEngine.Object</see>, normalizing destroyed
    /// references to <see langword="null"/> after the configured converter has run.
    /// </summary>
    /// <remarks>
    /// The converter runs first, so a converter that resolves one asset into another is still checked: whatever it
    /// returns is what reaches the property. See <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> for why
    /// the check is needed at all.
    /// </remarks>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target property.</typeparam>
    /// <typeparam name="TObject">The type of <see cref="Object">UnityEngine.Object</see> the property holds.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the bound value before applying it.</typeparam>
    public abstract class ComponentObjectMonoBinder<TComponent, TObject, TConverter> : ComponentMonoBinder<TComponent, TObject, TConverter>
        where TComponent : Component
        where TObject : Object
        where TConverter : IConverter<TObject, TObject>
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Returns <see langword="null"/> when the converted value refers to a destroyed object.
        /// </remarks>
        protected override TObject GetConvertedValue(TObject value)
        {
            var converted = base.GetConvertedValue(value);
            return (Object)converted ? converted : null;
        }
    }
}
