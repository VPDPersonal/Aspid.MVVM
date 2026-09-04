using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Checks that an <see cref="Animator"/> parameter exists with the expected type and reports the first problem.
    /// </summary>
    /// <remarks>
    /// A match is cached until the controller changes. The scan uses <see cref="Animator.parameterCount"/> and
    /// <see cref="Animator.GetParameter"/> rather than the allocating <see cref="Animator.parameters"/>.
    /// </remarks>
    internal struct AnimatorParameterProbe
    {
        private bool _isUsable;
        private RuntimeAnimatorController _controller;

        /// <summary>
        /// Reports whether <paramref name="parameterName"/> can be addressed on <paramref name="animator"/>.
        /// </summary>
        /// <param name="animator">The animator the binder writes to.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="type">The expected parameter type, or <see langword="null"/> to match by name only.</param>
        /// <param name="owner">The binder asking; named in the diagnostic.</param>
        /// <returns><see langword="true"/> when the parameter can be set.</returns>
        public bool IsUsable(
            Animator animator,
            string parameterName,
            AnimatorControllerParameterType? type,
            object owner)
        {
            if (!animator)
                return Refuse("its Animator is missing or destroyed", parameterName, owner);

            if (string.IsNullOrWhiteSpace(parameterName))
                return Refuse("no parameter name is set", parameterName, owner);

            var controller = animator.runtimeAnimatorController;

            if (!controller)
                return Refuse("the Animator has no controller", parameterName, owner);

            if (!ReferenceEquals(_controller, controller))
            {
                _controller = controller;
                _isUsable = false;
            }

            if (_isUsable) return true;

            // An empty parameter list may just not be ready yet.
            if (animator.parameterCount == 0) return true;

            _isUsable = Contains(animator, parameterName, type);
            if (_isUsable) return true;

            var expected = type is null ? "parameter" : $"{type} parameter";
            return Refuse($"its controller has no {expected} by that name", parameterName, owner);
        }

        private static bool Contains(Animator animator, string parameterName, AnimatorControllerParameterType? type)
        {
            for (var i = 0; i < animator.parameterCount; i++)
            {
                var parameter = animator.GetParameter(i);
                if (parameter.name != parameterName) continue;
                if (type is null || parameter.type == type) return true;
            }

            return false;
        }

        private static bool Refuse(string reason, string parameterName, object owner)
        {
            BinderLogger.LogError(
                binderType: owner.GetType(),
                problem: reason,
                consequence: $"The animator parameter {parameterName.Describe()} is not set.",
                context: owner as Object);

            return false;
        }
    }
}
