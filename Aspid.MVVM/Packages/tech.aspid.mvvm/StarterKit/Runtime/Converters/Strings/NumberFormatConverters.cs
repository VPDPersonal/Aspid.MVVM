using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with a standard .NET format string.
    /// </summary>
    /// <remarks>
    /// <see cref="GenericToString{TFrom}"/> takes a <i>composite</i> format, so <c>"N0"</c> comes
    /// back as the literal <c>N0</c> and the specifier has to be spelled <c>{0:N0}</c>. This takes
    /// the specifier everyone expects — the one on <see cref="int.ToString(string)"/>.
    /// </remarks>
    [Serializable]
    public sealed class NumberFormatConverter :
        IConverter<float, string>,
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>
    {
        [Tooltip("A standard numeric format string: N0 for thousands separators, F2 for two decimals, "
            + "P1 for a percentage, C2 for currency.")]
        [SerializeField] private string _format = "N0";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFormatConverter"/> class formatting with thousands separators.
        /// </summary>
        public NumberFormatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFormatConverter"/> class.
        /// </summary>
        /// <param name="format">A standard numeric format string.</param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public NumberFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(float value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(double value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) => value.ToString(_format, _culture.ToCultureInfo());
    }

    /// <summary>
    /// Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M.
    /// </summary>
    /// <remarks>
    /// Every idle game, every leaderboard and every currency counter reinvents this. The suffixes are
    /// authored rather than hard-coded, because past trillions games stop agreeing on what to call
    /// the next one.
    /// </remarks>
    [Serializable]
    public sealed class AbbreviatedNumberConverter : IConverter<double, string>
    {
        [Tooltip("The suffix for each power of a thousand, starting with none.")]
        [SerializeField] private string[] _suffixes = { "", "K", "M", "B", "T" };

        [Tooltip("How many decimals to show on a shortened number.")]
        [SerializeField] private int _decimals = 2;

        [Tooltip("Drop trailing zeros: 1.20M becomes 1.2M.")]
        [SerializeField] private bool _trimTrailingZeros = true;

        [Tooltip("Numbers below this are written out in full.")]
        [SerializeField] private double _threshold = 1000d;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbreviatedNumberConverter"/> class with K/M/B/T suffixes.
        /// </summary>
        public AbbreviatedNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbreviatedNumberConverter"/> class.
        /// </summary>
        /// <param name="decimals">How many decimals to show on a shortened number.</param>
        /// <param name="suffixes">The suffix for each power of a thousand, starting with none.</param>
        public AbbreviatedNumberConverter(int decimals, string[]? suffixes = null)
        {
            _decimals = decimals;
            if (suffixes is { Length: > 0 }) _suffixes = suffixes;
        }

        /// <summary>
        /// Shortens the specified number.
        /// </summary>
        /// <param name="value">The number to shorten.</param>
        /// <returns>The shortened number with its suffix.</returns>
        public string Convert(double value)
        {
            var culture = _culture.ToCultureInfo();
            var magnitude = Math.Abs(value);

            if (_suffixes is not { Length: > 0 } || magnitude < _threshold)
                return value.ToString("0.##", culture);

            var tier = 0;
            while (magnitude >= 1000d && tier < _suffixes.Length - 1)
            {
                magnitude /= 1000d;
                tier++;
            }

            var text = magnitude.ToString("F" + Math.Max(0, _decimals), culture);
            if (_trimTrailingZeros) text = TrimZeros(text, culture);

            return (value < 0 ? "-" : string.Empty) + text + _suffixes[tier];
        }

        private static string TrimZeros(string text, CultureInfo culture)
        {
            var separator = culture.NumberFormat.NumberDecimalSeparator;
            if (!text.Contains(separator)) return text;

            return text.TrimEnd('0').TrimEnd(separator.ToCharArray());
        }
    }

    /// <summary>
    /// The grammar <see cref="PluralizeConverter"/> follows when picking a form.
    /// </summary>
    public enum PluralRule
    {
        /// <summary>One form for 1, another for everything else.</summary>
        English,

        /// <summary>
        /// The Russian-style three-form rule: one, few (2-4), many — with the teens taking the many
        /// form regardless of their last digit.
        /// </summary>
        Slavic,
    }

    /// <summary>
    /// Picks the right word form for a count.
    /// </summary>
    /// <remarks>
    /// "1 предмет" / "2 предмета" / "5 предметов" cannot be reached by appending an "s", and a
    /// framework documented in Russian cannot treat the Slavic rule as an extra.
    /// </remarks>
    [Serializable]
    public sealed class PluralizeConverter : IConverter<int, string>
    {
        [Tooltip("Which grammar to follow.")]
        [SerializeField] private PluralRule _rule = PluralRule.English;

        [Tooltip("Used for zero. When empty, the many form is used.")]
        [SerializeField] private string _zeroForm = string.Empty;

        [Tooltip("Used for one.")]
        [SerializeField] private string _oneForm = string.Empty;

        [Tooltip("Used for two to four under the Slavic rule. Ignored by the English rule.")]
        [SerializeField] private string _fewForm = string.Empty;

        [Tooltip("Used for everything else.")]
        [SerializeField] private string _manyForm = string.Empty;

        [Tooltip("A composite format for the result: {0} is the count, {1} the word.")]
        [SerializeField] private string _format = "{0} {1}";

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralizeConverter"/> class with English grammar.
        /// </summary>
        public PluralizeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralizeConverter"/> class.
        /// </summary>
        /// <param name="rule">Which grammar to follow.</param>
        /// <param name="one">Used for one.</param>
        /// <param name="many">Used for everything else.</param>
        /// <param name="few">Used for two to four under the Slavic rule.</param>
        /// <param name="zero">Used for zero. When <see langword="null"/>, the many form is used.</param>
        public PluralizeConverter(PluralRule rule, string one, string many, string? few = null, string? zero = null)
        {
            _rule = rule;
            _oneForm = one;
            _manyForm = many;
            _fewForm = few ?? many;
            _zeroForm = zero ?? string.Empty;
        }

        /// <summary>
        /// Formats the specified count with the word form its grammar calls for.
        /// </summary>
        /// <param name="value">The count.</param>
        /// <returns>The formatted text.</returns>
        public string Convert(int value)
        {
            var word = Form(value);
            return string.IsNullOrEmpty(_format) ? word : string.Format(_format, value, word);
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rule is not a declared value.</exception>
        private string Form(int value)
        {
            if (value == 0 && !string.IsNullOrEmpty(_zeroForm)) return _zeroForm;

            var magnitude = Math.Abs(value);

            return _rule switch
            {
                PluralRule.English => magnitude == 1 ? _oneForm : _manyForm,
                PluralRule.Slavic => SlavicForm(magnitude),
                _ => throw new ArgumentOutOfRangeException(nameof(_rule), _rule, null)
            };
        }

        // The teens are the exception: 11 takes the many form even though it ends in 1, and 12-14
        // take it even though they end in 2-4.
        private string SlavicForm(int magnitude)
        {
            var lastTwo = magnitude % 100;
            if (lastTwo is >= 11 and <= 14) return _manyForm;

            return (magnitude % 10) switch
            {
                1 => _oneForm,
                2 or 3 or 4 => _fewForm,
                _ => _manyForm
            };
        }
    }

    /// <summary>
    /// Where <see cref="CurrencyConverter"/> puts the symbol.
    /// </summary>
    public enum SymbolPosition
    {
        /// <summary>Before the number.</summary>
        Before,

        /// <summary>After the number.</summary>
        After,
    }

    /// <summary>
    /// Formats a number as an amount of currency.
    /// </summary>
    /// <remarks>
    /// A game currency uses the game's own symbol, which the <c>"C"</c> format cannot express — it
    /// only knows the player's locale.
    /// </remarks>
    [Serializable]
    public sealed class CurrencyConverter : IConverter<double, string>
    {
        [Tooltip("The symbol placed beside the amount.")]
        [SerializeField] private string _symbol = "$";

        [Tooltip("Which side of the amount the symbol goes on.")]
        [SerializeField] private SymbolPosition _position = SymbolPosition.Before;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals;

        [Tooltip("Separate thousands.")]
        [SerializeField] private bool _groupDigits = true;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyConverter"/> class with a leading dollar sign.
        /// </summary>
        public CurrencyConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyConverter"/> class.
        /// </summary>
        /// <param name="symbol">The symbol placed beside the amount.</param>
        /// <param name="position">Which side of the amount the symbol goes on.</param>
        /// <param name="decimals">How many decimals to show.</param>
        public CurrencyConverter(string symbol, SymbolPosition position = SymbolPosition.Before, int decimals = 0)
        {
            _symbol = symbol;
            _position = position;
            _decimals = decimals;
        }

        /// <summary>
        /// Formats the specified amount.
        /// </summary>
        /// <param name="value">The amount.</param>
        /// <returns>The formatted amount with its symbol.</returns>
        public string Convert(double value)
        {
            var format = (_groupDigits ? "N" : "F") + Math.Max(0, _decimals);
            var text = value.ToString(format, _culture.ToCultureInfo());

            return _position is SymbolPosition.Before ? _symbol + text : text + _symbol;
        }
    }

    /// <summary>
    /// Formats a number as a percentage.
    /// </summary>
    [Serializable]
    public sealed class PercentStringConverter : IConverter<float, string>
    {
        [Tooltip("The incoming value is a 0..1 fraction rather than an already-scaled percentage.")]
        [SerializeField] private bool _inputIsNormalized = true;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals;

        [Tooltip("Placed after the number.")]
        [SerializeField] private string _suffix = "%";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentStringConverter"/> class expecting a 0..1 fraction.
        /// </summary>
        public PercentStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentStringConverter"/> class.
        /// </summary>
        /// <param name="decimals">How many decimals to show.</param>
        /// <param name="inputIsNormalized">Whether the incoming value is a 0..1 fraction.</param>
        public PercentStringConverter(int decimals, bool inputIsNormalized = true)
        {
            _decimals = decimals;
            _inputIsNormalized = inputIsNormalized;
        }

        /// <summary>
        /// Formats the specified value as a percentage.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted percentage.</returns>
        public string Convert(float value)
        {
            var percent = _inputIsNormalized ? value * 100f : value;
            return percent.ToString("F" + Math.Max(0, _decimals), _culture.ToCultureInfo()) + _suffix;
        }
    }

    /// <summary>
    /// Formats a number against a maximum: "35 / 100".
    /// </summary>
    /// <remarks>
    /// Ammunition, health, quest progress. The maximum is authored, so the ViewModel exposes only the
    /// number that changes.
    /// </remarks>
    [Serializable]
    public sealed class RatioToStringConverter : IConverter<float, string>
    {
        [Tooltip("The value the number is shown against.")]
        [SerializeField] private float _max = 100f;

        [Tooltip("A composite format: {0} is the value, {1} the maximum.")]
        [SerializeField] private string _format = "{0} / {1}";

        [Tooltip("Round both numbers to whole values.")]
        [SerializeField] private bool _round = true;

        [Tooltip("The culture the numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatioToStringConverter"/> class against 100.
        /// </summary>
        public RatioToStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RatioToStringConverter"/> class.
        /// </summary>
        /// <param name="max">The value the number is shown against.</param>
        /// <param name="format">A composite format: <c>{0}</c> is the value, <c>{1}</c> the maximum.</param>
        public RatioToStringConverter(float max, string format = "{0} / {1}")
        {
            _max = max;
            _format = format;
        }

        /// <summary>
        /// Formats the specified value against the authored maximum.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted ratio.</returns>
        public string Convert(float value)
        {
            var culture = _culture.ToCultureInfo();

            return _round
                ? string.Format(culture, _format, Mathf.RoundToInt(value), Mathf.RoundToInt(_max))
                : string.Format(culture, _format, value, _max);
        }
    }

    /// <summary>
    /// Formats a number with an explicit sign: "+15", "-3".
    /// </summary>
    /// <remarks>Floating combat text, stat deltas — where the sign is the point.</remarks>
    [Serializable]
    public sealed class SignedNumberStringConverter : IConverter<float, string>
    {
        [Tooltip("A standard numeric format string applied to the magnitude.")]
        [SerializeField] private string _format = "0.##";

        [Tooltip("Show a plus on positive numbers.")]
        [SerializeField] private bool _alwaysShowSign = true;

        [Tooltip("Return an empty string for zero.")]
        [SerializeField] private bool _hideZero;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedNumberStringConverter"/> class.
        /// </summary>
        public SignedNumberStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedNumberStringConverter"/> class.
        /// </summary>
        /// <param name="format">A standard numeric format string applied to the magnitude.</param>
        /// <param name="hideZero">If <see langword="true"/>, returns an empty string for zero.</param>
        public SignedNumberStringConverter(string format, bool hideZero = false)
        {
            _format = format;
            _hideZero = hideZero;
        }

        /// <summary>
        /// Formats the specified number with its sign.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The formatted number.</returns>
        public string Convert(float value)
        {
            if (_hideZero && value == 0f) return string.Empty;

            var text = Math.Abs(value).ToString(_format, _culture.ToCultureInfo());
            if (value < 0f) return "-" + text;

            return _alwaysShowSign ? "+" + text : text;
        }
    }

    /// <summary>
    /// Pads a number to a fixed width: 7 becomes "007".
    /// </summary>
    [Serializable]
    public sealed class PaddedNumberConverter : IConverter<int, string>
    {
        [Tooltip("The minimum number of digits.")]
        [SerializeField] private int _digits = 2;

        [Tooltip("The character used for padding.")]
        [SerializeField] private char _padChar = '0';

        /// <summary>
        /// Initializes a new instance of the <see cref="PaddedNumberConverter"/> class padding to two digits.
        /// </summary>
        public PaddedNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaddedNumberConverter"/> class.
        /// </summary>
        /// <param name="digits">The minimum number of digits.</param>
        /// <param name="padChar">The character used for padding.</param>
        public PaddedNumberConverter(int digits, char padChar = '0')
        {
            _digits = digits;
            _padChar = padChar;
        }

        /// <summary>
        /// Pads the specified number.
        /// </summary>
        /// <param name="value">The number to pad.</param>
        /// <returns>The padded number. A negative number keeps its sign outside the padding.</returns>
        public string Convert(int value)
        {
            var text = Math.Abs(value).ToString(CultureInfo.InvariantCulture).PadLeft(_digits, _padChar);
            return value < 0 ? "-" + text : text;
        }
    }

    /// <summary>
    /// Formats a number as an English ordinal: 1 becomes "1st".
    /// </summary>
    [Serializable]
    public sealed class OrdinalConverter : IConverter<int, string>
    {
        /// <summary>
        /// Formats the specified number as an ordinal.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The number with its ordinal suffix.</returns>
        public string Convert(int value)
        {
            var magnitude = Math.Abs(value);

            // 11th, 12th and 13th break the last-digit rule.
            var suffix = (magnitude % 100) is >= 11 and <= 13
                ? "th"
                : (magnitude % 10) switch { 1 => "st", 2 => "nd", 3 => "rd", _ => "th" };

            return value.ToString(CultureInfo.InvariantCulture) + suffix;
        }
    }

    /// <summary>
    /// Formats a byte count as a readable size.
    /// </summary>
    [Serializable]
    public sealed class ByteSizeConverter : IConverter<long, string>
    {
        [Tooltip("Use 1024 as the step and KiB-style units rather than 1000 and KB.")]
        [SerializeField] private bool _binaryUnits = true;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals = 1;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        private static readonly string[] BinaryUnits = { "B", "KiB", "MiB", "GiB", "TiB" };
        private static readonly string[] DecimalUnits = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Formats the specified byte count.
        /// </summary>
        /// <param name="value">The number of bytes.</param>
        /// <returns>The formatted size.</returns>
        public string Convert(long value)
        {
            var units = _binaryUnits ? BinaryUnits : DecimalUnits;
            var step = _binaryUnits ? 1024d : 1000d;

            var magnitude = (double)Math.Abs(value);
            var tier = 0;

            while (magnitude >= step && tier < units.Length - 1)
            {
                magnitude /= step;
                tier++;
            }

            var format = tier == 0 ? "F0" : "F" + Math.Max(0, _decimals);
            var text = magnitude.ToString(format, _culture.ToCultureInfo());

            return (value < 0 ? "-" : string.Empty) + text + " " + units[tier];
        }
    }

    /// <summary>
    /// Formats a number as a Roman numeral.
    /// </summary>
    /// <remarks>
    /// Tiers, chapters, upgrade levels. Numbers outside 1..3999 have no numeral and come back as
    /// digits.
    /// </remarks>
    [Serializable]
    public sealed class RomanNumeralConverter : IConverter<int, string>
    {
        [Tooltip("Write the numeral in lower case.")]
        [SerializeField] private bool _lowercase;

        private static readonly int[] Values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        private static readonly string[] Numerals =
            { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        /// <summary>
        /// Formats the specified number as a Roman numeral.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The numeral, or the number in digits when it is outside 1..3999.</returns>
        public string Convert(int value)
        {
            if (value is < 1 or > 3999) return value.ToString(CultureInfo.InvariantCulture);

            var builder = new System.Text.StringBuilder();
            var remaining = value;

            for (var i = 0; i < Values.Length; i++)
                while (remaining >= Values[i])
                {
                    builder.Append(Numerals[i]);
                    remaining -= Values[i];
                }

            var text = builder.ToString();
            return _lowercase ? text.ToLowerInvariant() : text;
        }
    }
}
