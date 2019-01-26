using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForwarder : MonoBehaviour
{
    [SerializeField]
    private PlayerWorm m_PlayerWorm;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        //m_PlayerWorm.OnCollisionEnter2D(collision2D);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        //m_PlayerWorm.OnTriggerEnter2D(collider2D);
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        //m_PlayerWorm.OnTriggerExit2D(collider2D);
    }
}
