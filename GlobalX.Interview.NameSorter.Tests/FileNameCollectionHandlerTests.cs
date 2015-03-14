using NUnit.Framework;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture]
    public class FileNameCollectionHandlerTests
    {
        [TestCase("")]
        public void ValidFileIsAcceptableForInput(string input)
        {
            Assert.Inconclusive();
        }

        [TestCase("")]
        public void ValidFileIsAceptableForOuput(string input)
        {
            Assert.Inconclusive();
        }

        #region Exceptions
        
        [Test]
        public void ThrowsOnNonExistentInputFile()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ThrowsOnOutputFileAlreadyExisting()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ThrowsOnImpossibleInputFile()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ThrowsOnImpossibleOutputFile()
        {
            Assert.Inconclusive();
        }

        #endregion
    }
}