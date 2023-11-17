namespace Syntax_1;

public class Token
{
    public enum Types
    {
        Identifier, Number, Character, Assignment, EndSentence, LogicalOp,
        RelationalOp, TermOp, IncreaseTerm, FactorOp, FactorIn, TernaryOp,
        String, Start, End, DataTypes, Loops, Structures
    }
    private string _content;
    private Types _clasification;
    
    // ReSharper disable once ConvertConstructorToMemberInitializers
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