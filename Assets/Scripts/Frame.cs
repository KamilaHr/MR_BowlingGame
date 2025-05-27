using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame
{
    public int firstThrow = -1; 
    public int secondThrow = -1; 
    public int thirdThrow = -1; // Only used in 10th frame
    public bool isStrike => firstThrow == 10; 
    public bool isSpare => !isStrike && firstThrow + secondThrow == 10; 
    public bool isComplete => (isStrike || secondThrow != -1) && !isTenthFrameWithBonus;

    private bool isTenthFrameWithBonus => isTenthFrame && (isStrike || isSpare) && thirdThrow == -1;

    public bool isTenthFrame = false;

    public int GetScore(Frame next1, Frame next2)
    {
        if (firstThrow == -1) return 0;

        if (isTenthFrame)
        {
            return firstThrow + (secondThrow != -1 ? secondThrow : 0) + (thirdThrow != -1 ? thirdThrow : 0);
        }

        if (isStrike)
        {
            if (next1 == null) return 0;
            if (next1.isStrike)
            {
                if (next2 != null && next2.firstThrow != -1)
                    return 10 + 10 + next2.firstThrow;
                else
                    return 0;
            }
            else if (next1.firstThrow != -1 && next1.secondThrow != -1)
            {
                return 10 + next1.firstThrow + next1.secondThrow;
            }
            return 0;
        }

        if (isSpare)
        {
            if (next1 != null && next1.firstThrow != -1)
                return 10 + next1.firstThrow;
            else
                return 0;
        }

        return firstThrow + (secondThrow != -1 ? secondThrow : 0);
    }
}

