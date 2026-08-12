using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests which values <see cref="GenericToString{TFrom}"/> and <see cref="StringFormatConverter"/> put through the format.
    /// </summary>
    /// <remarks>
    /// The decision lives in two places — a blank format short-circuits in the base, and blank input is the derived
    /// converter's own call — so the cases that matter are the corners: a null value, a blank value, and the
    /// <c>formatEmptyValues</c> flag that is supposed to reach both.
    /// </remarks>
    [TestFixture]
    public sealed class GenericToStringTests
    {
        [Test]
        public void ABlankFormat_LeavesTheValueInItsDefaultRepresentation()
        {
            Assert.AreEqual("5", new GenericToString<int>().Convert(5), "Пустой формат должен давать ToString()");
            Assert.AreEqual("5", new GenericToString<int>("   ").Convert(5), "Формат из пробелов должен давать ToString()");
        }

        [Test]
        public void AFormat_IsAppliedToTheTypedValue()
        {
            Assert.AreEqual("05", new GenericToString<int>("{0:D2}").Convert(5), "Числовой спецификатор не применился");
        }

        [Test]
        public void ANullValue_IsReportedAsNull()
        {
            Assert.IsNull(new GenericToString<int?>("{0:D2}").Convert(null), "null должен оставаться null");
            Assert.IsNull(new StringFormatConverter("HP: {0}").Convert(null), "null должен проходить насквозь");
        }

        [Test]
        public void FormattingEmptyValues_ReachesNullAsWell()
        {
            Assert.AreEqual(
                "HP: ",
                new StringFormatConverter("HP: {0}", formatEmptyValues: true).Convert(null),
                "Со взведённым formatEmptyValues null обязан форматироваться — это обещает тултип поля");
        }

        [Test]
        public void ABlankValue_PassesThroughUntilFormattingEmptyValuesIsEnabled()
        {
            Assert.AreEqual("", new StringFormatConverter("HP: {0}").Convert(""), "Пустая строка должна пройти насквозь");
            Assert.AreEqual(" ", new StringFormatConverter("HP: {0}").Convert(" "), "Строка из пробелов должна пройти насквозь");

            Assert.AreEqual(
                "HP: ",
                new StringFormatConverter("HP: {0}", formatEmptyValues: true).Convert(""),
                "Со взведённым formatEmptyValues пустая строка обязана форматироваться");
        }

        [Test]
        public void ANonBlankValue_IsFormattedRegardlessOfTheFlag()
        {
            Assert.AreEqual("HP: 10", new StringFormatConverter("HP: {0}").Convert("10"));
            Assert.AreEqual("HP: 10", new StringFormatConverter("HP: {0}", formatEmptyValues: true).Convert("10"));
        }
    }
}
