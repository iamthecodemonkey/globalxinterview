using System;
using GlobalX.Interview.NameSorter.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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

        [TestCase()]
        public void ValidFileIsProcessed(string inputFile)
        {
            Assert.Inconclusive();
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