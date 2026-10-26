import React, {useEffect, useRef, useState} from 'react';
import clsx from 'clsx';
import {translate} from '@docusaurus/Translate';
import {useDoc} from '@docusaurus/plugin-content-docs/client';
import {useNavbarMobileSidebar} from '@docusaurus/theme-common/internal';
import TOCItems from '@theme/TOCItems';

/**
 * Laptop-width table of contents (997–1400px, see custom.css): a pinned button in the lower right corner of the
 * viewport opens a popover with the same outline as the desktop column. Picking a section, Esc or a click
 * outside closes it. It steps aside while the mobile drawer is open, whose footer buttons it would cover.
 * The popover stays mounted so it can grow out of the button (custom.css); while closed it is inert.
 */
export default function FloatingToc() {
  const {toc, frontMatter} = useDoc();
  const [open, setOpen] = useState(false);
  const mobileSidebar = useNavbarMobileSidebar();
  const root = useRef(null);
  const label = translate({id: 'theme.TOCCollapsible.toggleButtonLabel', message: 'On this page'});

  // Opening the drawer also closes the outline, so it does not pop back up when the drawer closes.
  useEffect(() => { if (mobileSidebar.shown) setOpen(false); }, [mobileSidebar.shown]);

  useEffect(() => {
    if (!open) return undefined;
    const onKey = (event) => { if (event.key === 'Escape') setOpen(false); };
    const onPointer = (event) => { if (!root.current?.contains(event.target)) setOpen(false); };
    document.addEventListener('keydown', onKey);
    document.addEventListener('pointerdown', onPointer);
    return () => {
      document.removeEventListener('keydown', onKey);
      document.removeEventListener('pointerdown', onPointer);
    };
  }, [open]);

  if (frontMatter.hide_table_of_contents || toc.length === 0 || mobileSidebar.shown) return null;
  return (
    <div ref={root} className={clsx('floating-toc', open && 'floating-toc--open')}>
      <div
        id="floating-toc-panel"
        className="floating-toc__panel"
        inert={!open}
        onClick={(event) => { if (event.target.closest('a')) setOpen(false); }}>
        <div className="floating-toc__title">{label}</div>
        <TOCItems
          toc={toc}
          minHeadingLevel={frontMatter.toc_min_heading_level}
          maxHeadingLevel={frontMatter.toc_max_heading_level}
          className="table-of-contents"
          linkClassName="table-of-contents__link"
          linkActiveClassName="table-of-contents__link--active"
        />
      </div>
      <button
        type="button"
        className="clean-btn floating-toc__button"
        aria-label={label}
        title={label}
        aria-expanded={open}
        aria-controls="floating-toc-panel"
        onClick={() => setOpen((value) => !value)}>
        <svg width="18" height="18" viewBox="0 0 18 18" fill="none" stroke="currentColor" strokeWidth="1.6" strokeLinecap="round" aria-hidden="true">
          <path className="floating-toc__bar floating-toc__bar--top" d="M3 4.5h12" />
          <path className="floating-toc__bar floating-toc__bar--middle" d="M3 9h12" />
          <path className="floating-toc__bar floating-toc__bar--bottom" d="M3 13.5h12" />
        </svg>
      </button>
    </div>
  );
}
