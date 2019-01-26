using DG.Tweening;
using Sjabloon;
using UnityEngine;

public class PlayerWorm : MonoBehaviour, IGrowable
{
    [SerializeField]
    private float m_Speed;

    [Space(5)]
    [Header("Growing")]
    [SerializeField]
    private Transform m_VisualTransform;

    [SerializeField]
    private CircleCollider2D m_Collider;

    [Space(5)]
    [Header("Visuals")]
    [SerializeField]
    private SimpleDecalPool m_DecalPool;

    //Level reset
    private Vector3 m_StartPosition;
    private float m_StartSize;
    private float m_StartRadius;

    private void Start()
    {
        //Level reset
        m_StartPosition = transform.position.Copy();

        if (m_VisualTransform != null)
            m_StartSize = m_VisualTransform.localScale.x;

        if (m_Collider != null)
            m_StartRadius = m_Collider.radius;

        LevelDirector.Instance.LevelResetEvent += OnLevelReset;

        //Register input
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

    private void FixedUpdate()
    {
        InputManager inputManager = InputManager.Instance;

        Vector3 offset = Vector3.zero;
        offset.x = inputManager.GetAxis("PlayerWorm_Vertical") * m_Speed * Time.deltaTime;
        offset.y = inputManager.GetAxis("PlayerWorm_Horizontal") * m_Speed * Time.deltaTime;

        float distance = offset.magnitude;

        //Place decals along this axis
        if (m_DecalPool != null && distance > 0.0f)
            m_DecalPool.PlaceDecal(new Vector3(transform.position.x + (offset.x * 0.5f), transform.position.y + (offset.y * 0.5f), 0.0f));
        
        transform.Translate(offset);
    }

    private void Update()
    {
        //Debug Utility
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grow(0.1f);
        }
    }

    public void Grow(float amount)
    {
        SetSize(m_VisualTransform.localScale.x + amount);
    }

    private void SetSize(float size)
    {
        //Gameplay adjusts immediatly
        if (m_Collider != null)
            m_Collider.radius = size;

        //Visuals can take their time
        if (m_VisualTransform != null)
            m_VisualTransform.DOScale(size, 0.25f).SetEase(Ease.OutElastic);
    }

    private void OnLevelReset()
    {
        transform.position = m_StartPosition;
        SetSize(m_StartSize);
    }
}
