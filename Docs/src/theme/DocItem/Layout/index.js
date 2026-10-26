import React from 'react';
import clsx from 'clsx';
import {useWindowSize} from '@docusaurus/theme-common';
import {useDoc} from '@docusaurus/plugin-content-docs/client';
import DocItemPaginator from '@theme/DocItem/Paginator';
import DocVersionBanner from '@theme/DocVersionBanner';
import DocVersionBadge from '@theme/DocVersionBadge';
import DocItemFooter from '@theme/DocItem/Footer';
import DocItemTOCDesktop from '@theme/DocItem/TOC/Desktop';
import DocItemContent from '@theme/DocItem/Content';
import FloatingToc from '../../../components/FloatingToc';
import DocBreadcrumbs from '@theme/DocBreadcrumbs';
import ContentVisibility from '@theme/ContentVisibility';
import Footer from '@theme/Footer';
import styles from './styles.module.css';

/** Decide if the toc should be rendered, on mobile or desktop viewports. */
function useDocTOC() {
  const {frontMatter, toc} = useDoc();
  const windowSize = useWindowSize();
  const hidden = frontMatter.hide_table_of_contents;
  const canRender = !hidden && toc.length > 0;
  // Below 1400px the right column is hidden (custom.css) and the floating popover serves every width.
  const floating = canRender ? <FloatingToc /> : undefined;
  const desktop = canRender && (windowSize === 'desktop' || windowSize === 'ssr') ? <DocItemTOCDesktop /> : undefined;
  return {hidden, floating, desktop};
}

/**
 * The stock layout plus the site footer at the bottom of the article column. On desktop the article column is the
 * central panel, so the page-wide footer is hidden there (custom.css) and this copy closes the column instead.
 */
export default function DocItemLayout({children}) {
  const docTOC = useDocTOC();
  const {metadata} = useDoc();
  return (
    <div className="row">
      <div className={clsx('col', !docTOC.hidden && styles.docItemCol)}>
        <ContentVisibility metadata={metadata} />
        <DocVersionBanner />
        <div className={styles.docItemContainer}>
          <article>
            <DocBreadcrumbs />
            <DocVersionBadge />
            <DocItemContent>{children}</DocItemContent>
          </article>
          <DocItemPaginator />
        </div>
        {metadata.id !== 'README' && docTOC.floating}
        <div className={styles.pageFooter}>
          <DocItemFooter />
          <div className={clsx(styles.columnFooter, 'doc-column-footer')}>
            <Footer />
          </div>
        </div>
      </div>
      {docTOC.desktop && <div className="col col--3">{docTOC.desktop}</div>}
    </div>
  );
}
