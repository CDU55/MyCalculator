namespace MyCalculator.Parsers
{
    public interface ITokenValidator
    {
        bool IsNumber(string input);
        bool IsOperator(string input);
        bool IsParenthesis(string input);
        bool IsLeftParenthesis(string input);
        bool IsRightParenthesis(string input);
        bool IsSpace(string input);
        bool IsValid(string input);
    }
}