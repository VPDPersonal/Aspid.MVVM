import React, {useRef} from 'react';
import clsx from 'clsx';
import {Highlight} from 'prism-react-renderer';
import {usePrismTheme} from '@docusaurus/theme-common';
import {useInView, useLoop} from './effects';
import styles from './styles.module.css';

/**
 * The preview slot of an introduction feature card (src/remark/introBanner.js). A page registers an animated preview
 * under its doc slug (`03-binding-modes.md` → `binding-modes`) in PREVIEWS; the others keep their README capture or
 * code block. Every preview is the code on top and its effect below, as on the Aspid.FastTools site.
 */

/** Highlighted C# lines; `active` marks the line the neighbouring preview currently shows. */
function Code({code, active = -1}) {
  const theme = usePrismTheme();
  return (
    <Highlight theme={theme} code={code} language="csharp">
      {({tokens, getTokenProps}) => (
        <pre className={styles.snippet} style={{color: theme.plain.color}}>
          {tokens.map((line, index) => (
            <span key={index} className={styles.snippetLine} data-active={index === active || undefined}>
              {line.map((token, tokenIndex) => {
                const {key, ...props} = getTokenProps({token});
                return <span key={tokenIndex} {...props} />;
              })}
            </span>
          ))}
        </pre>
      )}
    </Highlight>
  );
}

/* ---------- ViewModels: the command changes the generated property, the bound text follows ---------- */

const VIEW_MODEL_CODE = `[ViewModel]
public partial class CounterViewModel
{
    [Bind] private int _count;

    [RelayCommand]
    private void Increment() => Count++;
}`;

function ViewModelPreview() {
  const ref = useRef(null);
  // 0 idle, 1–3 a press each, 4 rest.
  const step = useLoop(5, 1000, useInView(ref), 4);
  const count = Math.min(step, 3);
  const pressing = step >= 1 && step <= 3;
  return (
    <div ref={ref} className={styles.counterBody}>
      <Code code={VIEW_MODEL_CODE} active={pressing ? 6 : 3} />
      <div className={styles.counterStage} aria-hidden="true">
        <span key={count} className={styles.counterValue} data-flash={pressing || undefined}>{count}</span>
        <span className={styles.counterButton} data-press={pressing || undefined}>+</span>
        <span className={styles.counterCaption}>{pressing ? 'IncrementCommand → Count' : 'TextBinder ← Count'}</span>
      </div>
    </div>
  );
}

/* ---------- Binding Modes: the direction each mode carries a value ---------- */

const MODES_CODE = `[OneWayBind] private int _health;
[TwoWayBind] private string _name;
[OneTimeBind] private Sprite _icon;
[OneWayToSourceBind] private float _input;`;

const MODES = [
  ['OneWay', 'ViewModel → View'],
  ['TwoWay', 'ViewModel ↔ View'],
  ['OneTime', 'ViewModel → View ×1'],
  ['OneWayToSource', 'ViewModel ← View'],
];

function ModesPreview() {
  const ref = useRef(null);
  const step = useLoop(MODES.length + 1, 1100, useInView(ref), MODES.length);
  const active = step < MODES.length ? step : -1;
  return (
    <div ref={ref} className={styles.namesBody}>
      <Code code={MODES_CODE} active={active} />
      <ul className={styles.names} aria-label="Binding modes">
        {MODES.map(([mode, direction], index) => (
          <li key={mode} data-done={step >= index || undefined} data-active={active === index || undefined}>
            <code>{mode}</code>
            <span className={styles.namesLabel}>{direction}</span>
          </li>
        ))}
      </ul>
    </div>
  );
}

/* ---------- Commands: CanExecute follows the bound text ---------- */

const COMMAND_CODE = `[Bind] private string _text;

[RelayCommand(CanExecute = nameof(CanSubmit))]
private void Submit() { }

private bool CanSubmit() => !string.IsNullOrEmpty(Text);`;

const TYPED = ['', 'As', 'Aspid', 'Aspid', ''];

function CommandPreview({ru}) {
  const ref = useRef(null);
  // 0 empty, 1–2 typing, 3 Submit pressed, 4 cleared.
  const step = useLoop(5, 1100, useInView(ref), 2);
  const text = TYPED[step];
  const enabled = text.length > 0;
  return (
    <div ref={ref} className={styles.counterBody}>
      <Code code={COMMAND_CODE} active={step === 3 ? 3 : step === 1 || step === 2 ? 0 : 5} />
      <div className={styles.counterStage} aria-hidden="true">
        <span className={styles.counterField} data-empty={!enabled || undefined}>{enabled ? text : ru ? 'Имя…' : 'Name…'}</span>
        <span className={styles.counterButton} data-disabled={!enabled || undefined} data-press={step === 3 || undefined}>Submit</span>
        <span className={styles.counterCaption}>{`CanSubmit() → ${enabled}`}</span>
      </div>
    </div>
  );
}

const PREVIEWS = {
  viewmodels: ViewModelPreview,
  'binding-modes': ModesPreview,
  commands: CommandPreview,
};

export default function FeaturePreview({doc, ru, children}) {
  const Preview = PREVIEWS[doc];
  return Preview ? <div className={clsx(styles.featurePreview, 'feature-card__live')}><Preview ru={ru} /></div> : children;
}
