using NUnit.Framework;
using System;
using System.IO;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private Program _Subject;

        [TestFixtureSetUp]
        public void Setup()
        {
            _Subject = new Program();
        }

        [TestCase("input.txt")]
        public void ValidFileIsProcessed(string inputFile)
        {
            //cleanup test output
            File.Delete(inputFile + Program.OutputFileNameSuffix);
            var test = new TestDelegate(() => _Subject.Execute(new[] { inputFile }));
            Assert.DoesNotThrow(test);
        }

        #region Exceptions

        [TestCase(@"c:\somethingthatdoesntexist.txt")]
        public void ThrowsIfFileIsInvalid(string inputFile)
        {
            Assert.Throws(typeof(ApplicationException), () => _Subject.Execute(new[] { inputFile }));
        }

        #endregion
    }
}