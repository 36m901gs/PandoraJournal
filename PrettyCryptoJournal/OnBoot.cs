using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace PrettyCryptoJournal
{
    internal class OnBoot
    {

        //generate password hash hash function 

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        void HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            var hashSave = Convert.ToHexString(hash);
            File.WriteAllText(@"C:\Users\njiso\Desktop\test", hashSave); //gonna have to make this save path more dynamic later
            File.WriteAllBytes(@"C:\Users\njiso\Desktop\salt", salt);
        }

        //verify password
        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            var booltest = hashToCompare.SequenceEqual(Convert.FromHexString(hash));
            Console.WriteLine(booltest);
            return booltest;
        }







        public bool Xml()
        {

         if(File.Exists(@"C:\Users\njiso\Desktop\test") && File.Exists(@"C:\Users\njiso\Desktop\salt"))
           {
                return true;
            }
            return false;

        }

        public bool PasswordRules(string password)
        {
            if (password.Length > 4)
            {
                return true;
            }
            else
            {
                return false;
            }

            

        }


        public bool GenerateNewXML()
        {

            //make dialogue box asking for password -- will later figure out how to use this hash in a key?
            //wrap this up in a function later

            string password ="";
            string password2 = "";

            while(!PasswordRules(password)) 
            {
                password = Microsoft.VisualBasic.Interaction.InputBox("Password", "Security", "Please Type in A Password");
            }

            while(password != password2 && PasswordRules(password2)==false)
            {
                password2 = Microsoft.VisualBasic.Interaction.InputBox("Confirm Password", "Security", "Please Retype Password");
            }

            //use input to create password and salt
            HashPasword(password2, out var salt);


            return true;
        }


        //user access - noticing, generating, authenticating users -- will have to make from scratch to avoid using sql db
        public bool AppBoot()
        {
            string userinput = "";

            while (!Xml())
            {
                GenerateNewXML();
            }

            //password compare 
            while(!(VerifyPassword(userinput, File.ReadAllText(@"C:\Users\njiso\Desktop\test"),File.ReadAllBytes(@"C:\Users\njiso\Desktop\salt"))))
            {
                //once this works, add a count to it
                userinput = Microsoft.VisualBasic.Interaction.InputBox("Password", "Security", "Please Type in A Password");

            };

            return true;


        }
            
         

        //(2) 

        //key management - noticing, generating keys

        //spark application after first steps are done
    }
}
