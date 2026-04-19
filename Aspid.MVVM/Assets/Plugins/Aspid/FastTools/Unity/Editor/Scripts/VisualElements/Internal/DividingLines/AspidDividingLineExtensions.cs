using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidDividingLine"/> and related enums.
    /// </summary>
    public static class AspidDividingLineExtensions
    {
        #region Set
        /// <summary>
        /// Sets <see cref="AspidDividingLine.Theme"/> and returns the element for chaining.
        /// </summary>
        public static AspidDividingLine SetTheme(this AspidDividingLine element, ThemeStyle value)
        {
            element.Theme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLine.Status"/> and returns the element for chaining.
        /// </summary>
        public static AspidDividingLine SetStatus(this AspidDividingLine element, StatusStyle value)
        {
            element.Status = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLine.Size"/> and returns the element for chaining.
        /// </summary>
        public static AspidDividingLine SetSize(this AspidDividingLine element, DividingLineSize value)
        {
            element.Size = value;
            return element;
        }
        #endregion

        #region ToUss
        /// <summary>
        /// Returns the USS class name for the given <see cref="DividingLineSize"/>,
        /// or <see langword="null"/> for <see cref="DividingLineSize.None"/>.
        /// </summary>
        public static string ToUss(this DividingLineSize size) => size switch
        {
            DividingLineSize.None => null,
            DividingLineSize.Thin => StyleClasses.DividingLine.Thin,
            DividingLineSize.Medium => StyleClasses.DividingLine.Medium,
            DividingLineSize.Bold => StyleClasses.DividingLine.Bold,
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };

        /// <summary>
        /// Returns the USS class name for the given <see cref="DividingLineDirection"/>.
        /// </summary>
        public static string ToUss(this DividingLineDirection direction) => direction switch
        {
            DividingLineDirection.Vertical => StyleClasses.DividingLine.Vertical,
            DividingLineDirection.Horizontal => StyleClasses.DividingLine.Horizontal,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        #endregion
    }
}
