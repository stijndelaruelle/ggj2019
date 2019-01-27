using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupData : ScriptableObject
{
    [Header("Settings")]
    [Space(5)]
    [SerializeField]
    private float m_GrowAmount;
    public float GrowAmount
    {
        get { return m_GrowAmount; }
    }

    [SerializeField]
    private float m_MoveSpeedAmount;
    public float MoveSpeedAmount
    {
        get { return m_MoveSpeedAmount; }
    }
    
    [Header("Visuals")]
    [Space(5)]
    [SerializeField]
    private Sprite m_Sprite;
    public Sprite Sprite
    {
        get { return m_Sprite; }
    }
}
