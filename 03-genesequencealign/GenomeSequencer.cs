using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticsLab
{
    class GenomeSequencer
    {

        private enum Location { UP, LEFT, DIAGONAL }; //this is used for the previous matrix to know where the value came from
        private GeneSequence sequenceA;
        private GeneSequence sequenceB;

        private int rowSize;
        private int colSize;

        private string alignmentA;
        private string alignmentB;

        private int[][] alignmentCost; //the way this is being setup, the first [] is the coloumn, and the second [] is the row, alignmentCost[col][row]
        private Location[][] previousMatrix;

        private void initializeAlignmentCost()
        {
            alignmentCost = new int[colSize][];
            for (int i = 0; i < alignmentCost.Length; i++)
            {
                alignmentCost[i] = new int[rowSize];
            }
        }

        private void initializePreviousMatrix()
        {
            previousMatrix = new Location[colSize][];
            for (int i = 0; i < previousMatrix.Length; i++)
            {
                previousMatrix[i] = new Location[rowSize];
            }

        }

        public GenomeSequencer(GeneSequence sequenceA, GeneSequence sequenceB, int maxSize)
        {
            //sequence A will the the "top" word, so it will be the columns
            //sequence B will be the "side" word, so it will be the rows
            this.sequenceA = sequenceA;
            this.sequenceB = sequenceB;

            rowSize = Math.Min(maxSize, sequenceB.Sequence.Length) + 1; //add one to add base case row and column
            colSize = Math.Min(maxSize, sequenceA.Sequence.Length) + 1;

            //set up the sequencer arrays to be the size of the sequences to be used when calculating the cost of the sequences
            int alength = sequenceA.Sequence.Length;
            int blength = sequenceB.Sequence.Length;// used for debugging
            initializeAlignmentCost();
            initializePreviousMatrix();
        }

        public int calculateSequenceCost(bool isBanded)
        {
            //initialize base cases, setting to 0
            //set first row to be 0
            for (int i = 0; i < alignmentCost.Length; i++) {
                alignmentCost[i][0] = i * 5;
            }
            //set first coloumn to be 0
            for (int i = 0; i < alignmentCost[0].Length; i++)
            {
                alignmentCost[0][i] = i * 5;
            }

            //start top and work my way down, so I fill in every column in a row, then move to the next row
            for (int i = 1; i < alignmentCost[0].Length; i++ ) // loop through each row
            {
                for (int j = 1; j < alignmentCost.Length; j++)
                {
                    setCost(i, j);
                }
            }

            return alignmentCost[colSize-1][rowSize-1];
        }

        private void setCost(int row, int col)
        {
            int topIndelCost = alignmentCost[col][row - 1] + 5;
            int leftIndelCost = alignmentCost[col - 1][row] + 5;
            int diagonalCost = alignmentCost[col-1][row - 1];
            if (sequenceA.Sequence[col -1] == sequenceB.Sequence[row - 1])
            {
                diagonalCost -= 3;
            }
            else
            {
                diagonalCost += 1;
            }

            //compare top, left, and diagonal for the least
            int lowestCost = Math.Min(topIndelCost, leftIndelCost); 
            lowestCost = Math.Min(lowestCost, diagonalCost);

            //set the previous matrix so we can rebuild the string
            if (lowestCost == topIndelCost)
            {
                previousMatrix[col][row] = Location.UP;
            }
            else if (lowestCost == leftIndelCost)
            {
                previousMatrix[col][row] = Location.LEFT;
            }
            else
            {
                previousMatrix[col][row] = Location.DIAGONAL;
            }
            alignmentCost[col][row] = lowestCost;
        }

        private string reconstructSequenceB()
        {
            return "";
        }

        private string reconstructSequenceA()
        {
            return "";
        }
    }
}
