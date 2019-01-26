using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowLevelBorder : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_Player;

    [SerializeField]
    private float m_Radius;

    [SerializeField]
    private SpriteRenderer m_Visuals;

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

    private void OnPlayerMove(PlayerController player, Vector3 newPosition)
    {
        Vector3 diff = newPosition - transform.position;

        //Check if a player is trying to leave the level
        if (diff.magnitude >= m_Radius)
        {
            //The player doesn't have the right amount of power yet, bounce him back.
            Vector3 direction = diff.normalized;
            player.SetPosition(direction * m_Radius);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
