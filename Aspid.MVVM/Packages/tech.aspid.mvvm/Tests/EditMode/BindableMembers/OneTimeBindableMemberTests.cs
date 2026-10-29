using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="OneTimeBindableMember{T}"/>, <see cref="OneTimeStructBindableMember{T}"/> and
    /// <see cref="OneTimeEnumBindableMember{T}"/>.
    /// </summary>
    [TestFixture]
    public sealed class OneTimeBindableMemberTests
    {
        [Test]
        public void Members_KeepTheirOwnValues()
        {
            var first = new OneTimeBindableMember<string>("first");
            var second = new OneTimeBindableMember<string>("second");

            Assert.AreEqual("first", Push(first));
            Assert.AreEqual("second", Push(second));
        }

        [Test]
        public void StructMembers_KeepTheirOwnValues()
        {
            var first = new OneTimeStructBindableMember<int>(1);
            var second = new OneTimeStructBindableMember<int>(2);

            Assert.AreEqual(1, Push(first));
            Assert.AreEqual(2, Push(second));
        }

        [Test]
        public void EnumMembers_KeepTheirOwnValues()
        {
            var first = new OneTimeEnumBindableMember<BindMode>(BindMode.OneWay);
            var second = new OneTimeEnumBindableMember<BindMode>(BindMode.TwoWay);

            Assert.AreEqual(BindMode.OneWay, Push(first));
            Assert.AreEqual(BindMode.TwoWay, Push(second));
        }

        [Test]
        public void SetValue_ReachesTheNextBinderOnly()
        {
            var member = new OneTimeBindableMember<string>("old");
            var firstValues = new List<string?>();
            new DelegateOneTimeBinder<string>(firstValues.Add).Bind(member);

            member.Value = "new";

            Assert.AreEqual(new[] { "old" }, firstValues);
            Assert.AreEqual("new", Push(member));
        }

        private static T? Push<T>(IReadOnlyValueBindableMember<T> member)
        {
            var received = new List<T?>();
            new DelegateOneTimeBinder<T>(received.Add).Bind(member);

            Assert.AreEqual(1, received.Count);
            return received[0];
        }
    }
}
