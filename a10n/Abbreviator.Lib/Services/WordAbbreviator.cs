using System.Linq;
using System.Text.RegularExpressions;

namespace Abbreviator.Lib.Services
{
    public class WordAbbreviator : IWordAbbreviator
    {
        public string Abbreviate(string word)
        {
            var wordLength = word.Length;
            
            if (wordLength < 4) return word;

            var abbreviation = $"{word.First()}{wordLength - 2}{word.Last()}";

            return abbreviation;
        }

        public string AbbreviateText(string text)
        {
            var wordsOnly = Regex.Split(text, "\\W");

            return wordsOnly.Aggregate(text, (current, word) => Regex.Replace(current, word, Abbreviate(word)));
        }
    }
}