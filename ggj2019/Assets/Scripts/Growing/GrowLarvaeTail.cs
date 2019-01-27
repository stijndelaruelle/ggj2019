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

    private void Start()
    {
        if (LevelDirector.Instance != null)
        {
            LevelDirector.Instance.LevelStartEvent += OnLevelStart;
        }

        if (m_Player != null)
        {
            m_Player.MoveEvent += OnPlayerMove;
            m_Player.GrowEvent += OnPlayerGrow;
        }
    }

    private void OnDestroy()
    {
        if (LevelDirector.Instance != null)
        {
            LevelDirector.Instance.LevelStartEvent -= OnLevelStart;
        }

        if (m_Player != null)
        {
            m_Player.MoveEvent -= OnPlayerMove;
            m_Player.GrowEvent -= OnPlayerGrow;
        }
    }

    private void OnLevelStart()
    {
        transform.DOKill();
        transform.localPosition = new Vector3(0.0f, m_MinY, 0.0f);
    }

    private void OnPlayerMove(PlayerController player, Vector3 position, Quaternion rotation)
    {
        transform.DORotate(rotation.eulerAngles, 0.5f, RotateMode.Fast);
    }

    private void OnPlayerGrow(float newSize)
    {
        float t = Mathf.Clamp01(newSize - 1.0f);
        float lerpValue = Mathf.Lerp(m_MinY, m_MaxY, t);

        transform.DOLocalMoveY(lerpValue, 0.25f).SetEase(Ease.OutElastic);
    }
}
