using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OUTsuranceTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        #region Static Declarations...

        private static string _location;
        private const string _testFileName = @"\data.csv";

        #endregion

        #region Constructors...
        
        public UnitTest1()
        {
            _location = string.Empty;

        }

        #endregion

        #region Test Management...

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        [TestInitialize()]
         public void MyTestInitialize()
        {
        
            // Clear out prior results...

            if (System.IO.File.Exists(_location + @"\output1.csv")) System.IO.File.Delete(_location + @"\output1.csv");
            if (System.IO.File.Exists(_location + @"\output2.csv")) System.IO.File.Delete(_location + @"\output2.csv");

        }

        #endregion

        #region Tests...

        /// <summary>
        /// NEGATIVE TESTING:
        /// Should raise an exception due to invalid file name...
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                OUTsurance.Program.InputFile("");
            }
            catch(System.ArgumentException exArg)
            {
                return;
            }
            throw new Exception("Failure to throw exception");
        }

        /// <summary>
        /// POSITIVE TESTING:
        /// Test the output files...
        /// ** File content must be verified!!!
        /// ** Ensure test file name is as per constant!!!
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {

            // Check test data file accessibility...

            string _location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (!System.IO.File.Exists(_location + _testFileName)) throw new System.IO.FileNotFoundException("Missing Data.csv file: \r\n" + _location);

            // Process Test...

            OUTsurance.Program.InputFile(_location + _testFileName);

            // CLook for output files..

            if (!System.IO.File.Exists(_location + @"\output1.csv")) throw new System.IO.FileNotFoundException("Failed to generate Output 1 file");
            if (!System.IO.File.Exists(_location + @"\output2.csv")) throw new System.IO.FileNotFoundException("Failed to generate Output 2 file");

        }

        #endregion

    }
}