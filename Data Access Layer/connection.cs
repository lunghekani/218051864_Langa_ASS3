using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Data_Access_Layer
{
    public class connection
    {
    }
        
    public class clsRegexOperations{
     

        public  StreamReader CreateStreamConnection() {
            var CSV_Stream = new StreamReader("../../netflix_titles.csv");

            return CSV_Stream;
        }

        

          

        }
    }

