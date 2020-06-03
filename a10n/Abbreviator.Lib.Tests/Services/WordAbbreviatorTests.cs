using Abbreviator.Lib.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Abbreviator.Lib.Tests.Services
{
    public class WordAbbreviatorTests
    {
        private WordAbbreviator _sut;
        
        [SetUp]
        public void Setup()
        {
            _sut = new WordAbbreviator();
        }

        [Test]
        [TestCase("elephant", "e6t")]
        [TestCase("rides", "r3s")]
        [TestCase("really", "r4y")]
        public void Abbreviate_WordLongerThan3Characters_CorrectlyAbbreviates(string word, string abbreviation)
        {
            var actual = _sut.Abbreviate(word);

            actual.Should().Be(abbreviation);
        }
        
        [Test]
        [TestCase("are")]
        [TestCase("fun")]
        [TestCase("is")]
        public void Abbreviate_WordShorterThan4Characters_DoesNotChangeTheWord(string word)
        {
            var actual = _sut.Abbreviate(word);

            actual.Should().Be(word);
        }

        [Test]
        [TestCase("elephant-rides are really fun!", "e6t-r3s are r4y fun!")]
        public void AbbreviateTextTest(string text, string expected)
        {
            var actual = _sut.AbbreviateText(text);

            actual.Should().Be(expected);
        }
    }
}