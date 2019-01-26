using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowLevelBorder : MonoBehaviour
{
    [Serializable]
    public class BreakSetting
    {
        [SerializeField]
        private float m_RequiredPower;
        public float RequiredPower
        {
            get { return m_RequiredPower; }
        }

        [SerializeField]
        private Color m_VisualColor; //Will change into a sprite
        public Color VisualColor
        {
            get { return m_VisualColor; }
        }
    }

    [Header("Settings")]
    [Space(5)]
    [SerializeField]
    private float m_Radius;

    [SerializeField]
    private List<BreakSetting> m_BreakSettings;
    private int m_CurrentBreakSetting = 0;

    [Header("Required References")]
    [Space(5)]
    [SerializeField]
    private PlayerController m_Player;

    [SerializeField]
    private SpriteRenderer m_Visuals;


    private void Start()
    {
        if (LevelDirector.Instance != null)
            LevelDirector.Instance.LevelStartEvent += OnLevelStart;

        if (m_Player != null)
            m_Player.MoveEvent += OnPlayerMove;
    }

    private void OnDestroy()
    {
        if (LevelDirector.Instance != null)
            LevelDirector.Instance.LevelStartEvent -= OnLevelStart;

        if (m_Player != null)
            m_Player.MoveEvent -= OnPlayerMove;
    }

    private void OnLevelStart()
    {
        if (m_BreakSettings.Count > 0)
            m_Visuals.color = m_BreakSettings[0].VisualColor;

        m_CurrentBreakSetting = 0;
    }

    private void OnPlayerMove(PlayerController player, Vector3 newPosition)
    {
        bool isAllowedToPass = false;

        Vector3 diff = newPosition - transform.position;
        Vector3 direction = diff.normalized;

        //Check if a player is trying to leave the level
        if (diff.magnitude >= (m_Radius - player.ColliderSize))
        {
            //Check if the player is within the wall range
            float dot = Vector3.Dot(direction, new Vector3(0.0f, 1.0f, 0.0f));
            if (dot < 0.15f && dot > -0.15f) //Super dirty & hardcoded way to limit the wall breaking & passing range
            {
                //Check if the power of the player is enough to break trough the next stage (and if there is a next stage)
                if (m_CurrentBreakSetting >= 0 && (m_CurrentBreakSetting + 1) < m_BreakSettings.Count)
                {
                    if (player.GetPower() >= m_BreakSettings[m_CurrentBreakSetting].RequiredPower)
                    {
                        m_CurrentBreakSetting += 1;
                        m_Visuals.color = m_BreakSettings[m_CurrentBreakSetting].VisualColor;
                    }
                }

                if (IsBroken())
                    isAllowedToPass = true;
            }

            //Bounce the player back unless the border just got broken.
            if (isAllowedToPass == false)
            {
                player.SetPosition(direction * (m_Radius - player.ColliderSize));
            }
        }
    }

    public bool IsBroken()
    {
        return (m_CurrentBreakSetting >= (m_BreakSettings.Count - 1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
