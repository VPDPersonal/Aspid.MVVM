import React from 'react';
import clsx from 'clsx';
import Logo from '@theme/Logo';
import SearchBar from '@theme/SearchBar';
import {useThemeConfig} from '@docusaurus/theme-common';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import SectionSwitcher from './SectionSwitcher';
import LocaleSwitcher from './LocaleSwitcher';
import AppearanceSwitcher from './AppearanceSwitcher';
import styles from './styles.module.css';

function UnityIcon() {
  return <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true"><path d="m12.9288 4.2939 3.7997 2.1929c.1366.077.1415.2905 0 .3675l-4.515 2.6076a.4192.4192 0 0 1-.4246 0L7.274 6.8543c-.139-.0765-.1415-.2905 0-.3675l3.7972-2.1929V0L1.3758 5.5977V16.793l3.7177-2.1456v-4.3858c-.0025-.1565.1813-.2634.3157-.1838l4.5148 2.6076a.4252.4252 0 0 1 .2114.3657v5.2127c.0025.1565-.1813.2634-.3157.1838l-3.7996-2.1929-3.7177 2.1457L12 24l9.6954-5.5977-3.7177-2.1457-3.7996 2.1929c-.1346.082-.3182-.0273-.3157-.1838V13.052c0-.1565.0855-.2946.2114-.3657l4.5147-2.6076c.1344-.082.3182.0273.3157.1838v4.3858l3.7177 2.1456V5.5977L12.9288 0Z" /></svg>;
}

function GitHubIcon() {
  return <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true"><path d="M12 .5C5.37.5 0 5.87 0 12.5c0 5.3 3.44 9.8 8.21 11.39.6.11.82-.26.82-.58v-2.23c-3.34.73-4.04-1.42-4.04-1.42-.55-1.39-1.33-1.76-1.33-1.76-1.09-.75.08-.73.08-.73 1.2.09 1.84 1.24 1.84 1.24 1.07 1.83 2.81 1.3 3.49.99.11-.78.42-1.3.76-1.6-2.67-.3-5.47-1.34-5.47-5.93 0-1.31.47-2.38 1.24-3.22-.13-.3-.54-1.53.12-3.18 0 0 1.01-.32 3.3 1.23a11.5 11.5 0 0 1 6 0c2.29-1.55 3.3-1.23 3.3-1.23.66 1.65.25 2.88.12 3.18.77.84 1.24 1.91 1.24 3.22 0 4.6-2.81 5.62-5.49 5.92.43.38.82 1.1.82 2.22v3.3c0 .32.22.7.83.58A12 12 0 0 0 24 12.5C24 5.87 18.63.5 12 .5z" /></svg>;
}

/**
 * The navigation chrome shared by the desktop sidebar and the mobile drawer: a pinned header with the brand,
 * the section switcher and search, the scrolling document list, and a pinned footer with the remaining
 * navbar utilities. `trailing` renders after the brand (the mobile close button).
 */
export function NavigationPanel({className, trailing, children}) {
  return (
    <div className={clsx(styles.panel, className)}>
      <header className={styles.header}>
        <div className={styles.brandRow}>
          <Logo className={styles.brandLink} imageClassName={styles.brandLogo} titleClassName={styles.brandTitle} />
          {trailing}
        </div>
        <div className={styles.tools}>
          <SectionSwitcher />
          <SearchBar />
        </div>
      </header>
      <div className={styles.list}>{children}</div>
      <PanelFooter />
    </div>
  );
}

function PanelFooter() {
  const {navbar} = useThemeConfig();
  const {siteConfig: {customFields}} = useDocusaurusContext();
  const github = navbar.items.find((item) => item.className?.includes('navbar-github'));
  const assetStoreLabel = 'Unity Asset Store';
  return (
    <footer className={styles.footer}>
      {github && <a className={styles.icon} href={github.href} target="_blank" rel="noopener noreferrer" aria-label={github['aria-label'] ?? github.label} title={github.label}><GitHubIcon /></a>}
      {customFields.assetStore && <a className={clsx(styles.icon, styles.unity)} href={customFields.assetStore} target="_blank" rel="noopener noreferrer" aria-label={assetStoreLabel} title={assetStoreLabel}><UnityIcon /></a>}
      <LocaleSwitcher />
      <AppearanceSwitcher />
    </footer>
  );
}

export {styles};
