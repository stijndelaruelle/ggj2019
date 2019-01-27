using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disco Dungball/Player Save Data")]
public class PlayerSaveData : ScriptableObject
{
    [SerializeField]
    private float m_MoveSpeed = 5.0f;
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
        set { m_MoveSpeed = value; }
    }
}
