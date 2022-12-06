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

        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        //verify password
        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }







        public bool Xml()
        {

         if(File.Exists("file"))
           {
                return true;
            }
            return false;

        }

        public bool PasswordRules(string password)
        {

            return true;

        }


        public bool GenerateNewXML()
        {

            //make dialogue box asking for password -- will later figure out how to use this hash in a key?
            //wrap this up in a function later
           var password = Microsoft.VisualBasic.Interaction.InputBox("Password", "Title", "Default Text");
            if (!PasswordRules(password)) 
            {
                password = Microsoft.VisualBasic.Interaction.InputBox("Password", "Title", "Default Text");
            }
            var password2 = Microsoft.VisualBasic.Interaction.InputBox("Confirm Password", "Title", "Default Text");

            while(password != password2)
            {
                password2 = Microsoft.VisualBasic.Interaction.InputBox("Confirm Password", "Title", "Default Text");

            }


            //use input to create password


            return true;
        }


        //user access - noticing, generating, authenticating users -- will have to make from scratch to avoid using sql db
        public void AppBoot()
        {
            if (Xml())
            {
                //(1) match hashes in XML - if it doesn't match, close the app

                //ask for user input

                //compare inputs

                //if it works, close dialoge box. if it doesnt, prompt again

            }
            else
            {
                GenerateNewXML();
            }


        }
            
         

        //(2) 

        //key management - noticing, generating keys

        //spark application after first steps are done
    }
}
