using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FloatRange
{
    public float Min;
    public float Max;
    public float Weight;

    public FloatRange(float min, float max, float weight)
    {
        Min = min;
        Max = max;
        Weight = weight;
    }
}

public static class RandomRange
{
    public static float Range(params FloatRange[] ranges)
    {
        if (ranges.Length == 0) { throw new System.ArgumentException("need more arguments"); }
        if (ranges.Length == 1) { return Random.Range(ranges[0].Max, ranges[0].Min); }
        float total = 0f;

        for (int i = 0; i < ranges.Length; i++) { total += ranges[i].Weight; }

        float randomNumber = Random.value;
        float s = 0;
        int count = ranges.Length - 1;
        for (int i = 0; i < count; i++)
        {
            s += ranges[i].Weight / total;
            // choose wheather or not to pick this range
            if (s > randomNumber)
            {
                // return a random number from that range
                return Random.Range(ranges[i].Max, ranges[i].Min);
            }
        }

        return Random.Range(ranges[count].Max, ranges[count].Min);


    }
}
