import React, {useEffect, useId, useMemo, useRef, useState} from 'react';
import {createPortal} from 'react-dom';
import {useHistory, useLocation} from '@docusaurus/router';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import {isSearchShortcut, prepareIndex, searchEntries} from '../../components/search';
import styles from './styles.module.css';

let indexPromise;
// Several bars can be mounted at once (panel, hidden navbar); only one may answer the shortcut.
const instances = [];
const loadIndex = () => indexPromise ??= import('@aspid-search-index').then((module) => prepareIndex(module.default));

function CloseIcon() {
  return <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" strokeLinecap="round" aria-hidden="true"><path d="M6 6l12 12M18 6 6 18" /></svg>;
}

function SearchIcon() {
  return <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" aria-hidden="true"><circle cx="10.5" cy="10.5" r="6.5" /><path d="m16 16 5 5" /></svg>;
}

export default function SearchBar() {
  const ru = useDocusaurusContext().i18n.currentLocale === 'ru';
  const sectionLabel = (section) => ru ? ({Docs: 'Документация', Tutorials: 'Уроки', Changelog: 'Изменения'}[section] || section) : section;
  const history = useHistory();
  const location = useLocation();
  const id = useId();
  const dialog = useRef(null);
  const input = useRef(null);
  const trigger = useRef(null);
  const [mounted, setMounted] = useState(false);
  const [open, setOpen] = useState(false);
  const [query, setQuery] = useState('');
  const [entries, setEntries] = useState(null);
  const [error, setError] = useState(false);
  const [active, setActive] = useState(0);
  const [shortcut, setShortcut] = useState('Ctrl K');
  const results = useMemo(() => entries ? searchEntries(entries, query) : [], [entries, query]);
  const close = () => setOpen(false);
  const select = (result) => { close(); history.push(result.url); };

  useEffect(() => {
    setMounted(true);
    if (/Mac|iPhone|iPad/.test(navigator.platform)) setShortcut('⌘ K');
    const self = {trigger};
    instances.push(self);
    const onKey = (event) => {
      if (!isSearchShortcut(event)) return;
      // The first bar whose button is laid out owns the shortcut; a hidden one answers only when no bar is visible.
      const owner = instances.find((instance) => instance.trigger.current?.getClientRects().length) ?? instances[0];
      if (owner !== self) return;
      event.preventDefault(); setOpen((value) => !value);
    };
    document.addEventListener('keydown', onKey);
    return () => {
      instances.splice(instances.indexOf(self), 1);
      document.removeEventListener('keydown', onKey);
    };
  }, []);
  useEffect(() => { setOpen(false); }, [location.pathname]);
  useEffect(() => {
    if (!mounted) return undefined;
    if (!open) { dialog.current?.close(); return undefined; }
    const previousFocus = document.activeElement;
    const overflow = document.body.style.overflow;
    dialog.current.showModal();
    document.body.style.overflow = 'hidden';
    setQuery(''); setActive(0); setError(false);
    input.current.focus();
    let cancelled = false;
    loadIndex().then((data) => { if (!cancelled) setEntries(data); }).catch(() => {
      indexPromise = undefined;
      if (!cancelled) setError(true);
    });
    return () => {
      cancelled = true;
      dialog.current?.close();
      document.body.style.overflow = overflow;
      (previousFocus?.isConnected ? previousFocus : trigger.current)?.focus();
    };
  }, [open, mounted]);
  useEffect(() => {
    if (open) document.getElementById(`${id}-${active}`)?.scrollIntoView({block: 'nearest'});
  }, [active, open, id]);

  return <>
    <button ref={trigger} type="button" className={styles.trigger} onClick={() => setOpen(true)} aria-label={ru ? 'Поиск по документации' : 'Search documentation'} title={ru ? `Поиск (${shortcut})` : `Search (${shortcut})`} aria-haspopup="dialog">
      <SearchIcon />
    </button>
    {mounted && createPortal(<dialog ref={dialog} className={styles.dialog} aria-label={ru ? 'Поиск по документации' : 'Search documentation'} onCancel={close} onClick={(event) => { if (event.target === event.currentTarget) close(); }}>
      <div className={styles.surface}>
        <div className={styles.inputRow}>
          <SearchIcon />
          <input ref={input} value={query} onChange={(event) => { setQuery(event.target.value); setActive(0); }}
            placeholder={ru ? 'Найти тип, возможность или пример…' : 'Find a type, feature or sample…'}
            aria-label={ru ? 'Поисковый запрос' : 'Search query'} role="combobox" aria-autocomplete="list" aria-expanded={open}
            aria-controls={`${id}-results`} aria-activedescendant={results[active] ? `${id}-${active}` : undefined}
            onKeyDown={(event) => {
              if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
                event.preventDefault();
                if (results.length) setActive((value) => (value + (event.key === 'ArrowDown' ? 1 : -1) + results.length) % results.length);
              }
              if (event.key === 'Enter' && results[active]) { event.preventDefault(); select(results[active]); }
              // The dialog's own cancel covers Esc in most browsers; this also covers synthetic key events.
              if (event.key === 'Escape') { event.preventDefault(); close(); }
            }} />
          <button type="button" className={styles.close} onClick={close} aria-label={ru ? 'Закрыть поиск' : 'Close search'} title={ru ? 'Закрыть (Esc)' : 'Close (Esc)'}><CloseIcon /></button>
        </div>
        <p className={styles.status} role="status">{error ? (ru ? 'Не удалось загрузить поиск. Закройте и попробуйте снова.' : 'Search could not load. Close and try again.')
          : !entries ? (ru ? 'Загрузка…' : 'Loading…')
          : !query.trim() ? (ru ? 'Начните с документации' : 'Start exploring')
          : results.length ? (ru ? `Результаты: ${results.length}` : `${results.length} results`)
          : (ru ? 'Ничего не найдено. Попробуйте другое название или термин.' : 'No results. Try another name or term.')}</p>
        <ul id={`${id}-results`} role="listbox" aria-label={ru ? 'Результаты поиска' : 'Search results'} className={styles.results}>
          {results.map((result, index) => <li key={result.url} id={`${id}-${index}`} role="option" aria-selected={active === index}
            className={styles.result} onMouseMove={() => setActive(index)} onClick={() => select(result)}>
            <span className={styles.resultHeading}><strong>{result.title}</strong><span className={styles.section}>{sectionLabel(result.section)}</span></span>
            <span className={styles.description}>{result.snippet || result.description}</span>
          </li>)}
        </ul>
      </div>
    </dialog>, document.body)}
  </>;
}
