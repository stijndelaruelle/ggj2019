using UnityEngine;
using DG.Tweening;

public class DungBallController : MonoBehaviour, IGrowable
{
	[SerializeField]
	private Transform m_VisualTransform;

	private CircleCollider2D m_Collider; 

	private Vector3 m_StartEulerAngles;
	private Vector3 m_StartGyroAttitudeToEuler;

	private Camera m_Camera;

	[SerializeField]
	private float m_Movespeed = 3;
	private float m_KeyboardAngle;

	private bool m_Move;

	private void Start()
	{
		m_Collider = GetComponent<CircleCollider2D>(); 
		m_Camera = Camera.main;

		Input.gyro.enabled = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Grow(0.1f);
		}

		if (!m_Move)
			return;

		transform.position += Move();
		transform.position = ClampEdge();
	}

	private Vector3 Move()
	{
		Vector3 deltaEulerAngles = m_StartGyroAttitudeToEuler - Input.gyro.attitude.eulerAngles;
		deltaEulerAngles.x = 0.0f;
		deltaEulerAngles.y = 0.0f;

#if UNITY_EDITOR
		m_KeyboardAngle += Input.GetAxis("Horizontal");
		deltaEulerAngles.z = m_KeyboardAngle;
#endif
		transform.eulerAngles = deltaEulerAngles;

		return (m_Movespeed * transform.up) * Time.deltaTime;
	}

	private Vector3 ClampEdge()
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

		return pos;
	}

	public void Go()
	{
		m_StartEulerAngles = transform.eulerAngles;
		m_StartGyroAttitudeToEuler = Input.gyro.attitude.eulerAngles;

		m_Move = true;
	}

	public void Grow(float amount)
	{
		if (m_Collider != null)
			m_Collider.radius += amount;

		if (m_VisualTransform != null)
			m_VisualTransform.DOScale(m_VisualTransform.localScale.x + amount, 0.25f).SetEase(Ease.OutElastic); 
	}

	public float Radius
	{
		get { return m_Collider != null ? m_Collider.radius : 0.0f; }
	}
}
