namespace GlobalX.Interview.NameSorter.Interfaces
{
    /// <summary>
    /// Describes a class which is capable of performing operations on a collection of names
    /// </summary>
    /// <typeparam name="T">The type of item we are a collection for</typeparam>
    public interface INameCollectionWriter<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// Pushes the specified item to the name collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index to push the item to.</param>
        /// <remarks>
        /// A null <paramref name="index" /> indicates the end of the collection
        /// </remarks>
        void Push(T item, int? index = null);

        /// <summary>
        /// Commits any modifications to the name collection
        /// </summary>
        void Commit();
    }
}
