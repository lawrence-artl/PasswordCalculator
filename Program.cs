using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using passwordcalc.engines;



namespace passwordcalc
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //variables
            int[] passwordprops = { 0, 0, 0, 0 };
            var words = (characters: "special characters", numbers: "numbers", upperletters: "letters", lowerletters: "letters");
            int[] passwordset = { 0, 0, 0, 0 }; //array for totals of character sets
            int characterset = 0;

            //start program

            Console.WriteLine("Password Calculator, Ver 1.01");

            string password = Privacy.TypePW();

            PWDeconstruct(passwordprops, passwordset, password); //passes passwordprops by ref automatically for arrays
            for (int i = 0; i < 4; i++)
            { characterset += passwordset[i]; }
            double combinations = PasswordCombinations(characterset, password); //stores the total number of possible combinations based on 
                                                                                //character sets used and password length
            {
                if (passwordprops[0] == 1)      //use these functions to modify the output of the password information sentence
                { words.characters = "special character"; }
                if (passwordprops[1] == 1)
                { words.numbers = "number"; }
                if (passwordprops[2] == 1)
                { words.upperletters = "letter"; }
                if (passwordprops[3] == 1)
                { words.lowerletters = "letter"; }
            }
            Paragraph output = new Paragraph();     //create paragraph object

            string paragraph = "Your password is " + StrngLngth(password) + " character(s) long, and contains " + passwordprops[0] + " " + words.characters + ", " + passwordprops[1] + " " + words.numbers +
                ", " + passwordprops[2] + " uppercase " + words.upperletters + ", and " + passwordprops[3] + " lowercase " + words.lowerletters + ".";


            Console.WriteLine("\n\nPASSWORD INFORMATION:");
            Paragraph.Wrap(paragraph);  //wrap paragraph object
            
            Console.WriteLine("\nPress 'ENTER' to test..."); Console.ReadLine();
            TestPW(password, characterset, combinations);
            Console.WriteLine("\nPress 'ENTER' to Exit."); Console.ReadLine();

        }
        /*static string TypePW(string password)
        {
            Console.Write("Type in your pASSword:");
            //string yourpassword = "";   //for testing only
            string newpassword = "";
            string fullpassword = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true); //reads key presses from input

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter) //press any key other than enter or bs
                {
                    password += key.KeyChar;    //add that character to the password string
                    //yourpassword += key.KeyChar; //for testing only, comment out when program goes live
                    byte[] ascii = Encoding.ASCII.GetBytes(password);   //makes an array of ascii characters
                    foreach (Byte b in ascii)
                    {
                        newpassword = b.ToString(); //converts the number to a string
                        int result = Int32.Parse(newpassword); //converts string to int 
                        if ((result >= 33 && result <= 47) || (result >= 58 && result <= 64) || (result >= 91 && result <= 96)
                            || (result >= 123 && result <= 126))
                        { fullpassword += "A"; }    //for a character}
                        if (result >= 48 && result <= 57)
                        { fullpassword += "B"; }    //for a number
                        if (result >= 65 && result <= 90)
                        { fullpassword += "C"; }    //for a capital letter
                        if (result >= 97 && result <= 122)
                        { fullpassword += "D"; }    //for a lowercase letter
                    }
                    password = "";  //reset password to blank; this process prevents password leakage
                    Console.Write("*"); //write a * for the character for secrecy
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && fullpassword.Length > 0) //if bs is pressed and something is writte
                    {
                        fullpassword = fullpassword.Substring(0, (fullpassword.Length - 1));    //change the length of the password by one character
                        Console.Write("\b \b"); //remove a *
                    }
                    else if (key.Key == ConsoleKey.Enter)   //indicate password is finalized
                    { break; }
                }
            } while (true);
            //Console.Write("\n" + yourpassword); //for testing only
            return fullpassword;                //return the modified password
        }

        */
        static int StrngLngth(string pass)
        {
            int length = pass.Length;   //find the length of the password
            return length;
        }
        static void PWDeconstruct(int[] passwordprops, int[] passwordset, string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                ;
                if (password[i].Equals('A'))
                { passwordprops[0]++; passwordset[0] = 30; }      //counts characters
                else if (password[i].Equals('B'))
                { passwordprops[1]++; passwordset[1] = 10; }     //counts numbers
                else if (password[i].Equals('C'))
                { passwordprops[2]++; passwordset[2] = 26; }     //counts uppercase letters
                else if (password[i].Equals('D'))
                { passwordprops[3]++; passwordset[3] = 26; }     //counts lowercase letters
            }

        }
        static double PasswordCombinations(int characterset, string password)
        {
            int passwordlength = StrngLngth(password);
            double combinations = (Math.Pow(characterset, passwordlength));
            return combinations;
        }
        static void PasswordSuggestions()
        {
            //suggest ways to make the password stronger based on length and characters used
        }
        
        /*public static void WordWrap(string origtext)
        {
            //variables
            int window_width = Console.WindowWidth - 1;
            /*  get width of the window
                must subtract '1' bc a line that goes to the very edge of the window
                makes a blank line below 
            //string origtext = Console.ReadLine();                   //get input from user
            string text = origtext + " ";                           //append 1 space to end of text
            string line = " ";                                      //create string called 'line' for storage of substrings
            int string_length = text.Length;                        //get length of full text input
            int total_lines = string_length / window_width;         //find total lines that text takes up
            int remainder_lines = string_length % window_width;     //find remainder of text that doesn't go to end of window
            int j;                                                  //initialize 'j' to be used outside of loop
            if (total_lines <= 0)           //don't do anything special, just reprint the text later
            {   }
            else
            {
                while (total_lines > 0)     //loop while lines of text still remain
                {
                    for (j = window_width; !text[j].Equals(' '); j--)      //loop to find best place to split line
                    { }
                    line = text.Substring(0, j);        //line equals the substring from 0 to j
                    text = text.Substring(j + 1);       //text is reset to remove previous line, start at j+1 to remove space in front 
                    Console.WriteLine(line);            //print the extracted line
                    total_lines--;                      //let the loop know we've removed one line
                }
                if (remainder_lines > 0)
                {
                    Console.WriteLine(text);
                }
                else
                { }
            }
        } */

        public static void TestPW(String password, int characterset, double combinations)
        {
            //double seconds, minutes, hours, days, years;

            int roundunit = 0;                          //how many units to round by
            String timeunits = "";                      //for use with string at the end
            int factor = 0;                             //to tell string which unit to print
            Double timetotal = 0.0;                     //total amount of time it takes to crack
            timetotal = combinations / 10000000000;     //10000 computers working in cloud service trying 1,000,000 pw's a second
                                                        //number of seconds it would take

            if (timetotal > 60)                         //determine how long the time really is
            {
                timetotal = timetotal / 60; factor++;    //1
                if (timetotal > 60)
                {
                    timetotal = timetotal / 60; factor++;   //2
                    if (timetotal > 24)
                    {
                        timetotal = timetotal / 24; factor++;   //3
                        if (timetotal > 365)
                        {
                            timetotal = timetotal / 365;    factor++;   //4
                        }
                    }
                }

            }
            switch (factor)                         //determine which units to display for the password crack time
            {
                case 0:
                    timeunits = "seconds";  roundunit = 6;  break;
                case 1:
                    timeunits = "minutes";  roundunit = 2;  break;
                case 2:
                    timeunits = "hours";    roundunit = 1;  break;
                case 3:
                    timeunits = "days"; roundunit = 1;  break;
                case 4:
                    timeunits = "years";    roundunit = 0;  break;
                default:
                    timeunits = "null"; break;
            }

            timetotal = Math.Round(timetotal, roundunit);
            string readabletime = timetotal.ToString("N0");
            string readablecombinations = combinations.ToString("N0"); 


            Console.WriteLine("Your password has the following structure: " + password);

            
            string phrase = "Based on it's structure, a computer would have to test " + readablecombinations + " combinations of characters " +
                "in order to Brute Force your password. This would take a cloud-network of 10,000 computers, trying " +
                "1 million passwords per second, about " + readabletime + " " + timeunits + ".";

            Paragraph.Wrap(phrase);
            
            
            
            Console.Write("");
        }

    }
}

