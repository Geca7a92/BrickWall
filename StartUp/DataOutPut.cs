using System;
using System.Text;

namespace BrickWallMain
{
    //Takes care of the data output
    public class DataOutPut
    {
        public char dash = '-';
        public char space = ' ';

        public DataOutPut()
        {
            this.BrickBuilder = new StringBuilder();
            this.BrickLineBuilder = new StringBuilder();
        }

        public StringBuilder BrickBuilder { get; set; }
        public StringBuilder BrickLineBuilder { get; set; }

        //Builds output and returns string
        public string Print(int[,] brickLayer)
        {
            this.BrickBuilder.Append(new String(dash, brickLayer.GetLength(1) * 2 + 1));
            this.BrickBuilder.Append(Environment.NewLine);
            for (int row = 0; row < brickLayer.GetLength(0); row++)
            {
                this.BrickBuilder.Append(this.dash);
                for (int col = 0; col < brickLayer.GetLength(1); col++)
                {
                    var currentBrickId = brickLayer[row, col];
                    var charCount = 1;

                    if (currentBrickId >= 10)
                    {
                        charCount = 2;
                    }

                    if (col == brickLayer.GetLength(1) - 1)
                    {
                        this.BrickBuilder.Append(string.Format("{0}", currentBrickId));
                    }
                    else if (currentBrickId == brickLayer[row, col + 1])
                    {
                        this.BrickBuilder.Append(string.Format("{0} ", currentBrickId));

                    }
                    else
                    {
                        this.BrickBuilder.Append(string.Format("{0}-", currentBrickId));
                    }

                    if (row < brickLayer.GetLength(0) - 1)
                    {
                        if (currentBrickId >= 10)
                        {
                            charCount = 2;
                        }
                        if (col == 0)
                        {
                            BrickLineBuilder.Append(this.dash);
                        }
                        if (currentBrickId == brickLayer[row + 1, col])
                        {
                            BrickLineBuilder.Append(new string(this.space, charCount));
                        }
                        else
                        {
                            BrickLineBuilder.Append(new string(this.dash, charCount));
                        }
                        BrickLineBuilder.Append(this.dash);
                    }
                }
                this.BrickBuilder.Append(this.dash);
                this.BrickBuilder.Append(Environment.NewLine);
                if (!string.IsNullOrEmpty(BrickLineBuilder.ToString()))
                {
                    this.BrickBuilder.Append(BrickLineBuilder);
                    this.BrickBuilder.Append(Environment.NewLine);
                    BrickLineBuilder.Clear();
                }
            }
            this.BrickBuilder.Append(new String(dash, brickLayer.GetLength(1) * 2 + 1));

            return this.BrickBuilder.ToString().TrimEnd();
        }
    }
}
