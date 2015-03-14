using GlobalX.Interview.NameSorter.Comparers;
using GlobalX.Interview.NameSorter.Interfaces;
using System;
using System.Linq;

namespace GlobalX.Interview.NameSorter
{
    class Program
    {
        private const string OutputFileNameSuffix = "-sorted.txt";

        /// <summary>
        /// Main entry point for execution
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var inputFileName = args.FirstOrDefault();
            INameCollectionReader<string> inputHandler = new FileNameCollectionHandler(inputFileName);
            if (!inputHandler.IsValid) Fail(Resources.InvalidInputFile);

            var outputFileName = inputFileName + OutputFileNameSuffix;
            INameCollectionWriter<string> outputHandler = new FileNameCollectionHandler(outputFileName);
            if (!outputHandler.IsValid) Fail(Resources.InvalidOutputFile);

            var invalidComparisonTokens = string.Join(string.Empty, Enumerable.Range(char.MinValue, char.MaxValue - char.MinValue).Where(i => char.IsPunctuation((char)i)).Select(i => (char)i));
            var comparer = new NameComparer(invalidComparisonTokens);
            var sorter = new NameListSorter<string>(comparer, inputHandler, outputHandler);

            sorter.Sort();
            outputHandler.Commit();
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Fail(e.ExceptionObject != null ? e.ExceptionObject.ToString() : null);
        }

        /// <summary>
        /// Fails the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        private static void Fail(string reason = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Resources.ApplicationEnding, reason ?? Resources.UnknownReason);
            Console.WriteLine(Resources.PressAnyKeyToEnd);
            Console.ReadKey();

            Environment.Exit(string.IsNullOrEmpty(reason) ? int.MaxValue : reason.GetHashCode());
        }
    }
}