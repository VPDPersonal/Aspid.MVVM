import React from 'react';
import Desktop from '@theme-original/DocSidebar/Desktop';
import {NavigationPanel} from '../../../components/NavigationPanel';

/**
 * On desktop the navbar is hidden and the sidebar becomes the only chrome. Below 997px Docusaurus renders
 * the mobile drawer instead, which wraps the same panel in `Navbar/MobileSidebar/Layout`.
 */
export default function DesktopWrapper(props) {
  return (
    <NavigationPanel>
      <Desktop {...props} />
    </NavigationPanel>
  );
}
