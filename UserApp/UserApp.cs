/* PROJECT: WorldDataAppCS (C#)         PROGRAM: UserApp
 * AUTHOR:
 * OOP CLASSES USED (for Asgn 1):  UserInterface, NameIndex
 *      PLUS FOR FUTURE ASGN:  DataStorage, CodeIndex
 * FILES ACCESSED: (only INDIRECTLY through the OOP classes)
 *      INPUT:   TransData*.txt         (handled by UserInterface class)
 *      OUTPUT:  Log.txt                (handled by UserInterface class)
 *      OUTPUT:  NameIndexBackup*.txt   (handled by NameIndex class)
 *      PLUS FOR FUTURE ASGN:
 *          OUTPUT: MainData*.bin (& "INPUT" to check for "empty locations")
 *                                      (handled by DataStorage class)
 *          OUTPUT: CodeIndexBackup*.bin (handled by CodeIndex class)
 *          AND NameIndexBackup*.bin will be a BINARY file, not TEXT
 *      where * is the appropriate fileNameSuffix.
 * DESCRIPTION:  The program itself is just the CONTROLLER which UTILIZES
 *      the SERVICES (public methods) of various OOP classes.
 *      It processes the transaction requests in TransData file, using
 *      NameIndex to facilitate efficient processing.  It sends the request
 *      and the result to the Log file.  [NameIndex will initially need to
 *      be loaded from the backup file back into the internal structure].
 *      creates an internal NameIndex from data in the RawData file,
 *      PLUS FOR FUTURE ASGN:
 *          The ACTUAL MainData will be provided to user (in Log file)
 *          rather than just the DataRecordPtr.  Also, transaction requests
 *          based on Id and Code are handled using CodeIndex and the random
 *          access MainData file.
 * CONTROLLER ALGORITHM:  Traditional sequential-stream processing - i.e., 
 *      loop til done with TransData
 *      {   input 1 transaction request from TransData
 *          switch to use that data to call appropriate service in NameIndex
 *                  class to handle request
 *      }
 *      finish up with TransData
 *      finish up with NameIndex
 *      PLUS FOR FUTURE ASGN: expanded range of services to handled additional
 *          Id and Code based requests
 *          (Similarly, finish up would need to. . .)
 * CAUTION:  The program code below DOES NOT DEAL DIRECTLY WITH
 *      TransData or NameIndex or Log.  Appropriate OOP classes handle those.  
 *******************************************************************************/

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





        }
        //*********************** PRIVATE METHODS ********************************




    }
}
