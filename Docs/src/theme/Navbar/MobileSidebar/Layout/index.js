import React, {useEffect} from 'react';
import clsx from 'clsx';
import {useLocation} from '@docusaurus/router';
import {translate} from '@docusaurus/Translate';
import {ThemeClassNames} from '@docusaurus/theme-common';
import {useNavbarMobileSidebar, useNavbarSecondaryMenu} from '@docusaurus/theme-common/internal';
import IconClose from '@theme/Icon/Close';
import {NavigationPanel, styles} from '../../../../components/NavigationPanel';

function CloseButton() {
  const mobileSidebar = useNavbarMobileSidebar();
  return (
    <button type="button" className={clsx('clean-btn', styles.close)} onClick={() => mobileSidebar.toggle()}
      aria-label={translate({id: 'theme.docs.sidebar.closeSidebarButtonAriaLabel', message: 'Close navigation bar'})}>
      <IconClose />
    </button>
  );
}

/**
 * The mobile drawer is the desktop panel at full width: the section switcher replaces the primary menu and its
 * "back" button, so the current doc sidebar is the only list. Pages without a doc sidebar fall back to the navbar items.
 */
export default function NavbarMobileSidebarLayout({primaryMenu}) {
  const {content} = useNavbarSecondaryMenu();
  const mobileSidebar = useNavbarMobileSidebar();
  const {pathname} = useLocation();

  // The section switcher and the doc list navigate without closing the drawer themselves.
  useEffect(() => {
    if (mobileSidebar.shown) mobileSidebar.toggle();
  }, [pathname]); // eslint-disable-line react-hooks/exhaustive-deps

  return (
    <div className={clsx(ThemeClassNames.layout.navbar.mobileSidebar.container, 'navbar-sidebar')}>
      <NavigationPanel className={styles.panelMobile} trailing={<CloseButton />}>
        <div className={clsx(ThemeClassNames.layout.navbar.mobileSidebar.panel, 'menu')}>{content ?? primaryMenu}</div>
      </NavigationPanel>
    </div>
  );
}
