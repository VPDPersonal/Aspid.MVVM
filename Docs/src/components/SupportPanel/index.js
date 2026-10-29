import React from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import styles from './styles.module.css';

const TEXT = {
  en: {
    title: 'Found a bug or have a question?',
    hint: 'Include your Unity version, package version and steps to reproduce.',
    issue: 'Open an issue',
    star: 'Star on GitHub',
  },
  ru: {
    title: 'Нашли ошибку или есть вопрос?',
    hint: 'Укажите версию Unity, версию пакета и шаги воспроизведения.',
    issue: 'Открыть issue',
    star: 'Поставить звезду',
  },
};

/** The site's closing call to action: report an issue, or star the repository. */
export default function SupportPanel({issues, repo}) {
  const {i18n} = useDocusaurusContext();
  const text = TEXT[i18n.currentLocale] ?? TEXT.en;
  return (
    <section className={styles.support}>
      <div className={styles.copy}>
        <p className={styles.title}>{text.title}</p>
        <p className={styles.hint}>{text.hint}</p>
      </div>
      <div className={styles.actions}>
        <a className={styles.primary} href={issues} target="_blank" rel="noopener noreferrer">{text.issue}</a>
        <a className={styles.secondary} href={repo} target="_blank" rel="noopener noreferrer"><svg className={styles.star} viewBox="0 0 24 24" aria-hidden="true">
            <path d="M12 2.5l2.94 5.96 6.56.95-4.75 4.63 1.12 6.54L12 17.49l-5.87 3.09 1.12-6.54L2.5 9.41l6.56-.95z" />
          </svg>
          {text.star}</a>
      </div>
    </section>
  );
}
