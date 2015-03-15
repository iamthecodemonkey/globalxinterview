using System.Collections.Generic;
using System.Linq;
using GlobalX.Interview.NameSorter.Comparers;
using NUnit.Framework;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture()]
    public class NameComparerTests
    {
        private IComparer<string> _Subject;

        [TestFixtureSetUp]
        public void Setup()
        {
            _Subject = new NameComparer();
        }

        [TestCase("Ashley, Michael", "Zeus, Michael", -1)]
        [TestCase("Bernard, Michael", "Bernard, Michael", 0)]
        [TestCase("Adams, Michael", "Adams, Michael", 0)]
        [TestCase("Bernard, Michael", "Adams, Michael", 1)]
        [TestCase("Adams, Michael", "Bernard, Michael", -1)]
        public void HandlesBasicAlphabeticalComparison(string first, string second, int expectation)
        {
            Assert.AreEqual(expectation, _Subject.Compare(first, second));
        }

        [TestCase("BernersLee, Tim", "Berners-Lee, Tim", -1)]
        [TestCase("Berners-Lee, Tim", "Berners-Lee, Tim", 0)]
        [TestCase("Berners-Lee, Edward", "Berners-Lee, Tim", -1)]
        [TestCase("Snowden, Edward", "Berners-Lee, Tim", 1)]
        public void HandlesHyphenatedSurnames(string first, string second, int expectation)
        {
            Assert.AreEqual(expectation, _Subject.Compare(first, second));
        }

        [TestCase("Ber-ners-Lee, Tim", "Bern-ers-Lee, Tim", 0, "-")]
        [TestCase("Ber-nersLee, Tim", "Berners-Lee, Tim", 0, "-")]
        [TestCase("BernersLee, Tim", "Berners-Lee, Tim", 0, "-")]
        [TestCase("Berners-Lee, Tim", "BernersLee, Tim", 0, "-")]
        [TestCase("Berners-Lee, Tim", "Berners-Lee, Tim", 0, "-")]
        [TestCase("Berners-Lee, Edward", "Berners-Lee, Tim", -1, "-")]
        [TestCase("Snowden, Edward", "Berners-Lee, Tim", 1, "-")]
        public void HandlesHyphenatedSurnamesWhenPunctuationIsIgnored(string first, string second, int expectation, string punctuationToIgnore)
        {
            IComparer<string> subject = new NameComparer(punctuationToIgnore);
            Assert.AreEqual(expectation, subject.Compare(first, second));
        }

        [TestCase("Van Der Muelen, Robert", "Van Der Berg, Robert", 1)]
        [TestCase("Van Muelen, Robert", "VanMuelen, Robert", -1)]
        [TestCase("Van Muelen, Robert", "Van Muelen, Robert", 0)]
        public void HandlesMultiPartNames(string first, string second, int expectation)
        {
            Assert.AreEqual(expectation, _Subject.Compare(first, second));
        }

        [TestCase("Van Der Muelen, Robert", "Van Der Berg, Robert", 1)]
        [TestCase("Van Muelen, Robert", "VanMuelen, Robert", -1)]
        [TestCase("Van Muelen, Robert", "Van Muelen, Robert", 0)]
        public void AllReplaceableTokensAreRespected(string first, string second, int expectation)
        {
            var invalidComparisonTokens = string.Join(string.Empty, Enumerable.Range(char.MinValue, char.MaxValue - char.MinValue).Where(i => char.IsPunctuation((char)i)).Select(i => (char)i));
            IComparer<string> subject = new NameComparer(invalidComparisonTokens);
            Assert.AreEqual(expectation, subject.Compare(invalidComparisonTokens + first, string.Join(string.Empty, invalidComparisonTokens.Reverse()) + second));
        }

        [TestCase("Rodger, David")]
        [TestCase("Howard, Hughes")]
        public void IdenticalNamesResultIsEqual(string first)
        {
            Assert.AreEqual(0, _Subject.Compare(first, first));
        }

        [TestCase("Madonna", "Prince", -1)]
        [TestCase("Prince", "Jimi", 1)]
        [TestCase("Prince", "Prince", 0)]
        [TestCase("Eddy", "Sting", -1)]
        [TestCase("Catalina", "Cat", 1)]
        [TestCase("Zeus", "Zeus", 0)]
        [TestCase("Zeus", "Frank, Paul", 1)]
        [TestCase("Andrews, Andrew", "Zeus", -1)]
        public void HandlesSingleNameNames(string first, string second, int expectation)
        {
            Assert.AreEqual(expectation, _Subject.Compare(first, second));
        }

        [TestCase("Zeus, John", "Frank, Paul", 1)]
        [TestCase("Andrews, Andrew", "Andrews, Bevan", -1)]
        public void FirstNameAppliesAfterSurnameMatches(string first, string second, int expectation)
        {
            Assert.AreEqual(expectation, _Subject.Compare(first, second));
        }

        [TestCase("Wesley's, James", "Wesley's, James", 0, " -,.'")]
        [TestCase("Van deBeek, James", "Van de-Beek, James", 0, " -,.`")]
        [TestCase("Van deBeek, James", "Van de-Adams, James", 1, " -,.`")]
        [TestCase("Van de Beek, James", "Van de Adams, James", 1, " ")]
        public void IgnoresSpecifiedIgnorableContent(string first, string second, int expectation, string punctuationToIgnore)
        {
            IComparer<string> subject = new NameComparer(punctuationToIgnore);
            Assert.AreEqual(expectation, subject.Compare(first, second));
        }

        [TestCase("Alpha, John", "Maven, John", -1)]
        [TestCase("Maven, John", "Karl, John", 1)]
        [TestCase("Maven, John", "Maven, John", 0)]
        public void MultipleInvocationReturnIdenticalResult(string first, string second, int expectation)
        {
            //10 chosen at random: could be business logic related
            for (var ii = 0; ii < 10; ii++)
            {
                Assert.AreEqual(expectation, _Subject.Compare(first, second));
            }
        }
    }
}