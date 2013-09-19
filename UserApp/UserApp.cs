

using System;

using SharedClassLibrary;

namespace UserApp
{
    public class UserApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OK, starting UserApp");

            // Detect whether this program is being run by AutoTesterUtility,
            //      or manually by developer & fix fileNameSuffix accordingly.
            // <WRITE APPROPRIATE CODE HERE>
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            int CommandCount        = 0;
            int ErrorCommandCount   = 0;
            int SuccessCommandCount = 0;
            string command = "";
            SharedClassLibrary.UserInterface UI = new UserInterface(true, true);
            SharedClassLibrary.MainData MD = new MainData(false);

            while(UI.GetOneTransdata(out command) == false)
            {
                switch(command.Substring(0, 2))
                {
                    case "QI":
                        MD.QueryByID(QueryData(command));
                        break;
                    case "LI":
                        MD.ListById(QueryData(command));
                        break;
                    case "IN":
                        MD.InsertRecord(QueryData(command));
                        break;
                    case "DI":
                        MD.DeleteRecordByID(QueryData(command));
                        break;

                    default:
                        Console.WriteLine("No Valid Command found");
                        break;
                }

                CommandCount++;

            }

            UI.WriteToLog(string.Format("UserApp completed: {0} transactions handled", CommandCount));


        }
        //*********************** PRIVATE METHODS ********************************

        private static string QueryData(string input)
        {
            return input.Substring(2);
        }



    }
}
