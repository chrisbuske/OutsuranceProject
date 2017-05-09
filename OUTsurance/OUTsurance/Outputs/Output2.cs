using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OUTsurance.Outputs
{
    /// <summary>
    /// Manage Address Input / Output...
    /// </summary>
    public class Output2 : Output
    {
        #region Declarations...

        /// <summary>
        /// Include a list to hold the addresses...
        /// </summary>
        private List<string> _addresses;

        #endregion

        #region Constructors...

        internal Output2()
        {
            _addresses = new List<string>();
        }

        #endregion

        #region File Interactions...

        /// <summary>
        /// Add the entry...
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        internal override void AddEntry(string firstName, string lastName, string address, string phoneNumber)
        {
            if (!_addresses.Contains(address)) _addresses.Add(address);
        }

        /// <summary>
        /// Output the file data...
        /// </summary>
        /// <param name="textwriter"></param>
        internal override void WriteFile(TextWriter textwriter)
        {

            //// Sort the address list...

            _addresses.Sort((a, b) => (Regex.Replace(a, @"^[\d.]* ", "").CompareTo(Regex.Replace(b, @"^[\d.]* ", ""))));

            // Write out the ordered list to file...

            foreach (var address in _addresses)
            {
                textwriter.WriteLine(address);
            }
        }

        #endregion

        #region IDispose...

        public override void Dispose()
        {
            _addresses.Clear();
        }

        #endregion
    }
}
