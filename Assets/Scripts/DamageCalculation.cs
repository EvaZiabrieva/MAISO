using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculation
{
    public static float GetDamageScale(Element agressor, Element victim)
    {
        if ((int)agressor == ((int)victim + 1) % 4) 
            return 0.75f;
        if ((int)victim == ((int)agressor + 1) % 4)
            return 1.25f;
        else
            return 1f;
    }
}
