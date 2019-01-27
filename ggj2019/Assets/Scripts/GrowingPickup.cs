
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GrowingPickup : MonoBehaviour
{
	public delegate void PickupDelegate(GrowingPickup pickup); 

    [SerializeField]
    private PickupData m_PickupData;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

	private Collider2D m_Collider;

	public event PickupDelegate PickupEvent; 

	public PickupData PickupData
	{
		get { return m_PickupData; }
		set { m_PickupData = value; }
	}


	private void Start()
	{
		LevelDirector.Instance.LevelStartEvent += OnLevelStart;
		m_Collider = GetComponent<Collider2D>();
	}

	private void OnDestroy()
	{
		LevelDirector levelDirector = LevelDirector.Instance;

		if (levelDirector != null)
			levelDirector.LevelStartEvent -= OnLevelStart;
	}

	private void OnLevelStart()
	{
		gameObject.SetActive(true);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_PickupData == null)
            return;

        //Growing
        if (m_PickupData.GrowAmount > 0.0f)
        {
            IGrowable growable = collision.collider.GetComponent<IGrowable>();

            if (growable != null)
            {
                //Check if we should get picked up!
                bool canPickup = true;

                if (m_PickupData is OutdoorPickupData)
                {
                    if (growable.Size < ((OutdoorPickupData)m_PickupData).PickupSize || m_Collider == null)
                        canPickup = false;
                }

                if (canPickup)
                {
                    if (PickupEvent != null)
                        PickupEvent(this);

                    growable.Grow(m_PickupData.GrowAmount);
                    gameObject.SetActive(false);
                }
            }
        }

        //Move speed
        if (m_PickupData.MoveSpeedAmount > 0.0f)
        {
            IMoveable moveable = collision.collider.GetComponent<IMoveable>();

            if (moveable != null)
            {
                moveable.IncreaseMoveSpeed(m_PickupData.MoveSpeedAmount);
                gameObject.SetActive(false);
            }
        }

        //Damage
        if (m_PickupData is IndoorPickupData)
        {
            if (((IndoorPickupData)m_PickupData).DealsDamage)
            {
                IDamageable damageable = collision.collider.GetComponent<IDamageable>();

                if (damageable != null)
                    damageable.Damage();
            }
        }
    }

	public void SetSprite(Sprite sprite)
	{
		if (m_SpriteRenderer != null)
			m_SpriteRenderer.sprite = sprite; 
	}

    public void SetSprite()
    {
        if (m_PickupData != null)
            SetSprite(m_PickupData.Sprite);
    }
}