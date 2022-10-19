using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeTheGame
{
    internal class BaseView
    {
        char snakePart = '#';
        char snakeHead = 'O';
        char yBoder = '|';
        char xBorder = '_';
        char emptySpace = ' ';
        char food = '@';

        internal void showPlayingField(char[,] playingField)
        {
            int playingFieldWidth = playingField.GetLength(0);
            int playingFieldHeight = playingField.GetLength(1);
            char[] line = new char[playingFieldWidth];

            for (int i = 0; i < playingFieldHeight; i++)
            {
                for (int j = 0; j < playingFieldWidth; j++)
                {
                    line[j] = playingField[j, i];
                }
                Console.WriteLine(line);
            }
            Console.SetCursorPosition(0, 0);
        }

        internal void clearConsole()
        {
            Console.SetCursorPosition(0, 0);
        }

        internal char getSnakePart()
        {
            return snakePart;
        }

        internal char getSnakeHead()
        {
            return snakeHead;
        }

        internal char getBorderX()
        {
            return yBoder;
        }

        internal char getBorderY()
        {
            return xBorder;
        }

        internal char getFood()
        {
            return food;
        }

        internal char getEmptySpace()
        {
            return emptySpace;
        }
    }
}
