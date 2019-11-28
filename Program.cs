using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace THREE_IN_A_ROW
{
    class Program
    {
        static void NewLine()
        {
            Console.WriteLine("");
        }

        static void ShortDelay()
        {
            Thread.Sleep(300);
        }
        static void LongDelay()
        {
            Thread.Sleep(1600);
        }

        static void DisplayBoard(string[,] Board) //DISPLAY BOARD
        {
            NewLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Board[i, j] + "    ");
                }
                NewLine();
                NewLine();
            }
        }

        //CHECK IF SPACE IS OCCUPIED (IF MOVE IS VALID)
        static bool validationFree(ref string[,] Board, int input, bool playerTurn)
        {
            bool inputValid = true;
            int xCord;
            int yCord;

            //CALCULATE COORDINATES OF MATRIX 
            xCord = (input - 1) / 3;
            yCord = input - 3 * xCord - 1;

            if (Board[xCord, yCord] == "X" || Board[xCord, yCord] == "O")
            {
                return false;
            }

            //UPDATE WITH INPUTTED MOVE
            if (inputValid == true)
            {
                if (playerTurn == true)
                {
                    Board[xCord, yCord] = "X";
                }
                else
                {
                    Board[xCord, yCord] = "O";
                }
            }
            return true;
        }

        static void Main(string[] args)
        {

            string[,] Board;
            Board = new string[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } }; //INITIALISE BOARD

            bool isWinner = false;
            bool playerTurn = true;
            int input;
            string counter; //to check for draw

            Console.WriteLine("Welcome to Naughts and Crosses!" + '\n');

            while (isWinner == false) //IF WINNER IS DETECTED, GAME ENDS
            {

                if (playerTurn == true) //PLAYER'S TURN
                {
                    DisplayBoard(Board);
                    Console.Write("It is your turn. Enter a number: ");
                    input = int.Parse(Console.ReadLine());

                    //VALIDATION 
                    if (input < 1 || input > 9)
                    {
                        Console.WriteLine("Out of range!");
                        continue;
                    }
                    if (validationFree(ref Board, input, playerTurn) == false)
                    {
                        Console.WriteLine("Space occupied!");
                        continue;
                    }

                    DisplayBoard(Board);
                    playerTurn = false;
                }
                else //COMPUTER'S TURN
                {
                    Console.Write("It is the computer's turn");
                    ShortDelay();

                    Console.Write(".");
                    ShortDelay();
                    Console.Write(".");
                    ShortDelay();
                    Console.Write(".");
                    ShortDelay();
                    while (true)
                    {
                        Random rnd = new Random();
                        input = rnd.Next(1, 9);

                        //VALIDATION
                        if (validationFree(ref Board, input, playerTurn) == false)
                        {
                            continue;
                        }
                        playerTurn = true;
                        NewLine();
                        break;
                    }
                }

                //code to check for winning three in a row
                if ((Board[0, 0] == Board[0, 1] && Board[0, 1] == Board[0, 2] && Board[0, 2] == "X") || // horizontal
                    (Board[1, 0] == Board[1, 1] && Board[1, 1] == Board[1, 2] && Board[1, 2] == "X") ||
                    (Board[2, 0] == Board[2, 1] && Board[2, 1] == Board[2, 2] && Board[2, 2] == "X") ||

                    (Board[0, 0] == Board[1, 0] && Board[1, 0] == Board[2, 0] && Board[2, 0] == "X") || //vertical
                    (Board[0, 1] == Board[1, 1] && Board[1, 1] == Board[2, 1] && Board[2, 1] == "X") ||
                    (Board[0, 2] == Board[1, 2] && Board[1, 1] == Board[2, 2] && Board[2, 2] == "X") ||

                    (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[2, 2] == "X") || //diagonal
                    (Board[2, 0] == Board[1, 1] && Board[1, 1] == Board[0, 2] && Board[0, 2] == "X"))
                {
                    Console.WriteLine("YOU WON! ");
                    LongDelay();
                    Console.WriteLine("Well done!");
                    isWinner = true;
                }
                else if (
                    (Board[0, 0] == Board[0, 1] && Board[0, 1] == Board[0, 2] && Board[0, 2] == "O") || //horizontal
                    (Board[1, 0] == Board[1, 1] && Board[1, 1] == Board[1, 2] && Board[1, 2] == "O") ||
                    (Board[2, 0] == Board[2, 1] && Board[2, 1] == Board[2, 2] && Board[2, 2] == "O") ||

                    (Board[0, 0] == Board[1, 0] && Board[1, 0] == Board[2, 0] && Board[2, 0] == "O") || //vertical
                    (Board[0, 1] == Board[1, 1] && Board[1, 1] == Board[2, 1] && Board[2, 1] == "O") ||
                    (Board[0, 2] == Board[1, 2] && Board[1, 1] == Board[2, 2] && Board[2, 2] == "O") ||

                    (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[2, 2] == "O") || //diagonal
                    (Board[2, 0] == Board[1, 1] && Board[1, 1] == Board[0, 2] && Board[0, 2] == "O"))
                {
                    DisplayBoard(Board);

                    Console.WriteLine("THE COMPUTER HAS WON! ");
                    LongDelay();
                    Console.WriteLine("Unlucky, better luck next time.");
                    isWinner = true;
                }
                else
                {
                    isWinner = true;
                    counter = "1";
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Board[i, j] == counter)
                            {

                                isWinner = false;
                            }
                            int intCounter = int.Parse(counter);
                            intCounter++;
                            counter = intCounter.ToString();
                        }
                    }
                    if (isWinner)
                    {
                        Console.WriteLine("The game resulted in a draw!");
                    }
                }
            }
            Console.ReadLine(); //end
        }
    }
}