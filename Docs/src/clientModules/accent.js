import ExecutionEnvironment from '@docusaurus/ExecutionEnvironment';
import siteConfig from '@generated/docusaurus.config';
import {applyAccent, readAccent} from '../accents';

// The boot script in <head> has already set the palette; the favicon link only exists once the head is parsed.
if (ExecutionEnvironment.canUseDOM) applyAccent(readAccent(), siteConfig.baseUrl);
