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

        private int[][] alignmentCost; //the way this is being setup, the first [] is the coloumn, and the second [] is the row
        private Location[][] previousMatrix;

        private void initializeAlignmentCost()
        {
            alignmentCost = new int[colSize][];
            for(int i = 0; i < alignmentCost.Length; i++)
            {
                alignmentCost[i] = new int[rowSize];
            }
        }

        private void initializePreviousMatrix()
        {
            previousMatrix = new Location[colSize][];
            for(int i = 0; i < previousMatrix.Length; i++)
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

            rowSize = Math.Min(maxSize, sequenceB.Sequence.Length);
            colSize = Math.Min(maxSize, sequenceA.Sequence.Length);

            //set up the sequencer arrays to be the size of the sequences to be used when calculating the cost of the sequences
            int alength = sequenceA.Sequence.Length;
            int blength = sequenceB.Sequence.Length;// used for debugging
            initializeAlignmentCost();
            initializePreviousMatrix();
        }

        public void calculateSequenceCost(bool isBranded)
        {
            //initialize base cases, setting to 0
            //set first row to be 0
            for (int i = 0; i < alignmentCost.Length; i++) {
                alignmentCost[i][0] = i * 5;
            }
            //set first coloumn to be 0
            for(int i = 0; i <alignmentCost[0].Length; i++)
            {
                alignmentCost[0][i] = i * 5;
            }
        }
    }
}
