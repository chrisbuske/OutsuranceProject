using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OUTsurance.Outputs
{
    /// <summary>
    /// Manage First and Last Name Input / Output...
    /// </summary>
    public class Output1 : Output
    {

        #region Declarations...

        /// <summary>
        /// Include a dictionary for both first and last names as the 
        /// </summary>
        private Dictionary<string, int> _dictionary;
         
        #endregion

        #region Constructors...

        internal Output1()
        {
            
            _dictionary = new Dictionary<string, int>();
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
            if (_dictionary.ContainsKey(firstName))
                _dictionary[firstName]++;
            else
                _dictionary.Add(firstName, 1);

            if (_dictionary.ContainsKey(lastName))
                _dictionary[lastName]++;
            else
                _dictionary.Add(lastName, 1);
        }

        /// <summary>
        /// Output the file data...
        /// </summary>
        /// <param name="textwriter"></param>
        internal override void WriteFile(TextWriter textwriter)
        {

            // Order the dictionary object by frequesncy DESC and value ASC...

            IOrderedEnumerable<System.Collections.Generic.KeyValuePair<string, int>> sortedCollection = _dictionary.OrderByDescending(keyValue => keyValue.Value).ThenBy(keyValue => keyValue.Key);

            // Write out the ordered list to file...

            foreach(var keyValuePair in sortedCollection)
            {
                textwriter.WriteLine(keyValuePair.Key + Seperator + keyValuePair.Value);
            }
        }

        #endregion

        #region IDispose...

        public override void Dispose()
        {
            _dictionary.Clear();            
        }

        #endregion

    }
}
