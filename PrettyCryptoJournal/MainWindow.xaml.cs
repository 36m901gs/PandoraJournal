using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using System.Security.Cryptography;

namespace PrettyCryptoJournal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*Application PRoperties*/

        bool current_state;
        string text_store; //for holding text during camo mode






        public MainWindow()
        {

            // boot up functon
            var password = Microsoft.VisualBasic.Interaction.InputBox("Password", "Title", "Default Text");

            InitializeComponent();
        }

        private void textEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        public byte[] EncryptTest()
        {
            byte[] encrypted;
            //dependency injection gonna be valuable here!
            using (Aes myAes = Aes.Create())
            {
                var keytest = myAes.Key; //alright so the key is here, gonna assume the IV is there too. So, two questions (1) save it? [answer:yes] (2) no others!
                ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(textEditor.Text);

                            //save the key (use for decrypt test)

                            //(1)step one - convert key to string (success)

                            //(2)step two, save it and use it to decrypt
                            var testKey = Convert.ToBase64String(myAes.Key);

                            var testIv = Convert.ToBase64String(myAes.IV);
                            File.WriteAllBytes("C:\\Users\\njiso\\Desktop\\Key", myAes.Key);
                            File.WriteAllBytes("C:\\Users\\njiso\\Desktop\\IV", myAes.IV);
                            Console.WriteLine(testKey);
                            Console.WriteLine(testIv);
                        }

                    }
                    encrypted = msEncrypt.ToArray();

                }


            }

            return encrypted; // (1) this is the thing that needs to be saved

        }

        private void textEditor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var visualLines = textEditor.Text;

            if (current_state)
            {
                if (current_state)
                {
                    Asterisk_Dropper(e);
  
                }
            }
        }


        private void Asterisk_Dropper(System.Windows.Input.KeyEventArgs e)
        {
            //(ADD) -- need to add functionality to manually add backspace

            //(1) store last non hidden value in text_store
            text_store += GetCharFromKey(e.Key);   //10-25 NEED TO MODIFY THIS TO ACTUALLY PUT THE CHAR AND NOT THE EVENT
            //(2) change it to an asterisk and add last typed value on text
            textEditor.Text += "*";
            textEditor.Select(textEditor.Text.Length, 0);
        }
        string currentFileName;



        void openBtn_Click(object sender, RoutedEventArgs e)
        {

            //if file is bytes, decrypt. else, use below

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentFileName = dlg.FileName;
                textEditor.Load(currentFileName);
                textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(System.IO.Path.GetExtension(currentFileName));
            }

        }
        void EncrypTextFunction()
        {
            //generate key, IV. Save them to directory 
            // Encrypt text 
            // Open save dialog(?)
            // Save file




        }
        void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //(1)encrypts the text area, now in bytes. we need to drop this from memory though. need to save this
            var savefile = EncryptTest();
           // string filepath = "C:\\Users\\njiso\\Desktop\\"; 

            
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".txt";
                //(2) sets file name. we need a manual save method. write that up. put one here
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentFileName = dlg.FileName;
                   // filepath += currentFileName;
                    File.WriteAllBytes(currentFileName, savefile);
                    //DecryptFile()
                }
                else
                {
                    return;
                }
            

        }

        void KeyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void scrambleBtn_Click(object sender, RoutedEventArgs e)
        {
            stateMaker();

            if (current_state == true) // toggle on
            {

                textEditor.IsReadOnly = true;
                text_store = textEditor.Text; // store all current text before we initialize everything
                textEditor.Text = String.Concat(Enumerable.Repeat("*", (textEditor.Text.Length)));
                textEditor.Select(textEditor.Text.Length, 0);


            }


            else // toggle off
            {
                //restore text - BROKEN
                textEditor.Text = text_store;
                textEditor.Select(textEditor.Text.Length, 0);
                text_store = "";
                textEditor.IsReadOnly = false;



            }


        }



        private void stateMaker()
        {
            if (current_state == true)
            {
                current_state = false;
            }
            else
            {
                current_state = true;
            }

        }

        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
            }
            return ch;
        }


    }
}
