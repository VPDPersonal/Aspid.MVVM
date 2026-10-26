/**
 * The introduction's entrance (src/components/IntroBanner): the logo assembles from the dots, a wave runs through them and
 * the name follows it. It plays once per tab session and never under reduced motion.
 * Plain module: `docusaurus.config.js` imports it for the boot script, so nothing here may touch the DOM on load.
 */
export const INTRO_KEY = 'aspid-intro-played';

/** Runs in `<head>` before the first paint, so a banner that is about to play never shows its final state first. */
export const INTRO_BOOT_SCRIPT =
  `try{if(!sessionStorage.getItem('${INTRO_KEY}')&&!matchMedia('(prefers-reduced-motion: reduce)').matches)` +
  `document.documentElement.setAttribute('data-intro','')}catch(e){}`;
