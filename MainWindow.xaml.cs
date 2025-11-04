// Author: Muntadher Al-Bawi
// Created: September 17, 2025
// Description: This program is a Tic Tac Toe game that allows you to put your name, track the score, play the game, show current player, exit and reset.

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicTicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Set the default state of the game
        private bool IsXTurn = true;
        private int XWins = 0;
        private int OWins = 0;
        private int Ties = 0;

        /// <summary>
        /// This method has the update scores function and randomly selects a starting player.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Match the score text boxes with the global variables
            UpdateScoreDisplays();

            // Randomly select a starting player
            Random player = new Random();
            IsXTurn = player.Next(2) == 0;
            if (IsXTurn)
                CurrentPlayerDisplay.Text = "X";
            else
                CurrentPlayerDisplay.Text = "O";
        }

        /// <summary>
        /// Depending on what button is clicked in the event handlers, this function checks who's turn it is and sets the button to that players letter and switches turns.It also disables the clicked button and checks to see if the game has ended.
        /// </summary>
        /// <param name="button"></param>
        private void HandleMove(Button button)
        {
            // If the button is disabled, the code exits.
            if (!button.IsEnabled)
                return;

            if (IsXTurn)
            {
                button.Content = "X";
                CurrentPlayerDisplay.Text = "O";
            }
            else
            {
                button.Content = "O";
                CurrentPlayerDisplay.Text = "X";
            }

            // Disable the clicked button
            button.IsEnabled = false;

            // Check if the won
            bool GameEnded = CheckForWinner();

            // Winner keeps playing, so only switch turns if the game hasn't ended
            if (!GameEnded)
            {
                IsXTurn = !IsXTurn;
                if (IsXTurn)
                {
                    CurrentPlayerDisplay.Text = "X";
                }
                else
                {
                    CurrentPlayerDisplay.Text = "O";
                }
            }
        }

        /// <summary>
        /// This function checks for a winner by inputting all possible ways a player can win into the checkline function and if there is a tie, it shows an approriate message box and resets the game buttons
        /// </summary>
        /// <returns></returns>
        private bool CheckForWinner()
        {
            // Rows
            if (CheckLine(One, Two, Three)) return true;
            if (CheckLine(Four, Five, Six)) return true;
            if (CheckLine(Seven, Eight, Nine)) return true;

            // Columns
            if (CheckLine(One, Four, Seven)) return true;
            if (CheckLine(Two, Five, Eight)) return true;
            if (CheckLine(Three, Six, Nine)) return true;

            // Diagonals
            if (CheckLine(One, Five, Nine)) return true;
            if (CheckLine(Three, Five, Seven)) return true;

            // Tie
            if (IsBoardFull())
            {
                Ties++;
                TieScoreDisplay.Text = Ties.ToString();
                MessageBox.Show("It's a tie!");

                // Reset Buttons without changing the players turn
                ResetGameButtons();
                return true;
            }

            // If none of the above works, return false and the game continues.
            return false;
        }

        /// <summary>
        /// This function resets the game buttons to their default state and makes sure that the current player display matches the player turn.
        /// </summary>
        private void ResetGameButtons()
        {
            One.Content = ""; One.IsEnabled = true;
            Two.Content = ""; Two.IsEnabled = true;
            Three.Content = ""; Three.IsEnabled = true;
            Four.Content = ""; Four.IsEnabled = true;
            Five.Content = ""; Five.IsEnabled = true;
            Six.Content = ""; Six.IsEnabled = true;
            Seven.Content = ""; Seven.IsEnabled = true;
            Eight.Content = ""; Eight.IsEnabled = true;
            Nine.Content = ""; Nine.IsEnabled = true;

            // Make sure the current player display matches the current players turn
            if (IsXTurn)
            {
                CurrentPlayerDisplay.Text = "X";
            }
            else
            {
                CurrentPlayerDisplay.Text = "O";
            }
        }

        /// <summary>
        /// This function checks if the buttons have content in them and shows a winning message box if there is three of the same content in three buttons
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <returns></returns>
        private bool CheckLine(Button b1, Button b2, Button b3)
        {
            if (b1.Content != null && b1.Content.ToString() != "" && b1.Content == b2.Content && b2.Content == b3.Content)
            {
                string winner = b1.Content.ToString();
                if (winner == "X")
                {
                    XWins++;
                    XScoreDisplay.Text = XWins.ToString();
                    MessageBox.Show(XPlayerName.Text + " (X) Wins!"); 
                    IsXTurn = true; 
                }
                else
                {
                    OWins++;
                    OScoreDisplay.Text = OWins.ToString();
                    MessageBox.Show(OPlayerName.Text + " (O) Wins!");
                    IsXTurn = false;
                }

                ResetGameButtons();
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function checks if the game board is full
        /// </summary>
        /// <returns></returns>
        private bool IsBoardFull()
        {
            return One.Content != null && One.Content.ToString() != "" &&
                   Two.Content != null && Two.Content.ToString() != "" &&
                   Three.Content != null && Three.Content.ToString() != "" &&
                   Four.Content != null && Four.Content.ToString() != "" &&
                   Five.Content != null && Five.Content.ToString() != "" &&
                   Six.Content != null && Six.Content.ToString() != "" &&
                   Seven.Content != null && Seven.Content.ToString() != "" &&
                   Eight.Content != null && Eight.Content.ToString() != "" &&
                   Nine.Content != null && Nine.Content.ToString() != "";
        }

        /// <summary>
        /// This function turns the global variables into strings and makes them equal to the text boxes in the window
        /// </summary>
        private void UpdateScoreDisplays()
        {
            XScoreDisplay.Text = XWins.ToString();
            OScoreDisplay.Text = OWins.ToString();
            TieScoreDisplay.Text = Ties.ToString();
        }

        /// <summary>
        /// Resets everything in the window to it's default state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            XPlayerName.Clear();
            OPlayerName.Clear();

            // Reset all Tic Tac Toe buttons individually.
            One.Content = "";
            One.IsEnabled = true;

            Two.Content = "";
            Two.IsEnabled = true;

            Three.Content = "";
            Three.IsEnabled = true;

            Four.Content = "";
            Four.IsEnabled = true;

            Five.Content = "";
            Five.IsEnabled = true;

            Six.Content = "";
            Six.IsEnabled = true;

            Seven.Content = "";
            Seven.IsEnabled = true;

            Eight.Content = "";
            Eight.IsEnabled = true;

            Nine.Content = "";
            Nine.IsEnabled = true;

            // Randomly select a starting player
            Random player = new Random();
            IsXTurn = player.Next(2) == 0;
            if (IsXTurn)
                CurrentPlayerDisplay.Text = "X";
            else
                CurrentPlayerDisplay.Text = "O";

            // Reset scores.
            XWins = 0;
            OWins = 0;
            Ties = 0;
            XScoreDisplay.Text = "0";
            OScoreDisplay.Text = "0";
            TieScoreDisplay.Text = "0";

            XPlayerName.Focus();
        }

        /// <summary>
        /// Me Close Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(One);
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Two);
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Three);
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Four);
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Five);
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Six);
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Seven);
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Eight);
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            HandleMove(Nine);
        }
    }
}