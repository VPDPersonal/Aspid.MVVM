using Aspid.FastTools.Types;
using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Joins a collection into one string.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// Tag lists, party rosters, ingredient lines. The builder is reused between calls, and so is the
    /// text it produced: a binder pushes on every notification rather than on every change, so the
    /// converter reuses the previous result when the text it builds is unchanged, where
    /// <c>string.Join</c> hands back a new string on every push.
    /// <para>
    /// Each item can carry a wrapper of its own — brackets around a tag, a bullet in front of a line.
    /// <c>string.Join</c> cannot express that without projecting the whole collection first, which is
    /// a second allocation on top of the one this converter exists to avoid.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "List To String", Tooltip = "Joins a collection into one string")]
    public sealed class ListToStringConverter<T> : IConverter<IEnumerable<T>?, string>
    {
        [Tooltip("Placed between items.")]
        [SerializeField] private string _separator = ", ";

        [Tooltip("A composite format applied to each item: {0} is the item. When empty, the item is written as it is.")]
        [SerializeField] private string _itemFormat = string.Empty;

        [Tooltip("How many items to show. Zero shows all of them.")]
        [SerializeField] private int _maxItems;

        [Tooltip("A composite format for the overflow: {0} is how many were left out.")]
        [SerializeField] private string _overflowFormat = " +{0} more";

        [Tooltip("Shown when the collection is empty.")]
        [SerializeField] private string _emptyText = string.Empty;

        [NonSerialized] private StringBuilder? _builder;
        [NonSerialized] private string? _cached;

        /// <remarks>Default: joining with commas.</remarks>
        public ListToStringConverter() { }

        /// <param name="separator">Placed between items.</param>
        /// <param name="maxItems">How many items to show. Zero shows all of them.</param>
        /// <param name="emptyText">Shown when the collection is empty.</param>
        /// <param name="itemFormat">
        /// A composite format applied to each item, where <c>{0}</c> is the item. When empty, the item
        /// is written as it is.
        /// </param>
        public ListToStringConverter(string separator, int maxItems = 0, string emptyText = "", string itemFormat = "")
        {
            _separator = separator;
            _maxItems = maxItems;
            _emptyText = emptyText;
            _itemFormat = itemFormat;
        }

        /// <summary>
        /// Joins the specified collection.
        /// </summary>
        /// <param name="value">The collection to join.</param>
        /// <returns>
        /// The joined text, or the empty text when there is nothing to join. The same string is
        /// returned again while the text being built is unchanged.
        /// </returns>
        public string Convert(IEnumerable<T>? value)
        {
            if (value is null) return _emptyText;

            _builder ??= new StringBuilder();
            _builder.Clear();

            var shown = 0;
            var total = 0;

            foreach (var item in value)
            {
                total++;
                if (_maxItems > 0 && shown >= _maxItems) continue;

                if (shown > 0) _builder.Append(_separator);

                // Not AppendFormat with a "{0}" default: it would re-parse that format for every item
                // of every push, and no format at all is the common case. T is unconstrained, so both
                // calls bind to the object overload and a value-type item is boxed on the way in.
                if (string.IsNullOrEmpty(_itemFormat)) _builder.Append(item);
                else _builder.AppendFormat(_itemFormat, item);

                shown++;
            }

            if (total == 0) return _emptyText;

            var hidden = total - shown;
            if (hidden > 0 && !string.IsNullOrEmpty(_overflowFormat))
                _builder.AppendFormat(_overflowFormat, hidden);

            return Take();
        }

        // Reusing the builder only saves its char buffer; ToString allocates a fresh string on every
        // push. Comparing what was built against the last result costs no allocation, so a push that
        // reads the same as the one before it hands back the string it already has.
        private string Take()
        {
            if (_cached is not null && Matches(_cached)) return _cached;

            _cached = _builder!.ToString();
            return _cached;
        }

        private bool Matches(string text)
        {
            var builder = _builder!;
            if (builder.Length != text.Length) return false;

            for (var i = 0; i < text.Length; i++)
                if (builder[i] != text[i])
                    return false;

            return true;
        }
    }
}
