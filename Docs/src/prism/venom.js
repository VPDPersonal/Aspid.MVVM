/** Prism themes in the Ayu palette (Light and Dark), Dark uses the Ayu editor background as in Rider. */

const light = {
  plain: { color: '#4b463d', backgroundColor: '#fcf9f3' },
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#9a9184' } },
    { types: ['punctuation'], style: { color: '#4b463d' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#fa8d3e' } },
    { types: ['builtin', 'class-name', 'namespace', 'maybe-class-name', 'return-type'], style: { color: '#2f8fd6' } },
    { types: ['function'], style: { color: '#f2ae49' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#7a9e12' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#a37acc' } },
    { types: ['regex'], style: { color: '#4cbf99' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#55b4d4' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#f07171' } },
    { types: ['annotation', 'decorator', 'attribute'], style: { color: '#2f8fd6' } },
  ],
};

const dark = {
  plain: { color: '#bfbdb6', backgroundColor: '#0e1015' },
  styles: [
    { types: ['comment', 'prolog', 'doctype', 'cdata'], style: { color: '#5c6470' } },
    { types: ['punctuation'], style: { color: '#bfbdb6' } },
    { types: ['keyword', 'operator', 'important'], style: { color: '#f29750' } },
    { types: ['builtin', 'class-name', 'namespace', 'maybe-class-name', 'return-type'], style: { color: '#73c0f8' } },
    { types: ['function'], style: { color: '#ffb454' } },
    { types: ['string', 'char', 'attr-value', 'inserted'], style: { color: '#b0d860' } },
    { types: ['number', 'boolean', 'constant', 'symbol'], style: { color: '#d2a6ff' } },
    { types: ['regex'], style: { color: '#95e6cb' } },
    { types: ['tag', 'selector', 'deleted'], style: { color: '#39bae6' } },
    { types: ['attr-name', 'property', 'variable'], style: { color: '#f07178' } },
    { types: ['annotation', 'decorator', 'attribute'], style: { color: '#73c0f8' } },
  ],
};

export default { light, dark };
