using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Detects double-clicks based on the elapsed time between consecutive calls to <see cref="Detect"/>.
    /// Uses <see cref="EditorApplication.timeSinceStartup"/>, so it is editor-only and does not require frame updates.
    /// </summary>
    internal struct DoubleClickTracker
    {
        private const float DefaultThresholdSeconds = 0.3f;

        private float _lastClickTime;

        /// <summary>
        /// Records a click and returns whether it qualifies as the second click of a double-click within
        /// <paramref name="thresholdSeconds"/> of the previous one. The first call always returns <see langword="false"/>.
        /// </summary>
        /// <param name="thresholdSeconds">Maximum gap between two clicks for them to count as a double-click.</param>
        /// <returns><see langword="true"/> if this click pairs with the previous one as a double-click; otherwise <see langword="false"/>.</returns>
        public bool Detect(float thresholdSeconds = DefaultThresholdSeconds)
        {
            var currentTime = (float)EditorApplication.timeSinceStartup;
            var isDouble = _lastClickTime > 0f && currentTime - _lastClickTime < thresholdSeconds;
            _lastClickTime = currentTime;
            return isDouble;
        }
    }
}
