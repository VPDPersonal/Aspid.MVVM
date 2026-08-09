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
    /// Tag lists, party rosters, ingredient lines. The builder is reused between calls, because a
    /// binder pushes on every notification and <c>string.Join</c> would allocate on each one.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "List To String", Tooltip = "Joins a collection into one string")]
    public sealed class ListToStringConverter<T> : IConverter<IEnumerable<T>?, string>
    {
        [Tooltip("Placed between items.")]
        [SerializeField] private string _separator = ", ";

        [Tooltip("How many items to show. Zero shows all of them.")]
        [SerializeField] private int _maxItems;

        [Tooltip("A composite format for the overflow: {0} is how many were left out.")]
        [SerializeField] private string _overflowFormat = " +{0} more";

        [Tooltip("Shown when the collection is empty.")]
        [SerializeField] private string _emptyText = string.Empty;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: joining with commas.</remarks>
        public ListToStringConverter() { }

        /// <param name="separator">Placed between items.</param>
        /// <param name="maxItems">How many items to show. Zero shows all of them.</param>
        /// <param name="emptyText">Shown when the collection is empty.</param>
        public ListToStringConverter(string separator, int maxItems = 0, string emptyText = "")
        {
            _separator = separator;
            _maxItems = maxItems;
            _emptyText = emptyText;
        }

        /// <summary>
        /// Joins the specified collection.
        /// </summary>
        /// <param name="value">The collection to join.</param>
        /// <returns>The joined text, or the empty text when there is nothing to join.</returns>
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
                _builder.Append(item);
                shown++;
            }

            if (total == 0) return _emptyText;

            var hidden = total - shown;
            if (hidden > 0 && !string.IsNullOrEmpty(_overflowFormat))
                _builder.AppendFormat(_overflowFormat, hidden);

            return _builder.ToString();
        }
    }
}
