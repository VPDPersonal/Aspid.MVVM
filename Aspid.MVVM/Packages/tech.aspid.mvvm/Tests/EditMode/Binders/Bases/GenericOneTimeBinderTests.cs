using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="GenericOneTimeBinder{T}"/> and <see cref="GenericOneTimeBinder{TTarget,T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class GenericOneTimeBinderTests
    {
        [Test]
        public void Ctor_FixesTheModeToOneTime()
        {
            var binder = new GenericOneTimeBinder<int>(_ => { });

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void BoundThroughAMember_TheSecondPush_DoesNotReachTheSetter()
        {
            var received = new List<int>();
            var binder = new GenericOneTimeBinder<int>(received.Add);
            var member = new OneWayBindableMember<int>(1);

            binder.Bind(member);
            member.Value = 2;

            Assert.AreEqual(new[] { 1 }, received, "A second push must not reach a OneTime binder.");
        }

        [Test]
        public void TargetCtor_FixesTheModeToOneTime()
        {
            var binder = new GenericOneTimeBinder<object, int>(new object(), (_, _) => { });

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void TargetBoundThroughAMember_TheSecondPush_DoesNotReachTheSetter()
        {
            var received = new List<int>();
            var binder = new GenericOneTimeBinder<object, int>(new object(), (_, value) => received.Add(value));
            var member = new OneWayBindableMember<int>(1);

            binder.Bind(member);
            member.Value = 2;

            Assert.AreEqual(new[] { 1 }, received, "A second push must not reach a OneTime binder.");
        }
    }
}
