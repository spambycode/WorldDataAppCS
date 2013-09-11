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
        private byte _sizeOfHeaderRec;
        private byte _sizeOfDataRec;
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
            mainDataFile = new FileStream("MainData.txt", FileMode.Truncate);
            _sizeOfHeaderRec =(byte)_headerRec.Length;
            _sizeOfDataRec = (byte)(_id.Length + _code.Length + _name.Length + _continent.Length 
                             + _region.Length + _surfaceArea.Length +_yearOfIndep.Length 
                             + _population.Length + _lifeExpectancy.Length);
        }
        //**************************** PUBLIC SERVICE METHODS **********************

        public bool StoreOneCountry(RawData RD)
        {

            _id              = StringToFixedCharArray(RD.ID, _id.Length);
            _code            = StringToFixedCharArray(RD.CODE, _code.Length);
            _name            = StringToFixedCharArray(RD.NAME, _name.Length);
            _continent       = StringToFixedCharArray(RD.CONTINENT, _continent.Length);
            _region          = StringToFixedCharArray(RD.REGION, _region.Length);
            _surfaceArea     = StringToFixedCharArray(RD.SURFACEAREA, _surfaceArea.Length);
            _population      = StringToFixedCharArray(RD.POPULATION, _population.Length);
            _lifeExpectancy  = StringToFixedCharArray(RD.LIFEEXPECTANCY, _lifeExpectancy.Length);
          
            int byteOffSet = CalculateByteOffSet(_id);

            if(RecordIsFilled(byteOffSet))
            {
                return false;
            }


            ++headerCount; //increase amount of records
            WriteOneCountry(byteOffSet);

            return true;

        }


        //**************************** PRIVATE METHODS *****************************

        //---------------------------------------------------------------------------
        /// <summary>
        /// Obtain the offset to where the file pointer needs to point
        /// </summary>
        /// <param name="RRN">An ID to what record that needs to be obtained</param>
        /// <returns>offset to file positions</returns>
        private int CalculateByteOffSet(char[] RRN)
        {

            int rrn = Int32.Parse(RRN.ToString());

            return _sizeOfHeaderRec + ((rrn - 1) * _sizeOfDataRec);
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
            mainDataFile.Read(Data, 0, _sizeOfDataRec);

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
            _headerRec = headerCount.ToString().ToCharArray(0, 3);
            
            mainDataFile.Write(Encoding.ASCII.GetBytes(_headerRec), 0, _headerRec.Length);

            mainDataFile.Seek(byteOffSet, SeekOrigin.Begin);

            string input = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", _id, _code, 
                _name, _continent, _region, _surfaceArea, _yearOfIndep, _population, _lifeExpectancy);

            mainDataFile.Write(Encoding.ASCII.GetBytes(input), 0, _sizeOfDataRec);

        }



        private void FixLengthVaraibles(RawData RD)
        {
            _id = RD.ID.PadLeft(_id.Length, '0').ToCharArray();
            _code = RD.CODE.PadLeft(_code.Length, '0').ToCharArray();

        }

    }
}
