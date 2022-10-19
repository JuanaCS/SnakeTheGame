using System.Collections.Generic;

namespace SnakeTheGame
{
    internal class Model
    {
        public char[,] PlayingField
        {
            get; set;
        }
        public int[] PosOfFood
        {
            get; set;
        }
        public List<int[]> PosOfSnakeParts
        {
            get; set;
        }

        public enum Directions
        {
            up, down, left, right
        }
    }
}