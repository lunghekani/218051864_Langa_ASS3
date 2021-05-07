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
        public int DirCount = 0; // count for the director
        public int ShowRatingCount = 0; // count for the Tv ratings
        public List<string> lstDirectors = new List<string>();
        public List<string> lstRatings = new List<string>();

        public clsRegexResponses GetContentFromNewCriteria(string ContentType, string ReleaseYear)
        {
            clsRegexOperations localRegexOperations = new clsRegexOperations();
            List<string> lstFoundCriteria = new List<string>();
           
            List<string> lstFinal = new List<string>();
            var stream = localRegexOperations.CreateStreamConnection(); // assigning the object to a local variable

            string container = string.Empty; // placeholders for using regex and sending a message to the user
            string globalMessage = string.Empty;
                       
            // running the regex to validate the user input for the movie
            if (Regex.Match(ContentType, "^movie$", RegexOptions.IgnoreCase).Success.Equals(true))
            {// running the regex to validate the user input for the year
                if (Regex.Match(ReleaseYear, "([1][9][2-9][0-9]|20[0-1][0-8])").Success.Equals(true))
                {
                                    
                container = ",Movie"; // regex string
                while (!stream.EndOfStream)
                {
                    String line = stream.ReadLine(); // reading from the CSV file line by line

              
                    if (line.Contains(container)) // if the line contains Movie I put it into a list
                    {

                        lstFoundCriteria.Add(line); // placing it inside a list to be cleaned later into relevant columns
                        lstDirectors.Add(line); // keep track of all the movie directors for the flat files
                        DirCount++; // increasing the count of directors
                    }
            

                }
                string patt = @"," + ReleaseYear + ","; // regex string to search for the year the user has eneterd
                foreach (var item in lstFoundCriteria) // loop to split the list into relevant chunks using a regex pattern
                {
                    MatchCollection matches = Regex.Matches(item, patt); // checking the regex matches and storing them into a match collections list

                    foreach (var m in matches) //looping through each match
                    {
                        string[] result = Regex.Split(item, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"); // splitting the lines by columns and accessing their values using regex
                        
                        lstFinal.Add(result[0] + "\t\t" + result[1] + "\t\t" + result[7] + "\t\t" + result[9] + "\t\t" + result[11]); // creating a base for the expected output and this list will be exported
                        
                    }
                }


                globalMessage = "Your input was successful now displaying all Movies from " + ReleaseYear; // creating a message to tell the user about their slecetion
                }
                else
                {
                    // notifying the user that they enetered the incorrect year
                globalMessage = "Your year is invalid, please enter a valid year";
                }
            }
            // NB THIS CODE MATCHES THE EXACT THING AS THE BLOCK ABOVE THIS JUST SEARCHES FOR TV SHOWS
            else if (Regex.Match(ContentType, "^tv show$", RegexOptions.IgnoreCase).Success.Equals(true))
            {
                if (Regex.Match(ReleaseYear, "([1][9][2-9][0-9]|20[0-1][0-8])").Success.Equals(true))
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
                    globalMessage = "Your year is invalid, please enter a valid year";
                }
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
    


        // block used to create the directors CSV
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
        // block used to create the ratings CSV
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
