import React, {useEffect, useLayoutEffect, useRef, useState} from 'react';
import {createPortal} from 'react-dom';
import OriginalImg from '@theme-original/MDXComponents/Img';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import {useLocation} from '@docusaurus/router';

const CLOSE_MS = 140;      // must match the closing animation of .doc-image-dialog in custom.css
const DRAG_THRESHOLD = 4;  // px a pressed mouse travels before the press pans instead of clicking
const MAX_ZOOM = 3;        // a raster opens at most this many times its fitted width, so phones pan a sane distance
const VECTOR_ZOOM = 2;     // an SVG has no natural pixel size to stop at

const isVector = (src) => /^data:image\/svg\+xml|\.svg(?:[?#]|$)/i.test(src);
const reducedMotion = () => window.matchMedia('(prefers-reduced-motion: reduce)').matches;

// The width a click enlarges the fitted image to, or 0 when enlarging would add nothing.
function zoomWidth(img, src) {
  const fitted = img.offsetWidth;  // the layout width, unaffected by the opening scale animation
  const width = isVector(src) ? fitted * VECTOR_ZOOM : Math.min(img.naturalWidth, fitted * MAX_ZOOM);
  return width > fitted * 1.15 ? Math.round(width) : 0;
}

// `…/Images/serializable-type-quick-start-light-<hash>.gif` → `serializable-type-quick-start`.
const captureName = (src) => src.replace(/^.*\//, '').replace(/\.[a-z]+(?:[?#].*)?$/i, '').replace(/-[0-9a-f]{20,}$/, '')
  .replace(/-light$/, '');

// Captures of a whole editor window carry the window's own margin around its cards; doc pages crop it to the same inset
// as the framed captures (custom.css). Add the capture names here once the docs have such captures.
const WINDOW_CAPTURES = /(?!)/;
const WINDOW_MARGIN = 24;

export default function DocImage(props) {
  const {i18n, siteConfig} = useDocusaurusContext();
  const ru = i18n.currentLocale === 'ru';
  const {pathname} = useLocation();
  const [preview, setPreview] = useState(null);
  const [closing, setClosing] = useState(false);
  const [zoomable, setZoomable] = useState(0);
  const [zoom, setZoom] = useState(null);
  const [panning, setPanning] = useState(false);
  const dialog = useRef(null);
  const view = useRef(null);
  const picture = useRef(null);
  const opener = useRef(null);
  const drag = useRef(null);
  // Every editor capture in a guide or tutorial gets the full-width capture wrapper; scene and demo
  // captures keep the plain sample-scene look in tutorials and get the wrapper only in guides.
  const article = /\/(?:docs|tutorials)\/./.test(pathname);
  const sceneCapture = typeof props.src === 'string'
    && /\/(?:demo|scene)(?:-light)?(?:-[0-9a-f]{8,})?\.(?:gif|png)$/i.test(props.src);
  const framedCapture = article && (!sceneCapture || /\/docs\//.test(pathname));
  // A capture framed in its file is drawn by its padding as a border image (custom.css, `.doc-image-capture`).
  const {margin, names} = siteConfig.customFields.framedCaptures;
  const marginCapture = framedCapture && typeof props.src === 'string' && Number(props.width) > 0
    && names.includes(captureName(props.src));
  const windowCapture = framedCapture && !marginCapture && typeof props.src === 'string' && Number(props.width) > 0
    && WINDOW_CAPTURES.test(captureName(props.src));
  useEffect(() => {
    if (!preview) return undefined;
    const overflow = document.body.style.overflow;
    dialog.current.showModal();
    // The scrolling view takes focus, so a mouse user sees no focus ring, arrow keys pan an enlarged image and Tab
    // still reaches the buttons.
    view.current.focus({preventScroll: true});
    document.body.style.overflow = 'hidden';
    return () => {
      document.body.style.overflow = overflow;
      opener.current?.focus();
    };
  }, [preview]);
  useEffect(() => {
    if (!preview || zoom) return undefined;
    const measure = () => picture.current?.complete && setZoomable(zoomWidth(picture.current, preview.src));
    measure();
    window.addEventListener('resize', measure);
    return () => window.removeEventListener('resize', measure);
  }, [preview, zoom]);
  // Keeps the point that was clicked under the pointer once the image is at its enlarged size.
  useLayoutEffect(() => {
    if (!zoom) return;
    const box = view.current.getBoundingClientRect();
    const rect = picture.current.getBoundingClientRect();
    view.current.scrollLeft = rect.left - box.left + zoom.fx * rect.width - zoom.x;
    view.current.scrollTop = rect.top - box.top + zoom.fy * rect.height - zoom.y;
  }, [zoom]);
  // Status badges are links, not documentation screenshots to enlarge.
  if (props.className?.split(' ').includes('readme-status-badge')
    || (typeof props.src === 'string' && props.src.startsWith('https://img.shields.io/'))) {
    return <OriginalImg {...props} />;
  }
  function show(event) {
    if (event.currentTarget.closest('a')) return;
    opener.current = event.currentTarget;
    setZoom(null);
    setZoomable(0);
    setClosing(false);
    // A lazy image that has not started loading yet, such as the other theme's variant, has no currentSrc.
    const {currentSrc, src} = event.currentTarget;
    setPreview({src: currentSrc || src, scene: !!event.currentTarget.closest('.sample-scene')});
  }
  function close() {
    if (closing) return;
    if (reducedMotion()) { setPreview(null); return; }
    setClosing(true);
    setTimeout(() => { setPreview(null); setClosing(false); }, CLOSE_MS);
  }
  // Enlarges around (x, y) in viewport pixels, the centre of the screen when the zoom button or a key asks for it.
  function zoomIn(x = innerWidth / 2, y = innerHeight / 2) {
    if (!zoomable || zoom) return;
    const rect = picture.current.getBoundingClientRect();
    const clamp = (value) => Math.min(1, Math.max(0, value));
    setZoom({width: zoomable, x, y, fx: clamp((x - rect.left) / rect.width), fy: clamp((y - rect.top) / rect.height)});
  }
  function onPictureClick(event) {
    event.stopPropagation();
    if (drag.current?.moved) return;
    if (zoom) setZoom(null);
    else if (zoomable) zoomIn(event.clientX, event.clientY);
    else close();
  }
  function onPointerDown(event) {
    if (!zoom || event.pointerType !== 'mouse' || event.button !== 0 || event.target.closest('button')) return;
    drag.current = {x: event.clientX, y: event.clientY, left: view.current.scrollLeft, top: view.current.scrollTop, moved: false};
  }
  function onPointerMove(event) {
    const pan = drag.current;
    if (!pan || !(event.buttons & 1)) return;
    const dx = event.clientX - pan.x;
    const dy = event.clientY - pan.y;
    if (!pan.moved && Math.hypot(dx, dy) < DRAG_THRESHOLD) return;
    // Captured only once it is a drag: a capture retargets the click, and a plain click must reach the image.
    if (!pan.moved) { pan.moved = true; setPanning(true); dialog.current.setPointerCapture(event.pointerId); }
    view.current.scrollLeft = pan.left - dx;
    view.current.scrollTop = pan.top - dy;
  }
  function onPointerUp(event) {
    if (!drag.current) return;
    if (dialog.current.hasPointerCapture(event.pointerId)) dialog.current.releasePointerCapture(event.pointerId);
    setPanning(false);
    // The click that follows the release still has to see the drag, so it is cleared after that click.
    setTimeout(() => { drag.current = null; });
  }
  function onKeyDown(event) {
    if (event.key === '+' || event.key === '=') { event.preventDefault(); zoomIn(); }
    else if ((event.key === '-' || event.key === '0') && zoom) { event.preventDefault(); setZoom(null); }
  }
  const image = <OriginalImg {...props} className={`doc-zoom-image ${props.className || ''}`} role="button" tabIndex={0}
      aria-label={`${ru ? 'Увеличить изображение' : 'Enlarge image'}: ${props.alt || ''}`}
      aria-haspopup="dialog" onClick={show} onKeyDown={(event) => {
        if (event.key === 'Enter' || event.key === ' ') { event.preventDefault(); show(event); }
      }} />;
  const zoomLabel = zoom ? (ru ? 'Вписать в экран' : 'Fit to screen') : (ru ? 'Увеличить' : 'Zoom in');
  const pictureClass = ['doc-image-picture', isVector(preview?.src || '') && 'doc-image-picture--vector',
    zoomable && !zoom && 'doc-image-picture--zoomable'].filter(Boolean).join(' ');
  const img = <img ref={picture} className={pictureClass} src={preview?.src} alt={props.alt || ''} draggable={false}
    style={zoom ? {width: zoom.width} : undefined} onClick={onPictureClick}
    onLoad={() => !zoom && setZoomable(zoomWidth(picture.current, preview.src))} />;
  return <>
    {marginCapture
      ? <span className="doc-image-panel doc-image-panel--margin"
        style={{'--capture-width': Number(props.width), '--capture-margin-x': margin, '--capture-margin-y': margin}}>
        <span className="doc-image-capture" style={{borderImageSource: `url("${props.src}")`}}>{image}</span>
      </span>
      : windowCapture
        ? <span className="doc-image-panel doc-image-panel--margin doc-image-panel--window"
          style={{'--capture-width': Number(props.width), '--capture-margin': WINDOW_MARGIN}}>{image}</span>
        : framedCapture
          ? <span className="doc-image-panel">{image}</span>
          : image}
    {preview && createPortal(<dialog ref={dialog} className="doc-image-dialog"
      aria-label={props.alt || (ru ? 'Просмотр изображения' : 'Image preview')}
      data-zoomed={zoom ? '' : undefined} data-closing={closing ? '' : undefined} data-panning={panning ? '' : undefined}
      onCancel={(event) => { event.preventDefault(); close(); }} onClose={() => setPreview(null)}
      onClick={() => !drag.current?.moved && close()}
      onKeyDown={onKeyDown} onPointerDown={onPointerDown} onPointerMove={onPointerMove} onPointerUp={onPointerUp}
      onPointerCancel={onPointerUp}>
      <div className="doc-image-controls">
        <button type="button" className="doc-image-control" onClick={(event) => { event.stopPropagation(); close(); }}
          aria-label={ru ? 'Закрыть' : 'Close'} aria-keyshortcuts="Escape">
          <svg viewBox="0 0 16 16" aria-hidden="true"><path d="M4 4l8 8M12 4l-8 8" /></svg>
          <kbd aria-hidden="true">Esc</kbd>
        </button>
        {(zoomable > 0 || zoom) && <button type="button" className="doc-image-control" aria-label={zoomLabel}
          aria-pressed={!!zoom} aria-keyshortcuts={zoom ? '-' : '+'}
          onClick={(event) => { event.stopPropagation(); zoom ? setZoom(null) : zoomIn(); }}>
          <svg viewBox="0 0 16 16" aria-hidden="true">
            <circle cx="7" cy="7" r="4.5" /><path d={zoom ? 'M5 7h4M10.4 10.4L14 14' : 'M5 7h4M7 5v4M10.4 10.4L14 14'} />
          </svg>
          <kbd aria-hidden="true">{zoom ? '−' : '+'}</kbd>
        </button>}
      </div>
      {/* The dialog's backdrop blur would pin fixed children to its scrolling box, so an inner view scrolls instead. */}
      <div ref={view} className="doc-image-view" tabIndex={-1}>
        <figure className={`doc-image-figure${props.alt ? ' doc-image-figure--captioned' : ''}`}>
          {preview.scene ? <div className="sample-scene">{img}</div> : img}
          {props.alt && <figcaption onClick={(event) => event.stopPropagation()}>{props.alt}</figcaption>}
        </figure>
      </div>
    </dialog>, document.body)}
  </>;
}
