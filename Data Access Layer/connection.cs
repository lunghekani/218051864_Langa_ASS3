using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Data_Access_Layer
{

    // LV LANGA 218051864
    public class connection
    {
    }
        
    public class clsRegexOperations{
     

        public  StreamReader CreateStreamConnection() {
            var CSV_Stream = new StreamReader("../../netflix_titles.csv");  // Creating the connection string to the CSV File

            return CSV_Stream;
        }

        

          

        }
    }

