/* NOTE:  NOT USED IN ASGN 1
 * */

/* PROJECT:  WorldDataAppCS (C#)            PROGRAM: AutoTesterUtility
 * AUTHOR:  Kaminski/CS3310
 * PROGRAMS ACCESSED:  SetupProgram, UserApp, PrettyPrintUtility
 * OOP CLASSES USED:  none - this program is just a developer utility program
 *      and doesn't use the OOP paradigm
 * FILES ACCESSED:  4 output files from this project (using suffix for *):
 *      Log.txt, MainData*.bin, NameIndexBackup*.bin, codeIndexBackup*.bin
 * DESCRIPTION:  Utility program which aids developer to automate testing of the
 *      project with various test data sets.  It deletes output files from
 *      previous runs and executes the 3 programs with various test files
 *      (and N's) as parameters when calling those programs' Main methods.
 ******************************************************************************/

using System;
using System.IO;

using SetupProgram;
using UserApp;
using PrettyPrintUtility;

namespace WorldDataAppCS
{
    class AutoTesterUtility
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OK, starting AutoTesterUtility");

            // The 3 parallel arrays (all strings, including the N's) with
            //      - hard-coded SUFFIX values to designate which files to use
            //      - N's to limit how many records to display during testing
            // The dataFileSuffix is used for RawData*.csv, MainData*.bin,
            //      NameIndexBackup*.bin, CodeIndexBackup*.bin
            string[] dataFileSuffix = { "Tester", "" };
            string[] transFileSuffix = { "", "All" };
            string[] nRecToShow = { "All", "60" };

            //Delete the SINGLE output Log.txt file (if it exists)
            DeleteFile("Log.txt");

            for (int i = 0; i < dataFileSuffix.Length; i++)
            {
                //Delete 3 other output files (if they exist)
                DeleteFile("MainData" + dataFileSuffix[i] + ".bin");
                DeleteFile("NameIndexBackup" + dataFileSuffix[i] + ".bin");
                DeleteFile("CodeIndexBackup" + dataFileSuffix[i] + ".bin");

                SetupProgram.SetupProgram.Main(new string[] { dataFileSuffix[i] });
                UserApp.UserApp.Main(new string[] { dataFileSuffix[i], transFileSuffix[i] });
                PrettyPrintUtility.PrettyPrintUtility.Main(new string[] { dataFileSuffix[i], nRecToShow[i] });
            }
        }
        //**************************************************************************
        private static bool DeleteFile(String fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true;
            }
            else
            {
                return false;
            }
        }
        //**************************************************************************
    }
}
