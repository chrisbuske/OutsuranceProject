using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OUTsurance.Outputs
{

    /// <summary>
    /// Delegate for entry reads from the input file...
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Address"></param>
    /// <param name="phoneNumber"></param>
    public delegate void ReadLine(string firstName, string LastName, string Address, string phoneNumber);

    /// <summary>
    /// Abstract class for all classes interacting with file import entries...
    /// </summary>
    public abstract class Output : IDisposable
    {

        /// <summary>
        /// CSV record seperator character
        /// </summary>
        internal static char Seperator { get { return ','; } }
        /// <summary>
        /// Entry point for new entries read from the import file...
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        internal abstract void AddEntry(string firstName, string lastName, string address, string phoneNumber);
        /// <summary>
        /// Routine to write out the results to file...
        /// </summary>
        /// <param name="textwriter"></param>
        internal abstract void WriteFile(System.IO.TextWriter textwriter);
        /// <summary>
        /// IDisposal Interface...
        /// </summary>
        public abstract void Dispose();

    }
}
