/* NOTE:  NOT USED IN ASGN 1
 * */

/* PROJECT: WorldDataAppCS (C#)         CLASS: MainData
 * AUTHOR:
 * FILES ACCESSED:
 * FILE STRUCTURE:
 * DESCRIPTION:
*******************************************************************************/

using System;
using System.IO;
using System.Text;

namespace SharedClassLibrary
{
    public class MainData
    {
        //**************************** PRIVATE DECLARATIONS ************************

        private FileStream mainDataFile;
        private int headerCount = 0;                    //Counts how many recorders
        private int _sizeOfHeaderRec;
        private int _sizeOfDataRec;
        private char[] _headerRec = new char[3];        //Header rec of the document
        private char[] _id = new char[3];               //ID of record
        private char[] _code = new char[3];             //Country code
        private char[] _name = new char[17];            //Name of country
        private char[] _continent = new char[11];       //What continent the country is located
        private char[] _region = new char[10];          //What region the country is located
        private char[] _surfaceArea = new char[8];      //Size of the country
        private char[] _yearOfIndep = new char[5];      //What year they went independent
        private char[] _population = new char[10];      //Total population of the country
        private char[] _lifeExpectancy = new char[4];   //The average time someone is alive in the country

        //**************************** PUBLIC GET/SET METHODS **********************




        //**************************** PUBLIC CONSTRUCTOR(S) ***********************
        public MainData()
        {
            mainDataFile = new FileStream("MainData.txt", FileMode.Create);
            _sizeOfHeaderRec =_headerRec.Length;
            _sizeOfDataRec = _id.Length + _code.Length + _name.Length + _continent.Length 
                             + _region.Length + _surfaceArea.Length +_yearOfIndep.Length 
                             + _population.Length + _lifeExpectancy.Length;
        }
        //**************************** PUBLIC SERVICE METHODS **********************

        /// <summary>
        /// Stores one country to the main data file
        /// </summary>
        /// <param name="RD">Raw data class that holds parsed values</param>
        /// <returns>The boolean value of whether the main data file has a dup</returns>
        public bool StoreOneCountry(RawData RD)
        {

            InitializeFixLengthVaraibles(RD);

            int RRN = CalculateRRN(RD.ID);
            int byteOffSet = CalculateByteOffSet(RRN);

            if(RecordIsFilled(byteOffSet))
            {
                return false;
            }


            ++headerCount; //increase amount of records
            WriteOneCountry(byteOffSet);

            return true;

        }

        //-------------------------------------------------------------------------
        /// <summary>
        /// Closes the main data file 
        /// </summary>
        public void CloseFile()
        {
            mainDataFile.Close();
        }

        //**************************** PRIVATE METHODS *****************************

        //---------------------------------------------------------------------------
        /// <summary>
        /// Obtain the offset to where the file pointer needs to point
        /// </summary>
        /// <param name="RRN">An ID to what record that needs to be obtained</param>
        /// <returns>offset to file positions</returns>
        private int CalculateByteOffSet(int RRN)
        {
            return _sizeOfHeaderRec + ((RRN - 1) * _sizeOfDataRec);
        }

        //--------------------------------------------------------------------------
        /// <summary>
        /// Gives the RRN to where the information needs to be stored to stored or 
        /// queried
        /// </summary>
        /// <param name="id">Id to calculate the RRN of the document</param>
        /// <returns>The RRN of the main data file</returns>
        private int CalculateRRN(string id)
        {
            return Int32.Parse(id);
        }

        //--------------------------------------------------------------------------
        /// <summary>
        /// Checks the record to see if something is in its place already
        /// </summary>
        /// <param name="byteOffSet">Where in the file it needs to look at</param>
        /// <returns>If true file is already filled</returns>
        private bool RecordIsFilled(int byteOffSet)
        {
            byte[] Data = new Byte[_sizeOfDataRec];   //Data being read from file

            mainDataFile.Seek(byteOffSet, SeekOrigin.Begin);
            mainDataFile.Read(Data, 0, Data.Length);

            if (Data[0] == 0)
                return false;
            

            return true;
        }


        //--------------------------------------------------------------------------
        /// <summary>
        /// Writes one country to the file by the given byteOffSet
        /// </summary>
        /// <param name="byteOffSet">Where in the file to begin the writing process</param>
        private void WriteOneCountry(int byteOffSet)
        {
            mainDataFile.Seek(0, SeekOrigin.Begin);
            _headerRec = headerCount.ToString().PadLeft(_headerRec.Length, '0').ToCharArray();

            WriteOneRecord(_headerRec);
            if(mainDataFile.Length < byteOffSet)
                mainDataFile.SetLength(byteOffSet);

            //Move file pointer to new location
            mainDataFile.Seek(byteOffSet, SeekOrigin.Begin);

            //Write the information to the maindata file
            WriteOneRecord(_id);
            WriteOneRecord(_code);
            WriteOneRecord(_name);
            WriteOneRecord(_continent);
            WriteOneRecord(_region);
            WriteOneRecord(_surfaceArea);
            WriteOneRecord(_yearOfIndep);
            WriteOneRecord(_population);
            WriteOneRecord(_lifeExpectancy);

        }


        //-----------------------------------------------------------------------------
        /// <summary>
        /// Initializes all the variables that require a fixed length
        /// </summary>
        /// <param name="RD">Class that holds strings at random lengths</param>
        private void InitializeFixLengthVaraibles(RawData RD)
        {
            _id          = RD.ID.PadLeft(_id.Length, '0').ToCharArray();
            _code        = RD.CODE.PadLeft(_code.Length, ' ').ToCharArray();
            _name        = RD.NAME.PadRight(_name.Length, ' ').ToCharArray();
            _continent   = RD.CONTINENT.PadRight(_continent.Length, ' ').ToCharArray();
            _region      = RD.REGION.PadRight(_region.Length, ' ').ToCharArray();
            _surfaceArea = RD.SURFACEAREA.PadLeft(_surfaceArea.Length, '0').ToCharArray();
            _yearOfIndep = RD.YEAROFINDEP.PadLeft(_yearOfIndep.Length, '0').ToCharArray();
            _population  = RD.POPULATION.PadLeft(_population.Length, '0').ToCharArray();

            //Check this (needs to be XX.X or X.XX)
            _lifeExpectancy = RD.LIFEEXPECTANCY.PadLeft(_lifeExpectancy.Length, '0').ToCharArray();


        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Uses the write function in file stream to write a record to the main file
        /// </summary>
        /// <param name="input">Input wanted to write to file</param>

        private void WriteOneRecord(char[] input)
        {
            mainDataFile.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
        }

    }
}
