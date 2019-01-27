using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disco Dungball/Indoor Pickup Data")]
public class IndoorPickupData : PickupData
{
    [Header("Indoor")]
    [Space(5)]
    [SerializeField]
    private bool m_DealsDamage;
    public bool DealsDamage
    {
        get { return m_DealsDamage; }
    }
}
