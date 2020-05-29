namespace Abbreviator.Lib.Services
{
    public interface IWordAbbreviator
    {
        string Abbreviate(string word);
        string AbbreviateText(string text);
    }
}