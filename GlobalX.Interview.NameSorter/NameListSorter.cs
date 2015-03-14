using System.Linq;
using GlobalX.Interview.NameSorter.Interfaces;
using System;
using System.Collections.Generic;

namespace GlobalX.Interview.NameSorter
{
    /// <summary>
    /// Handles acquiring a collection of names and applying sort logic using Linq to the result
    /// </summary>
    /// <typeparam name="T">The type we are sorting</typeparam>
    public class NameListSorter<T>
    {
        /// <summary>
        /// Gets the comparer.
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public IComparer<T> Comparer { get; private set; }

        /// <summary>
        /// Gets or sets the handler responsible for retrieving the source
        /// collection of names
        /// </summary>
        /// <remarks>Assumption is that the input is not of an orderly manner</remarks>
        /// <value>
        /// The input source.
        /// </value>
        public INameCollectionReader<T> InputSource { get; set; }

        /// <summary>
        /// Gets or sets the output source.
        /// </summary>
        /// <value>
        /// The output source.
        /// </value>
        public INameCollectionWriter<T> OutputSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameListSorter{T}" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="inputSource">The input source.</param>
        /// <param name="outputSource">The output source.</param>
        /// <exception cref="System.ArgumentNullException">comparer</exception>
        public NameListSorter(IComparer<T> comparer, INameCollectionReader<T> inputSource, INameCollectionWriter<T> outputSource)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            if (inputSource == null) throw new ArgumentNullException("inputSource");
            if (outputSource == null) throw new ArgumentNullException("outputSource");

            Comparer = comparer;
            InputSource = inputSource;
            OutputSource = outputSource;
        }

        /// <summary>
        /// Sorts the source from the specified handler.
        /// </summary>
        public void Sort()
        {
            //enumerates the entire collection: 
            //potential for excessive memory usage and/or file/network io
            //depending on input provider
            var source = InputSource.ToList();
            
            source.Sort(Comparer);

            foreach (var item in source)
            {
                OutputSource.Push(item);
            }
        }
    }
}