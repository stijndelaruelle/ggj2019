using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disco Dungball/Pickup Data")]
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
    private float m_PickupSize;
    public float PickupSize
    {
        get { return m_PickupSize; }
    }

    [SerializeField]
    private bool m_IsBadIndoors;
    public bool IsBadIndoors
    {
        get { return m_IsBadIndoors; }
    }

    [Header("Visuals")]
    [Space(5)]
    [SerializeField]
    private Sprite m_OutdoorSprite;
    public Sprite OutdoorSprite
    {
        get { return m_OutdoorSprite; }
    }

    [SerializeField]
    private Sprite m_IndoorSprite;
    public Sprite IndoorSprite
    {
        get { return m_IndoorSprite; }
    }
}
