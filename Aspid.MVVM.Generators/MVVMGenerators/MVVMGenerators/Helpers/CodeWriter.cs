using System;
using System.IO;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis.Text;

namespace MVVMGenerators.Helpers;

public sealed class CodeWriter
{
    private readonly MemoryStream _sourceStream;
    private readonly IndentedTextWriter _textWriter;
    
    public int Indent
    {
        get => _textWriter.Indent;
        set => _textWriter.Indent = value;
    }
    
    public CodeWriter()
    {
        _sourceStream = new MemoryStream();
        var sourceStreamWriter = new StreamWriter(_sourceStream, Encoding.UTF8);
        _textWriter = new IndentedTextWriter(sourceStreamWriter);
    }
    
    public CodeWriter Append(string value = "")
    {
        _textWriter.Write(value);
        return this;
    }
    
    public CodeWriter AppendLine(string value = "")
    {
        _textWriter.WriteLine(value);
        return this;
    }

    public CodeWriter AppendMultiline(string value)
    {
        var indent = Indent;
        Indent = 0;

        var tab = new string('\t', indent);
        value = $"{tab}{value}";
        value = value.Replace("\n", $"\n{tab}");
        AppendLine(value);

        Indent = indent;
        return this;
    }
    
    public IDisposable BeginIndentScope() => 
        new IndentScope(this);
    
    public IDisposable BeginBlockScope() => 
        new BlockScope(this);
    
    public CodeWriter BeginBlock()
    {
        AppendLine("{");
        IncreaseIndent();
        
        return this;
    }
    
    public CodeWriter EndBlock()
    {
        DecreaseIndent();
        AppendLine("}");
        
        return this;
    }
    
    public CodeWriter IncreaseIndent()
    {
        _textWriter.Indent++;
        return this;
    }
    
    public CodeWriter DecreaseIndent()
    {
        _textWriter.Indent--;
        return this;
    }
    
    public SourceText GetSourceText()
    {
        _textWriter.Flush();
        return SourceText.From(_sourceStream, Encoding.UTF8, canBeEmbedded: true);
    }
    
    private readonly struct IndentScope : IDisposable
    {
        private readonly CodeWriter _source;
        
        public IndentScope(CodeWriter source)
        {
            _source = source;
            source.IncreaseIndent();
        }
        
        public void Dispose() =>
            _source.DecreaseIndent();
    }
    
    private readonly struct BlockScope : IDisposable
    {
        private readonly CodeWriter _source;
        
        public BlockScope(CodeWriter source)
        {
            _source = source;
            source.BeginBlock();
        }
        
        public void Dispose() =>
            _source.EndBlock();
    }
}