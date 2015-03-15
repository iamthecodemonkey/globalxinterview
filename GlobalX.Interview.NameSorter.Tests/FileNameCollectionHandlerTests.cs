using System;
using System.IO;
using GlobalX.Interview.NameSorter.Interfaces;
using NUnit.Framework;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture]
    public class FileNameCollectionHandlerTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            //nothing
        }

        [TestCase("input.txt")]
        public void ValidFileIsAcceptableForInput(string input)
        {
            INameCollectionReader<string> subject = new FileNameCollectionHandler(input);
            Assert.IsTrue(subject.IsValid);
        }

        [TestCase("outputFileThatDoesntExist.txt")]
        public void ValidFileIsAceptableForOuput(string input)
        {
            INameCollectionWriter<string> subject = new FileNameCollectionHandler(input);
            Assert.IsTrue(subject.IsValid);
        }

        #region Exceptions

        [TestCase("inputFileThatDoesntExist.txt")]
        public void InvalidOnNonExistentInputFile(string input)
        {
            INameCollectionReader<string> subject = new FileNameCollectionHandler(input);
            Assert.IsFalse(subject.IsValid);
        }

        [TestCase("input.txt")]
        public void InvalidOnOutputFileAlreadyExisting(string input)
        {
            INameCollectionWriter<string> subject = new FileNameCollectionHandler(input);
            Assert.IsFalse(subject.IsValid);
        }

        [TestCase("sdfjnsnlsng:\\input.txt")]
        public void DoesNotThrowOnImpossibleInputFile(string input)
        {
            INameCollectionReader<string> subject = new FileNameCollectionHandler(input);
            Assert.DoesNotThrow(() => { if (subject.IsValid) Assert.Fail("Should not have been a valid handler"); } );
        }

        [TestCase("ZZ:\\input.txt")]
        public void ThrowsOnImpossibleOutputFile(string input)
        {
            INameCollectionWriter<string> subject = new FileNameCollectionHandler(input);
            Assert.DoesNotThrow(() => { if (subject.IsValid) Assert.Fail("Should not have been a valid handler"); });
        }

        #endregion
    }
}