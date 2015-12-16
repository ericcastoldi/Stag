//using System;
//using System.Collections.Generic;
//using System.IO.Abstractions;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Stag.Service
//{
//    public class ClassSplitterService
//    {
//        private IList<CodeScope> _scopes;
//        private IFileSystem _fileSystem;
//        private const string ClassExtension = "*.cs";

//        public ClassSplitterService()
//            : this(new FileSystem())
//        {
//        }

//        public ClassSplitterService(IFileSystem fileSystem)
//        {
//            _fileSystem = fileSystem;
//        }

//        public void Split(string classesDirectory)
//        {
//            EvaluateScopes(classesDirectory);
//            foreach (var scope in _scopes)
//                GenerateCSharpFile(scope, classesDirectory);
//        }

//        private void GenerateCSharpFile(CodeScope scope, string classesDirectory)
//        {
//            if (!_fileSystem.Directory.Exists(_fileSystem.Path.Combine(classesDirectory, "NOME_DA_CLASSE")))
//                _fileSystem.Directory.CreateDirectory(settings.StorageBasePath);

//            _fileSystem.File.WriteAllText(_storepath, serializedItems, Encoding.UTF8);
//            throw new NotImplementedException();
//        }

//        private void EvaluateScopes(string classesDirectory)
//        {
//            var files = GetCSharpFiles(classesDirectory);

//            foreach (var filename in files)
//            {
//                var lines = _fileSystem.File.ReadLines(classesDirectory);
//                var scopeManager = new CodeScopeManager();

//                scopeManager.EvaluateScopes(lines);
//                _scopes.Add(scopeManager.Scope);
//            }
//        }

//        private IEnumerable<string> GetCSharpFiles(string classesDirectory)
//        {
//            var dir = _fileSystem.Directory.EnumerateFiles(classesDirectory, ClassExtension);
//            return dir;
//        }
//    }

//    public class CodeFile
//    {
//        public IList<string> Lines { get; set; }
//        public string[] ClassKeywords = new string[] { "public", "private", "internal", "static", "class", "abstract", "partial" };

//        public CodeFile()
//        {
//            Lines = new List<String>();
//        }

//        public void EvaluateScope(CodeScope scope)
//        {
//            if (scope.Type == ScopeType.Global)
//            {
//                foreach (var childScope in scope.ChildScopes)
//                {
//                    EvaluateScope(childScope, Lines);
//                }
//            }
//            else
//            {
//                EvaluateScope(scope, Lines);
//            }
//        }

//        public void EvaluateScope(CodeScope scope, IList<string> lineList)
//        {
//            foreach (var token in scope.Tokens)
//            {
//                lineList.Add(token.Content);
//            }

//            if (scope.ChildScopes != null && scope.ChildScopes.Count > 0)
//            {
//                foreach (var childScope in scope.ChildScopes)
//                {
//                    EvaluateScope(childScope, lineList);
//                }
//            }
//        }

//    }

//    public class CodeScopeManager
//    {
//        private int _openedScopes = 0;
//        private TokenType[] _scopeOpeners;
//        private CodeScope _currentScope;
//        private Dictionary<TokenType, ScopeType> _tokenScopeCorrelation;

//        public CodeScope Scope { get { return _currentScope; } }

//        public CodeScopeManager()
//        {
//            _currentScope = new CodeScope(ScopeType.Global);
//            _scopeOpeners = new TokenType[]
//            {
//                TokenType.NamespaceDeclaration,
//                TokenType.EnumDeclaration,
//                TokenType.ClassDeclaration,
//                TokenType.PropertyDeclaration,
//                TokenType.PropertyGetter,
//                TokenType.PropertySetter,
//                TokenType.MethodDeclaration
//            };

//            _tokenScopeCorrelation = new Dictionary<TokenType, ScopeType>();
//            _tokenScopeCorrelation.Add(TokenType.NamespaceDeclaration, ScopeType.Namespace);
//            _tokenScopeCorrelation.Add(TokenType.EnumDeclaration, ScopeType.Class);
//            _tokenScopeCorrelation.Add(TokenType.ClassDeclaration, ScopeType.Enum);
//            _tokenScopeCorrelation.Add(TokenType.PropertyDeclaration, ScopeType.Property);
//            _tokenScopeCorrelation.Add(TokenType.PropertyGetter, ScopeType.PropertyGetter);
//            _tokenScopeCorrelation.Add(TokenType.PropertySetter, ScopeType.PropertySetter);
//            _tokenScopeCorrelation.Add(TokenType.MethodDeclaration, ScopeType.Method);
//        }

//        public void EvaluateScopes(IEnumerable<string> lines)
//        {
//            var tokenChain = new TokenParseChain();

//            foreach (var line in lines)
//            {
//                var parseResult = tokenChain.Parse(line);
//                if (parseResult.Success)
//                {
//                    _currentScope.AddToken(parseResult.Token);

//                    if (_scopeOpeners.Contains(parseResult.Token.Type))
//                    {
//                        var newScope = new CodeScope(_tokenScopeCorrelation[parseResult.Token.Type], _currentScope);

//                        _currentScope.AddChildScope(newScope);
//                        _currentScope = newScope;
//                        _openedScopes++;
//                    }
//                    else if (parseResult.Token.Type == TokenType.ScopeEnding)
//                    {
//                        _currentScope = _currentScope.Parent;
//                        _openedScopes--;
//                    }
//                }
//            }

//            if (_openedScopes != 0)
//            {
//                throw new InvalidOperationException(string.Format("Não foi possível detectar os escopos do arquivo. Ficaram {0} escopos pendentes de fechamento.", _openedScopes));
//            }
//        }
//    }

//    public enum ScopeType
//    {
//        Global,
//        Namespace,
//        Class,
//        Enum,
//        Property,
//        PropertyGetter,
//        PropertySetter,
//        Method
//    }

//    public class CodeScope
//    {
//        private CodeScope _parent;
//        private ScopeType _type;
//        private IList<IToken> _tokens;
//        private IList<CodeScope> _scopes;

//        public CodeScope(ScopeType type, CodeScope parent = null)
//        {
//            _type = type;
//            _parent = parent;
//            _tokens = new List<IToken>();
//            _scopes = new List<CodeScope>();
//        }

//        public CodeScope Parent { get { return _parent; } }
//        public ScopeType Type { get { return _type; } }
//        public IList<IToken> Tokens { get { return _tokens; } }
//        public IList<CodeScope> ChildScopes { get { return _scopes; } }

//        public void AddToken(IToken token)
//        {
//            _tokens.Add(token);
//        }

//        public void AddChildScope(CodeScope scope)
//        {
//            _scopes.Add(scope);
//        }
//    }

//    public enum TokenType
//    {
//        Invalid,
//        BlankLine,
//        Comment,
//        NamespaceDeclaration,
//        UsingStatement,
//        XmlDocumentation,
//        EnumDeclaration,
//        Attribute,
//        ClassDeclaration,
//        FieldDeclaration,
//        PropertyDeclaration,
//        PropertyGetter,
//        PropertySetter,
//        MethodDeclaration,
//        Code,
//        ScopeEnding
//    }

//    public interface ITokenParseResult
//    {
//        bool Success { get; }

//        IToken Token { get; }
//    }

//    public interface IToken
//    {
//        string Content { get; }

//        TokenType Type { get; }
//    }

//    public class Token : IToken
//    {
//        private string _content;
//        private TokenType _type;

//        public Token(string content, TokenType type)
//        {
//            _content = content;
//            _type = type;
//        }

//        public string Content { get { return _content; } }
//        public TokenType Type { get { return _type; } }
//    }

//    public class TokenParseResult : ITokenParseResult
//    {
//        private bool _success;
//        private IToken _token;

//        public TokenParseResult()
//            : this(false)
//        {
//        }

//        public TokenParseResult(bool success)
//            : this(success, TokenType.Invalid, null)
//        {
//        }

//        public TokenParseResult(bool success, TokenType type)
//            : this(success, type, null)
//        {
//        }

//        public TokenParseResult(bool success, TokenType type, string content)
//        {
//            _token = new Token(content, type);
//            _success = success;
//        }

//        public bool Success
//        {
//            get { return _success; }
//        }

//        public IToken Token { get { return _token; } }

//        public static ITokenParseResult Empty
//        {
//            get
//            {
//                return new TokenParseResult(true);
//            }
//        }
//    }

//    public interface ITokenParser
//    {
//        ITokenParser NextParser { get; set; }

//        ITokenParseResult ParseLine(string line);
//    }

//    public class TokenParseChainBuilder
//    {
//        public static TokenParseChainLinkBuilder Build(ITokenParser mainParser)
//        {
//            return new TokenParseChainLinkBuilder(mainParser);
//        }
//    }

//    public class TokenParseChainLinkBuilder
//    {
//        private ITokenParser _mainParser;
//        private ITokenParser _currentParser;

//        public TokenParseChainLinkBuilder(ITokenParser mainParser)
//        {
//            _mainParser = mainParser;
//            _currentParser = mainParser;
//        }

//        public TokenParseChainLinkBuilder AddLink(ITokenParser parser)
//        {
//            _currentParser.NextParser = parser;
//            _currentParser = parser;

//            return this;
//        }

//        public ITokenParser Done()
//        {
//            return _mainParser;
//        }
//    }

//    public class TokenParseChain
//    {
//        private ITokenParser _mainParser;

//        public TokenParseChain()
//            : this(DefaultChain)
//        {
//        }

//        public TokenParseChain(ITokenParser mainParser)
//        {
//            _mainParser = mainParser;
//        }

//        public ITokenParseResult Parse(string line)
//        {
//            var currentParser = _mainParser;
//            while (currentParser != null)
//            {
//                var parseResult = currentParser.ParseLine(line.Trim());
//                if (parseResult.Success)
//                {
//                    return parseResult;
//                }

//                currentParser = currentParser.NextParser;
//            }

//            return TokenParseResult.Empty;
//        }

//        public static ITokenParser DefaultChain
//        {
//            get
//            {
//                return TokenParseChainBuilder.Build(new NamespaceParser())
//                                                 .AddLink(new UsingDeclarationParser())
//                                                 .AddLink(new BlankLineParser())
//                                                 .AddLink(new AttributeParser())
//                                                 .AddLink(new ClassDeclarationParser())
//                                                 .AddLink(new EnumDeclarationParser())
//                                                 .AddLink(new PropertyParser())
//                                                 .AddLink(new PropertyGetterParser())
//                                                 .AddLink(new PropertySetterParser())
//                                                 .AddLink(new MethodParser())
//                                                 .AddLink(new ScopeEndingParser())
//                                                 .AddLink(new XmlDocumentationParser())
//                                                 .AddLink(new CommentParser())
//                                                 .AddLink(new DefaultParser())
//                                                 .Done();
//            }
//        }
//    }

//    public class BlankLineParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (string.IsNullOrWhiteSpace(line))
//                return new TokenParseResult(true, TokenType.BlankLine, line);

//            return new TokenParseResult();
//        }
//    }

//    public class MethodParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.Contains("(") && line.Contains(")") && line.EndsWith("{"))
//            {
//                var words = line.Split(' ');
//                var tokens = new string[] { "using", "for", "while", "foreach", "if", "switch", "catch", "lock", "()" };

//                foreach (var token in tokens)
//                {
//                    if (words.Contains(token))
//                        return new TokenParseResult();
//                }

//                return new TokenParseResult(true, TokenType.MethodDeclaration, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class PropertyParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            var words = line.Split(' ');
//            var tokens = new string[] { "class", "namespace", "using", "partial", "enum", "struct", "()" };

//            if (words.Length == 4 && words[3] == "{")
//            {
//                foreach (var token in tokens)
//                {
//                    if (words.Contains(token))
//                        return new TokenParseResult();
//                }

//                return new TokenParseResult(true, TokenType.PropertyDeclaration, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class PropertyGetterParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("get") && line.EndsWith("{"))
//            {
//                return new TokenParseResult(true, TokenType.PropertyGetter, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class PropertySetterParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("set") && line.EndsWith("{"))
//            {
//                return new TokenParseResult(true, TokenType.PropertySetter, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class DefaultParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            return new TokenParseResult(true, TokenType.Code, line);
//        }
//    }

//    public class ScopeEndingParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line == "}")
//            {
//                return new TokenParseResult(true, TokenType.Code, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class ClassDeclarationParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.Contains("class ") && line.EndsWith("{"))
//            {
//                return new TokenParseResult(true, TokenType.ClassDeclaration, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class EnumDeclarationParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.Contains("enum ") && line.EndsWith("{"))
//            {
//                return new TokenParseResult(true, TokenType.ClassDeclaration, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class UsingDeclarationParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("using") && line.EndsWith(";"))
//            {
//                return new TokenParseResult(true, TokenType.UsingStatement, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class CommentParser : ITokenParser
//    {
//        public ITokenParser NextParser { get; set; }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("//"))
//            {
//                return new TokenParseResult(true, TokenType.Comment, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class XmlDocumentationParser : ITokenParser
//    {
//        public ITokenParser NextParser
//        {
//            get;
//            set;
//        }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("/// "))
//            {
//                return new TokenParseResult(true, TokenType.XmlDocumentation, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class AttributeParser : ITokenParser
//    {
//        public ITokenParser NextParser
//        {
//            get;
//            set;
//        }

//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("[") && line.EndsWith("]"))
//            {
//                return new TokenParseResult(true, TokenType.Attribute, line);
//            }

//            return new TokenParseResult();
//        }
//    }

//    public class NamespaceParser : ITokenParser
//    {
//        public ITokenParseResult ParseLine(string line)
//        {
//            if (line.StartsWith("namespace "))
//            {
//                return new TokenParseResult(true, TokenType.NamespaceDeclaration, line);
//            }

//            return new TokenParseResult();
//        }

//        public ITokenParser NextParser
//        {
//            get;
//            set;
//        }
//    }

//}