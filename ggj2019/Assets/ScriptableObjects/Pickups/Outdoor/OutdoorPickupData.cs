using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disco Dungball/Outdoor Pickup Data")]
public class OutdoorPickupData : PickupData
{
    [Header("Outdoor")]
    [Space(5)]
    [SerializeField]
    private float m_PickupSize;
    public float PickupSize
    {
        get { return m_PickupSize; }
    }

    [SerializeField]
    private IndoorPickupData m_IndoorPickupData;
    public IndoorPickupData IndoorPickupData
    {
        get { return m_IndoorPickupData; }
    }
}

