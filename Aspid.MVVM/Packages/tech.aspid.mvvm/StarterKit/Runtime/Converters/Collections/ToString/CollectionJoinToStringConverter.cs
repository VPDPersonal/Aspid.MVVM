using System;
using System.Text;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Joins a collection into one string.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// A <see langword="null"/> item renders as nothing, leaving a hole between two separators — "a, , b".
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To String",
        Name = "Join",
        Tooltip = "Joins a collection into one string")]
    public class CollectionJoinToStringConverter<T> : IConverter<IEnumerable<T?>?, string>
    {
        [Tooltip("Placed between items.")]
        [SerializeField] private string _separator = ", ";

        [Tooltip("Writes each item. When empty, the item is written as it is.")]
        [TypeSelector]
        [SerializeReference] private IConverter<T?, string?>? _item = new GenericToStringConverter<T?>();

        [Tooltip("How many items to show. Zero shows all of them.")]
        [SerializeField] [Min(0)] private int _maxItems;

        [Tooltip("Composite format for the overflow: {0} is how many were left out. When blank, nothing is added.")]
        [SerializeField] private string _overflowFormat = " +{0} more";

        [Tooltip("Shown when the collection is empty.")]
        [SerializeField] private string _emptyText = string.Empty;

        [NonSerialized] private string? _cached;
        [NonSerialized] private StringBuilder _builder = new();

        /// <remarks>Default: joining with commas.</remarks>
        public CollectionJoinToStringConverter() { }

        /// <param name="separator">Placed between items.</param>
        /// <param name="maxItems">How many items to show. Zero shows all of them.</param>
        /// <param name="emptyText">Shown when the collection is empty.</param>
        /// <param name="item">
        /// Writes each item. When omitted, the item is written with <see cref="object.ToString"/>.
        /// </param>
        public CollectionJoinToStringConverter(
            string separator,
            int maxItems = 0,
            string emptyText = "",
            IConverter<T?, string?>? item = null)
        {
            _maxItems = maxItems;
            _emptyText = emptyText;
            _separator = separator;
            _item = item ?? _item;
        }

        /// <summary>
        /// Joins the specified collection.
        /// </summary>
        /// <param name="value">The collection to join.</param>
        /// <returns>
        /// The joined text, or the empty text when there is nothing to join.
        /// An invalid overflow format is reported and the overflow left out.
        /// </returns>
        public string Convert(IEnumerable<T?>? value)
        {
            if (value is null)
                return _emptyText;

            var shown = 0;
            var total = 0;
            _builder.Clear();

            foreach (var item in value)
            {
                total++;
                if (_maxItems > 0 && shown >= _maxItems) continue;

                if (shown > 0) _builder.Append(_separator);

                if (_item is null) _builder.Append(item);
                else _builder.Append(_item.Convert(item));

                shown++;
            }

            if (total == 0) return _emptyText;

            var hidden = total - shown;
            if (hidden > 0 && !string.IsNullOrWhiteSpace(_overflowFormat))
                AppendOverflow(_builder, hidden);

            return Take(_builder);
        }

        // AppendFormat can have written part of the format before the parse fails, so the rollback
        // point is captured first and the builder is cut back to it on the way out.
        private void AppendOverflow(StringBuilder builder, int hidden)
        {
            var mark = builder.Length;

            try
            {
                builder.AppendFormat(_overflowFormat, hidden);
            }
            catch (FormatException exception)
            {
                builder.Length = mark;

                this.LogError(
                    problem: $"\"{_overflowFormat}\" is not a composite format ({exception.Message})",
                    consequence: "Leaving the overflow out.");
            }
        }

        private string Take(StringBuilder builder)
        {
            if (_cached is not null && Matches(builder, _cached))
                return _cached;

            _cached = builder.ToString();
            return _cached;
        }

        private static bool Matches(StringBuilder builder, string text)
        {
            if (builder.Length != text.Length)
                return false;

            for (var i = 0; i < text.Length; i++)
            {
                if (builder[i] != text[i])
                    return false;
            }

            return true;
        }
    }
}
