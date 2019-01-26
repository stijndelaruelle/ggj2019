using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowLarvaeTail : MonoBehaviour
{
    [SerializeField]
    private float m_MinY;

    [SerializeField]
    private float m_MaxY;

    [SerializeField]
    private PlayerController m_Player;
    private Quaternion m_LastRotation = Quaternion.identity;

    private void Start()
    {
        if (m_Player != null)
        {
            m_Player.MoveEvent += OnPlayerMove;
            m_Player.GrowEvent += OnPlayerGrow;
        }
    }

    private void OnDestroy()
    {
        if (m_Player != null)
        {
            m_Player.MoveEvent -= OnPlayerMove;
            m_Player.GrowEvent -= OnPlayerGrow;
        }
    }

    private void OnPlayerMove(PlayerController player, Vector3 position, Quaternion rotation)
    {
        if (m_LastRotation == Quaternion.identity) { transform.rotation = rotation; }
        else                                       { transform.rotation = m_LastRotation; }

        m_LastRotation = rotation;
    }

    private void OnPlayerGrow(float newSize)
    {
        float t = Mathf.Clamp01(newSize - 1.0f);
        float lerpValue = Mathf.Lerp(m_MinY, m_MaxY, t);

        transform.DOLocalMoveY(lerpValue, 0.25f).SetEase(Ease.OutElastic);
    }
}
