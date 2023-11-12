namespace Syntax_1;

public class Token
{
    public enum Types
    {
        Identifier, Number, Character, Assignment, EndSentence, LogicalOp,
        RelationalOp, TermOp, IncrementoTermino, FactorOp, FactorIn, TernaryOp,
        String, Start, End, DataTypes, Reserved, Loops, Structures
    }
    private string _content;
    private Types _clasification;
    
    public Token()
    {
        _content = "";
        _clasification = Types.Identifier;
    }
    public void SetContent(string content)
    {
        _content = content;
    }
    public void SetClassification(Types classification)
    {
        _clasification = classification;
    }
    public string GetContent()
    {
        return _content;
    }
    public Types GetClassification()
    {
        return _clasification;
    }
}