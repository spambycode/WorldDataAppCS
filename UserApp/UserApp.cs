

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

            int CommandCount = 0;
            string command = "";
            SharedClassLibrary.UserInterface UI = new UserInterface(true, true);
            SharedClassLibrary.MainData MD = new MainData(false);

            while(UI.GetOneTransdata(out command) == false)
            {
                switch(command.Substring(0, 2))
                {
                    case "QI":
                        MD.QueryByID(command);
                        break;
                    case "LI":
                        MD.ListById(command);
                        break;
                    case "IN":
                        MD.InsertRecord(command);
                        break;
                    case "DI":
                        MD.DeleteRecordByID(command);
                        break;

                    default:
                        break;
                }
            }





        }
        //*********************** PRIVATE METHODS ********************************




    }
}
