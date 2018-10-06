using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Recursion_Practice {
    /// <summary>
    /// Practice creating recursive methods
    /// M. Moody 10/2018
    /// Interaction logic for MainWindow.xaml
    /// 
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        //Class-level variables
        private int[] sortReverseArray;


        /// <summary>
        /// Create Arrays and structures needed for methods when window is loaded
        /// </summary>
        private void wdwLoaded(object sender, RoutedEventArgs e) {
            rbSortRandom.IsChecked = false;
            rbSortSequential.IsChecked = true;
            rbSize17.IsChecked = true;
            rbSize18.IsChecked = false;
            createSortReverseArray();
        }


        /// <summary>
        /// Display a 1D array in a provided textbox
        /// </summary>
        /// <param name="array">a 1D array</param>
        /// <param name="textbox">the textbox to display in</param>
        private void dispayArray(int[] array, TextBox textbox) {

            textbox.Text = "";
            for (int i = 0; i < array.Length; i++) {
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
            if (num < 1 || num == 0)
                return 1;

            return num * factorial(num - 1);
        }
        #endregion Factorial

        #region Reverse Array
        /// <summary>
        /// button handler to reverse an array
        /// </summary>
        private void btnReverseArray_Click(object sender, RoutedEventArgs e) {
            reverseArray(sortReverseArray, 0);
            dispayArray(sortReverseArray, txtReversedArray);
        }

        /// <summary>
        /// reverseArray - a generic method
        /// </summary>
        /// <typeparam name="T">the type of variables in the array</typeparam>
        /// <param name="array">the array to reverse</param>
        /// <param name="index">the index to begin reversing at</param>
        private void reverseArray<T>(T[] array, int index) {

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
        #endregion Reverse Array

        /// <summary>
        /// button handler if radio button for reversed array size is changed
        /// </summary>
        private void rbArrayChanged(object sender, RoutedEventArgs e) {
            createSortReverseArray();
        }

        /// <summary>
        ///create a sequential or random array of numbers needed for the reverse and quick sort array section
        /// </summary>
        private void createSortReverseArray() {

            //size and sequence are determined by the radio buttons
            int size = rbSize17.IsChecked == true ? 17 : 18;
            bool random = rbSortRandom.IsChecked == true;
            sortReverseArray = new int[size];

            if (!random) {
                for (int i = 0; i < sortReverseArray.Length; i++)
                    sortReverseArray[i] = i;
            }
            else {
                Random rnd = new Random();
                for (int i = 0; i < sortReverseArray.Length; i++)
                    sortReverseArray[i] = rnd.Next(0, 40);
            }
            dispayArray(sortReverseArray, txtInitialArray);
            txtReversedArray.Text = "";
        }

        #region Quick Sort

        /// <summary>
        /// button handler for Quick Sort button
        /// Calls the quickSort Method with the entire array
        /// </summary>
        private void btnSortArray_Click(object sender, RoutedEventArgs e) {
            quickSort(sortReverseArray, 0, sortReverseArray.Length - 1);
            dispayArray(sortReverseArray, txtReversedArray);
        }

        /// <summary>
        /// Recursive Quick Sort method
        /// Sorts an array by 
        /// 1. Selects a pivot value
        /// 2. places all numbers less than the pivot to the left of the pivot and all numbers
        ///     greater than the pivot to its right
        /// 3. Recursively calls the quickSort again with each half of the array until the sort is complete
        /// </summary>
        /// <param name="array">the entire array to sort</param>
        /// <param name="left">the left index of the section to sort</param>
        /// <param name="right">the right index of the section to sort</param>
        private void quickSort(int[] array, int left, int right) {

            //base case: section has been sorted
            if (left >= right)
                return;

            //the pivot is chosen as the middle number in the array and placed in the leftmost index
            //This will speed up the sorting if the array is pre - sorted
            int middleIndex = (right-left) / 2 + left;
            int pivot = array[middleIndex];
            array[middleIndex] = array[left];
            array[left] = pivot;

            //lastSmall indicates the index of the last number known to be smaller than the pivot
            int lastSmall = left;

            int temp;

            //look for any numbers smaller than pivot and stack them at the front of the array
            for (int i = left + 1; i <= right; i++) {
                if (array[i] < pivot) {
                    lastSmall++;
                    temp = array[i];
                    array[i] = array[lastSmall];
                    array[lastSmall] = temp;
                }
            }

            //put pivot in the correct place
            temp = array[lastSmall];
            array[lastSmall] = pivot;
            array[left] = temp;

            //recursively call the quickSort again with the left and right halves of the array;
            quickSort(array, left, lastSmall - 1);
            quickSort(array, lastSmall + 1, right);
        }
        #endregion  Quick Sort

        #region All Letters

        /// <summary>
        /// button handler for testing all letters
        /// </summary>
        private void btnAllLetters_Click(object sender, RoutedEventArgs e) {
            bool answer = allLettersIn2ndWord(txtLetters1.Text, txtLetters2.Text);
            lblAllLettersAnswer.Content = answer ? "Yes" : "No";
        }

        /// <summary>
        /// Recursive allLettersIn2ndWord
        /// Determines if every letter from the first word can be found in the second word
        /// </summary>
        /// <param name="word1">1st word (provides letters)</param>
        /// <param name="word2">2nd word (searched for letters)</param>
        /// <returns>true if every letter from 1st word is found in 2nd word</returns>
        private bool allLettersIn2ndWord(string word1, string word2) {

            //base case #1: Every letter in word 1 has been tested
            if (word1.Length <= 0)
                return true;

            //base case #2: A letter was found that was not in word 2
            if (!isLetterInWord(word1[0], word2))
                return false;

            //recursively search the rest of the string
            return allLettersIn2ndWord(word1.Substring(1), word2);

        }

        /// <summary>
        /// isLetterInWord
        /// Determine if a particular character is in a word
        /// </summary>
        /// <param name="letter">the char to check for</param>
        /// <param name="word">the word to search</param>
        /// <returns>true if found</returns>
        private bool isLetterInWord(char letter, string word) {

            //string is empty
            if (word.Length <= 0)
                return false;
            //letter is found
            if (word.IndexOf(letter) >= 0)
                return true;
            //letter is not found
            return false;
        }
        #endregion All Letters

        #region Sierpinski
        //Button handler for Sierpinski Triangle
        //Reads controls and calls the wrapper function with side length of 300
        private void btnSierpinski_Click(object sender, RoutedEventArgs e) {

            cvSierpinski.Children.Clear();

            int order = (int)sldSierpinski.Value;
            siepinskiWrapper(order, 300);
        }

        /// <summary>
        /// Wrapper for recursive method
        /// </summary>
        /// <param name="order">the order to create</param>
        /// <param name="sidelen">the original side length</param>
        private void siepinskiWrapper(int order, double sidelen) {

            //Sierpinski of order 0 is just a triangle
            if (rbSierpinski.IsChecked == true) {
                if (order == 0)
                    drawTriangle(sidelen, 0, 300);
                else {
                    //double sierSideLen = sidelen / (2 * order);
                    recursiveSubDivideTriangle(sidelen, 0, 300, order);
                }
            }
            else {
                //Divided triangle
                double divSideLength = sidelen / (order + 1);
                recursiveMakeDivided(divSideLength, 0, 300, order);
            }
        }

        /// <summary>
        /// Recursively draws a sierpinski triangle
        /// Takes each triangle and divides it into 3 smaller traingles
        /// calls the same function with each of the smaller triangles until order 0 is reached
        /// </summary>
        /// <param name="sideLen">The length of each side</param>
        /// <param name="LLx">lower left x coordinate</param>
        /// <param name="LLy">lower left y coordinate</param>
        /// <param name="order">the order of this particular triangle</param>
        private void recursiveSubDivideTriangle(double sideLen, double LLx, double LLy, int order) {

            //Base Case: Order 0 has been reached, draw the triangle
            if (order == 0) {
                drawTriangle(sideLen, LLx, LLy);
                return;
            }

            sideLen = sideLen / 2;
            int leftOrder = order, rightOrder = order, topOrder = order;
            double height = Math.Sqrt(sideLen * sideLen - sideLen / 2 * sideLen / 2);

            //Break into 3 smaller triangles and subdivide them
            recursiveSubDivideTriangle(sideLen, LLx, LLy, --leftOrder);
            recursiveSubDivideTriangle(sideLen, LLx + sideLen, LLy, --rightOrder);
            recursiveSubDivideTriangle(sideLen, LLx + sideLen / 2, LLy - height, --topOrder);
        }

        /// <summary>
        /// Recursively draws a divided triangle
        /// Creates a triangle from repeated smaller triangles
        /// </summary>
        /// <param name="sideLen">The length of each side</param>
        /// <param name="LLx">lower left x coordinate</param>
        /// <param name="LLy">lower left y coordinate</param>
        /// <param name="order">the order of this particular triangle</param>
        private void recursiveMakeDivided(double sideLen, double LLx, double LLy, int order) {
            if (order < 0)
                return;

            int rightOrder = order, topOrder = order;

            //draw triangle
            double height =drawTriangle(sideLen, LLx, LLy);


            //draw right triangle
            recursiveMakeDivided(sideLen, LLx + sideLen, LLy, --rightOrder);

            //draw top triangle
            recursiveMakeDivided(sideLen, LLx + sideLen / 2, LLy - height, --topOrder);
        }

        //
        /// <summary>
        /// Actually draws the triangle to the canvas using a Polygon class
        /// </summary>
        /// <param name="sideLen">the length of each side</param>
        /// <param name="LLx">lower left x coordinate</param>
        /// <param name="LLy">lower left y coordinate</param>
        /// <returns>the height of the triangle so it does not have to be computed later</returns>
        private double drawTriangle(double sideLen, double LLx, double LLy) { 

            Polygon triangle = new Polygon();

            triangle.Stroke = Brushes.DarkCyan;
            triangle.StrokeThickness = 1;

            double height = Math.Sqrt(sideLen * sideLen - sideLen / 2 * sideLen / 2);
            triangle.Points = new PointCollection() { new Point(LLx,LLy), new Point(LLx + sideLen,LLy), new Point(LLx + sideLen/2, LLy - height) };

            cvSierpinski.Children.Add(triangle);
            return height;
        }
        #endregion Sierpinski
    }
}
