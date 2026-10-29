import React from 'react';
import Head from '@docusaurus/Head';
import { translate } from '@docusaurus/Translate';
import { Redirect } from '@docusaurus/router';
import useBaseUrl from '@docusaurus/useBaseUrl';

/**
 * The site root is the documentation index; there is no separate landing page yet. `<Redirect>` runs only after
 * hydration, so the static page also redirects by itself, points crawlers and link previews at /docs and stays out
 * of the sitemap.
 */
export default function Home() {
  const docs = useBaseUrl('/docs');
  const canonical = useBaseUrl('/docs', { absolute: true });
  const description = translate({id: 'homepage.description', message: 'Source Generator-based MVVM framework for Unity'});
  return (
    <>
      <Head>
        <meta httpEquiv="refresh" content={`0; url=${docs}`} />
        <meta name="robots" content="noindex" />
        <meta name="description" content={description} />
        <meta property="og:description" content={description} />
        <meta property="og:url" content={canonical} />
        <link rel="canonical" href={canonical} />
      </Head>
      <Redirect to={docs} />
    </>
  );
}
