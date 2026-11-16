using System;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Aspid.MVVM.Validation;

namespace Aspid.MVVM.Tests
{
    [Category("AuditP2")]
    public sealed class BinderCompatibilityTests : SceneFixture
    {
        [Test]
        public void InspectorAndRequiredField_AcceptTheSameReverseOnlyBinder()
        {
            var host = Spawn<CompatibilityHost>("View");
            var group = Spawn<CanvasGroup>("Target");
            var binder = group.gameObject.AddComponent<CanvasGroupToSourceMonoBinder>();
            var serialized = new SerializedObject(host);
            var row = new MonoBinderPropertyField(serialized.FindProperty(nameof(CompatibilityHost.Source)), "Source", typeof(CanvasGroup).AssemblyQualifiedName);
            Assert.AreNotEqual(CompatibleBinderWithField.None, row.IsCompatibleBinderWithField((IMonoBinderValidatable)binder));
            var field = new RequiredFieldForMonoBinder(host, typeof(CompatibilityHost).GetField(nameof(CompatibilityHost.Source)));
            Assert.IsTrue(field.IsBinderMatchRequiredType(binder));
            binder.Mode = BindMode.TwoWay;
            Assert.AreEqual(CompatibleBinderWithField.None, row.IsCompatibleBinderWithField((IMonoBinderValidatable)binder));
            Assert.IsFalse(field.IsBinderMatchRequiredType(binder));
        }

        [Test]
        public void ReferenceVariance_FollowsTheChannelDirection()
        {
            Assert.IsTrue(BinderCompatibility.Matches(new DelegateOneWayBinder<Texture>(_ => { }), typeof(Texture2D)));
            Assert.IsFalse(BinderCompatibility.Matches(new DelegateOneWayBinder<Texture2D>(_ => { }), typeof(Texture)));
            Assert.IsTrue(BinderCompatibility.Matches(new Reverse<Texture2D>(), typeof(Texture)));
            Assert.IsFalse(BinderCompatibility.Matches(new Reverse<Texture>(), typeof(Texture2D)));
        }

        [Test]
        public void TwoWayRequiresBothChannels_AndBoxingRequiresMemberSupport()
        {
            Assert.IsFalse(BinderCompatibility.Matches(new Reverse<int>(BindMode.TwoWay), typeof(int)));
            var binder = new DelegateOneWayBinder<Enum>(_ => { });
            Assert.IsTrue(BinderCompatibility.Matches(binder, typeof(BindMode)));
            Assert.IsFalse(BinderCompatibility.Matches(binder, typeof(BindMode), boxedChannels: false));
            Assert.IsFalse(BinderCompatibility.Matches(new Reverse<int>(), typeof(int), BindMode.OneWay));
            Assert.IsFalse(BinderCompatibility.Matches(new DelegateOneWayBinder<int>(_ => { }), typeof(float)));
        }

        private sealed class Reverse<T> : IReverseBinder<T>
        {
            public Reverse(BindMode mode = BindMode.OneWayToSource) => Mode = mode;
            public bool IsBound => false;
            public BindMode Mode { get; }
            public event Action<T> ValueChanged { add { } remove { } }
            public void Bind(in BindContext context) { }
            public void Unbind(in UnbindContext context) { }
        }
    }
    public sealed class CompatibilityHost : MonoBehaviour
    {
        [RequireBinder(typeof(CanvasGroup))]
        [Tooltip("Binders used by the compatibility regression.")]
        public MonoBinder[] Source;
    }
}
