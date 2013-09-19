/* PROJECT: WorldDataAppCS (C#)         PROGRAM: PrettyPrintUtility
 * AUTHOR:
 * OOP CLASSES USED:  none (this does not use the OOP paradigm)
 * FILES ACCESSED:  (all files handled by DIRECTLY by THIS program)
 *      INPUT:   NameIndexBackup*.txt 
 *      OUTPUT:  Log.txt
 *      PLUS FOR FUTURE ASGN:
 *          INPUT: MainData*.bin (& "INPUT" to check for "empty locations")
 *          OUTPUT: CodeIndexBackup*.bin
 *          AND NameIndexBackup*.bin will be a BINARY file, not TEXT
 *      where * is the appropriate fileNameSuffix.
 * DESCRIPTION:  This is a utility program for the developer.  As such, it's
 *      just a quick non-OOP program which accesses the files DIRECTLY.
 *      It pretty-prints (SEE SPECS) the NameIndexBackup file to the Log file.
 *      PLUS FOR FUTURE ASGN:
 *          Also prints MainData*.bin file and CodeIndexBackup*.bin file.
 *          AND, NameIndexBackup*.bin will be a BINARY file. 
 * CONTROLLER ALGORITHM:  Traditional sequential file processing . . .
 *******************************************************************************/

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PrettyPrintUtility
{
    public class PrettyPrintUtility
    {
        static FileStream mainDataFile;
        static int _sizeOfHeaderRec = 3;
        static int _sizeOfDataRec = 71;
        static private string _id;               //ID of record
        static private string _code;             //Country code
        static private string _name;             //Name of country
        static private string _continent;        //What continent the country is located
        static private string _region;           //What region the country is located
        static private string _surfaceArea;      //Size of the country
        static private string _yearOfIndep;      //What year they went independent
        static private string _population;       //Total population of the country
        static private string _lifeExpectancy;   //The average time someone is alive in the country
        static List<string> RecordList = new List<string>();

        public static void Main(string[] args)
        {
            if (File.Exists("MainData.txt"))
            {
                mainDataFile = new FileStream("MainData.txt", FileMode.Open);
                HandleDataFile();
                PrintResults();
            }
            else
            {
                Console.WriteLine("Error MainData.txt does not exist!");
            }
          
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Handles the main data file and its records
        /// </summary>
        private static void HandleDataFile()
        {
            int RecordCount = ReadHeaderRec(); //Amount of records in file
            int id = 1;                        //Starting point in searching for ID's
            int RRN = 0;                       //RRN of file location
            byte[] QueryRecord;                //Record that was returned

            for(int i = 0; i < RecordCount;)
            {
                RRN = CalculateRRN(id++);
                QueryRecord = ReadOneRecord(RRN);

                //Check for empty record
                if(QueryRecord[0] != 0)
                {
                    RecordList.Add(FormatRecord(Encoding.UTF8.GetString(QueryRecord)));
                    ++i;
                }
                else
                {
                    RecordList.Add(string.Format("[{0}]".PadRight(7, ' ') +  
                                "Empty", Convert.ToString(RRN).PadLeft(3, '0')));
                }
            }

        }

        /*/------------------------------------------------------------------------------
        /// <summary>
        /// Close the file used with this class
        /// </summary>

        private static void CloseFile()
        {
            prettyPrintFile.Close();
        }*/

        //-------------------------------------------------------------------------
        /// <summary>
        /// Returns the RRN value of the given parameter
        /// </summary>
        /// <param name="id">ID of query record</param>
        /// <returns></returns>
        private static int CalculateRRN(int id)
        {
            return id;
        }

        //--------------------------------------------------------------------------
        /// <summary>
        /// Gives the RRN to where the information needs to be stored to stored or 
        /// queried
        /// </summary>
        /// <param name="id">Id to calculate the RRN of the document</param>
        /// <returns>The RRN of the main data file</returns>
        private static int CalculateRRN(string id)
        {
            return CalculateRRN(Int32.Parse(id));
        }

        //---------------------------------------------------------------------------
        /// <summary>
        /// Obtain the offset to where the file pointer needs to point
        /// </summary>
        /// <param name="RRN">An ID to what record that needs to be obtained</param>
        /// <returns>offset to file positions</returns>
        private static int CalculateByteOffSet(int RRN)
        {
            return _sizeOfHeaderRec + ((RRN - 1) * _sizeOfDataRec);
        }


        //------------------------------------------------------------------------------
        /// <summary>
        /// Reads one record using the RRN
        /// </summary>
        /// <param name="RRN">File Location of the record</param>
        /// <returns>An array in byte form that came from the maind ata file</returns>
        private static byte[] ReadOneRecord(int RRN)
        {

            byte[] recordData = new byte[_sizeOfDataRec];
            int byteOffSet = CalculateByteOffSet(RRN);

            mainDataFile.Seek(byteOffSet, SeekOrigin.Begin);
            mainDataFile.Read(recordData, 0, recordData.Length);




            return recordData;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Reads the header that contains the amount of records inside
        /// </summary>
        /// <returns>Record amount number</returns>

        private static int ReadHeaderRec()
        {
            byte[] recordData = new byte[_sizeOfHeaderRec];

            mainDataFile.Seek(0, SeekOrigin.Begin);
            mainDataFile.Read(recordData, 0, recordData.Length);

            return Convert.ToInt32(Encoding.UTF8.GetString(recordData));
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Formates the string to de aligned with its header columns
        /// </summary>
        /// <param name="record">record from main data</param>
        /// <returns>formatted string ready to be used</returns>
        private static string FormatRecord(string record)
        {
            int stringPos = 0;

            _id             = record.Substring(stringPos, 3).Trim();
            stringPos       += 3;
            _code           = record.Substring(stringPos, 3).Trim();
            stringPos       += 3;
            _name           = record.Substring(stringPos, 17).Trim();
            stringPos       += 17;
            _continent      = record.Substring(stringPos, 11).Trim();
            stringPos       += 11;
            _region         = record.Substring(stringPos, 10).Trim();
            stringPos       += _region.Length;
            _surfaceArea    = record.Substring(stringPos, 8).Trim();
            stringPos       += 8;
            _yearOfIndep    = record.Substring(stringPos, 5).Trim();
            stringPos       += 5;
            _population     = record.Substring(stringPos, 10).Trim();
            stringPos       += 10;
            _lifeExpectancy = record.Substring(stringPos, 4).Trim();




            string t = "[" + _id + "]".PadRight(3, ' ') +
                        _id.PadRight(4, ' ') +
                        _code.PadRight(5, ' ')+
                        _name.PadRight(20, ' ') +
                        _continent.PadRight(18)+
                        _region.PadRight(15, ' ') +
                        _surfaceArea.PadRight(15, ' ') +
                        _yearOfIndep.PadRight(9, ' ') +
                        _population.PadRight(13, ' ') +
                        _lifeExpectancy;

            return t;
        }


        //------------------------------------------------------------------------------
        /// <summary>
        /// Formates the header to be displayed
        /// </summary>
        /// <returns>A ready to use string aligned in its columns</returns>
        private static string FormatHeader()
        {

            string t = "[RRN]".PadRight(7, ' ') +
                        "ID".PadRight(4, ' ') +
                        "CODE".PadRight(5, ' ') +
                        "NAME".PadRight(20, ' ') +
                        "CONTINENT".PadRight(18, ' ') +
                        "REGION".PadRight(15, ' ') +
                        "AREA".PadRight(15, ' ') +
                        "INDEP".PadRight(9, ' ') +
                        "POPULATION".PadRight(13, ' ') +
                        "L.EXP";

            return t;
        }


        //------------------------------------------------------------------------------
        /// <summary>
        /// Print the results from the formatted text
        /// </summary>
        private static void PrintResults()
        {
            string Header = FormatHeader();

            Console.WriteLine(Header);

            foreach (string s in RecordList)
            {
                Console.WriteLine(s);
            }

        }

    }
}
