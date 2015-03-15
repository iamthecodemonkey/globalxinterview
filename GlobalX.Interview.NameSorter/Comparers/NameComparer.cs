using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlobalX.Interview.NameSorter.Comparers
{
    /// <summary>
    /// Performs a comparison of check based on a pair of given names
    /// </summary>
    public class NameComparer : IComparer<string>
    {
        private readonly Regex _InvalidComparisonTokensRegex;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameComparer" /> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        public NameComparer(string tokens = null)
        {
            _InvalidComparisonTokensRegex = new Regex(GetRegexString(tokens), RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// Gets the regex string free of any characters which can be misinterpreted.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The regex string to use for replacing noise in the input upon comparison</returns>
        private string GetRegexString(string tokens)
        {
            tokens = Regex.Escape(tokens ?? string.Empty).Replace("-", @"\-");
            return string.IsNullOrEmpty(tokens.Trim()) ? string.Empty : string.Format("[{0}]", tokens);
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
                : _InvalidComparisonTokensRegex.Replace(name, string.Empty);
        }
    }
}