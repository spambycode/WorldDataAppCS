﻿/* PROJECT: WorldDataAppCS (C#)         CLASS: UserInterface
 * AUTHOR:
 * FILES ACCESSED:
 * DESCRIPTION: 
 *******************************************************************************/

using System;
using System.IO;

namespace SharedClassLibrary
{
    public class UserInterface
    {
        //**************************** PRIVATE DECLARATIONS ************************
        private StreamReader transDataFile;
        private StreamWriter logFile;

        //**************************** PUBLIC GET/SET METHODS **********************


        //**************************** PUBLIC CONSTRUCTOR(S) ***********************
        public UserInterface(bool OpenLog, bool OpenTrans, string TransFileName = "Transdata.txt")
        {
            Console.WriteLine("OK, UserInterface object created");

            if(OpenLog)
            {
                logFile = new StreamWriter("Log.txt", true);
                WriteToLog("Log File Open");
            }

            if (OpenTrans)
            {
                transDataFile = new StreamReader(TransFileName);
                WriteToLog("Trans Data File Open");
            }
        }

        public UserInterface()
        {
            logFile = new StreamWriter("Log.txt", true);
            WriteToLog("Log File Open");
        }
        //**************************** PUBLIC SERVICE METHODS **********************


        //--------------------------------------------------------------------------
        /// <summary>
        /// Write to the log file
        /// </summary>
        /// <param name="input">string that wants to be inputted to the log file</param>
        public void WriteToLog(string input)
        {
            if(logFile != null)
            {
                logFile.WriteLine(input);
            }
            else
            {
                Console.WriteLine("Log file is not opened");
            }
        }


        //---------------------------------------------------------------------------
        /// <summary>
        /// Reads a line in transdata.
        /// </summary>
        /// <param name="query">Query data of what to return to calle</param>
        /// <returns>returns EOF state</returns>

        public bool GetOneTransdata(out string query)
        {
            query = "";

            if (transDataFile != null)
            {
                if (transDataFile.EndOfStream == false)
                {
                    query = transDataFile.ReadLine();

                    return false;
                }
            }
            return true;
        }


        //---------------------------------------------------------------------------
        /// <summary>
        /// Close the files that where open during the start of the class
        /// </summary>
        /// <param name="closeLog">Close log file</param>
        /// <param name="closeTransData">Close trans data file</param>
        public void CloseFile(bool closeLog, bool closeTransData)
        {
            if (closeLog)
                logFile.Close();
            if (closeTransData)
                transDataFile.Close();
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Close all files open
        /// </summary>
        public void CloseFile()
        {
            WriteToLog("Log File Closed");
            WriteToLog("Trans Data File Closed");
            logFile.Close();
            transDataFile.Close();
        }

        //**************************** PRIVATE METHODS *****************************


    }
}
