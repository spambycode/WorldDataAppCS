/* PROJECT: WorldDataAppCS (C#)         CLASS: RawData
 * AUTHOR:
 * FILES ACCESSED:
 * FILE STRUCTURE:  
 * DESCRIPTION:   
 *******************************************************************************/

using System;
using System.IO;

namespace SharedClassLibrary
{
    public class RawData
    {
        //**************************** PRIVATE DECLARATIONS ************************
        private StreamReader fileReader;

        //**************************** PUBLIC GET/SET METHODS **********************

        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string CONTINENT { get; set; }
        public string REGION { get; set; }
        public string SURFACEAREA { get; set; }
        public string YEAROFINDEP { get; set; }
        public string POPULATION { get; set; }
        public string LIFEEXPECTANCY { get; set; }
        

        //**************************** PUBLIC CONSTRUCTOR(S) ***********************
        public RawData(string filename)
        {
            fileReader = new StreamReader(filename);
        }
        //**************************** PUBLIC SERVICE METHODS **********************


        public bool ReadOneCountry()
        {
            if (fileReader.EndOfStream != true)
            {
                var line = fileReader.ReadLine();
                var split = line.Split(',');


                //Asign varaibles from the read record
                ID   = split[0];
                CODE = split[1];
                NAME = split[2];
                CONTINENT = split[3];
                REGION = split[4];
                SURFACEAREA = split[5];
                YEAROFINDEP = split[6];
                POPULATION = split[7];
                LIFEEXPECTANCY = split[8];


                return false;

            }
            return true;
        }




        //**************************** PRIVATE METHODS *****************************


    }
}
