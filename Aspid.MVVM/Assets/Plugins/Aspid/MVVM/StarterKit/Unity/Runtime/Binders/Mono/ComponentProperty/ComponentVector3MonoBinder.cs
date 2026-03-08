using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder for binding a <see cref="UnityEngine.Vector3"/> property on a Unity <see cref="UnityEngine.Component"/>.
    /// Implements <see cref="IVectorBinder"/> and <see cref="INumberBinder"/>: scalar values (int, long, float, double)
    /// are broadcast to all three parts as <c>new Vector3(value, value, value)</c>.
    /// </summary>
    public abstract class ComponentVector3MonoBinder<TComponent> : ComponentMonoBinder<TComponent, Vector3, Converter>,
        IVectorBinder,
        INumberBinder
        where TComponent : Component
    {
        [BinderLog]
        public void SetValue(Vector2 value) =>
            base.SetValue(value);
        
        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue(new Vector3(value, value, value));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
        
    }
}