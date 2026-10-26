import React from 'react';
import {useLocation} from '@docusaurus/router';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import {useThemeConfig} from '@docusaurus/theme-common';
import {translate} from '@docusaurus/Translate';
import {useActivePlugin, useAllDocsData} from '@docusaurus/plugin-content-docs/client';
import Dropdown from './Dropdown';
import styles from './styles.module.css';

const join = (...parts) => `/${parts.join('/')}`.replace(/\/+/g, '/').replace(/(.)\/$/, '$1');

/** The left navbar items (sections) as `{label, path, to}`; `path` is the docs plugin route used to spot the active one. */
function useSections() {
  const {navbar} = useThemeConfig();
  const {siteConfig: {baseUrl}} = useDocusaurusContext();
  const allDocs = useAllDocsData();
  return navbar.items.filter((item) => item.position === 'left').map((item) => {
    if (item.type === 'docSidebar') {
      const plugin = allDocs[item.docsPluginId ?? 'default'];
      const version = plugin.versions.find((candidate) => candidate.isLast) ?? plugin.versions[0];
      return {label: item.label, path: join(plugin.path), to: version.sidebars[item.sidebarId]?.link?.path ?? version.path};
    }
    return {label: item.label, path: join(baseUrl, item.to), to: item.to};
  });
}

export default function SectionSwitcher() {
  const sections = useSections();
  const activePlugin = useActivePlugin();
  const {pathname} = useLocation();
  const activePath = activePlugin ? join(activePlugin.pluginData.path) : pathname;
  const current = sections.find((section) => section.path === activePath) ?? sections[0];
  const items = sections.map((section) => ({key: section.path, label: section.label, to: section.to, active: section === current}));
  return (
    <Dropdown items={items} buttonClassName={styles.switcherButton} aria-label={`${translate({id: 'section.label', message: 'Section'})}: ${current.label}`}>
      <span>{current.label}</span>
      <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true"><path d="m6 9 6 6 6-6" /></svg>
    </Dropdown>
  );
}
