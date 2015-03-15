using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalX.Interview.NameSorter.Comparers
{
    /// <summary>
    /// Performs a comparison of check based on a pair of given names
    /// </summary>
    public class NameComparer : IComparer<string>
    {
        private readonly string _TokensToIgnoreOnComparison;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameComparer" /> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public NameComparer(string tokens = null)
        {
            _TokensToIgnoreOnComparison = tokens ?? string.Empty;
        }

        /// <summary>
        /// Compares two names and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="left"/> and <paramref name="right"/>, as shown in the following table.Value Meaning Less than zero<paramref name="left"/> is less than <paramref name="right"/>.Zero<paramref name="left"/> equals <paramref name="right"/>.Greater than zero<paramref name="left"/> is greater than <paramref name="right"/>.
        /// </returns>
        /// <param name="left">The first name to compare.</param>
        /// <param name="right">The second name to compare.</param>
        int IComparer<string>.Compare(string left, string right)
        {
            return string.Compare(GetName(left), GetName(right), StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the surname component from the given string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The surname component of the given name</returns>
        private string GetName(string name)
        {
            return string.IsNullOrEmpty(name) ? name
                : string.Join(string.Empty, name.ToCharArray().Except(_TokensToIgnoreOnComparison));
        }
    }
}