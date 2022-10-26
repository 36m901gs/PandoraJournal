using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using ICSharpCode.AvalonEdit.Rendering;

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
            InitializeComponent();
        }

        private void textEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textEditor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var visualLines = textEditor.Text;

            if (current_state)
            {

                if (current_state)
                {

                    Asterisk_Dropper(e);

                    /*
                    if (e != (char)Keys.Back)
                    {

                        Asterisk_Dropper(e);

                    }*/
                    //FIGURED IT OUT! THIS IS HOW I MODIFY/PULL TEXT ON SCREEN. YAAAY
                    // Debug.WriteLine(visualLines);
                    //textEditor.Text = "penis";

                    /*
                    else
                    {
                        textEditor.Text = textEditor.Text.Substring(0, textEditor.Text.Length - 1);
                        text_store = text_store.Substring(0, text_store.Length - 1);
                        textEditor.Select(textEditor.Text.Length, 0);
                    } */
                }






                /*
                if (((char)e.Key) != (char)Keys.Back)
                {
                    var test = e.Key; //This works!

                    //  Asterisk_Dropper(e);
                    Debug.WriteLine(test);

                }
                //FIGURED IT OUT! THIS IS HOW I MODIFY/PULL TEXT ON SCREEN. YAAAY
                // Debug.WriteLine(visualLines);
                //textEditor.Text = "penis";
                else
                {
                    textEditor.Text = textEditor.Text.Substring(0, textEditor.Text.Length - 1);
                    text_store = text_store.Substring(0, text_store.Length - 1);
                    textEditor.Select(textEditor.Text.Length, 0);
                }*/


            }

        }

        /* void OnKeyDown(object sender, KeyPressEventArgs e)
        {
            var visualLines = textEditor.Text; 

            if (current_state)
            {

                
                if (e.KeyChar != (char)Keys.Back)
                {

                    Asterisk_Dropper(e);

                }
                //FIGURED IT OUT! THIS IS HOW I MODIFY/PULL TEXT ON SCREEN. YAAAY
                // Debug.WriteLine(visualLines);
                //textEditor.Text = "penis";
                else
                {
                    textEditor.Text = textEditor.Text.Substring(0, textEditor.Text.Length - 1);
                    text_store = text_store.Substring(0, text_store.Length - 1);
                    textEditor.Select(textEditor.Text.Length, 0);
                }
            }

        } */

        private void Asterisk_Dropper(System.Windows.Input.KeyEventArgs e)
        {
            //(ADD) -- need to add functionality to manually add backspace

            //(1) store last non hidden value in text_store
            text_store += e.SystemKey;   //10-25 NEED TO MODIFY THIS TO ACTUALLY PUT THE CHAR AND NOT THE EVENT
            //(2) change it to an asterisk and add last typed value on text
            textEditor.Text += "*";
            textEditor.Select(textEditor.Text.Length, 0);
        }

       

        void openBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        void saveBtn_Click(object sender, RoutedEventArgs e)
        {

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

       
    }
}
