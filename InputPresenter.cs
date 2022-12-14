using static SnakeTheGame.Model;
using System.Collections.Generic;
using System;

namespace SnakeTheGame
{
    internal class InputPresenter
    {
        internal Model model;
        internal InputView view;

        float timeInSeconds = 0.09f;
        Model.Directions direction = Directions.down;

        //Model und View initialisieren
        public void setModel(Model model)
        {
            this.model = model;
        }

        public void setView(InputView view)
        {
            this.view = view;
        }

        //Feld behandeln
        public void initField()
        {
            direction = Directions.down;
            model.PlayingField = new char[61, 21];
            view.clearConsole();
            setEmptyField();
            setInitSnakePart();
            drawSnakeParts();
            setRandomFoodLocation();

            showField();
        }

        public void updateField()
        {

            System.Threading.Thread.Sleep((int)(timeInSeconds * 1000));
            view.clearConsole();
            setEmptyField();
            updateSnakeParts();
            setFood();
            showField();

        }

        public void setEmptyField()
        {
            char[,] tempField = model.PlayingField;
            int playingFieldWidth = model.PlayingField.GetLength(0);
            int playingFieldHeight = model.PlayingField.GetLength(1);

            for (int x = 0; x < playingFieldWidth; x++)
            {
                for (int y = 0; y < playingFieldHeight; y++)
                {
                    if (x == 0 || x == playingFieldWidth - 1)
                    {
                        tempField[x, y] = view.getBorderX();
                    }
                    else if (y == 0 || y == playingFieldHeight - 1)
                    {
                        tempField[x, y] = view.getBorderY();
                    }

                    else
                    {
                        tempField[x, y] = view.getEmptySpace();
                    }
                }
            }
            model.PlayingField = tempField;
        }

        private void showField()
        {
            view.showPlayingField(model.PlayingField);
        }

        //Schlange behandeln 
        private void setInitSnakePart()
        {
            int halfOfPlayingFieldWidth = model.PlayingField.GetLength(0);
            model.PosOfSnakeParts = new List<int[]>();

            model.PosOfSnakeParts.Add(new int[] { halfOfPlayingFieldWidth / 2, 1 });
        }

        private void drawSnakeParts()
        {
            int snakePartCount = model.PosOfSnakeParts.Count;

            for (int i = 0; i < snakePartCount; i++)
            {
                int snakePartXCoordAti = model.PosOfSnakeParts[i][0];
                int snakePartYCoordAti = model.PosOfSnakeParts[i][1];

                if (i == 0)
                {
                    model.PlayingField[snakePartXCoordAti, snakePartYCoordAti] = view.getSnakeHead();
                }
                else
                {
                    model.PlayingField[snakePartXCoordAti, snakePartYCoordAti] = view.getSnakePart();
                }
            }
        }

        private void updateSnakeParts()
        {
            moveSnakePart();
            checkCollision();
            drawSnakeParts();
        }

        private void moveSnakePart()
        {
            int snakeHeadXCoord = model.PosOfSnakeParts[0][0];
            int snakeHeadYCoord = model.PosOfSnakeParts[0][1];
            int[] tempPart = new int[] { };

            switch (direction)
            {
                case Model.Directions.up:
                    tempPart = new int[] { snakeHeadXCoord, snakeHeadYCoord - 1 };
                    break;
                case Model.Directions.down:
                    tempPart = new int[] { snakeHeadXCoord, snakeHeadYCoord + 1 };
                    break;
                case Model.Directions.left:
                    tempPart = new int[] { snakeHeadXCoord - 1, snakeHeadYCoord };
                    break;
                case Model.Directions.right:
                    tempPart = new int[] { snakeHeadXCoord + 1, snakeHeadYCoord };
                    break;

                default:
                    tempPart = new int[] { snakeHeadXCoord, snakeHeadYCoord + 1 };
                    break;
            }
            model.PosOfSnakeParts.Insert(0, tempPart);
        }

        //Collision behandeln
        private void checkCollision()
        {
            checkFoodCollision();

            if (onBorderCollision())
            {
                model.PosOfSnakeParts.Clear();
                view.showRanIntoWallGameoverScreen();
            }

            else if (onSnakeCollision())
            {
                model.PosOfSnakeParts.Clear();
                view.showAteItselfGameoverScreen();
            }
        }

        private void checkFoodCollision()
        {
            int snakeHeadXCoord = model.PosOfSnakeParts[0][0];
            int snakeHeadYCoord = model.PosOfSnakeParts[0][1];
            int lastSnakeElement = model.PosOfSnakeParts.Count - 1;
            int foodXCoord = model.PosOfFood[0];
            int foodYCoord = model.PosOfFood[1];

            if (foodXCoord == snakeHeadXCoord && foodYCoord == snakeHeadYCoord)
            {
                setRandomFoodLocation();
                speedUpGame();
            }

            else
            {
                if (model.PosOfSnakeParts.Count > 1)
                {
                    model.PosOfSnakeParts.RemoveAt(lastSnakeElement);
                }
            }
        }

        private bool onSnakeCollision()
        {
            int snakeHeadX = model.PosOfSnakeParts[0][0];
            int snakeHeadY = model.PosOfSnakeParts[0][1];
            bool collided = false;

            if (model.PosOfSnakeParts.Count > 1)
            {
                for (int i = 1; i < model.PosOfSnakeParts.Count - 1; i++)
                {
                    int snakePartXAti = model.PosOfSnakeParts[i][0];
                    int snakePartYAti = model.PosOfSnakeParts[i][1];

                    if (snakeHeadX == snakePartXAti && snakeHeadY == snakePartYAti)
                    {
                        collided = true;
                        break;
                    }
                }
            }
            return collided;
        }

        private bool onBorderCollision()
        {
            int snakeHeadX = model.PosOfSnakeParts[0][0];
            int snakeHeadY = model.PosOfSnakeParts[0][1];
            int playingFieldWidth = model.PlayingField.GetLength(0);
            int playingFieldHeight = model.PlayingField.GetLength(1);

            if (snakeHeadX == 0 || snakeHeadX == playingFieldWidth - 1 || snakeHeadY == 0 || snakeHeadY == playingFieldHeight - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void speedUpGame()
        {
            if (timeInSeconds - 0.025 > 0.05)
            {
                timeInSeconds -= 0.025f;
            }
        }

        //Food behandeln
        private void setFood()
        {
            int foodX = model.PosOfFood[0];
            int foodY = model.PosOfFood[1];

            model.PlayingField[foodX, foodY] = view.getFood();
        }

        private void setRandomFoodLocation()
        {
            Random random = new Random();
            int randomX = random.Next(1, model.PlayingField.GetLength(0) - 1);
            int randomY = random.Next(1, model.PlayingField.GetLength(1) - 1);

            model.PosOfFood = new int[] { randomX, randomY };
            model.PlayingField[randomX, randomY] = view.getFood();
        }

        //Input behandeln 
        public void handleInput(ConsoleKey key)
        {

            switch (key)
            {
                case ConsoleKey.W:
                    direction = Model.Directions.up;
                    break;
                case ConsoleKey.S:
                    direction = Model.Directions.down;
                    break;
                case ConsoleKey.A:
                    direction = Model.Directions.left;
                    break;
                case ConsoleKey.D:
                    direction = Model.Directions.right;
                    break;
                default:
                    direction = Model.Directions.down;
                    break;
            }
        }
    }
}