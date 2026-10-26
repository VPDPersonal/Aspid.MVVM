import React from 'react';
import Link from '@docusaurus/Link';
import useBaseUrl, {useBaseUrlUtils} from '@docusaurus/useBaseUrl';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import {useThemeConfig} from '@docusaurus/theme-common';
import {ACCENTS, accentLogo} from '../../accents';

/**
 * The theme logo in every accent colour: `src/css/accents.css` hides all but the one matching `html[data-accent]`,
 * so the static page already shows the stored accent before React hydrates. The hidden ones load lazily.
 */
export default function Logo({imageClassName, titleClassName, ...props}) {
  const {siteConfig: {title}} = useDocusaurusContext();
  const {navbar: {title: navbarTitle, logo}} = useThemeConfig();
  const {withBaseUrl} = useBaseUrlUtils();
  const logoLink = useBaseUrl(logo?.href || '/');
  const alt = logo?.alt ?? (navbarTitle ? '' : title);

  const images = logo && ACCENTS.map((accent) => (
    <img key={accent} className="accent-logo" data-accent-logo={accent} src={withBaseUrl(accentLogo(accent))}
      width={logo.width} height={logo.height} alt={alt} loading="lazy" />
  ));

  return (
    <Link to={logoLink} {...props} {...(logo?.target && {target: logo.target})}>
      {images && (imageClassName ? <div className={imageClassName}>{images}</div> : images)}
      {navbarTitle != null && <b className={titleClassName}>{navbarTitle}</b>}
    </Link>
  );
}
