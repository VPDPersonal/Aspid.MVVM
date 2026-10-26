import React from 'react';
import clsx from 'clsx';
import styles from './styles.module.css';

// Preview flask: the bubbles boil up from the bottom (cx, r, delay in s), then the brew bursts out of the neck (dx, dy).
const BUBBLES = [[15.6, 0.7, 0], [18.2, 0.9, 0.14], [20.6, 0.6, 0.07], [22.6, 0.75, 0.21]];
const SPARKS = [[0, -11], [-8, -8], [8, -8], [-11, -2], [11, -2], [-4, -12], [4, -12]];

// The marks from the README badge SVGs (`Website/docs/Images/status-badge-*.svg`), drawn inline so they can move.
const ICONS = {
  // Unity mark: https://github.com/simple-icons/simple-icons/blob/develop/icons/unity.svg
  unity: (
    <svg className={styles.icon} viewBox="0 0 24 24" aria-hidden="true">
      <path fill="currentColor" d="m12.9288 4.2939 3.7997 2.1929c.1366.077.1415.2905 0 .3675l-4.515 2.6076a.4192.4192 0 0 1-.4246 0L7.274 6.8543c-.139-.0745-.1415-.293 0-.3675l3.7972-2.193V0L1.3758 5.5977V16.793l3.7177-2.1456v-4.3858c-.0025-.1565.1813-.2682.318-.1838l4.5148 2.6076a.4252.4252 0 0 1 .2136.3676v5.2127c.0025.1565-.1813.2682-.3179.1838l-3.7996-2.1929-3.7178 2.1457L12 24l9.6954-5.5977-3.7178-2.1457-3.7996 2.1929c-.1341.082-.3229-.0248-.3179-.1838V13.053c0-.1565.087-.2956.2136-.3676l4.5149-2.6076c.134-.082.3228.0224.3179.1838v4.3858l3.7177 2.1456V5.5977L12.9288 0Z" />
    </svg>
  ),
  preview: (
    <svg className={styles.icon} viewBox="10 6 18 20" aria-hidden="true">
      <path className={styles.liquid} fill="currentColor" d="M13.9 19h10.2l1.9 3a1.3 1.3 0 0 1-1.1 2H13.1a1.3 1.3 0 0 1-1.1-2Z" />
      <path fill="none" stroke="currentColor" strokeWidth="1.6" strokeLinecap="round" strokeLinejoin="round" d="M16 8h6 M17 8v6l-5 8a1.3 1.3 0 0 0 1.1 2h11.8a1.3 1.3 0 0 0 1.1-2l-5-8V8 M15 19h8" />
      {BUBBLES.map(([cx, r, delay]) => (
        <circle key={cx} className={styles.bubble} cx={cx} cy="22.6" r={r} fill="none" stroke="currentColor" strokeWidth="0.5" style={{animationDelay: `${delay}s`}} />
      ))}
      <circle className={styles.flash} cx="19" cy="7" r="1.5" fill="none" stroke="currentColor" strokeWidth="0.6" />
      {SPARKS.map(([dx, dy]) => (
        <circle key={`${dx},${dy}`} className={styles.spark} cx="19" cy="7.5" r="1.1" fill="currentColor" style={{'--dx': `${dx}px`, '--dy': `${dy}px`}} />
      ))}
    </svg>
  ),
  license: (
    <svg className={styles.icon} viewBox="0 0 24 24" aria-hidden="true">
      <circle className={styles.ring} cx="12" cy="12.5" r="10" fill="none" stroke="currentColor" strokeWidth="1" />
      <g className={styles.stamp}>
        <path fill="none" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round" d="m12 2 9 3v7c0 5-4 8-9 10-5-2-9-5-9-10V5Z" />
        <path className={styles.check} fill="none" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round" pathLength="1" d="M8 12l3 3 5-6" />
      </g>
    </svg>
  ),
};

/** A README status badge redrawn for the site: the same chip, with an icon that moves on hover. */
export default function StatusBadge({kind, label, href}) {
  const [lead, ...rest] = label.split(' ');
  const text = kind === 'preview'
    ? <><span className={styles.accent}>{lead}</span> {rest.join(' ')}</>
    : kind === 'license' ? label.replace(/ License$/, '') : label;
  const content = <>{ICONS[kind]}<span>{text}</span></>;
  const className = clsx('readme-status-badge', styles.badge, styles[kind]);
  return href
    ? <a className={className} href={href} target="_blank" rel="noopener noreferrer" aria-label={label}>{content}</a>
    : <span className={className} aria-label={label}>{content}</span>;
}
