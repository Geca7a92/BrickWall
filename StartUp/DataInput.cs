using System;
using System.Linq;

namespace BrickWallMain
{
    //Takes care of data input
    public class DataInput
    {
        //Reads dimentions for the brick layers
        public int[] ReadInputDimention()
        {
            //Reads input for the dimentions from console
            var dimentions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            return dimentions;
        }

        //Populates first brick layer
        public int[,] BuildInputBrickLayer(int[] dimentions)
        {
            //Rows count
            var rows = dimentions[0];
            //Cols count
            var cols = dimentions[1];
            //Empty layer layout
            var layer = BuildBrickLayer(dimentions);

            for (int row = 0; row < rows; row++)
            {
                //Reads row of input for the layer
                var rowInputArr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                for (int col = 0; col < cols; col++)
                {
                    layer[row, col] = rowInputArr[col];
                }
            }
            return layer;
        }

        //Creates empty layer layout
        public int[,] BuildBrickLayer(int[] dimentions)
        {
            //Rows count
            var rows = dimentions[0];
            //Cols count
            var cols = dimentions[1];
            return new int[rows, cols];
        }
    }
}
