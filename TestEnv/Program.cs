using System;
using MyCalculator.Parsers;

namespace TestEnv
{
    class Program
    {
        static void Main(string[] args)
        {
            TextTokenizer tokenizer = new TextTokenizer(new TokenValidator());
            var result = tokenizer.Tokenize("2  + 5  * #16");
            PostfixConverter converter = new PostfixConverter(new TokenValidator());
            var postfix = converter.ConvertToPostfix(result);
            foreach (var element in postfix)
            {
             Console.WriteLine(element);   
            }

        }
    }
}
