import React from 'react';
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

const FEATURES = [
  {
    icon: '⚡',
    title: <Translate id="home.feature.generator.title">Source Generator, not reflection</Translate>,
    body: (
      <Translate id="home.feature.generator.body">
        Bindings are emitted at compile time as direct calls. No reflection, no boxing, no string lookups per frame.
      </Translate>
    ),
  },
  {
    icon: '🧩',
    title: <Translate id="home.feature.attributes.title">Attributes instead of boilerplate</Translate>,
    body: (
      <Translate id="home.feature.attributes.body">
        A field with [Bind] becomes a notifying property. A method with [RelayCommand] becomes a command. Nothing else to write.
      </Translate>
    ),
  },
  {
    icon: '🔀',
    title: <Translate id="home.feature.modes.title">Four binding modes</Translate>,
    body: (
      <Translate id="home.feature.modes.body">
        OneWay, TwoWay, OneTime and OneWayToSource. The ViewModel sets the upper bound, every binder picks its own mode in the Inspector.
      </Translate>
    ),
  },
  {
    icon: '🧰',
    title: <Translate id="home.feature.starterkit.title">StarterKit out of the box</Translate>,
    body: (
      <Translate id="home.feature.starterkit.body">
        Hundreds of binders for uGUI, TextMeshPro, Transform, Animator, AudioSource and more, plus a catalogue of 190+ converters.
      </Translate>
    ),
  },
  {
    icon: '📚',
    title: <Translate id="home.feature.collections.title">Observable collections</Translate>,
    body: (
      <Translate id="home.feature.collections.body">
        ObservableList, FilteredList and CreateSync keep model and ViewModel lists in step; virtualized lists render only what is visible.
      </Translate>
    ),
  },
  {
    icon: '🔍',
    title: <Translate id="home.feature.analyzers.title">Analyzers that catch mistakes early</Translate>,
    body: (
      <Translate id="home.feature.analyzers.body">
        Roslyn analyzers flag a missing partial, a wrong CanExecute signature or a field used where the property was meant, with code fixes.
      </Translate>
    ),
  },
];

function Hero() {
  return (
    <header className={styles.hero}>
      <div className={styles.heroGlow} aria-hidden="true" />
      <div className="container">
        <div className={styles.heroGrid}>
          <div className={styles.heroText}>
            <span className={styles.badge}>
              <Translate id="home.badge">Unity 2022.3+ · MIT</Translate>
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
                A Source Generator–based MVVM framework. Clean separation of View, ViewModel and logic, zero reflection in bindings, minimal allocations.
              </Translate>
            </p>
            <div className={styles.actions}>
              <Link className={`button button--primary button--lg ${styles.primary}`} to="/docs/getting-started">
                <Translate id="home.cta.start">Get Started</Translate>
              </Link>
              <Link className={`button button--outline button--lg ${styles.secondary}`} to="/tutorials/counter">
                <Translate id="home.cta.tutorials">Tutorials</Translate>
              </Link>
              <Link className={styles.linkAction} href="https://assetstore.unity.com/packages/slug/298463">
                <Translate id="home.cta.store">Asset Store →</Translate>
              </Link>
            </div>
          </div>
          <div className={styles.heroCode}>
            <div className={styles.codeCard}>
              <div className={styles.codeTab}>CounterViewModel.cs</div>
              <CodeBlock language="csharp">{VIEW_MODEL}</CodeBlock>
            </div>
            <div className={`${styles.codeCard} ${styles.codeCardSecond}`}>
              <div className={styles.codeTab}>CounterView.cs</div>
              <CodeBlock language="csharp">{VIEW}</CodeBlock>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
}

function Features() {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className={styles.featureGrid}>
          {FEATURES.map((feature) => (
            <article key={feature.icon} className={styles.feature}>
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
  const steps = [
    ['01', 'Counter', '/tutorials/counter'],
    ['02', 'Greeter', '/tutorials/greeter'],
    ['03', 'Bind Modes', '/tutorials/bind-modes'],
    ['04', 'Stats', '/tutorials/stats'],
    ['05', 'Todo List', '/tutorials/todo-list'],
    ['06', 'Custom Binder', '/tutorials/custom-binder'],
  ];
  return (
    <section className={styles.path}>
      <div className="container">
        <h2 className={styles.sectionTitle}>
          <Translate id="home.path.title">Learn it in six samples</Translate>
        </h2>
        <p className={styles.sectionSubtitle}>
          <Translate id="home.path.subtitle">
            Each sample adds exactly one concept. Import it from the Package Manager, its README is the tutorial.
          </Translate>
        </p>
        <ol className={styles.steps}>
          {steps.map(([number, label, to]) => (
            <li key={number}>
              <Link to={to} className={styles.step}>
                <span className={styles.stepNumber}>{number}</span>
                <span className={styles.stepLabel}>{label}</span>
              </Link>
            </li>
          ))}
        </ol>
      </div>
    </section>
  );
}

export default function Home() {
  return (
    <Layout
      title={translate({ id: 'home.meta.title', message: 'MVVM for Unity without the boilerplate' })}
      description={translate({
        id: 'home.meta.description',
        message: 'Aspid.MVVM: a Source Generator-based MVVM framework for Unity with zero-reflection bindings.',
      })}>
      <Hero />
      <main>
        <Features />
        <Path />
      </main>
    </Layout>
  );
}
