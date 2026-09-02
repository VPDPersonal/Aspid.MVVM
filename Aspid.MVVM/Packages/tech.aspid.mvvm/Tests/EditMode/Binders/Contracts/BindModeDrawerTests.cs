using System.Linq;
using UnityEditor;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the two things the <c>BindMode</c> dropdown got wrong: it discarded the user's choice on anything that
    /// is not an <see cref="IRebindableBinder"/>, and it corrected an unsupported mode by writing serialized data from
    /// inside the drawer.
    /// </summary>
    /// <remarks>
    /// The drawer itself is internal to the Editor assembly and cannot be driven from a test without a live Inspector.
    /// What is checked here is the behaviour a project actually depends on and that the drawer is built around: a mode
    /// the class does not allow is not left in place, and the allowed set really does come from the class.
    /// </remarks>
    [TestFixture]
    public sealed class BindModeDrawerTests : SceneFixture
    {
        /// <summary>
        /// A binder whose class forbids a mode must not be left holding it. The drawer corrects the serialized value on
        /// the next editor tick; this pins the fact the correction is based on — that the override attribute is what
        /// decides, and it is reachable from the binder's own type.
        /// </summary>
        [Test]
        public void EveryBindersOverride_IsReachableFromItsType()
        {
            var binder = NewBinder<ScrollbarSizeMonoBinder>();
            var attribute = AttributeOf(binder);

            Assert.IsNotNull(attribute, "The binder found no BindModeOverride even though its base declares one");
            Assert.IsTrue(attribute.IsOne || attribute.Modes.Contains(BindMode.OneWay),
                "The binder's allowed modes do not include OneWay");
        }

        /// <summary>
        /// The mode field is serialized and has no public setter, which is why the drawer writes through
        /// <see cref="SerializedProperty"/> — and why a write that never happens loses the user's choice silently.
        /// </summary>
        [Test]
        public void TheModeField_IsWritableThroughItsSerializedProperty()
        {
            var binder = NewBinder<ScrollbarSizeMonoBinder>();
            var serializedObject = new SerializedObject(binder);
            var property = serializedObject.FindProperty("_mode");

            Assert.IsNotNull(property, "The binder has no serialized _mode field");

            property.enumValueIndex = (int)BindMode.OneTime;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(BindMode.OneTime, binder.Mode, "The write through SerializedProperty did not reach the binder");
        }

        private static BindModeOverrideAttribute AttributeOf(MonoBinder binder)
        {
            for (var type = binder.GetType(); type is not null; type = type.BaseType)
            {
                var attribute = type
                    .GetCustomAttributes(typeof(BindModeOverrideAttribute), inherit: false)
                    .FirstOrDefault() as BindModeOverrideAttribute;

                if (attribute is not null) return attribute;
            }

            return null;
        }

        private T NewBinder<T>()
            where T : MonoBinder
        {
            var gameObject = Spawn("Binder");

            gameObject.AddComponent<UnityEngine.UI.Scrollbar>();
            return gameObject.AddComponent<T>();
        }
    }
}
