import React from 'react';
import {Highlight} from 'prism-react-renderer';
import {usePrismTheme} from '@docusaurus/theme-common';

/**
 * One line of code highlighted with the site's Prism theme, keeping the inline-code chip.
 * `string`, `class-name` and `function` are token types, not languages: the text takes that token's colour
 * (a Profiler marker name, a lone type such as `T`, a bare method name such as `Update`). A qualified type or method keeps
 * its qualifier plain, as the code blocks do: in `System.Type` only `Type` is coloured, in `styleSheets.Add` only `Add`.
 * Punctuation stays plain too, so a generic type reads like a code block: in `List<Weapon>` both names are coloured and
 * the brackets are not.
 */
const TOKEN_TYPES = ['string', 'class-name', 'function'];

export default function InlineCode({code, language}) {
  const theme = usePrismTheme();
  if (TOKEN_TYPES.includes(language)) {
    const color = theme.styles.find((style) => style.types.includes(language))?.style.color;
    if (language === 'string') {
      return <code style={{color: theme.plain.color}}><span style={{color}}>{code}</span></code>;
    }
    const generic = code.indexOf('<');
    const split = code.lastIndexOf('.', generic < 0 ? code.length : generic) + 1;
    return (
      <code style={{color: theme.plain.color}}>
        {code.slice(0, split)}
        {code.slice(split).split(/(\W+)/).map((part, index) =>
          index % 2 === 0 && part ? <span key={index} style={{color}}>{part}</span> : part)}
      </code>
    );
  }
  return (
    <Highlight theme={theme} code={code} language={language}>
      {({tokens, getTokenProps}) => (
        <code className={`language-${language}`} style={{color: theme.plain.color}}>
          {tokens.flat().map((token, index) => {
            const {key, ...props} = getTokenProps({token});
            return <span key={index} {...props} />;
          })}
        </code>
      )}
    </Highlight>
  );
}
