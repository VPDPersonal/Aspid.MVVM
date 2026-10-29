import React, {useEffect, useState} from 'react';
import {translate} from '@docusaurus/Translate';
import {useColorMode} from '@docusaurus/theme-common';
import useBaseUrl, {useBaseUrlUtils} from '@docusaurus/useBaseUrl';
import IconDarkMode from '@theme/Icon/DarkMode';
import IconLightMode from '@theme/Icon/LightMode';
import {ACCENTS, DEFAULT_ACCENT, accentLogo, applyAccent, readAccent} from '../../accents';
import Dropdown from './Dropdown';
import styles from './styles.module.css';

const accentName = (accent) => ({
  green: translate({id: 'accent.green', message: 'Green'}),
  red: translate({id: 'accent.red', message: 'Red'}),
  blue: translate({id: 'accent.blue', message: 'Blue'}),
  yellow: translate({id: 'accent.yellow', message: 'Yellow'}),
  mono: translate({id: 'accent.mono', message: 'Monochrome'}),
})[accent];

/**
 * One footer button for the look of the site: the theme options, then the accent colours as a row of logos.
 * The menu stays open on choice, so accents can be compared in place.
 */
export default function AppearanceSwitcher() {
  const {colorMode, setColorMode} = useColorMode();
  const baseUrl = useBaseUrl('/');
  const {withBaseUrl} = useBaseUrlUtils();
  // The static page renders the default; the stored accent is already painted by the boot script in <head>.
  const [accent, setAccent] = useState(DEFAULT_ACCENT);
  useEffect(() => setAccent(readAccent()), []);

  const themes = [
    {mode: 'dark', Icon: IconDarkMode, label: translate({id: 'appearance.dark', message: 'Dark'})},
    {mode: 'light', Icon: IconLightMode, label: translate({id: 'appearance.light', message: 'Light'})},
  ];
  const accentLabel = translate({id: 'accent.label', message: 'Accent'});

  const menu = (
    <>
      {/* The theme is a two-part switch as wide as the accent row below it, marked the way the picked accent is. */}
      <li>
        <div role="group" aria-label={translate({id: 'appearance.theme', message: 'Theme'})} className={styles.themeRow}>
          {themes.map(({mode, Icon, label}) => (
            <button key={mode} type="button" aria-pressed={colorMode === mode}
              className={styles.themeOption} onClick={() => setColorMode(mode)}>
              <Icon aria-hidden />{label}
            </button>
          ))}
        </div>
      </li>
      <li aria-hidden="true" className={styles.menuSeparator} />
      <li>
        <div role="group" aria-label={accentLabel} className={styles.accentRow}>
          {ACCENTS.map((option) => (
            <button key={option} type="button" aria-pressed={option === accent}
              className={styles.accentOption} aria-label={accentName(option)} title={accentName(option)}
              onClick={() => { applyAccent(option, baseUrl, {save: true}); setAccent(option); }}>
              <img src={withBaseUrl(accentLogo(option))} alt="" loading="lazy" />
            </button>
          ))}
        </div>
      </li>
    </>
  );

  const label = translate({id: 'appearance.label', message: 'Appearance'});
  return (
    <Dropdown menu={menu} up buttonClassName={styles.locale} aria-label={label} title={label}>
      <IconDarkMode aria-hidden className={styles.themeIconDark} />
      <IconLightMode aria-hidden className={styles.themeIconLight} />
    </Dropdown>
  );
}
