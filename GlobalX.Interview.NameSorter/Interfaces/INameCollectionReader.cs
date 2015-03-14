using System.Collections.Generic;

namespace GlobalX.Interview.NameSorter.Interfaces
{
    /// <summary>
    /// Describes a class which is capable of performing operations on a collection of names
    /// </summary>
    /// <typeparam name="T">The item type handled by this name collection handler</typeparam>
    public interface INameCollectionReader<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }
    }
}