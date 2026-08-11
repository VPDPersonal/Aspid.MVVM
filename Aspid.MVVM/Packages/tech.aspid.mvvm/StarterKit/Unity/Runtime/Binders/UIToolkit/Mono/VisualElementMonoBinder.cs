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
    /// The runtime layer was uGUI and TextMeshPro from end to end, so a project on the stack Unity itself recommends
    /// could not use the framework at all. This is the piece everything else in the UI Toolkit family stands on.
    /// <para/>
    /// The element is looked up lazily rather than when the binder is bound. A document builds its tree in
    /// <c>OnEnable</c>, and a View that binds from <c>Awake</c> would otherwise search an empty root — the failure would
    /// look like a wrong name.
    /// <para/>
    /// A missing document, a blank name and a name that matches nothing are each reported once. A lookup that failed is
    /// not retried every value: a UI Toolkit query walks the tree, and an error per value would cost more than the
    /// binding it is complaining about.
    /// </remarks>
    /// <typeparam name="TElement">The type of <see cref="VisualElement"/> this binder drives.</typeparam>
    public abstract partial class VisualElementMonoBinder<TElement> : MonoBinder
        where TElement : VisualElement
    {
        [Tooltip("The document the element lives in. Left empty, the binder looks for one on this object or its parents.")]
        [SerializeField] private UIDocument _document;

        [Tooltip("Name of the element, as set in UXML. Leave empty to use the class name below instead.")]
        [SerializeField] private string _elementName;

        [Tooltip("USS class to search by when no name is given. The first element of the right type carrying it is used.")]
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

                _isResolved = true;
                _element = Resolve();

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
