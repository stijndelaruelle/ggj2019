using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleController : MonoBehaviour
{
	[SerializeField]
	private Transform m_Beetle;

	private DungBall m_DungBall;

	private float m_DeadZone = 0.7f;
	private float m_Movespeed = 3;

	private Camera m_Camera;

	[SerializeField]
	private Text m_DebugText;

	private Vector3 m_StartEulerAngles;
	private Vector3 m_StartGyroAttitudeToEuler;

	private float m_OriginalRotation;

	private bool m_Move;

	private void Start()
	{
		m_DungBall = GetComponentInChildren<DungBall>();
		m_Camera = Camera.main;

		Input.gyro.enabled = true;
	}

	private void Update()
	{
		if (!m_Move)
			return; 

		Vector3 deltaEulerAngles =  m_StartGyroAttitudeToEuler - Input.gyro.attitude.eulerAngles;
		deltaEulerAngles.x = 0.0f;
		deltaEulerAngles.y = 0.0f;

#if UNITY_EDITOR
		m_OriginalRotation += Input.GetAxis("Horizontal");
		deltaEulerAngles.z = m_OriginalRotation;
#endif
		transform.eulerAngles = deltaEulerAngles;

		Debug.DrawRay(transform.position, transform.up, Color.red); 

		m_DebugText.text = transform.eulerAngles.ToString();

		Vector3 newPosition = transform.position + ((m_Movespeed * transform.up) * Time.deltaTime);
		transform.position = newPosition;


		ClampEdge();
	}

	private Vector3 GetKeyboardInput()
	{
		return new Vector3(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Vertical"),
				0.0f
			);
	}

	private void ClampEdge()
	{
		Vector3 pos = transform.position;

		float minX = m_Camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, m_Camera.transform.position.z)).x;
		float maxX = m_Camera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, m_Camera.transform.position.z)).x;

		float minY = m_Camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, m_Camera.transform.position.z)).y;
		float maxY = m_Camera.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, m_Camera.transform.position.z)).y;

		if (pos.x < minX) pos.x = minX;
		if (pos.x > maxX) pos.x = maxX;
		if (pos.y < minY) pos.y = minY;
		if (pos.y > maxY) pos.y = maxY;

		transform.position = pos;
	}

	public void Go()
	{
		m_StartEulerAngles = transform.eulerAngles;
		m_StartGyroAttitudeToEuler = Input.gyro.attitude.eulerAngles;

		m_Move = true; 
	}
}
