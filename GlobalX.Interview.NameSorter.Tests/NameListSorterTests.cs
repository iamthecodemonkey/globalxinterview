using System;
using System.Collections.Generic;
using GlobalX.Interview.NameSorter.Interfaces;
using Moq;
using NUnit.Framework;

namespace GlobalX.Interview.NameSorter.Tests
{
    [TestFixture()]
    public class NameListSorterTests
    {
        private NameListSorter<string> _Subject;
        private Mock<IComparer<string>> _Comparer;
        private Mock<INameCollectionReader<string>> _InputSource;
        private Mock<INameCollectionWriter<string>> _OutputSource;

        [TestFixtureSetUp]
        public void Setup()
        {
            _Comparer = new Mock<IComparer<string>>();
            _InputSource = new Mock<INameCollectionReader<string>>();
            _OutputSource = new Mock<INameCollectionWriter<string>>();

            _InputSource.Setup(i => i.GetEnumerator()).Returns(new List<string> { "Brasky, Bill", "Dean, James" }.GetEnumerator());
            _Comparer.Setup(c => c.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(0);
            _OutputSource.Setup(c => c.Commit()).Verifiable();

            _Subject = new NameListSorter<string>(_Comparer.Object, _InputSource.Object, _OutputSource.Object);
        }

        [Test]
        public void HandleValidConstruction()
        {
            Assert.Pass();
        }

        [Test]
        public void UsesExpectedRetrieverOnSort()
        {
            Reset();
            _Subject.Sort();
            _InputSource.VerifyAll();
            Assert.Pass();
        }

        [Test]
        public void UsesExpectedSorter()
        {
            Reset();
            _Subject.Sort();
            _Comparer.VerifyAll();
            Assert.Pass();
        }

        [Test]
        public void DoesNotPushOnEmptySort()
        {
            Reset();
            var inputSource = new Mock<INameCollectionReader<string>>();

            inputSource.Setup(i => i.GetEnumerator()).Returns(new List<string>().GetEnumerator());

            var subject = new NameListSorter<string>(_Comparer.Object, inputSource.Object, _OutputSource.Object);
            subject.Sort();

            _OutputSource.Verify(m => m.Push(It.IsAny<string>(), It.IsAny<int?>()), Times.Never, "Output source should not have been pushed on");
            Assert.Pass();
        }

        [Test]
        public void DoesNotCommitOutputOnSort()
        {
            Reset();
            _Subject.Sort();

            _OutputSource.Verify(m => m.Commit(), Times.Never, "Output source should not have been committed on");
            Assert.Pass();
        }

        [Test]
        public void ThrowsOnInvalidConstruction()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new NameListSorter<string>(null, _InputSource.Object, _OutputSource.Object));
        }

        private void Reset()
        {
            _InputSource.ResetCalls();
            _Comparer.ResetCalls();
            _OutputSource.ResetCalls();
        }
    }
}