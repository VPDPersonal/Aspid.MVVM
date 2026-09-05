import React, { useEffect, useRef } from 'react';
import Link from '@docusaurus/Link';
import Layout from '@theme/Layout';
import CodeBlock from '@theme/CodeBlock';
import Translate, { translate } from '@docusaurus/Translate';
import styles from './index.module.css';

const VIEW_MODEL = `[ViewModel]
public sealed partial class CounterViewModel
{
    [Bind] private int _count;

    [RelayCommand]
    private void Increment() => Count++;
}`;

const VIEW = `[View]
public sealed partial class CounterView : MonoView
{
    [RequireBinder(typeof(int))]
    [SerializeField] private MonoBinder[] _count;

    [RequireBinder(typeof(IRelayCommand))]
    [SerializeField] private MonoBinder[] _incrementCommand;
}`;

const BEFORE = `public class CounterViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private int _count;

    public int Count
    {
        get => _count;
        set
        {
            if (_count == value) return;
            _count = value;
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Count)));
        }
    }

    public ICommand IncrementCommand { get; }

    public CounterViewModel()
    {
        IncrementCommand = new RelayCommand(() => Count++);
    }
}`;

const ATTRIBUTES = ['[ViewModel]', '[Bind]', '[RelayCommand]', '[View]', '[RequireBinder]'];
const MODES = ['OneWay', 'TwoWay', 'OneTime', 'OneWayToSource'];

const Icon = {
  Bolt: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M13 2 3 14h9l-1 8 10-12h-9l1-8z" />
    </svg>
  ),
  Tag: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M20 12l-8 8-9-9V3h8l9 9z" />
      <circle cx="7.5" cy="7.5" r="1.5" />
    </svg>
  ),
  Arrows: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M4 7h14M14 3l4 4-4 4M20 17H6M10 13l-4 4 4 4" />
    </svg>
  ),
  Box: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M21 8 12 3 3 8v8l9 5 9-5V8z" />
      <path d="M3 8l9 5 9-5M12 13v8" />
    </svg>
  ),
  List: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M8 6h13M8 12h13M8 18h13M3 6h.01M3 12h.01M3 18h.01" />
    </svg>
  ),
  Shield: () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
      <path d="m9 12 2 2 4-4" />
    </svg>
  ),
};

const FEATURES = [
  {
    icon: <Icon.Bolt />,
    title: <Translate id="home.feature.generator.title">Source Generator, not reflection</Translate>,
    body: (
      <Translate id="home.feature.generator.body">
        Bindings are emitted at compile time as direct calls. No reflection, no boxing, no string lookups per frame.
      </Translate>
    ),
  },
  {
    icon: <Icon.Tag />,
    title: <Translate id="home.feature.attributes.title">Attributes instead of boilerplate</Translate>,
    body: (
      <Translate id="home.feature.attributes.body">
        A field with [Bind] becomes a notifying property. A method with [RelayCommand] becomes a command. Nothing else to write.
      </Translate>
    ),
  },
  {
    icon: <Icon.Arrows />,
    title: <Translate id="home.feature.modes.title">Four binding modes</Translate>,
    body: (
      <Translate id="home.feature.modes.body">
        OneWay, TwoWay, OneTime and OneWayToSource. The ViewModel sets the upper bound, every binder picks its own mode in the Inspector.
      </Translate>
    ),
  },
  {
    icon: <Icon.Box />,
    title: <Translate id="home.feature.starterkit.title">StarterKit out of the box</Translate>,
    body: (
      <Translate id="home.feature.starterkit.body">
        Hundreds of binders for uGUI, TextMeshPro, Transform, Animator, AudioSource and more, plus a catalogue of 190+ converters.
      </Translate>
    ),
  },
  {
    icon: <Icon.List />,
    title: <Translate id="home.feature.collections.title">Observable collections</Translate>,
    body: (
      <Translate id="home.feature.collections.body">
        ObservableList, FilteredList and CreateSync keep model and ViewModel lists in step; virtualized lists render only what is visible.
      </Translate>
    ),
  },
  {
    icon: <Icon.Shield />,
    title: <Translate id="home.feature.analyzers.title">Analyzers that catch mistakes early</Translate>,
    body: (
      <Translate id="home.feature.analyzers.body">
        Roslyn analyzers flag a missing partial, a wrong CanExecute signature or a field used where the property was meant, with code fixes.
      </Translate>
    ),
  },
];

const STEPS = [
  ['01', 'Counter', '/tutorials/counter', <Translate id="home.path.counter">One field, one command, one binder.</Translate>],
  ['02', 'Greeter', '/tutorials/greeter', <Translate id="home.path.greeter">Two-way text input with converters.</Translate>],
  ['03', 'Bind Modes', '/tutorials/bind-modes', <Translate id="home.path.bindModes">All four modes on one screen.</Translate>],
  ['04', 'Stats', '/tutorials/stats', <Translate id="home.path.stats">Derived values and formatting.</Translate>],
  ['05', 'Todo List', '/tutorials/todo-list', <Translate id="home.path.todoList">Observable collections and item views.</Translate>],
  ['06', 'Custom Binder', '/tutorials/custom-binder', <Translate id="home.path.customBinder">Write your own binder in a few lines.</Translate>],
];

/** Adds `is-visible` to every `[data-reveal]` descendant once it scrolls into view. */
function useReveal() {
  const ref = useRef(null);
  useEffect(() => {
    const root = ref.current;
    if (!root || typeof IntersectionObserver === 'undefined') return undefined;
    const targets = root.querySelectorAll('[data-reveal]');
    if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
      targets.forEach((el) => el.classList.add(styles.visible));
      return undefined;
    }
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (!entry.isIntersecting) return;
          entry.target.classList.add(styles.visible);
          observer.unobserve(entry.target);
        });
      },
      { threshold: 0.05 },
    );
    targets.forEach((el) => observer.observe(el));
    return () => observer.disconnect();
  }, []);
  return ref;
}

function CodeCard({ title, code, className = '', highlight = false }) {
  return (
    <div className={`${styles.codeCard} ${highlight ? styles.codeCardHighlight : ''} ${className}`}>
      <div className={styles.codeTab}>
        <span className={styles.codeDot} />
        {title}
      </div>
      <CodeBlock language="csharp">{code}</CodeBlock>
    </div>
  );
}

function SectionHead({ kicker, title, body, wide = false }) {
  return (
    <div className={`${styles.sectionHead} ${wide ? styles.sectionHeadWide : ''}`} data-reveal>
      <div className={styles.kicker}>{kicker}</div>
      <h2 className={styles.sectionTitle}>{title}</h2>
      <p className={styles.sectionSubtitle}>{body}</p>
    </div>
  );
}

function Hero() {
  return (
    <header className={styles.hero}>
      <div className={styles.heroGlow} aria-hidden="true" />
      <div className={styles.heroGrid} aria-hidden="true" />
      <div className="container">
        <div className={styles.heroLayout}>
          <div className={styles.heroText}>
            <span className={styles.badge}>
              <span className={styles.badgeDot} />
              <Translate id="home.badge">Unity 2022.3+ · Source Generators · MIT</Translate>
            </span>
            <h1 className={styles.title}>
              <Translate id="home.title.line1">MVVM for Unity</Translate>
              <br />
              <span className={styles.titleAccent}>
                <Translate id="home.title.line2">without the boilerplate</Translate>
              </span>
            </h1>
            <p className={styles.subtitle}>
              <Translate id="home.subtitle">
                A field becomes a notifying property. A method becomes a command. Bindings compile to direct calls: zero reflection, zero string lookups.
              </Translate>
            </p>
            <div className={styles.actions}>
              <Link className={`button button--lg ${styles.primary}`} to="/docs/getting-started">
                <Translate id="home.cta.start">Get Started</Translate>
              </Link>
              <Link className={`button button--lg ${styles.secondary}`} to="/tutorials/counter">
                <Translate id="home.cta.tutorials">Tutorials</Translate>
              </Link>
              <code className={styles.install}>openupm add tech.aspid.mvvm</code>
            </div>
          </div>
          <div className={styles.heroCode}>
            <CodeCard title="CounterViewModel.cs" code={VIEW_MODEL} className={styles.heroCardFirst} />
            <CodeCard title="CounterView.cs" code={VIEW} className={styles.heroCardSecond} />
          </div>
        </div>
      </div>
    </header>
  );
}

function Stats() {
  const stats = [
    ['0', <Translate id="home.stat.reflection">reflection in bindings</Translate>],
    ['190+', <Translate id="home.stat.converters">converters in StarterKit</Translate>],
    ['600+', <Translate id="home.stat.binders">ready-made binders</Translate>],
    ['4', <Translate id="home.stat.modes">binding modes</Translate>],
  ];
  return (
    <section className={styles.stats}>
      <div className="container">
        <div className={styles.statsGrid} data-reveal>
          {stats.map(([value, label]) => (
            <div key={value} className={styles.stat}>
              <div className={styles.statValue}>{value}</div>
              <div className={styles.statLabel}>{label}</div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}

function BeforeAfter() {
  return (
    <section className={styles.section}>
      <div className="container">
        <SectionHead
          kicker={<Translate id="home.beforeAfter.kicker">Before / After</Translate>}
          title={<Translate id="home.beforeAfter.title">Twenty-six lines become seven.</Translate>}
          body={
            <Translate id="home.beforeAfter.body">
              The generator writes the property, the change notification and the command. You keep the field and the method.
            </Translate>
          }
        />
        <div className={styles.beforeAfterGrid} data-reveal>
          <CodeCard
            title={translate({ id: 'home.beforeAfter.before', message: 'Hand-written INotifyPropertyChanged' })}
            code={BEFORE}
            className={styles.codeCardDimmed}
          />
          <div className={styles.afterColumn}>
            <CodeCard title={translate({ id: 'home.beforeAfter.after', message: 'With Aspid.MVVM' })} code={VIEW_MODEL} highlight />
            <div className={styles.chips}>
              {ATTRIBUTES.map((attribute) => (
                <code key={attribute} className={styles.chip}>
                  {attribute}
                </code>
              ))}
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

function FlowNode({ title, sub, x, highlight = false }) {
  return (
    <g transform={`translate(${x}, 60)`} className={highlight ? styles.flowNodeHighlight : styles.flowNode}>
      <rect width="300" height="112" rx="16" />
      <text x="150" y="50" textAnchor="middle" className={styles.flowTitle}>
        {title}
      </text>
      <text x="150" y="78" textAnchor="middle" className={styles.flowSub}>
        {sub}
      </text>
    </g>
  );
}

function FlowArrow({ x1, x2, y, label, dashed = false }) {
  return (
    <g className={dashed ? styles.flowArrowBack : styles.flowArrow}>
      <line x1={x1} y1={y} x2={x2} y2={y} strokeDasharray={dashed ? '6 6' : undefined} markerEnd="url(#home-arrow)" />
      <text x={(x1 + x2) / 2} y={y - 10} textAnchor="middle">
        {label}
      </text>
    </g>
  );
}

function Flow() {
  return (
    <section className={styles.section}>
      <div className="container">
        <SectionHead
          wide
          kicker={<Translate id="home.flow.kicker">How it flows</Translate>}
          title={<Translate id="home.flow.title">View, Binder, ViewModel. Nothing in between.</Translate>}
          body={
            <Translate id="home.flow.body">
              A binder is a MonoBehaviour on the UI element. The generated ViewModel hands it a typed member: no dictionary, no reflection, no string keys at runtime.
            </Translate>
          }
        />
        <div className={styles.flowCard} data-reveal>
          <svg className={styles.flowSvg} viewBox="0 0 1200 250" role="img" aria-label="View, Binder and ViewModel connected by typed bindings">
            <defs>
              <marker id="home-arrow" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="8" markerHeight="8" orient="auto-start-reverse">
                <path d="M0 0L10 5 0 10z" fill="context-stroke" />
              </marker>
            </defs>
            <FlowNode title="View" sub="MonoView · Unity scene" x={30} />
            <FlowNode title="Binder" sub="TextBinder · SliderBinder · …" x={450} highlight />
            <FlowNode title="ViewModel" sub="[ViewModel] partial class" x={870} />
            <FlowArrow x1={338} x2={442} y={98} label="SetValue(T)" />
            <FlowArrow x1={442} x2={338} y={136} label="ValueChanged" dashed />
            <FlowArrow x1={758} x2={862} y={98} label="IBinder<T>" />
            <FlowArrow x1={862} x2={758} y={136} label="OneWayToSource" dashed />
          </svg>
          <div className={styles.modes}>
            {MODES.map((mode, index) => (
              <span key={mode} className={styles.mode}>
                <span className={index % 2 ? styles.modeDotAlt : styles.modeDot} />
                {mode}
              </span>
            ))}
            <span className={styles.modesNote}>
              <Translate id="home.flow.note">The ViewModel sets the upper bound. Every binder picks its own mode in the Inspector.</Translate>
            </span>
          </div>
        </div>
      </div>
    </section>
  );
}

function InspectorRow({ label, value, kind = 'text' }) {
  return (
    <div className={styles.inspectorRow}>
      <span className={styles.inspectorLabel}>{label}</span>
      <span className={`${styles.inspectorField} ${styles[`inspectorField_${kind}`]}`}>
        {value}
        {kind === 'enum' && <span className={styles.inspectorCaret}>▾</span>}
        {kind === 'ref' && <span className={styles.inspectorCaret}>⊙</span>}
      </span>
    </div>
  );
}

function Inspector() {
  return (
    <section className={styles.section}>
      <div className="container">
        <div className={styles.inspectorLayout}>
          <div>
            <SectionHead
              kicker={<Translate id="home.inspector.kicker">In the Editor</Translate>}
              title={<Translate id="home.inspector.title">Wire it up in the Inspector, not in code.</Translate>}
              body={
                <Translate id="home.inspector.body">
                  Drop a binder on any UI element, pick the ViewModel member and the mode. Converters, formats and fallbacks are serialized fields. Designers rebind without touching C#.
                </Translate>
              }
            />
            <ul className={styles.bullets} data-reveal>
              <li>
                <strong>
                  <Translate id="home.inspector.point1.title">Hundreds of binders.</Translate>
                </strong>{' '}
                <Translate id="home.inspector.point1.body">uGUI, TextMeshPro, Transform, Animator, AudioSource, Rigidbody and more.</Translate>
              </li>
              <li>
                <strong>
                  <Translate id="home.inspector.point2.title">Converters as slots.</Translate>
                </strong>{' '}
                <Translate id="home.inspector.point2.body">Chain a format, a clamp or a lookup, or write your own in a dozen lines.</Translate>
              </li>
              <li>
                <strong>
                  <Translate id="home.inspector.point3.title">Analyzer-checked.</Translate>
                </strong>{' '}
                <Translate id="home.inspector.point3.body">A missing partial or a wrong CanExecute signature is a compile-time diagnostic with a code fix.</Translate>
              </li>
            </ul>
          </div>
          <div className={styles.inspectorWrap} data-reveal>
            <div className={styles.inspectorGlow} aria-hidden="true" />
            <div className={styles.inspector} aria-hidden="true">
              <div className={styles.inspectorHeader}>
                <span className={styles.inspectorCube} />
                <span>Score Label</span>
                <span className={styles.inspectorStatic}>Static ▾</span>
              </div>
              <div className={styles.inspectorBody}>
                <div className={styles.inspectorComponent}>
                  <span>▾</span>
                  <span className={styles.inspectorIcon} />
                  Text Binder (Script)
                </div>
                <InspectorRow label="Target" value="ScoreLabel (TextMeshProUGUI)" kind="ref" />
                <InspectorRow label="Id" value="Score" />
                <InspectorRow label="Mode" value="OneWay" kind="enum" />
                <InspectorRow label="Converter" value="IntToStringConverter" kind="enum" />
                <InspectorRow label="Format" value="{0:N0} pts" />
                <div className={`${styles.inspectorComponent} ${styles.inspectorComponentNext}`}>
                  <span>▾</span>
                  <span className={`${styles.inspectorIcon} ${styles.inspectorIconAlt}`} />
                  Slider Binder (Script)
                </div>
                <InspectorRow label="Target" value="HealthBar (Slider)" kind="ref" />
                <InspectorRow label="Id" value="Health" />
                <InspectorRow label="Mode" value="TwoWay" kind="enum" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

function Features() {
  return (
    <section className={styles.section}>
      <div className="container">
        <SectionHead
          kicker={<Translate id="home.features.kicker">Why Aspid</Translate>}
          title={<Translate id="home.features.title">Built for the frame budget.</Translate>}
          body={<Translate id="home.features.body">Every design decision starts from the same question: what does this cost at runtime?</Translate>}
        />
        <div className={styles.featureGrid}>
          {FEATURES.map((feature, index) => (
            <article key={index} className={styles.feature} data-reveal style={{ '--delay': `${index * 60}ms` }}>
              <div className={styles.featureIcon} aria-hidden="true">
                {feature.icon}
              </div>
              <h3>{feature.title}</h3>
              <p>{feature.body}</p>
            </article>
          ))}
        </div>
      </div>
    </section>
  );
}

function Path() {
  return (
    <section className={styles.section}>
      <div className="container">
        <SectionHead
          kicker={<Translate id="home.path.kicker">Learn it in six samples</Translate>}
          title={<Translate id="home.path.title">One concept per sample.</Translate>}
          body={<Translate id="home.path.subtitle">Import a sample from the Package Manager. Its README is the tutorial.</Translate>}
        />
        <ol className={styles.steps}>
          {STEPS.map(([number, label, to, body], index) => (
            <li key={number} data-reveal style={{ '--delay': `${index * 50}ms` }}>
              <Link to={to} className={styles.step}>
                <span className={styles.stepNumber}>{number}</span>
                <span className={styles.stepLabel}>{label}</span>
                <span className={styles.stepBody}>{body}</span>
              </Link>
            </li>
          ))}
        </ol>
      </div>
    </section>
  );
}

function Cta() {
  return (
    <section className={styles.section}>
      <div className="container">
        <div className={styles.cta} data-reveal>
          <div>
            <h2 className={styles.ctaTitle}>
              <Translate id="home.cta.title">Ship your first bound screen in ten minutes.</Translate>
            </h2>
            <p className={styles.ctaBody}>
              <Translate id="home.cta.body">Install from OpenUPM or the Asset Store, open the Counter sample, press Play.</Translate>
            </p>
          </div>
          <div className={styles.actions}>
            <Link className={`button button--lg ${styles.primary}`} to="/docs/getting-started">
              <Translate id="home.cta.start">Get Started</Translate>
            </Link>
            <Link className={`button button--lg ${styles.secondary}`} href="https://assetstore.unity.com/packages/slug/298463">
              <Translate id="home.cta.store">Asset Store</Translate>
            </Link>
          </div>
        </div>
      </div>
    </section>
  );
}

export default function Home() {
  const ref = useReveal();
  return (
    <Layout
      title={translate({ id: 'home.meta.title', message: 'MVVM for Unity without the boilerplate' })}
      description={translate({
        id: 'home.meta.description',
        message: 'Aspid.MVVM: a Source Generator-based MVVM framework for Unity with zero-reflection bindings.',
      })}>
      <div ref={ref} className={styles.page}>
        <Hero />
        <main>
          <Stats />
          <BeforeAfter />
          <Flow />
          <Inspector />
          <Features />
          <Path />
          <Cta />
        </main>
      </div>
    </Layout>
  );
}
