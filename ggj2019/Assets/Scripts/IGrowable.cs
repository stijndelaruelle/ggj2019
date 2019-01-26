using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GrowDelegate(float newSize);

public interface IGrowable
{
    event GrowDelegate GrowEvent;

    float Size
    {
        get;
    }

    void Grow(float amount);
}
