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






        public MainWindow()
        {
            InitializeComponent();
        }

        private void textEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        void OnKeyDown(object sender, RoutedEventArgs e)
        {
            var visualLines = textEditor.Text; 

            if (current_state)
            {
                //FIGURED IT OUT! THIS IS HOW I MODIFY/PULL TEXT ON SCREEN. YAAAY
                // Debug.WriteLine(visualLines);
                //textEditor.Text = "penis";
            }

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
        //        TextEditor.
               
            }


            else // toggle off
            {
               
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
