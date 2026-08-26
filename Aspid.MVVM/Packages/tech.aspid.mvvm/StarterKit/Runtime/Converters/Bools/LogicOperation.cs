// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The boolean operations <see cref="BoolLogicConverter"/> can apply.
    /// </summary>
    /// <remarks>
    /// The order is the serialized value — append new members, never insert or move them.
    /// </remarks>
    public enum LogicOperation
    {
        /// <summary>
        /// Both the bound value and the operand must be <see langword="true"/>.
        /// </summary>
        And,

        /// <summary>
        /// At least one of the two must be <see langword="true"/>.
        /// </summary>
        Or,

        /// <summary>
        /// Exactly one of the two must be <see langword="true"/>.
        /// </summary>
        Xor,

        /// <summary>
        /// At least one of the two must be <see langword="false"/>.
        /// </summary>
        Nand,

        /// <summary>
        /// Both must be <see langword="false"/>.
        /// </summary>
        Nor,

        /// <summary>
        /// The two must be the same.
        /// </summary>
        Xnor,
    }
}
