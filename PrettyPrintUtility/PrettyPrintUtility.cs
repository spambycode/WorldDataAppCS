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
        static StreamWriter prettyPrintFile;
        static int _sizeOfHeaderRec = 3;
        static int _sizeOfDataRec = 71;
        static private string _id;               //ID of record
        static private string _code;             //Country code
        static private string _name;            //Name of country
        static private string _continent;       //What continent the country is located
        static private string _region;          //What region the country is located
        static private string _surfaceArea;      //Size of the country
        static private string _yearOfIndep;      //What year they went independent
        static private string _population;      //Total population of the country
        static private string _lifeExpectancy;   //The average time someone is alive in the country
        static List<string> RecordList = new List<string>();

        public static void Main(string[] args)
        {
            if (File.Exists("MainData.txt"))
            {
                prettyPrintFile = new StreamWriter("PrettyPrint.txt", false);
                mainDataFile = new FileStream("MainData.txt", FileMode.Open);
                HandleDataFile();
                PrintResults();
            }
            else
            {
                Console.WriteLine("Error MainData.txt does not exist!");
            }

            CloseFile();
          
        }

        private static void HandleDataFile()
        {
            int RecordCount = ReadHeaderRec();
            int id = 1;
            int RRN = 0;
            byte[] QueryRecord;

            for(int i = 0; i < RecordCount;)
            {
                RRN = CalculateRRN(id++);
                QueryRecord = ReadOneRecord(RRN);
                if(QueryRecord[0] != 0)
                {
                    RecordList.Add(FormatRecord(Encoding.UTF8.GetString(QueryRecord)));
                    ++i;
                }
                else
                {
                    RecordList.Add(string.Format("[{0}]".PadRight(7, ' ') +  "Empty", Convert.ToString(RRN).PadLeft(3, '0')));
                }
            }

        }


        private static void PrintResults()
        {
            string Header = FormatHeader();

            prettyPrintFile.WriteLine(Header);

            foreach(string s in RecordList)
            {
                prettyPrintFile.WriteLine(s);
            }

        }

        private static void CloseFile()
        {
            prettyPrintFile.Close();
        }

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

        private static byte[] ReadOneRecord(int RRN)
        {

            byte[] recordData = new byte[_sizeOfDataRec];
            int byteOffSet = CalculateByteOffSet(RRN);

            mainDataFile.Seek(byteOffSet, SeekOrigin.Begin);
            mainDataFile.Read(recordData, 0, recordData.Length);




            return recordData;
        }

        private static int ReadHeaderRec()
        {
            byte[] recordData = new byte[_sizeOfHeaderRec];

            mainDataFile.Seek(0, SeekOrigin.Begin);
            mainDataFile.Read(recordData, 0, recordData.Length);

            return Convert.ToInt32(Encoding.UTF8.GetString(recordData));
        }

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
                        _id.PadRight(6, ' ') +
                        _code.PadRight(7, ' ')+
                        _name.PadRight(20, ' ') +
                        _continent.PadRight(18)+
                        _region.PadRight(15, ' ') +
                        _surfaceArea.PadRight(15, ' ') +
                        _yearOfIndep.PadRight(9, ' ') +
                        _population.PadRight(13, ' ') +
                        _lifeExpectancy;

            return t;//string.Format("[{0}]\t{1,2}{2,2}{3,2}{4,10}{5:N,5}{6,5}{7,5}{8:N,10}{9,4}", 
                //_id, _id, _code,_name, _continent, _region, _surfaceArea, _yearOfIndep, _population, _lifeExpectancy);
        }

        private static string FormatHeader()
        {

            string t = "[RRN]".PadRight(7, ' ') +
                        "ID".PadRight(6, ' ') +
                        "CODE".PadRight(7, ' ') +
                        "NAME".PadRight(20, ' ') +
                        "CONTINENT".PadRight(18) +
                        "REGION".PadRight(15, ' ') +
                        "AREA".PadRight(15, ' ') +
                        "INDEP".PadRight(9, ' ') +
                        "POPULATION".PadRight(13, ' ') +
                        "L.EXP";

            return t;
            //return string.Format("[RRN]\tID\tCODE\tNAME\t\t\tCONTINENT\t\tREGION\t\t\tAREA\t\tINDEP\t\tPOPULATION\tL.EXP");
        }


    }
}
