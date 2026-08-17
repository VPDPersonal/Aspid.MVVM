// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The grammar <see cref="PluralizeConverter"/> follows when picking a form.
    /// </summary>
    public enum PluralRule
    {
        /// <summary>
        /// One form for 1, another for everything else.
        /// </summary>
        English,

        /// <summary>
        /// The Russian-style three-form rule: one, few (2-4), many — with the teens taking the many
        /// form regardless of their last digit.
        /// </summary>
        Slavic,
    }
}
