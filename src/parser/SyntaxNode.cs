using PascalCompiler.LexicalAnalyzer;

namespace PascalCompiler.SyntaxAnalyzer
{
    abstract class SyntaxNode
    {
        public SyntaxNode(Lexeme lexeme) => Lexeme = lexeme;

        public Lexeme Lexeme { get; }

        abstract public void PrintTree(int depth = 0, string indent = "");
        abstract public bool HasChildren();
    }

    class ExprNode : SyntaxNode
    {
        public ExprNode(Lexeme lexeme) : base(lexeme) { }
        public override void PrintTree(int depth = 0, string indent = "") { }
        public override bool HasChildren() => false;
    }

    class StmtNode : SyntaxNode
    {
        public StmtNode(Lexeme lexeme) : base(lexeme) { }
        public override void PrintTree(int depth = 0, string indent = "") { }
        public override bool HasChildren() => false;
    }

    class DeclareVarsNode : StmtNode
    {
        public DeclareVarsNode(Lexeme lexeme, Lexeme[] vars) : base(lexeme) { }
    }

    class WhileStmtNode : StmtNode
    {
        public WhileStmtNode(Lexeme lexeme, ExprNode cond, StmtNode body) : base(lexeme)
        {
            Condition = cond;
            Body = body;
        }

        public ExprNode Condition { get; }
        public StmtNode Body { get; }

        public override void PrintTree(int depth, string indent)
        {
            Console.WriteLine(this);

            Console.Write(indent + "├──── ");
            Condition.PrintTree(depth + 1, indent + "│".PadRight(6, ' '));

            Console.Write(indent + "└──── ");
            Body.PrintTree(depth + 1, indent + "".PadRight(6, ' '));
        }

        override public string ToString() => Lexeme.Source;
    }

    class IfStmtNode : StmtNode
    {
        public IfStmtNode(Lexeme lexeme, ExprNode cond, StmtNode ifPart, StmtNode elsePart)
        : base(lexeme)
        {
            Condition = cond;
            IfPart = ifPart;
            ElsePart = elsePart;
        }

        public ExprNode Condition { get; }
        public StmtNode IfPart { get; }
        public StmtNode? ElsePart { get; }
    }

    class AssignStmt : StmtNode
    {
        public AssignStmt(Lexeme lexeme, ExprNode left, ExprNode right) : base(lexeme)
        {
            Left = left;
            Right = right;
        }

        public ExprNode Left { get; }
        public ExprNode Right { get; }
    }

    class BlockStmt : StmtNode
    {
        public BlockStmt(Lexeme lexeme, List<StmtNode> body) : base(lexeme)
        {
            Body = body;
        }

        public List<StmtNode> Body { get; }
    }

    class RelOperNode : ExprNode
    {
        public RelOperNode(Lexeme lexeme, ExprNode left, ExprNode right)
        : base(lexeme)
        {
            Left = left;
            Right = right;
        }

        public ExprNode Left { get; }
        public ExprNode Right { get; }

        public override void PrintTree(int depth, string indent)
        {
            Console.WriteLine(this);

            Console.Write(indent + "├──── ");
            Left.PrintTree(depth + 1, indent + "│".PadRight(6, ' '));

            Console.Write(indent + "└──── ");
            Right.PrintTree(depth + 1, indent + "".PadRight(6, ' '));
        }

        public override bool HasChildren() => true;

        override public string ToString() => Lexeme.Source;
    }

    class BinOperNode : ExprNode
    {
        public BinOperNode(Lexeme lexeme, ExprNode left, ExprNode right)
        : base(lexeme)
        {
            Left = left;
            Right = right;
        }

        public ExprNode Left { get; }
        public ExprNode Right { get; }

        public override void PrintTree(int depth, string indent)
        {
            Console.WriteLine(this);

            Console.Write(indent + "├──── ");
            Left.PrintTree(depth + 1, indent + "│".PadRight(6, ' '));

            Console.Write(indent + "└──── ");
            Right.PrintTree(depth + 1, indent + "".PadRight(6, ' '));
        }

        public override bool HasChildren() => true;

        override public string ToString() => Lexeme.Source;
    }

    class NumberNode : ExprNode
    {
        public NumberNode(Lexeme lexeme) : base(lexeme) { }

        public override void PrintTree(int depth, string indent) =>
            Console.WriteLine(this);

        public override bool HasChildren() => false;

        override public string ToString() => $"{Lexeme.Value}";
    }

    class IdentifireNode : ExprNode
    {
        public IdentifireNode(Lexeme lexeme) : base(lexeme) { }

        public override void PrintTree(int depth, string indent) =>
            Console.WriteLine(this);

        public override bool HasChildren() => false;

        override public string ToString() => $"{Lexeme.Value}";
    }

}