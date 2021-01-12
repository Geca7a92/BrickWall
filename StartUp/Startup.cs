using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrickWallMain
{
    //Holds the logic of building second layer
    public class Startup
    {
        private int[,] firstLayer;
        private int[,] secondLayer;
        private Stack<Brick> usedBricks;
        private DataInput dataInput;
        private DataOutPut dataOutPut;

        public Startup()
        {
            this.dataInput = new DataInput();
            this.dataOutPut = new DataOutPut();
            this.usedBricks = new Stack<Brick>();
        }

        public void Run()
        {
            try
            {
                //Reads and saved inputed layer dimentions
                var dimention = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                //Creates and populates first layer
                this.firstLayer = this.dataInput.BuildInputBrickLayer(dimention);
                //Creates second layer layout
                this.secondLayer = this.dataInput.BuildBrickLayer(dimention);

                Loop();

                //Builded second layer
                var result = dataOutPut.Print(secondLayer);

                Console.WriteLine(result);
            }
            catch (Exception)
            {
                Console.WriteLine(-1);
            }
        }

        //Loops through first layer and selects current brick piece
        private void Loop()
        {
            for (int row = 0; row < firstLayer.GetLength(0); row++)
            {
                for (int col = 0; col < firstLayer.GetLength(1); col++)
                {
                    //Current brick Id(number)
                    var currBirckId = firstLayer[row, col];
                    Solve(currBirckId, row, col);
                }
            }
        }

        //Tries to find first fitting position for the brick in the second layer 
        private void Solve(int currBirckId, int row, int col)
        {
            if (!usedBricks.Any(b => b.Id == currBirckId))
            {
                //Current brick
                var brick = GetBrick(row, col);

                if (brick.IsVertical)
                {
                    brick.IsVertical = false;
                }
                if (!CanMoveRight(brick))
                {
                    brick.IsVertical = !brick.IsVertical;
                }
                //Determine if brick has been placed
                var placed = Move(brick);

                if (!placed)
                {
                    brick.IsVertical = !brick.IsVertical;
                    placed = Move(brick);
                }
                if (placed)
                {
                    usedBricks.Push(brick);
                }
                else
                {
                    RemoveLastBrick();
                }
            }
        }

        //Loops through the second layer looking for fitting position for the brick;
        private bool Move(Brick brick)
        {
            for (int row = 0; row < secondLayer.GetLength(0); row++)
            {
                for (int col = 0; col < secondLayer.GetLength(1); col++)
                {
                    if (brick.IsVertical)
                    {
                        if (secondLayer[row, col] == 0 && CheckFirstLayer(row, col, brick.IsVertical))
                        {
                            secondLayer[row, col] = brick.Id;
                            secondLayer[row + 1, col] = brick.Id;
                            return true;
                        }
                    }
                    else
                    {
                        if (secondLayer[row, col] == 0 && CheckFirstLayer(row, col, brick.IsVertical))
                        {
                            secondLayer[row, col] = brick.Id;
                            secondLayer[row, col + 1] = brick.Id;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Remove last used brick from second layer and from the stack
        private void RemoveLastBrick()
        {
            //Removes last brick and keeps temporary coppy
            var lastBrick = usedBricks.Pop();

            for (int row = 0; row < secondLayer.GetLength(0); row++)
            {
                for (int col = 0; col < secondLayer.GetLength(1); col++)
                {
                    if (secondLayer[row, col] == lastBrick.Id)
                    {
                        secondLayer[row, col] = 0;
                    }
                }
            }
        }

        //Checks first layer if a brick ocupates same position as current one
        private bool CheckFirstLayer(int row, int col, bool isVertical)
        {
            if (isVertical)
            {
                if (row % 2 != 0)
                {
                    return false;
                }
                if (firstLayer[row, col] == firstLayer[row + 1, col])
                {
                    return false;
                }
            }
            else
            {
                if (col == secondLayer.GetLength(1) - 1)
                {
                    return false;
                }
                if (firstLayer[row, col] == firstLayer[row, col + 1])
                {
                    return false;
                }
            }
            return true;
        }

        //Checks if the brick can be placed one step to the right
        private bool CanMoveRight(Brick brick)
        {
            //Length of the first layer
            var legth = firstLayer.GetLength(1);
            //Can the brick move
            bool canMove = false;

            if (!brick.IsVertical)
            {
                canMove = legth > brick.Col + 2;
            }

            return canMove;
        }

        //Returns brick model
        private Brick GetBrick(int currentRow, int currentCol)
        {
            for (int row = currentRow; row < firstLayer.GetLength(0); row++)
            {
                for (int col = currentCol; col < firstLayer.GetLength(1); col++)
                {
                    var currBrickId = firstLayer[row, col];
                    if (!usedBricks.Any(b => b.Id == currBrickId))
                    {
                        //Gets the orientation of the brick
                        var isVertical = IsVertical(currBrickId, row, col);

                        //Instance of a brick
                        Brick brick = new Brick()
                        {
                            Row = row,
                            Col = col,
                            IsVertical = isVertical,
                            Id = currBrickId,
                        };
                        return brick;
                    }
                }
            }
            return null;
        }

        //Checks current orientation of the brick
        private bool IsVertical(int currBrickId, int row, int col)
        {
            if (firstLayer.GetLength(1) > col + 1)
            {
                if (firstLayer[row, col + 1] == currBrickId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}