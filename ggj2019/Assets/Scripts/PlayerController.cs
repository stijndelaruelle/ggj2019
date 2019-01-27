using DG.Tweening;
using Sjabloon;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IGrowable, IDamageable
{
	public delegate void MoveDelegate(PlayerController player, Vector3 newPosition, Quaternion newRotation);

	[SerializeField]
	private float m_StartMoveSpeed;
	[SerializeField]
	private float m_MinSize; 
	private Vector3 m_StartPosition;
	private float m_StartSize;
	private float m_StartRadius;

	[SerializeField]
	private float m_DebugRotateSpeed;

	[Space(5)]
	[Header("Growing")]
	[SerializeField]
	private Transform m_VisualTransform;
    private float m_StartVisualSize;

    [SerializeField]
	private CircleCollider2D m_Collider;
	public float ColliderSize
	{
		get { return m_Collider.radius; }
	}

	//Movement
	private Vector3 m_StartGyroAttitudeToEuler;
	private float m_DebugGyroAngle;

	private float m_CurrentMoveSpeed;

	private float m_CurrentSize;
	public float Size
	{
		get { return m_CurrentSize; }
	}

	private Vector3 m_CurrentDirection;
	public Vector3 Direction
	{
		get { return m_CurrentDirection; }
	}

	//Events
	public event MoveDelegate MoveEvent;
	public event GrowDelegate GrowEvent;

	//Methods
	private void Start()
	{
		//Level reset
		m_StartPosition = transform.position.Copy();
		m_StartSize = 1.0f;

        if (m_VisualTransform != null)
            m_StartVisualSize = m_VisualTransform.localScale.x;

		if (m_Collider != null)
			m_StartRadius = m_Collider.radius;

        m_CurrentMoveSpeed = m_StartMoveSpeed;
        m_CurrentSize = m_StartSize;

        LevelDirector.Instance.LevelStartEvent += OnLevelStart;
		//LevelDirector.Instance.LevelStopEvent += OnLevelStop;
		LevelDirector.Instance.LevelUpdateEvent += OnLevelUpdate;
		//LevelDirector.Instance.LevelFixedUpdateEvent += OnLevelFixedUpdate;

		//Register input
		Input.gyro.enabled = true;

		InputManager inputManager = InputManager.Instance;

		if (inputManager != null)
		{
			//Keyboard
			inputManager.BindAxis("PlayerWorm_Vertical", KeyCode.RightArrow, KeyCode.LeftArrow);
			inputManager.BindAxis("PlayerWorm_Horizontal", KeyCode.UpArrow, KeyCode.DownArrow);

			//Controller
			inputManager.BindAxis("PlayerWorm_Vertical", 0, ControllerAxisCode.LeftStickX);
			inputManager.BindAxis("PlayerWorm_Horizontal", 0, ControllerAxisCode.LeftStickY);
		}
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector != null)
		{
			levelDirector.LevelStartEvent -= OnLevelStart;
			//levelDirector.LevelStopEvent -= OnLevelStop;
			levelDirector.LevelUpdateEvent -= OnLevelUpdate;
			//levelDirector.LevelFixedUpdateEvent -= OnLevelFixedUpdate;
		}
	}


	//Events
	private void OnLevelStart()
	{
		m_StartGyroAttitudeToEuler = Input.gyro.attitude.eulerAngles;
		m_DebugGyroAngle = 0.0f;

        transform.position = m_StartPosition;
        m_CurrentMoveSpeed = m_StartMoveSpeed;
        SetSize(m_StartSize);
	}

	private void OnLevelUpdate()
	{
		//Debug Utility
		if (Input.GetKeyDown(KeyCode.G))
		{
			Grow(0.5f);
		}
	}

	private void FixedUpdate()
	{
		//Should have been "OnLevelFixedUpdate" but this never get's sent as the LevelDirector doesn't have a rigidbody
		if (LevelDirector.Instance.IsLevelStarted == false)
			return;

#if UNITY_STANDALONE || UNITY_EDITOR
		GyroMove();
		//Move();
#endif

#if UNITY_ANDROID
		GyroMove();
#endif
	}


	//Mutators
	private void Move()
	{
		InputManager inputManager = InputManager.Instance;

		Vector3 offset = Vector3.zero;
		offset.x = inputManager.GetAxis("PlayerWorm_Vertical") * m_CurrentMoveSpeed * Time.deltaTime;
		offset.y = inputManager.GetAxis("PlayerWorm_Horizontal") * m_CurrentMoveSpeed * Time.deltaTime;

		m_CurrentDirection = offset.normalized;

        if (offset.sqrMagnitude > 0.0f)
        {
            transform.Translate(offset);

            //Let the world know!
            if (MoveEvent != null)
                MoveEvent(this, transform.position, transform.rotation);
        }
	}

	private void GyroMove()
	{
		Vector3 deltaEulerAngles = m_StartGyroAttitudeToEuler - Input.gyro.attitude.eulerAngles;

		//We only use the Z axis
		deltaEulerAngles.x = 0.0f;
		deltaEulerAngles.y = 0.0f;

        #if UNITY_STANDALONE || UNITY_EDITOR
		    m_DebugGyroAngle += InputManager.Instance.GetAxis("PlayerWorm_Vertical") * m_DebugRotateSpeed;
		    deltaEulerAngles.z = m_DebugGyroAngle;
        #endif

		Vector3 dir = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * deltaEulerAngles.z), Mathf.Cos(Mathf.Deg2Rad * deltaEulerAngles.z), 0.0f);

        #if UNITY_STANDALONE || UNITY_EDITOR
            dir.x *= -1;
        #endif

        m_CurrentDirection = dir.normalized;

		Vector3 offset = m_CurrentMoveSpeed * m_CurrentDirection * Time.deltaTime;

        if (offset.sqrMagnitude > 0.0f)
        {
            transform.position += offset;
            transform.rotation = Quaternion.Euler(0f, 0f, -deltaEulerAngles.z);

            //Let the world know!
            if (MoveEvent != null)
                MoveEvent(this, transform.position, transform.rotation);
        }
	}

	public void Grow(float amount)
	{
        m_CurrentMoveSpeed += (amount * 10.0f); //Cheese!
		SetSize(m_CurrentSize + amount);

        if (GrowEvent != null)
            GrowEvent(m_CurrentSize);
	}

    public void Damage()
    {
        //Hide
        SetSize(0);
        LevelDirector.Instance.CompleteLevel(false);
    }

	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	private void SetSize(float size)
	{
		m_CurrentSize = size;

		//Gameplay adjusts immediatly
		if (m_Collider != null)
			m_Collider.radius = m_StartRadius * m_CurrentSize;

		//Visuals can take their time
		if (m_VisualTransform != null)
			m_VisualTransform.DOScale(m_StartVisualSize * m_CurrentSize, 0.25f).SetEase(Ease.OutElastic);

		//Fail level when you lose your ball
		if (m_CurrentSize < m_MinSize)
			LevelDirector.Instance.CompleteLevel(false);  
    }

	//Accessors
	public float GetPower()
	{
        return m_CurrentMoveSpeed;// * //m_CurrentSize); // * 
    }

	//Debug
	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Angle: " + m_DebugGyroAngle);
	}
}
