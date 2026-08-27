using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that resolves one <typeparamref name="TElement"/> inside a
    /// <see cref="UIDocument"/> and binds it.
    /// </summary>
    /// <remarks>
    /// The element is resolved lazily rather than when the binder is bound, since a document builds its tree in
    /// <c>OnEnable</c> and an earlier lookup would search an empty root. A successful resolution is cached; a failed
    /// one is retried on the next access, so the element is found once the document's tree exists.
    /// </remarks>
    /// <typeparam name="TElement">The type of <see cref="VisualElement"/> this binder drives.</typeparam>
    public abstract partial class VisualElementMonoBinder<TElement> : MonoBinder
        where TElement : VisualElement
    {
        [Tooltip("The document the element lives in, or empty to search this object and parents.")]
        [SerializeField] private UIDocument _document;

        [Tooltip("Name of the element, as set in UXML. Empty falls back to the class below.")]
        [SerializeField] private string _elementName;

        [Tooltip("USS class to search by when no name is given. Uses the first match.")]
        [SerializeField] private string _elementClass;

        private TElement _element;
        private bool _isResolved;

        /// <summary>
        /// Gets the resolved element, or <see langword="null"/> when it could not be found.
        /// </summary>
        protected TElement Element
        {
            get
            {
                if (_isResolved) return _element;

                _element = Resolve();
                _isResolved = _element is not null;

                return _element;
            }
        }

        /// <summary>
        /// Called when the binder is unbound. Forgets the resolved element, so a binder reused on another document does
        /// not keep driving the previous one.
        /// </summary>
        protected override void OnUnbound()
        {
            _element = null;
            _isResolved = false;
        }

        private TElement Resolve()
        {
            var document = _document ? _document : GetComponentInParent<UIDocument>();

            if (!document)
            {
                Debug.LogError($"[{GetType().Name}] No UIDocument assigned and none found on this object or its parents.", context: this);
                return null;
            }

            var root = document.rootVisualElement;

            if (root is null)
            {
                Debug.LogError($"[{GetType().Name}] The document has no visual tree yet; is its source asset assigned?", context: this);
                return null;
            }

            var hasName = !string.IsNullOrWhiteSpace(_elementName);
            var hasClass = !string.IsNullOrWhiteSpace(_elementClass);

            if (!hasName && !hasClass)
            {
                Debug.LogError($"[{GetType().Name}] Neither an element name nor a USS class is set.", context: this);
                return null;
            }

            var element = hasName
                ? root.Q<TElement>(_elementName)
                : root.Q<TElement>(className: _elementClass);

            if (element is null)
            {
                var what = hasName ? $"named '{_elementName}'" : $"with class '{_elementClass}'";
                Debug.LogError($"[{GetType().Name}] No {typeof(TElement).Name} {what} in the document.", context: this);
            }

            return element;
        }
    }
}
