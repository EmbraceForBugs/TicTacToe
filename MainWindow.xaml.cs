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

namespace Крестики_ноликии
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true;
        private int moveCount = 0;
        private string[,] board = new string[3, 3];
        private List<Button> playerXMoves = new List<Button>();
        private List<Button> playerOMoves = new List<Button>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content == null)
            {
                int row = Grid.GetRow(button);
                int column = Grid.GetColumn(button);

                board[row, column] = isPlayerXTurn ? "X" : "O";
                button.Content = isPlayerXTurn ? "X" : "O";

                if (isPlayerXTurn)
                {
                    playerXMoves.Add(button);
                    if (playerXMoves.Count == 3)
                    {
                        if (CheckForWinner())
                        {
                            MessageBox.Show("X wins!");
                            ResetGame();
                            return;
                        }
                        else
                        {
                            ClearFirstMove(playerXMoves);
                        }
                    }
                }
                else
                {
                    playerOMoves.Add(button);
                    if (playerOMoves.Count == 3)
                    {
                        if (CheckForWinner())
                        {
                            MessageBox.Show("O wins!");
                            ResetGame();
                            return;
                        }
                        else
                        {
                            ClearFirstMove(playerOMoves);
                        }
                    }
                }

                isPlayerXTurn = !isPlayerXTurn;
                moveCount++;
            }
        }
        private void ClearFirstMove(List<Button> playerMoves)
        {
            Button firstMove = playerMoves[0];
            int row = Grid.GetRow(firstMove);
            int column = Grid.GetColumn(firstMove);
            board[row, column] = null;
            firstMove.Content = null;
            playerMoves.RemoveAt(0);
        }

        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && !string.IsNullOrEmpty(board[i, 0]))
                {
                    return true;
                }

                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && !string.IsNullOrEmpty(board[0, i]))
                {
                    return true;
                }
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && !string.IsNullOrEmpty(board[0, 0]))
            {
                return true;
            }

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && !string.IsNullOrEmpty(board[0, 2]))
            {
                return true;
            }

            return false;
        }

        private void ResetGame()
        {
            Button0.Content = Button1.Content = Button2.Content =
            Button3.Content = Button4.Content = Button5.Content =
            Button6.Content = Button7.Content = Button8.Content = null;

            board = new string[3, 3];
            isPlayerXTurn = true;
            moveCount = 0;
            playerXMoves.Clear();
            playerOMoves.Clear();
        }
    }
}
