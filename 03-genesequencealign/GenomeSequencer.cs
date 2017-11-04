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

        private string alignmentA;
        private string alignmentB;

        private int[,] alignmentCost;
        private Location[,] previousMatrix;

        public GenomeSequencer(GeneSequence sequenceA, GeneSequence sequenceB)
        {
            this.sequenceA = sequenceA;
            this.sequenceB = sequenceB;
            //set up the sequencer arrays to be the size of the sequences to be used when calculating the cost of the sequences
            int alength = sequenceA.Sequence.Length;
            int blength = sequenceB.Sequence.Length;
            alignmentCost = new int[sequenceA.Sequence.Length, sequenceB.Sequence.Length];
            previousMatrix = new Location[sequenceA.Sequence.Length, sequenceB.Sequence.Length];
        }

        public void calculateSequenceCost(bool isBranded)
        {
            //initialize base cases, setting to 0
            for (int i = 0; i < alignmentCost.GetLength(0); i++) {
                alignmentCost[0, i] = i * 5;
                alignmentCost[i, 0] = i * 5;
            }
        }
    }
}
