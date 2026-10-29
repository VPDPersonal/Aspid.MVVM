import React, {useEffect, useId, useRef, useState} from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import {useLocation} from '@docusaurus/router';
import styles from './styles.module.css';

/**
 * A click-toggled disclosure with a list of links that closes on outside pointer, Escape and navigation; Escape returns
 * the focus to the button. It is not an ARIA menu: Tab walks the links, and arrow keys are not handled.
 * `items` are `{key, label, to, active, ...linkProps}`; `menu` replaces them with custom `<li>` content that stays open
 * on choice (appearance). `up` opens the menu above the button (panel footer).
 */
export default function Dropdown({items = [], menu, up, buttonClassName, children, ...buttonProps}) {
  const [open, setOpen] = useState(false);
  const root = useRef(null);
  const button = useRef(null);
  const id = useId();
  const {pathname} = useLocation();

  useEffect(() => { setOpen(false); }, [pathname]);
  useEffect(() => {
    if (!open) return undefined;
    const onPointer = (event) => { if (!root.current?.contains(event.target)) setOpen(false); };
    const onKey = (event) => {
      if (event.key !== 'Escape') return;
      if (root.current?.contains(document.activeElement)) button.current?.focus();
      setOpen(false);
    };
    document.addEventListener('pointerdown', onPointer);
    document.addEventListener('keydown', onKey);
    return () => { document.removeEventListener('pointerdown', onPointer); document.removeEventListener('keydown', onKey); };
  }, [open]);

  return (
    <div ref={root} className={styles.switcher}>
      <button ref={button} type="button" {...buttonProps} className={buttonClassName} aria-expanded={open} aria-controls={id} onClick={() => setOpen((value) => !value)}>
        {children}
      </button>
      <ul id={id} className={clsx(styles.switcherMenu, up && styles.switcherMenuUp)} hidden={!open}>
        {menu ?? items.map(({key, label, active, ...linkProps}) => (
          <li key={key}>
            <Link {...linkProps} className={clsx(styles.switcherItem, active && styles.switcherItemActive)} aria-current={active ? 'page' : undefined}>
              {label}
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
