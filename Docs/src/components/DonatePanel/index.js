import React from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import styles from './styles.module.css';

const TEXT = {
  en: {title: 'Enjoying Aspid.MVVM?', action: 'Support on the Asset Store'},
  ru: {title: 'Нравится Aspid.MVVM?', action: 'Поддержать в Asset Store'},
};

const HEART = 'M12 21s-7.5-4.6-9.6-9.4C1 8.4 2.9 4.5 6.6 4.5c2.2 0 3.7 1.2 5.4 3.2 1.7-2 3.2-3.2 5.4-3.2 3.7 0 5.6 3.9 4.2 7.1C19.5 16.4 12 21 12 21Z';

/** The introduction's donation card: a beating heart, the README's sentence and a button to the Asset Store. */
export default function DonatePanel({href, children}) {
  const {i18n} = useDocusaurusContext();
  const text = TEXT[i18n.currentLocale] ?? TEXT.en;
  return (
    <section className={styles.donate}>
      <span className={styles.badge} aria-hidden="true">
        <svg className={styles.heart} viewBox="0 0 24 24"><path d={HEART} /></svg>
      </span>
      <div className={styles.copy}>
        <p className={styles.title}>{text.title}</p>
        <p className={styles.text}>{children}</p>
      </div>
      <a className={styles.action} href={href} target="_blank" rel="noopener noreferrer">
        <svg className={styles.icon} viewBox="0 0 24 24" aria-hidden="true"><path d={HEART} /></svg>
        {text.action}
      </a>
    </section>
  );
}
