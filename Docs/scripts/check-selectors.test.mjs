import fs from 'node:fs';
import assert from 'node:assert/strict';
import {test} from 'node:test';
import {classifyFragments, definedClasses, fragmentsIn, hashedClasses, isSiteClass, isUnhashedName, matchingClasses} from './check-selectors.mjs';

test('fragments are read from CSS and from selectors in JS strings', () => {
  const css = ".navbar__items--right > [class*='colorModeToggle_'] { order: 3; }\na[class^=\"nav\"], [class*=card_] {}";
  const js = `const ARTICLE = '[class*="docMainContainer_"] > .container'; const name = 'theme-doc-markdown';`;
  assert.deepEqual([...fragmentsIn(css)].sort(), ['card_', 'colorModeToggle_', 'nav']);
  assert.deepEqual([...fragmentsIn(js)], ['docMainContainer_']);
});

test('only CSS-module classes with a four character hash count as built classes', () => {
  const css = '.docMainContainer_q81F{flex:1}.navbar__item,.footer__item{margin:0}.card__body.sidebar_label{x:0}'
    + '.buttonGroup_w8dF>button:hover{x:0}a[href$=".woff2"]{x:0}';
  assert.deepEqual([...hashedClasses(css)].sort(), ['buttonGroup_w8dF', 'docMainContainer_q81F']);
});

test('a hash may contain an underscore', () => {
  const css = '.navbarHideable_f_bj{x:0}.footerLogoLink_H9z_{x:0}.announcementBarContent_U_hK{x:0}.menu__link{x:0}.card__body{x:0}';
  assert.deepEqual([...hashedClasses(css)].sort(), ['announcementBarContent_U_hK', 'footerLogoLink_H9z_', 'navbarHideable_f_bj']);
});

test('a fragment matches the hashed classes that contain it', () => {
  const classes = new Set(['docMainContainer_q81F', 'docMainContainer_a-b1', 'buttonGroup_w8dF']);
  assert.deepEqual(matchingClasses('docMainContainer_', classes), ['docMainContainer_q81F', 'docMainContainer_a-b1']);
  assert.deepEqual(matchingClasses('buttonGroup', classes), ['buttonGroup_w8dF']);
  assert.deepEqual(matchingClasses('docRoot_', classes), []);
});

test('classes read from another module are not defined by the stylesheet', () => {
  const css = ':global(.markdown) a.versions:not(:global(.card)) { color: red; }\n.panel { margin: 0.5rem; }';
  assert.deepEqual([...definedClasses(css)].sort(), ['panel', 'versions']);
});

test('a fragment that the site CSS modules also produce is the site\'s own', () => {
  const locals = new Set(['card', 'footer', 'docItemContainer']);
  assert.ok(isSiteClass('card_', locals));
  assert.ok(isSiteClass('docItemContainer_', locals));
  assert.ok(!isSiteClass('docMainContainer_', locals));
  assert.ok(!isSiteClass('sidebar_', locals));
});

test('public class names have no hash', () => {
  assert.ok(isUnhashedName('theme-doc-sidebar-menu'));
  assert.ok(isUnhashedName('menu__link'));
  assert.ok(isUnhashedName('button--primary'));
  assert.ok(!isUnhashedName('docMainContainer_'));
  assert.ok(!isUnhashedName('navbarHideable_f_bj'));
});

test('fragments are sorted into matched, missing, left to the site and unhashed', () => {
  const built = new Set(['docMainContainer_q81F', 'navbarHideable_f_bj', 'card_Ihdi']);
  const locals = new Set(['card']);
  const fragments = new Set(['navbarHideable_', 'card_', 'docRoot_', 'docMainContainer_', 'theme-doc-sidebar-menu', 'menu__link']);
  assert.deepEqual(classifyFragments(fragments, locals, built), {
    matched: ['docMainContainer_', 'navbarHideable_'],
    missing: ['docRoot_'],
    skipped: ['card_'],
    unhashed: ['menu__link', 'theme-doc-sidebar-menu'],
  });
});

test('the site sources still select Docusaurus classes by fragment', () => {
  const css = fs.readFileSync(new URL('../src/css/custom.css', import.meta.url), 'utf8');
  assert.ok(fragmentsIn(css).has('docMainContainer_'));
});
