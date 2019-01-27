using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonTouch : MonoBehaviour
{
    [SerializeField]
    private float m_TimeBeforeReset;
    private float m_Timer;
    private bool m_IsPressedDown = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_IsPressedDown = true;
            m_Timer = 0.0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_IsPressedDown = false;
        }

        if (m_IsPressedDown)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer > m_TimeBeforeReset)
            {
                LevelDirector.Instance.CompleteLevel(false);
                m_IsPressedDown = false;
                m_Timer = 0.0f;
            }
        }
    }
}
