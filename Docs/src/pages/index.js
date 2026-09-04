import React from 'react';
import { Redirect } from '@docusaurus/router';
import useBaseUrl from '@docusaurus/useBaseUrl';

// The site has no landing page: the documentation index is the front door.
export default function Home() {
  return <Redirect to={useBaseUrl('/docs')} />;
}
