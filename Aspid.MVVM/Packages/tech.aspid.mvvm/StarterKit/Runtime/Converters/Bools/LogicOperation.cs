// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The boolean operations <see cref="BoolLogicConverter"/> can apply.
    /// </summary>
    public enum LogicOperation
    {
        /// <summary>
        /// Both the bound value and the operand must be <see langword="true"/>.
        /// </summary>
        And,

        /// <summary>
        /// Either the bound value or the operand must be <see langword="true"/>.
        /// </summary>
        Or,

        /// <summary>
        /// Exactly one of the bound value and the operand must be <see langword="true"/>.
        /// </summary>
        Xor,

        /// <summary>
        /// The negation of <see cref="And"/>.
        /// </summary>
        Nand,

        /// <summary>
        /// The negation of <see cref="Or"/>.
        /// </summary>
        Nor,

        /// <summary>
        /// The negation of <see cref="Xor"/> — the two must agree.
        /// </summary>
        Xnor,
    }
}
