using NUnit.Framework;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture()]
    public class NameComparerTests
    {
        [TestCase("Adams", "Bernard")]
        public void HandlesBasicAlphabeticalComparison(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void HandlesHyphenatedNames(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void HandlesMultiPartNames(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void IdenticalNamesResultIsEqual(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void HandlesSingleNameNames(string first, string second)
        {
            Assert.Inconclusive();
        }
        
        [TestCase("", "")]
        public void Identical(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void IgnoresSpecifiedIgnorableContent(string first, string second)
        {
            Assert.Inconclusive();
        }

        #region Expected Exceptions

        [TestCase("", "")]
        public void ThrowsOnNonAsciiNames(string first, string second)
        {
            Assert.Inconclusive();
        }

        [TestCase("", "")]
        public void ThrowsOnInvalidFormatEntries(string first, string second)
        {
            Assert.Inconclusive();
        }
        
        #endregion
    }
}