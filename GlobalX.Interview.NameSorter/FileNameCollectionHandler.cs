using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GlobalX.Interview.NameSorter.Interfaces;

namespace GlobalX.Interview.NameSorter
{
    /// <summary>
    /// Handles I/O with a file on disk
    /// </summary>
    internal class FileNameCollectionHandler : INameCollectionReader<string>, INameCollectionWriter<string>
    {
        private readonly string _FileName;
        private readonly Lazy<List<string>> _Items = new Lazy<List<string>>();

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool INameCollectionReader<string>.IsValid { get { return IsValidFileForReading(); } }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool INameCollectionWriter<string>.IsValid { get { return IsValidFileForWriting(); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileNameCollectionHandler"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FileNameCollectionHandler(string fileName)
        {
            _FileName = fileName;
            _Items = new Lazy<List<string>>(true);
        }

        /// <summary>
        /// Determines whether the declared file name is valid.
        /// </summary>
        /// <returns>Whether the filename is valid for our reading purpose</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private bool IsValidFileForReading()
        {
            return File.Exists(_FileName);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return File.ReadAllLines(_FileName).AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)this).GetEnumerator();
        }

        /// <summary>
        /// Determines whether the declared file name is valid.
        /// </summary>
        /// <returns>
        /// Whether the filename is valid for our writing purpose
        /// </returns>
        private bool IsValidFileForWriting()
        {
            var result = false;
            try
            {
                result = !string.IsNullOrEmpty(_FileName)
                && !File.Exists(_FileName)
                && Directory.Exists(Path.GetDirectoryName(_FileName));
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Pushes the specified item to the name collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index to push the item to.</param>
        /// <remarks>
        /// A null <paramref name="index" /> indicates the end of the collection
        /// Consideration could be made to reduce memory imprint by buffering a smaller set
        /// of writes, or writing and indexing a temporary output file immediately
        /// </remarks>
        void INameCollectionWriter<string>.Push(string item, int? index)
        {
            _Items.Value.Insert(index ?? _Items.Value.Count, item);
        }

        /// <summary>
        /// Commits any modifications to the name collection
        /// </summary>
        void INameCollectionWriter<string>.Commit()
        {
            File.WriteAllLines(_FileName, _Items.Value);
        }
    }
}