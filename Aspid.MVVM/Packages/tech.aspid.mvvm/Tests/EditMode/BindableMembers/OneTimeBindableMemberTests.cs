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
        public void Get_ReturnsAMemberThatKeepsItsOwnValue()
        {
            var first = OneTimeBindableMember<string>.Get("first");
            var second = OneTimeBindableMember<string>.Get("second");

            Assert.AreNotSame(first, second);
            Assert.AreEqual("first", Push(first));
            Assert.AreEqual("second", Push(second));
        }

        [Test]
        public void StructGet_ReturnsAMemberThatKeepsItsOwnValue()
        {
            var first = OneTimeStructBindableMember<int>.Get(1);
            var second = OneTimeStructBindableMember<int>.Get(2);

            Assert.AreNotSame(first, second);
            Assert.AreEqual(1, Push(first));
            Assert.AreEqual(2, Push(second));
        }

        [Test]
        public void EnumGet_ReturnsAMemberThatKeepsItsOwnValue()
        {
            var first = OneTimeEnumBindableMember<BindMode>.Get(BindMode.OneWay);
            var second = OneTimeEnumBindableMember<BindMode>.Get(BindMode.TwoWay);

            Assert.AreNotSame(first, second);
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

        private static T? Push<T>(IBinderAdder member)
        {
            var received = new List<T?>();
            new DelegateOneTimeBinder<T>(received.Add).Bind(member);

            Assert.AreEqual(1, received.Count);
            return received[0];
        }
    }
}
