using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MajorityVotingBuffer
{
    private int bufferSize = 1;
    private GraspTypes[] majorityVoteBuffer;
    private int bufferCounter;

    public MajorityVotingBuffer(int size)
    {
        bufferSize = size;
        majorityVoteBuffer = Enumerable.Repeat(GraspTypes.Rest, bufferSize).ToArray();
    }

    public GraspTypes HandleNewPrediction(GraspTypes graspPrediction)
    {
        majorityVoteBuffer[bufferCounter] = graspPrediction;

        GraspTypes majorityPrediction = majorityVoteBuffer
            .GroupBy(i => i) //place all identical values into groups
            .OrderByDescending(g => g.Count()) //order groups by the size of the group desc
            .Select(g => g.Key) //key of the group is representative of items in the group
            .First(); //first in the list is the most frequent (modal) value

        bufferCounter = (bufferCounter + 1) % bufferSize;

        return majorityPrediction;
    }
}
