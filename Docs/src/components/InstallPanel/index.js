import React, {useEffect, useId, useRef, useState} from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import CodeBlock from '@theme/CodeBlock';
import {useWalkthrough} from '@site/src/components/FeaturePreview/effects';
import styles from './styles.module.css';

const RELEASES = 'https://github.com/VPDPersonal/Aspid.MVVM/releases';

const TEXT = {
  en: {
    steps: [
      <>Open <b>Window → Package Manager</b></>,
      <>Choose <b>+ → Install package from git URL…</b></>,
      <>Paste the URL and press <b>Install</b></>,
    ],
    channels: {'upm': 'Stable', 'upm-preview': 'Preview'},
    latest: 'Latest',
    unavailable: 'No releases yet',
    pick: 'Choose a version',
    versions: 'All versions',
    installed: 'Installed from Git',
  },
  ru: {
    steps: [
      <>Откройте <b>Window → Package Manager</b></>,
      <>Выберите <b>+ → Install package from git URL…</b></>,
      <>Вставьте URL и нажмите <b>Install</b></>,
    ],
    channels: {'upm': 'Стабильная', 'upm-preview': 'Preview'},
    latest: 'Последняя',
    unavailable: 'Релизов пока нет',
    pick: 'Выберите версию',
    versions: 'Все версии',
    installed: 'Установлен из Git',
  },
};

// Frames: 0 the window opens, 1 the + menu, 2 the URL is typed, 3 Install runs, 4 the package is in the list.
const DURATIONS = [1300, 1400, 1800, 1100, 2600];
// The instruction step each frame illustrates; the last frame marks every step done.
const STEP_OF_FRAME = [0, 1, 2, 2, 3];
const FIRST_FRAME_OF_STEP = [0, 1, 2];
// How many times the walk-through plays on its own before it rests on the last frame.
const PLAYS = 2;
const MENU = ['Install package from disk…', 'Install package from tarball…', 'Install package from git URL…', 'Install package by name…'];

function PackageManager({frame, url, version, text}) {
  const done = frame === 4;
  return (
    <div className={styles.pm} data-frame={frame} aria-hidden="true">
      <div className={styles.pmTitle}><span>Package Manager</span></div>
      <div className={styles.pmToolbar}>
        <span className={styles.pmPlus} data-press={frame === 1 || undefined}>+ ▾</span>
        <span className={styles.pmFilter}>Packages: In Project ▾</span>
      </div>
      <div className={styles.pmMain}>
        <ul className={styles.pmList}>
          <li><span>Input System</span><span>1.11.2</span></li>
          <li><span>Universal RP</span><span>17.0.3</span></li>
          <li className={styles.pmNew} data-show={done || undefined}><span>Aspid.MVVM</span><span>{version}</span></li>
        </ul>
        <div className={styles.pmDetail}>
          {done ? (
            <>
              <strong>Aspid.MVVM</strong>
              <span className={styles.pmId}>tech.aspid.mvvm</span>
              <span className={styles.pmBadge}>✓ {text.installed}</span>
            </>
          ) : (
            <>
              <span className={styles.pmSkeleton} style={{width: '62%'}} />
              <span className={styles.pmSkeleton} style={{width: '40%'}} />
              <span className={styles.pmSkeleton} style={{width: '78%'}} />
            </>
          )}
        </div>
      </div>
      <ul className={styles.pmMenu} data-open={frame === 1 || undefined}>
        {MENU.map((item, index) => <li key={item} data-hover={index === 2 || undefined}>{item}</li>)}
      </ul>
      <div className={styles.pmPopover} data-open={frame === 2 || frame === 3 || undefined}>
        <span className={styles.pmInput}><span className={styles.pmTyped} data-typed={frame >= 2 || undefined}>{url}</span></span>
        <span className={styles.pmInstall} data-press={frame === 3 || undefined}>Install</span>
        <span className={styles.pmProgress} data-run={frame === 3 || undefined} />
      </div>
    </div>
  );
}

/** Each channel opens its version menu upwards over the walk-through. */
function VersionTab({label, versions, value, active, text, onChoose}) {
  const ref = useRef(null);
  const trigger = useRef(null);
  const menuId = useId();
  const [open, setOpen] = useState(false);
  const disabled = versions.length === 0;
  const options = [null, ...versions];
  useEffect(() => {
    if (!open) return undefined;
    const close = (event) => {
      if (!ref.current?.contains(event.target)) setOpen(false);
    };
    document.addEventListener('pointerdown', close);
    return () => document.removeEventListener('pointerdown', close);
  }, [open]);
  useEffect(() => {
    if (open) ref.current?.querySelector('[aria-checked="true"]')?.focus();
  }, [open]);
  const choose = (version) => {
    setOpen(false);
    onChoose(version);
    trigger.current?.focus();
  };
  const onKeyDown = (event) => {
    if (event.key === 'Escape') {
      setOpen(false);
      trigger.current?.focus();
      event.stopPropagation();
    } else if (['ArrowDown', 'ArrowUp', 'Home', 'End'].includes(event.key)) {
      event.preventDefault();
      if (!open) {
        setOpen(true);
        return;
      }
      const items = [...ref.current.querySelectorAll('[role="menuitemradio"]')];
      const index = items.indexOf(document.activeElement);
      const step = event.key === 'ArrowDown' ? 1 : -1;
      const next = event.key === 'Home' ? 0 : event.key === 'End' ? items.length - 1
        : index < 0 ? (step > 0 ? 0 : items.length - 1) : (index + step + items.length) % items.length;
      items[next]?.focus();
    }
  };
  return (
    <div ref={ref} className={styles.versionTab} onKeyDown={onKeyDown}
      onBlur={(event) => {
        // Safari does not focus a clicked button, so a click inside blurs with no target: the pointerdown handler
        // above closes on outside clicks, and this one only on focus moving elsewhere (Tab).
        if (event.relatedTarget && !event.currentTarget.contains(event.relatedTarget)) setOpen(false);
      }}>
      <button
        ref={trigger}
        type="button"
        disabled={disabled}
        title={disabled ? text.unavailable : undefined}
        aria-pressed={active}
        aria-haspopup="menu"
        aria-controls={open ? menuId : undefined}
        aria-expanded={open}
        onClick={() => { onChoose(value); setOpen(!open); }}>
        <span>{label}</span>
        <span className={styles.versionName} data-pinned={value !== null || undefined}>{disabled ? '—' : value ?? text.latest}</span>
        <span className={styles.caret} aria-hidden="true" />
      </button>
      {open && <ul id={menuId} className={styles.versionMenu} role="menu" aria-label={`${label}: ${text.pick}`}>
        {options.map((version) => (
          <li key={version ?? 'latest'} role="none">
            <button type="button" role="menuitemradio" data-pinned={version !== null || undefined} aria-checked={version === value}
              tabIndex={-1} onClick={() => choose(version)}>{version ?? text.latest}</button>
          </li>
        ))}
      </ul>}
    </div>
  );
}

/** The site's install section: a Package Manager walk-through beside the steps, and the URL as a code block to copy. */
export default function InstallPanel({url}) {
  const {siteConfig, i18n} = useDocusaurusContext();
  const {packageVersions} = siteConfig.customFields;
  const text = TEXT[i18n.currentLocale] ?? TEXT.en;
  const ref = useRef(null);
  const [frame, setFrame] = useWalkthrough(ref, DURATIONS, PLAYS);
  const channels = Object.keys(text.channels);
  const initialBranch = url.split('#')[1];
  const [branch, setBranch] = useState(() => packageVersions[initialBranch]?.length
    ? initialBranch : channels.find((channel) => packageVersions[channel]?.length) ?? initialBranch);
  const [selected, setSelected] = useState({'upm': null, 'upm-preview': null});
  const version = selected[branch] ?? packageVersions[branch]?.[0];
  const shown = `${url.split('#')[0]}#${branch}${selected[branch] ? `/${selected[branch]}` : ''}`;
  // Another version types another URL, so the walk-through plays again from the start.
  const choose = (nextBranch, nextVersion) => {
    if (nextBranch === branch && nextVersion === selected[branch]) return;
    setBranch(nextBranch);
    setSelected((previous) => ({...previous, [nextBranch]: nextVersion}));
    setFrame(0);
  };
  const current = STEP_OF_FRAME[frame];

  return (
    <section ref={ref} className={styles.install}>
      <div className={`${styles.top} ${styles.installationTop}`}>
        <div className={styles.preview}><PackageManager frame={frame} url={shown} version={version} text={text} /></div>
        <ol className={styles.steps}>
          {text.steps.map((step, index) => (
            <li key={index} data-active={current === index || undefined} data-done={current > index || undefined}>
              <button type="button" onClick={() => setFrame(FIRST_FRAME_OF_STEP[index])}>
                <span className={styles.stepIndex}>{current > index ? '✓' : index + 1}</span>
                <span>{step}</span>
              </button>
            </li>
          ))}
        </ol>
      </div>
      <div className={styles.bar}>
        <div className={styles.meta}>
          <div className={styles.segmented} role="group" aria-label={text.pick}>
            {channels.map((channel) => <VersionTab key={channel} label={text.channels[channel]}
              versions={packageVersions[channel] ?? []} value={selected[channel]} active={branch === channel}
              text={text} onChoose={(value) => choose(channel, value)} />)}
          </div>
          <a className={styles.versions} href={RELEASES} target="_blank" rel="noopener noreferrer">{text.versions}</a>
        </div>
        <div className={styles.field}><CodeBlock language="text">{shown}</CodeBlock></div>
      </div>
    </section>
  );
}
