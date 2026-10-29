import React from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import {useAlternatePageUtils} from '@docusaurus/theme-common/internal';
import {useHistorySelector} from '@docusaurus/theme-common';
import {translate} from '@docusaurus/Translate';
import IconLanguage from '@theme/Icon/Language';
import Dropdown from './Dropdown';
import styles from './styles.module.css';

/** Click-toggled locale menu: the theme's desktop dropdown opens on hover only, which touch screens cannot do. */
export default function LocaleSwitcher() {
  const {i18n: {currentLocale, locales, localeConfigs}} = useDocusaurusContext();
  const {createUrl} = useAlternatePageUtils();
  const search = useHistorySelector((history) => history.location.search);
  const hash = useHistorySelector((history) => history.location.hash);
  const items = locales.map((locale) => ({
    key: locale, label: localeConfigs[locale].label, lang: localeConfigs[locale].htmlLang, active: locale === currentLocale,
    to: `pathname://${createUrl({locale, fullyQualified: false})}${search}${hash}`, target: '_self', autoAddBaseUrl: false,
  }));
  const label = translate({id: 'theme.navbar.mobileLanguageDropdown.label', message: 'Languages'});
  return (
    <Dropdown items={items} up buttonClassName={styles.locale} aria-label={label} title={label}>
      <IconLanguage />
    </Dropdown>
  );
}
