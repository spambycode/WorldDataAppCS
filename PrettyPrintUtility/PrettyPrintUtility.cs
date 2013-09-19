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
        static int _sizeOfDataRec = 74;
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
                    RecordList.Add(Encoding.UTF8.GetString(QueryRecord));
                    ++i;
                }
                else
                {
                    RecordList.Add(string.Format("[{0}]  Empty", RRN));
                }
            }

        }


        private static void PrintResults()
        {
            string Header = "[RRN]  ID  CODE  NAME\t\t\tCONTINENT" + 
                            "\tREGION\t\tAREA\t\tINDEP\t\tPOPULATION\tL.EXP";

            Console.WriteLine(Header);

            foreach(string s in RecordList)
            {
                Console.WriteLine(s);
            }
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


    }
}
