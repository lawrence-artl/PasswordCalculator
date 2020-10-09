using System;
using System.Collections.Generic;
using System.Text;

namespace passwordcalc.engines
{
    class Privacy
    {
        public string Password;

        public string TypePW()
        {
            
            Console.Write("Type in your password:");
            //Console.WriteLine("INSIDE TYPEPW CLASS\n");
            //string yourpassword = "";   //for testing only
            string newpassword = "";
            string fullpassword = "";
            string password = "";
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

    }
}
