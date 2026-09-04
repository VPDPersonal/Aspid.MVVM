using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that resolves one <typeparamref name="TElement"/> in a
    /// <see cref="UIDocument"/> by name or USS class.
    /// </summary>
    /// <remarks>
    /// The element is resolved lazily, since the document builds its tree in <c>OnEnable</c>; a failed lookup is
    /// reported and retried on the next access.
    /// </remarks>
    /// <typeparam name="TElement">The type of element this binder drives.</typeparam>
    public abstract class VisualElementMonoBinder<TElement> : MonoBinder
        where TElement : VisualElement
    {
        [Tooltip("Document holding the element; empty searches this object and its parents.")]
        [SerializeField] private UIDocument _document;

        [Tooltip("Element name from UXML; empty falls back to the class.")]
        [SerializeField] private string _elementName;

        [Tooltip("USS class to search by when no name is set; the first match is used.")]
        [SerializeField] private string _elementClass;

        private TElement _element;
        private bool _isResolved;

        /// <summary>
        /// Gets the resolved element, or <see langword="null"/> when it cannot be found.
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

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _element = null;
            _isResolved = false;
        }

        private TElement Resolve()
        {
            var document = _document ? _document : GetComponentInParent<UIDocument>();

            if (!document)
                return Refuse("no UIDocument is assigned or found on this object and its parents");

            if (document.rootVisualElement is not { } root)
                return Refuse("the document has no visual tree yet");

            var hasName = !string.IsNullOrWhiteSpace(_elementName);
            var hasClass = !string.IsNullOrWhiteSpace(_elementClass);

            if (!hasName && !hasClass)
                return Refuse("neither an element name nor a USS class is set");

            var element = hasName
                ? root.Q<TElement>(_elementName)
                : root.Q<TElement>(className: _elementClass);

            if (element is not null) return element;

            var query = hasName ? $"named '{_elementName}'" : $"with class '{_elementClass}'";
            return Refuse($"the document holds no {typeof(TElement).Name} {query}");
        }

        private TElement Refuse(string problem)
        {
            this.LogError(
                problem: problem,
                consequence: "The binder does nothing.");

            return null;
        }
    }
}
