/** Prism themes in the Ayu palette (Light and Dark). */

const light = {
  plain: { color: '#5c6166', backgroundColor: '#f8f9fa' },
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#787b8099', fontStyle: 'italic' } },
    { types: ['punctuation'], style: { color: '#5c6166b3' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#fa8d3e' } },
    { types: ['builtin', 'class-name', 'namespace', 'maybe-class-name'], style: { color: '#399ee6' } },
    { types: ['function'], style: { color: '#f2ae49' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#86b300' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#a37acc' } },
    { types: ['regex'], style: { color: '#4cbf99' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#55b4d4' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#f07171' } },
    { types: ['annotation', 'decorator'], style: { color: '#e6ba7e' } },
  ],
};

const dark = {
  plain: { color: '#bfbdb6', backgroundColor: '#0d1017' },
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#acb6bf8c', fontStyle: 'italic' } },
    { types: ['punctuation'], style: { color: '#bfbdb6b3' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#ff8f40' } },
    { types: ['builtin', 'class-name', 'namespace', 'maybe-class-name'], style: { color: '#59c2ff' } },
    { types: ['function'], style: { color: '#ffb454' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#aad94c' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#d2a6ff' } },
    { types: ['regex'], style: { color: '#95e6cb' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#39bae6' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#f07178' } },
    { types: ['annotation', 'decorator'], style: { color: '#e6b673' } },
  ],
};

export default { light, dark };
