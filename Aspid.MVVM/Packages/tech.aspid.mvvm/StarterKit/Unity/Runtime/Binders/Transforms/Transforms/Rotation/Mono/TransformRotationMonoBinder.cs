using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Transform, Quaternion, Converter}"/> that sets the <see cref="Transform.rotation"/> or
    /// <see cref="Transform.localRotation"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current rotation
    /// is sent back to the ViewModel.
    /// <para/>
    /// Also implements <see cref="INumberBinder"/> and <see cref="IRotationBinder"/>, allowing numeric values and
    /// direct <see cref="Quaternion"/> values to be bound.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Rotation")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation")]
    public partial class TransformRotationMonoBinder : ComponentMonoBinder<Transform, Quaternion, Converter>,
        INumberBinder,
        IRotationBinder
    {
        [Tooltip("The coordinate space in which the rotation is applied.")]
        [SerializeField] private Space _space = Space.World;
        
        protected sealed override Quaternion Property
        {
            get => transform.GetRotation(_space);
            set => transform.SetRotation(value, _space);
        }
        
        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see cref="float"/> and applies a uniform <see cref="Quaternion.Euler"/> rotation.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        /// <summary>
        /// Applies a uniform <see cref="Quaternion.Euler"/> rotation using the given angle on all three axes.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}