import React, {useEffect, useRef} from 'react';
import {sendWave} from '../DotRipple';
import {INTRO_KEY} from '../../intro';
import {startLogoMotion} from './motion';
import './styles.css';

const WAVE_POWER = 2; // the wave the assembled logo sends out: as strong as a fully charged click
// ms after the navigation by which the entrance must start: styles.css shows the banner by itself from 3 s on.
const LATEST_START = 2700;

/**
 * The README banner on the introduction. GitHub shows the PNG (`Docs/docs/Images/Aspid.MVVMHeaderImage.png`); the site
 * rebuilds it from the logo and live text, without a background: on wide screens the article panel leaves the banner open
 * (`custom.css`), so the page's dot canvas, its light and ripples included, shows through. Narrower, the banner paints the
 * dots itself, and `DotAmbient` draws its glow and sparks in the `__ambient` layer above them. The page hides its title,
 * so the name is the page's only `<h1>`; the banner must stay the article's first child.
 *
 * Once per tab session it makes an entrance (`src/intro.js` arms it before the first paint, `motion.js` draws it): the
 * logo's low-poly pieces take off from the dots and assemble the snake from the tail to the snout. Then a wave runs from it
 * through the dots and shakes the page as a burst does, the name follows the wave, the banner's glow rises and the eye
 * flashes. Afterwards the snake blinks now and then, and a light runs along its body every few seconds and on hover.
 */
export default function IntroBanner({alt}) {
  const ref = useRef(null);
  const brand = alt.indexOf('.') + 1;

  useEffect(() => {
    const root = document.documentElement;
    const banner = ref.current;
    if (!banner || matchMedia('(prefers-reduced-motion: reduce)').matches) return undefined;
    let entrance = root.hasAttribute('data-intro');
    if (entrance && performance.now() > LATEST_START) {
      root.removeAttribute('data-intro');
      entrance = false;
    }
    if (entrance) {
      banner.dataset.assembling = '';
      try {
        sessionStorage.setItem(INTRO_KEY, '1');
      } catch {
        // Storage may be blocked; the entrance then plays on every visit.
      }
    }

    // The logo is whole: styles.css shows it and starts the name, the wave runs from it.
    const close = () => {
      banner.dataset.closed = '';
      banner.dataset.glowAt = String(performance.now());
      // Below 997px the wave runs through the banner's own dots only.
      const box = banner.querySelector('.readme-banner__logo').getBoundingClientRect();
      sendWave(box.left + box.width / 2, box.top + box.height / 2, WAVE_POWER, {shake: true});
      // Animations removed with the banner reject, so leaving the page early ends the entrance too.
      requestAnimationFrame(() => Promise.allSettled(banner.getAnimations({subtree: true}).map((animation) => animation.finished))
        .then(() => root.removeAttribute('data-intro')));
    };

    const stop = startLogoMotion(banner, {entrance, onClose: close});
    return () => {
      stop();
      if (!banner.hasAttribute('data-closed')) root.removeAttribute('data-intro');
    };
  }, []);

  return (
    <div className="readme-banner" ref={ref}>
      <div className="readme-banner__ambient" aria-hidden="true" />
      <div className="readme-banner__content">
        <div className="readme-banner__mark" aria-hidden="true">
          <div className="readme-banner__logo" />
        </div>
        <div className="readme-banner__text">
          <h1 className="readme-banner__title"><span>{alt.slice(0, brand)}</span>{alt.slice(brand)}</h1>
          {/* The brand line: English in every locale, as on the README banner. */}
          <p className="readme-banner__tagline">MVVM Framework for Unity</p>
        </div>
      </div>
      <canvas className="readme-banner__fx" aria-hidden="true" />
    </div>
  );
}
