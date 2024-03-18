using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util{
    public static int GetRandomWeightedIndex(float[] weights)
    // code from lordofduct on unity forums
    {
        if(weights == null || weights.Length == 0) return -1;
    
        float total = 0;
        for(int i = 0; i < weights.Length; i++)
        {
            total += weights[i];
        }
    
        float r = Random.Range(0, total);
        float t = 0;
        for(int i = 0; i < weights.Length; i++)
        {
            t += (float)weights[i];
            if (t >= r) return i;
        }
    
        return -1;
    }
}