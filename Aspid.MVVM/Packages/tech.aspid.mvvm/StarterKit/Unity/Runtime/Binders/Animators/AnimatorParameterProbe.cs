using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Per-binder check that an <see cref="Animator"/> parameter identified by name actually exists and has the
    /// expected type, so a binder can refuse to address one that does not.
    /// </summary>
    /// <remarks>
    /// Only a <em>match</em> is cached, and only for as long as the controller stays the same, so swapping in a
    /// controller that does have the parameter starts working on its own. The scan avoids
    /// <see cref="Animator.parameters"/>, which allocates an array on every read, using
    /// <see cref="Animator.parameterCount"/> and <see cref="Animator.GetParameter"/> instead.
    /// </remarks>
    internal struct AnimatorParameterProbe
    {
        private bool _isUsable;
        private RuntimeAnimatorController _controller;

        /// <summary>
        /// Reports whether <paramref name="parameterName"/> can be addressed on <paramref name="animator"/>,
        /// describing the first problem it finds.
        /// </summary>
        /// <param name="animator">The animator the binder writes to.</param>
        /// <param name="parameterName">The serialized parameter name.</param>
        /// <param name="type">
        /// The parameter type the calling binder sets, or <see langword="null"/> when the binder cannot say —
        /// the name is then matched on its own.
        /// </param>
        /// <param name="owner">The binder asking; used to name the source in any diagnostic.</param>
        public bool IsUsable(Animator animator, string parameterName, AnimatorControllerParameterType? type, object owner)
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

            // An animator that reports no parameters at all cannot be told apart from one whose list is not ready,
            // so say nothing and let the call through — exactly as before this check existed.
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

        /// <summary>
        /// Logs the problem found for this binder and returns <see langword="false"/>.
        /// </summary>
        private static bool Refuse(string reason, string parameterName, object owner)
        {
            var name = string.IsNullOrWhiteSpace(parameterName) ? "<empty>" : parameterName;

            Debug.LogError(
                $"[{owner?.GetType().Name ?? "Animator binder"}] Animator parameter '{name}' is not set because " +
                $"{reason}.",
                owner as Object);

            return false;
        }
    }
}
