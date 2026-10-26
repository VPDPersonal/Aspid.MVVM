/**
 * The site's accent colours: green is the default, the others match the package's logo variants
 * (`Editor/Resources/Icons/aspid_icon_medium_<colour>_256x253.png`, copied to `static/img/logo-<colour>.png`);
 * `mono` uses `static/img/logo-mono.png`, a greyscale copy of the green logo.
 * The palettes live in `src/css/accents.css`, keyed by `html[data-accent]`; the default has no attribute.
 * Plain module: `docusaurus.config.js` imports it for the boot script, so nothing here may touch the DOM on load.
 */
export const ACCENTS = ['green', 'red', 'blue', 'yellow', 'mono'];
export const DEFAULT_ACCENT = 'green';

const STORAGE_KEY = 'aspid-accent';

/** Runs in `<head>` before the first paint, so the stored accent never flashes green. */
export const ACCENT_BOOT_SCRIPT =
  `try{var a=localStorage.getItem('${STORAGE_KEY}');` +
  `if(/^(${ACCENTS.filter((accent) => accent !== DEFAULT_ACCENT).join('|')})$/.test(a))` +
  `document.documentElement.setAttribute('data-accent',a)}catch(e){}`;

export const accentLogo = (accent) => (accent === DEFAULT_ACCENT ? 'img/logo.png' : `img/logo-${accent}.png`);

export function readAccent() {
  try {
    const stored = localStorage.getItem(STORAGE_KEY);
    return ACCENTS.includes(stored) ? stored : DEFAULT_ACCENT;
  } catch {
    return DEFAULT_ACCENT;
  }
}

/** Paints `accent` (palette, logo, favicon); `save` also remembers it for the next visit. */
export function applyAccent(accent, baseUrl, {save = false} = {}) {
  const root = document.documentElement;
  if (accent === DEFAULT_ACCENT) root.removeAttribute('data-accent');
  else root.setAttribute('data-accent', accent);

  for (const link of document.querySelectorAll('link[rel~="icon"]')) link.href = baseUrl + accentLogo(accent);

  if (!save) return;
  try {
    if (accent === DEFAULT_ACCENT) localStorage.removeItem(STORAGE_KEY);
    else localStorage.setItem(STORAGE_KEY, accent);
  } catch {
    // Private mode or blocked storage: the choice lasts until the page is reloaded.
  }
}
