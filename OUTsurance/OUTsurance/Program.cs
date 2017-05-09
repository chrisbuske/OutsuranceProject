using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OUTsurance
{
    public class Program
    {

        #region Events...

        /// <summary>
        /// Load new entry event...
        /// </summary>
        public static event Outputs.ReadLine LoadLine;

        #endregion

        #region Console Entry Point...

        /// <summary>
        /// Console Entry Point...
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // Obtain the location of the csv file...

            string fileLocation = args.Length == 1 ? args[0] : Console.ReadLine();

            do
            {

                // Close app if no string is provided...

                if (System.IO.File.Exists(fileLocation))
                    break;
                else if (fileLocation == string.Empty)
                    return;

                // Request file or exit command...

                Console.WriteLine("Invalid file. Please try again or press \"Enter\" to exit");

                fileLocation = Console.ReadLine();


            } while (!System.IO.File.Exists(fileLocation));

            InputFile(fileLocation);

            Console.ReadLine();

        }

        #endregion

        #region File Input Routine. Seperated for testing putposes!!!

        /// <summary>
        /// Import a new file...
        /// </summary>
        /// <param name="fileLocation"></param>
        public static void InputFile(string fileLocation)
        {

            List<Outputs.Output> outputs = new List<Outputs.Output>();

            try
            {
                // Read in the csv file values...

                Console.WriteLine();
                Console.WriteLine("Input csv file");
                Console.WriteLine();

                using (System.IO.TextReader textReader = System.IO.File.OpenText(fileLocation))
                {

                    // Prepare the output objects...

                    outputs.Add(new Outputs.Output1());
                    outputs.Add(new Outputs.Output2());

                    LoadLine += outputs[0].AddEntry;
                    LoadLine += outputs[1].AddEntry;

                    // Read each line into the objects...

                    string line = string.Empty;
                    bool isHeader = true;

                    while ((line = textReader.ReadLine()) != null)
                    {

                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        }

                        string[] lineEntry = line.Split(new char[1] { Outputs.Output.Seperator }, StringSplitOptions.None);

                        if (lineEntry.Length != 4) throw new Exception("The csv file is corrupted.");

                        LoadLine(lineEntry[0].Trim(), lineEntry[1].Trim(), lineEntry[2].Trim(), lineEntry[3].Trim());

                    }
                }

                using (System.IO.TextWriter textWriter1 = System.IO.File.CreateText(System.IO.Path.GetDirectoryName(fileLocation) + @"\output1.csv"))
                using (System.IO.TextWriter textWriter2 = System.IO.File.CreateText(System.IO.Path.GetDirectoryName(fileLocation) + @"\output2.csv"))
                {
                    Console.WriteLine("Output csv file #1");
                    outputs[0].WriteFile(textWriter1);
                    Console.WriteLine();
                    Console.WriteLine("Output csv file #2");
                    outputs[1].WriteFile(textWriter2);
                    Console.WriteLine();

                }

                outputs.Clear();

                Console.WriteLine("Complete. Output file may be located in the directory: " + System.IO.Path.GetDirectoryName(fileLocation));
               
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Unable to process the request due to an unxpected error:");
                Console.WriteLine(ex.Message);

                #if DEBUG
                throw ex;
                #endif

            }
            finally
            {
                outputs.Clear();
            }
        }

        #endregion

    }
}