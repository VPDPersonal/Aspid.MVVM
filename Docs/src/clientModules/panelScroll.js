/**
 * On docs pages the article scrolls inside its own panel. Docusaurus' TOC highlight listens to
 * `document` scroll events, which element scrolling never reaches, so re-dispatch them.
 */
import ExecutionEnvironment from '@docusaurus/ExecutionEnvironment';

let attached = null;

function attach() {
  const panel = document.querySelector("[class*='docItemContainer_']");
  if (panel === attached) return;
  attached?.removeEventListener('scroll', forward);
  attached = panel;
  panel?.addEventListener('scroll', forward, { passive: true });
}

function forward() {
  document.dispatchEvent(new Event('scroll'));
}

if (ExecutionEnvironment.canUseDOM) {
  window.addEventListener('load', attach);
}

export function onRouteDidUpdate() {
  // The new page's DOM is committed after this callback; wait one frame.
  requestAnimationFrame(attach);
}
