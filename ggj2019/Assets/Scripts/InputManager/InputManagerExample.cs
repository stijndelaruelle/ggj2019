using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sjabloon;

public class InputManagerExample : MonoBehaviour
{
    private InputManager m_InputManger;

    private void Start()
    {
        m_InputManger = InputManager.Instance;

        m_InputManger.BindButton("Player_Attack_1", KeyCode.Return, InputManager.ButtonState.OnRelease);
        m_InputManger.BindButton("Player_Attack_1", 0, ControllerButtonCode.A, InputManager.ButtonState.OnRelease);

        m_InputManger.BindButton("Player_Attack_2", KeyCode.Space, InputManager.ButtonState.OnRelease);
        m_InputManger.BindButton("Player_Attack_2", 1, ControllerButtonCode.A, InputManager.ButtonState.OnRelease);

        m_InputManger.BindAxis("Player_Vertical_1", KeyCode.D, KeyCode.A);
        m_InputManger.BindAxis("Player_Vertical_1", 0, ControllerAxisCode.LeftStickX);

        m_InputManger.BindAxis("Player_Vertical_2", KeyCode.RightArrow, KeyCode.LeftArrow);
        m_InputManger.BindAxis("Player_Vertical_2", 1, ControllerAxisCode.LeftStickX);
    }

    private void OnDestroy()
    {
        if (m_InputManger == null)
            return;

        m_InputManger.UnbindButton("Player_Attack_1");
        m_InputManger.UnbindButton("Player_Attack_2");

        m_InputManger.UnbindAxis("Player_Vertical_1");
        m_InputManger.UnbindAxis("Player_Vertical_2");
    }

    private void Update()
    {
        //Buttons
        if (m_InputManger.GetButton("Player_Attack_1"))
        {
            Debug.Log("Player 1 attacked!");
            ControllerInput.SetVibration(0, 1.0f, 1.0f, 0.2f);
        }

        if (m_InputManger.GetButton("Player_Attack_2"))
        {
            Debug.Log("Player 2 attacked!");
            ControllerInput.SetVibration(1, 1.0f, 1.0f, 0.2f);
        }

        //Axis
        float verticalAxis = m_InputManger.GetAxis("Player_Vertical_1");
        if (verticalAxis != 0.0f)
            Debug.Log("Player 1 vertical: " + verticalAxis);

        verticalAxis = m_InputManger.GetAxis("Player_Vertical_2");
        if (verticalAxis != 0.0f)
            Debug.Log("Player 2 vertical: " + verticalAxis);
    }
}
