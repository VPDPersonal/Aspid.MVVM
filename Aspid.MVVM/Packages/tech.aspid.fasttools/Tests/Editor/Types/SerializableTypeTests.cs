using System;
using NUnit.Framework;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Guards the implicit <c>SerializableType → Type</c> conversions: a <see langword="null"/> wrapper
    /// converts to <see langword="null"/> instead of throwing, matching the <c>Type</c> property's
    /// "resolved type or null" contract.
    /// </summary>
    [TestFixture]
    internal sealed class SerializableTypeTests
    {
        [Test]
        public void ImplicitConversion_NullWrapper_YieldsNull()
        {
            SerializableType wrapper = null;
            Type type = wrapper;

            Assert.IsNull(type);
        }

        [Test]
        public void ImplicitConversion_NullGenericWrapper_YieldsNull()
        {
            SerializableType<IComparable> wrapper = null;
            Type type = wrapper;

            Assert.IsNull(type);
        }
    }
}
