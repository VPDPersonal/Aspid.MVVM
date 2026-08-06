using NUnit.Framework;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Guards the default of <see cref="TypeSelectorAttribute.Allow"/>. Every "name a type" context
    /// (a raw <c>string</c> picker or a <see cref="SerializableType"/>) offers abstract classes and
    /// interfaces unless the field opts out with <see cref="TypeAllow.None"/>, so the attribute must
    /// default to <see cref="TypeAllow.All"/>.
    /// </summary>
    [TestFixture]
    internal sealed class TypeSelectorAttributeTests
    {
        [Test]
        public void Allow_DefaultsToAll()
        {
            var attribute = new TypeSelectorAttribute();
            Assert.AreEqual(TypeAllow.All, attribute.Allow);
        }
    }
}
