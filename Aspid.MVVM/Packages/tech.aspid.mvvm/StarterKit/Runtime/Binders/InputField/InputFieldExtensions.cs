using TMPro;
using System;
using UnityEngine.Events;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Event and number helpers shared by the <see cref="TMP_InputField"/> binders.
    /// </summary>
    public static class InputFieldExtensions
    {
        /// <summary>
        /// Returns the <see cref="TMP_InputField"/> event selected by <paramref name="updateEvent"/>.
        /// </summary>
        /// <param name="field">The field whose event is returned.</param>
        /// <param name="updateEvent">The event to select.</param>
        /// <returns>The selected event.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="updateEvent"/> is unknown.</exception>
        public static UnityEvent<string> GetEvent(this TMP_InputField field, UpdateInputFieldEvent updateEvent) =>
            updateEvent switch
        {
            UpdateInputFieldEvent.OnValueChanged => field.onValueChanged,
            UpdateInputFieldEvent.OnEndEdit => field.onEndEdit,
            UpdateInputFieldEvent.OnSubmit => field.onSubmit,
            UpdateInputFieldEvent.OnSelect => field.onSelect,
            UpdateInputFieldEvent.OnDeselect => field.onDeselect,
            _ => throw new ArgumentOutOfRangeException(nameof(updateEvent), updateEvent, null)
        };

        /// <summary>
        /// Removes <paramref name="listener"/> from every event <see cref="UpdateInputFieldEvent"/> can select.
        /// </summary>
        /// <remarks>
        /// Used when the selected event may have changed since the listener was added.
        /// </remarks>
        /// <param name="field">The field whose events are cleaned.</param>
        /// <param name="listener">The listener to remove.</param>
        public static void RemoveListenerFromAll(this TMP_InputField field, UnityAction<string> listener)
        {
            field.onValueChanged.RemoveListener(listener);
            field.onEndEdit.RemoveListener(listener);
            field.onSubmit.RemoveListener(listener);
            field.onSelect.RemoveListener(listener);
            field.onDeselect.RemoveListener(listener);
        }

        /// <summary>
        /// Parses <paramref name="text"/> and raises it on the numeric channels when the field holds a number.
        /// </summary>
        /// <remarks>
        /// Only <see cref="TMP_InputField.ContentType.IntegerNumber"/> and
        /// <see cref="TMP_InputField.ContentType.DecimalNumber"/> fields report numbers. An integer channel receives
        /// a <see langword="long"/> when the text fits one, since a <see langword="double"/> loses integer precision
        /// past 2^53.
        /// </remarks>
        /// <param name="field">The field the text came from.</param>
        /// <param name="channel">The numeric channels to raise.</param>
        /// <param name="text">The text to parse.</param>
        /// <param name="culture">The culture the text is parsed with.</param>
        public static void RaiseNumber(
            this TMP_InputField field,
            ref NumberReverseChannel channel,
            string text,
            CultureInfoMode culture)
        {
            var isNumeric = field.contentType
                is TMP_InputField.ContentType.IntegerNumber
                or TMP_InputField.ContentType.DecimalNumber;

            if (!isNumeric) return;
            if (channel is { HasIntegerListeners: false, HasFloatingPointListeners: false }) return;

            var cultureInfo = culture.ToCultureInfo();
            if (!double.TryParse(text, NumberStyles.Any, cultureInfo, out var number)) return;

            if (channel.HasIntegerListeners)
            {
                if (long.TryParse(text, NumberStyles.Any, cultureInfo, out var integer)) channel.RaiseIntegers(integer);
                else channel.RaiseIntegers(number);
            }

            if (channel.HasFloatingPointListeners) channel.RaiseFloatingPoint(number);
        }
    }
}
