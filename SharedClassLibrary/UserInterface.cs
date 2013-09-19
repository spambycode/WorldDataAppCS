/* PROJECT: WorldDataAppCS (C#)         CLASS: UserInterface
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
        public UserInterface(bool OpenLog, bool OpenTrans)
        {
            Console.WriteLine("OK, UserInterface object created");

            if(OpenLog)
            {
                logFile = new StreamWriter("Log.txt", true);
            }

            if(OpenTrans)
            {
                if(File.Exists("Transdata.txt"))
                {
                    transDataFile = new StreamReader("Transdata.txt");
                }
                else
                {
                    Console.WriteLine("Error no file called Transdata.txt");
                }
            }
        }

        public UserInterface()
        {

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


        public bool GetOneTransdata(out string query)
        {
            query = "";

            if(transDataFile.EndOfStream == false)
            {
                query = transDataFile.ReadLine();

                return false;
            }
            return true;
        }

        //**************************** PRIVATE METHODS *****************************


    }
}
