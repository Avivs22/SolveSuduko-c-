using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveSudoku
{
    public partial class Suduko : Form
    {
        TextBox[,] textsBoxs = new TextBox[9, 9];
        int startPointX = 150, startPointY = 150;
        public Suduko()
        {
            InitializeComponent();
            this.Width = 850;
            this.Height = 850;
        }
        
         /// <summary>
        /// Function Solve Board
        /// </summary>
        /// <param name="board"> '.' - Consider empty </param>
        /// <returns> true or false if its Work</returns>
        static bool SolveSudoku(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '.') // If the place is Empty
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c)) // Check If its Valid To Add Char
                            {
                                board[i, j] = c;
                                if (SolveSudoku(board))
                                {
                                    return true;
                                }
                                else // If the function run trough all loop and didnt find its set to '.' and change the previous
                                {
                                    board[i, j] = '.';
                                }
                            }
                        }
                        return false;
                    }

                }
            }
            return true;
        }
        static bool isValid(char[,] board, int row, int col, char ch)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                // Check Row
                if (board[i, col] != '.' && board[i, col] == ch)
                {
                    return false;
                }
                // Check column
                if (board[row, i] != '.' && board[row, i] == ch)
                {
                    return false;
                }
                //             startRow + i until lastBox   startCol +       i change every 3
                char num = board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3];
                // Check 3x3 box
                if (num != '.' && num == ch)
                {
                    return false;
                }
            }
            return true;
        }
        public void DesignBoard(int startPointX, int startPointY)
        {
            int gap = 20;
            int width = 50, height = 50;
            int posX = startPointX; int posY = startPointY;
            Label text = new Label();
            text.Text = "Solve Suduko";
            text.Location = new Point(width * 2 + startPointX, startPointY - 100);
            text.Font = new Font("Arial", 32);
            text.Size = new Size(300, 100);
            this.Controls.Add(text);
            for (int i = 0; i < textsBoxs.GetLength(0); i++)
            {
                posX = startPointX;
                for (int j = 0; j < textsBoxs.GetLength(1); j++)
                {
                    textsBoxs[i, j] = new TextBox();
                    textsBoxs[i, j].Location = new Point(posX, posY);
                    textsBoxs[i, j].Size = new Size(width, height);
                    textsBoxs[i, j].Text = ".";
                    textsBoxs[i, j].Font = new Font("Arial", 26);
                    this.Controls.Add(textsBoxs[i, j]);
                    if (j % 3 == 2)
                    {
                        posX += gap;
                    }
                    posX += width;
                }
                if (i % 3 == 2)
                {
                    posY += gap;
                }
                posY += height;
            }
            Button submitButton = new Button();
            submitButton.Location = new Point(posX / 2 + startPointX, posY);
            submitButton.Size = new Size(200, 100);
            submitButton.Text = "Submit Sudoku";
            submitButton.BackColor = Color.Gold;
            submitButton.Font = new Font("Arial", 24);
            submitButton.Click += SubmitButtonClick;
            this.Controls.Add(submitButton);
        }
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            char[,] board = new char[9, 9];
            for (int i = 0; i < textsBoxs.GetLength(0); i++)
            {
                for(int j = 0; j < textsBoxs.GetLength(1); j++)
                {
                    if(textsBoxs[i,j].Text.Length > 1)
                    {
                        MessageBox.Show("Cannot Over Length Of 1");
                        return;
                    }
                    else
                    {
                        board[i, j] = char.Parse(textsBoxs[i, j].Text);
                    }
                }
            }
            if (SolveSudoku(board))
            {
                for (int i = 0; i < textsBoxs.GetLength(0); i++)
                {
                    for (int j = 0; j < textsBoxs.GetLength(1); j++)
                    {
                        textsBoxs[i, j].Text = board[i, j].ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot Solve Suduko");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignBoard(startPointX, startPointY);
        }
    }
}
