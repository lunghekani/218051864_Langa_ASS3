using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;
using System.IO;
using System.Text.RegularExpressions;

namespace Business_Logic_Layer
{
    public class businesslogic
    {
    }
    public class clsRegexResponses {
        public string msg { get; set; }
        public List<string> exported { get; set; }

    }

    public class clsValidateRegex{
        public int DirCount = 0;
        public int ShowRatingCount = 0;
        public List<string> lstDirectors = new List<string>();
        public List<string> lstRatings = new List<string>();

        public clsRegexResponses GetContentFromNewCriteria(string ContentType, string ReleaseYear)
        {
            clsRegexOperations localRegexOperations = new clsRegexOperations();
            List<string> lstFoundCriteria = new List<string>();
           
            List<string> lstFinal = new List<string>();
            var stream = localRegexOperations.CreateStreamConnection(); // assigning the object to a local variable
            string container = string.Empty;
            string globalMessage = "There is an error in your selection : ";
            string finalPatt = ",(?=(?:[^" + "]*" + "[^" + "]*" + ")*[^" + "]*$)";
            bool testCase = Regex.Match(ContentType, "^tv show$", RegexOptions.IgnoreCase).Success;
            // backup pattern ([1][9][2-9][0-9]|20[0-1][0-8])
            if (Regex.Match(ContentType, "^movie$", RegexOptions.IgnoreCase).Success.Equals(true))
            {
                container = ",Movie";
                while (!stream.EndOfStream)
                {
                    String line = stream.ReadLine();

              
                    if (line.Contains(container))
                    {

                        lstFoundCriteria.Add(line);
                        lstDirectors.Add(line);
                        DirCount++;
                    }
            

                }
                string patt = @"," + ReleaseYear + ",";
                foreach (var item in lstFoundCriteria)
                {
                    MatchCollection matches = Regex.Matches(item, patt);

                    foreach (var m in matches)
                    {
                        string[] result = Regex.Split(item, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        
                        lstFinal.Add(result[0] + "\t\t" + result[1] + "\t\t" + result[7] + "\t\t" + result[9] + "\t\t" + result[11]);
                        
                    }
                }


                globalMessage = "Your input was successful now displaying all Movies from " + ReleaseYear;
            }
            else if (Regex.Match(ContentType, "^tv show$", RegexOptions.IgnoreCase).Success.Equals(true))
            {
                container = ",TV Show";
                while (!stream.EndOfStream)
                {
                    String line = stream.ReadLine();

                    
                    if (line.Contains(container)){ 
                    
                    lstFoundCriteria.Add(line);
                        lstRatings.Add(line);
                        ShowRatingCount++;
                    }
                    
                }
                string patt = @"," + ReleaseYear + ",";
                foreach (var item in lstFoundCriteria)
                {
                    MatchCollection matches = Regex.Matches(item, patt);

                    foreach (var m in matches)
                    {
                        string[] result = Regex.Split(item, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        lstFinal.Add(result[0] + "\t\t" + result[1] + "\t\t" + result[7] + "\t\t" + result[9] + "\t\t" + result[11]);
                        lstRatings.Add(item);
                    }
                }
                globalMessage = "Your input was successful now displaying all TV Shows from "+ ReleaseYear;
            }
            else
            {
                globalMessage = "Your input was invalid, Please enter a valid Movie/Tv Show between 1920-2018";
            }

            return new clsRegexResponses() {
                msg = globalMessage,
                exported = lstFinal
            };

        }
    


        
        public void generateDirectorReports()
        {
            var localData = new clsRegexOperations();


            //Path for the files to be created
            string file_path = @"../../";
            //The file are created
            StreamWriter swDirector = new StreamWriter(file_path + Path.DirectorySeparatorChar + "movie_directors.csv", true);

            foreach (var item in lstDirectors)
            {
                swDirector.WriteLine(item);
                DirCount++;
            }
            swDirector.Close();

        }
        public void generateShowReports()
        {
            var localData = new clsRegexOperations();
            List<string> lstOutput = new List<string>();

            //Path for the files to be created
            string file_path = @"../../";
            //The file are created

            StreamWriter swRatings = new StreamWriter(file_path + Path.DirectorySeparatorChar + "ratings_tvshows.csv", true);

            foreach (var item in lstRatings)
            {
                swRatings.WriteLine(item);
                ShowRatingCount++;
            }
            swRatings.Close();
        }


    }
}
