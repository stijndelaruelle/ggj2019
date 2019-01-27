using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecalPlacer : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_Player;

    [SerializeField]
    private SimpleDecalPool m_DecalPool;

    private void Start()
    {
        if (m_Player != null)
            m_Player.MoveEvent += OnPlayerMove;
    }

    private void OnDestroy()
    {
        if (m_Player != null)
            m_Player.MoveEvent -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerController player, Vector3 newPosition, Quaternion newRotation)
    {
        //Place decals along this axis
        if (m_DecalPool != null)
            m_DecalPool.PlaceDecal(new Vector3(transform.position.x, transform.position.y, 0.0f));
    }
}
