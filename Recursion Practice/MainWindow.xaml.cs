using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Recursion_Practice {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        //Class-level variables
        private int[] arrayToReverse;


        /// <summary>
        /// Create Arrays and structures needed for methods when window is loaded
        /// </summary>
        private void wdwLoaded(object sender, RoutedEventArgs e) {
            createReverseArray();
        }


        /// <summary>
        /// Display a 1D array in a provided textbox
        /// </summary>
        /// <param name="array">a 1D array</param>
        /// <param name="textbox">the textbox to display in</param>
        private void dispayArray(int[] array, TextBox textbox) {

            textbox.Text = "";
            for (int i = 0; i < array.Length; i++ ) {
                textbox.Text += array[i] + " ";
            }
        }

        /// <summary>
        /// Display a 2D array in a provided textbox
        /// </summary>
        /// <param name="array">a 2D array</param>
        /// <param name="textbox">the textbox to display in</param>
        private void dispayArray(int[,] array, TextBlock textblock) {

            textblock.Text = "";
            for (int r = 0; r < array.GetLength(0); r++) {
                for (int c = 0; c < array.GetLength(1); c++) {
                    textblock.Text += array[r, c].ToString().PadLeft(3);
                }
                textblock.Text += "\r\n";
            }
        }

        #region Factorial
        /// <summary>
        /// button handler for factorial code
        /// tests for invalid entries and calls recursive function
        /// </summary>
        private void btnFactorial_Click(object sender, RoutedEventArgs e) {
            if (!int.TryParse(txtFactorialInput.Text, out int factNum))
                MessageBox.Show("Please enter an integer");
            else if (factNum < 0)
                MessageBox.Show("please enter a positive integer");

            else
                lblFactorial.Content = factorial(factNum).ToString();
        }

        /// <summary>
        /// recursive factorial method
        /// </summary>
        /// <param name="num">the number to compute from</param>
        /// <returns>the factorial (n*(n-1)*(n-2)...)</returns>
        private int factorial(int num) {

            //base case: have added all numbers or 0 was entered
            if (num < 1 || num==0)
                return 1;

            return num * factorial(num - 1);
        }
        #endregion Factorial

        #region Reverse Array
        /// <summary>
        /// button handler to reverse an array
        /// </summary>
        private void btnReverseArray_Click(object sender, RoutedEventArgs e) {
            reverseArray(arrayToReverse, 0);
            dispayArray(arrayToReverse, txtReversedArray);
        }

        /// <summary>
        /// reverseArray - a generic method
        /// </summary>
        /// <typeparam name="T">the type of variables in the array</typeparam>
        /// <param name="array">the array to reverse</param>
        /// <param name="index">the index to begin reversing at</param>
        private void reverseArray<T> (T[] array, int index) {

            //base case: half of the array has been flipped
            if (index >= array.Length / 2)
                return;

            //flip elements at index
            T temp = array[index];
            array[index] = array[array.Length - 1 - index];
            array[array.Length - 1 - index] = temp;

            //continue with remainder of array
            reverseArray(array, index + 1);
        }

        /// <summary>
        /// button handler if radio button for reversed array size is changed
        /// </summary>
        private void rbCheckedChanged(object sender, RoutedEventArgs e) {
            createReverseArray();
        }

        /// <summary>
        ///create a sorted array of sequential numbers needed for the reverse array section
        /// </summary>
        private void createReverseArray() {

            //size is determined by the radio buttons
            int size = rbReverse19.IsChecked == true ? 19 : 20;
            arrayToReverse = new int[size];
            for (int i = 0; i < arrayToReverse.Length; i++)
                arrayToReverse[i] = i;
            dispayArray(arrayToReverse, txtInitialArray);
            txtReversedArray.Text = "";
        }
        #endregion Reverse Array
    }
}
