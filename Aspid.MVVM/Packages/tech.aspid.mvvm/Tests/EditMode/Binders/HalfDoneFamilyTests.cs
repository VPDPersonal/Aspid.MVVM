using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the properties their own domains had left out: <see cref="Scrollbar.size"/>,
    /// <see cref="ScrollRect.normalizedPosition"/>, <see cref="SpriteRenderer.size"/>,
    /// <see cref="Rigidbody.constraints"/>, <see cref="Rigidbody2D.bodyType"/> and the two
    /// <see cref="ParticleSystem"/> module values.
    /// </summary>
    /// <remarks>
    /// Each of these domains shipped with most of its properties bound and one or two missing, so a project would
    /// find the family, reach for the property it came for, and not find it.
    /// </remarks>
    [TestFixture]
    public sealed class HalfDoneFamilyTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        #region Scrollbar.size
        [Test]
        public void ScrollbarSize_ReachesTheScrollbar()
        {
            var scrollbar = NewGameObject("Scrollbar").AddComponent<Scrollbar>();
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.25f);

            Assert.AreEqual(0.25f, scrollbar.size, 0.001f, "Размер ручки не доехал до скроллбара");
        }

        [Test]
        public void ScrollbarSize_OutsideTheRange_IsClamped()
        {
            var scrollbar = NewGameObject("Scrollbar").AddComponent<Scrollbar>();
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);

            Assert.AreEqual(1f, scrollbar.size, 0.001f, "Размер вне 0..1 не обрезан");
        }

        [Test]
        public void ScrollbarSize_NonFinite_DoesNotReachTheScrollbar()
        {
            var scrollbar = NewGameObject("Scrollbar").AddComponent<Scrollbar>();
            var binder = scrollbar.gameObject.AddComponent<ScrollbarSizeMonoBinder>();

            ((IBinder<float>)binder).SetValue(float.NaN);

            Assert.IsFalse(float.IsNaN(scrollbar.size), "NaN дошёл до скроллбара");
        }
        #endregion

        #region ScrollRect.normalizedPosition
        [Test]
        public void ScrollRectNormalizedPosition_MovesBothAxesAtOnce()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectNormalizedPositionMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(1f, 1f));

            Assert.AreEqual(1f, scrollRect.horizontalNormalizedPosition, 0.001f, "Горизонтальная позиция не доехала");
            Assert.AreEqual(1f, scrollRect.verticalNormalizedPosition, 0.001f, "Вертикальная позиция не доехала");
        }

        [Test]
        public void ScrollRectNormalizedPosition_ClampsEachAxisSeparately()
        {
            var scrollRect = NewScrollRect();
            var binder = scrollRect.gameObject.AddComponent<ScrollRectNormalizedPositionMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(5f, float.NaN));

            Assert.AreEqual(1f, scrollRect.horizontalNormalizedPosition, 0.001f, "Позиция вне 0..1 не обрезана");
            Assert.IsFalse(float.IsNaN(scrollRect.verticalNormalizedPosition), "NaN дошёл до ScrollRect");
        }

        /// <summary>
        /// The reason this binder is on the Vector2 base: a Vector3 base would report a third component the
        /// property has not got.
        /// </summary>
        [Test]
        public void ScrollRectNormalizedPosition_ReportsAVector2Back()
        {
            var scrollRect = NewScrollRect();
            var binder = new ScrollRectNormalizedPositionBinder(scrollRect, mode: BindMode.OneWayToSource);

            var received = default(Vector2);
            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(scrollRect.normalizedPosition, received, "ViewModel получила не ту позицию, что в ScrollRect");
        }
        #endregion

        #region SpriteRenderer.size
        [Test]
        public void SpriteRendererSize_ReachesTheRenderer()
        {
            var renderer = NewGameObject("Sprite").AddComponent<SpriteRenderer>();
            renderer.drawMode = SpriteDrawMode.Sliced;

            var binder = renderer.gameObject.AddComponent<SpriteRendererSizeMonoBinder>();
            ((IBinder<Vector2>)binder).SetValue(new Vector2(3f, 4f));

            Assert.AreEqual(new Vector2(3f, 4f), renderer.size, "Размер не доехал до рендерера");
        }

        [Test]
        public void SpriteRendererSize_NegativeAndNonFinite_AreClampedToZero()
        {
            var renderer = NewGameObject("Sprite").AddComponent<SpriteRenderer>();
            renderer.drawMode = SpriteDrawMode.Sliced;

            var binder = renderer.gameObject.AddComponent<SpriteRendererSizeMonoBinder>();
            ((IBinder<Vector2>)binder).SetValue(new Vector2(-2f, float.NaN));

            Assert.AreEqual(Vector2.zero, renderer.size, "Отрицательный или нефинитный размер не обрезан");
        }
        #endregion

        #region Physics
        [Test]
        public void RigidbodyConstraints_ReachTheBody()
        {
            var body = NewGameObject("Body").AddComponent<Rigidbody>();
            var binder = body.gameObject.AddComponent<RigidbodyConstraintsMonoBinder>();

            ((IBinder<RigidbodyConstraints>)binder).SetValue(
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation);

            Assert.AreEqual(RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation,
                body.constraints, "Маска ограничений не доехала до тела");
        }

        [Test]
        public void RigidbodyConstraints_OneWayToSource_ReportsTheCurrentMask()
        {
            var body = NewGameObject("Body").AddComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezePositionZ;

            var binder = new RigidbodyConstraintsBinder(body, BindMode.OneWayToSource);
            var received = default(RigidbodyConstraints);

            binder.Bind(new OneWayToSourceStructBindableMember<RigidbodyConstraints>(value => received = value));

            Assert.AreEqual(RigidbodyConstraints.FreezePositionZ, received, "ViewModel не получила текущую маску");
        }

        [Test]
        public void Rigidbody2DBodyType_ReachesTheBody()
        {
            var body = NewGameObject("Body2D").AddComponent<Rigidbody2D>();
            var binder = body.gameObject.AddComponent<Rigidbody2DBodyTypeMonoBinder>();

            ((IBinder<RigidbodyType2D>)binder).SetValue(RigidbodyType2D.Static);

            Assert.AreEqual(RigidbodyType2D.Static, body.bodyType, "Тип тела не доехал");
        }
        #endregion

        #region ParticleSystem modules
        [Test]
        public void ParticleSystemEmissionRate_ReachesTheModule()
        {
            var particles = NewGameObject("Particles").AddComponent<ParticleSystem>();
            var binder = particles.gameObject.AddComponent<ParticleSystemEmissionRateMonoBinder>();

            ((IBinder<float>)binder).SetValue(42f);

            Assert.AreEqual(42f, particles.emission.rateOverTimeMultiplier, 0.001f, "Частота эмиссии не доехала");
        }

        [Test]
        public void ParticleSystemEmissionRate_NegativeAndNonFinite_AreClampedToZero()
        {
            var particles = NewGameObject("Particles").AddComponent<ParticleSystem>();
            var binder = particles.gameObject.AddComponent<ParticleSystemEmissionRateMonoBinder>();

            ((IBinder<float>)binder).SetValue(-5f);
            Assert.AreEqual(0f, particles.emission.rateOverTimeMultiplier, 0.001f, "Отрицательная частота не обрезана");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(particles.emission.rateOverTimeMultiplier), "NaN дошёл до модуля");
        }

        [Test]
        public void ParticleSystemStartColor_ReachesTheModule()
        {
            var particles = NewGameObject("Particles").AddComponent<ParticleSystem>();
            var binder = particles.gameObject.AddComponent<ParticleSystemStartColorMonoBinder>();

            ((IBinder<Color>)binder).SetValue(Color.red);

            Assert.AreEqual(Color.red, particles.main.startColor.color, "Стартовый цвет не доехал");
        }
        #endregion

        #region The serializable twins
        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var scrollbar = NewGameObject("Scrollbar").AddComponent<Scrollbar>();
            var renderer = NewGameObject("Sprite").AddComponent<SpriteRenderer>();
            var body = NewGameObject("Body").AddComponent<Rigidbody>();
            var body2D = NewGameObject("Body2D").AddComponent<Rigidbody2D>();
            var particles = NewGameObject("Particles").AddComponent<ParticleSystem>();

            Assert.IsTrue(new ScrollbarSizeBinder(scrollbar).IsBind);
            Assert.IsTrue(new SpriteRendererSizeBinder(renderer).IsBind);
            Assert.IsTrue(new RigidbodyConstraintsBinder(body).IsBind);
            Assert.IsTrue(new Rigidbody2DBodyTypeBinder(body2D).IsBind);
            Assert.IsTrue(new ParticleSystemEmissionRateBinder(particles).IsBind);
            Assert.IsTrue(new ParticleSystemStartColorBinder(particles).IsBind);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// A ScrollRect reports a position only once it has content and a viewport to measure against.
        /// </summary>
        private ScrollRect NewScrollRect()
        {
            var gameObject = NewGameObject("ScrollRect");
            var scrollRect = gameObject.AddComponent<ScrollRect>();

            var viewport = NewGameObject("Viewport").AddComponent<RectTransform>();
            viewport.SetParent(gameObject.transform, worldPositionStays: false);
            viewport.sizeDelta = new Vector2(100f, 100f);

            var content = NewGameObject("Content").AddComponent<RectTransform>();
            content.SetParent(viewport, worldPositionStays: false);
            content.sizeDelta = new Vector2(500f, 500f);

            scrollRect.viewport = viewport;
            scrollRect.content = content;

            return scrollRect;
        }

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
        #endregion
    }
}
