
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

	public static event PickupDelegate PickupEvent; 

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

        IGrowable growable = collision.collider.GetComponent<IGrowable>();
        IMoveable moveable = collision.collider.GetComponent<IMoveable>();

        //Check if we should get picked up!
        bool canPickup = true;

        if ((m_PickupData is OutdoorPickupData) && growable != null)
        {
            if (growable.Size < ((OutdoorPickupData)m_PickupData).PickupSize || m_Collider == null)
                canPickup = false;
        }

        if (m_PickupData is IndoorPickupData)
        {
            if (((IndoorPickupData)m_PickupData).CanBePickedUp == false)
                canPickup = false;
        }

        if (canPickup)
        {
            if (growable != null)
                growable.Grow(m_PickupData.GrowAmount);

            if (moveable != null)
                moveable.IncreaseMoveSpeed(m_PickupData.MoveSpeedAmount);

            //Let the world know!
            if (PickupEvent != null)
                PickupEvent(this);

            gameObject.SetActive(false);
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

    public void SetSprite()
    {
        if (m_PickupData != null)
            SetSprite(m_PickupData.Sprite);
    }

    public void SetSprite(bool randomRotate)
    {
        if (m_PickupData != null)
            SetSprite(m_PickupData.Sprite, randomRotate);
    }

    public void SetSprite(Sprite sprite, bool randomRotate = false)
    {
        if (m_SpriteRenderer != null)
            m_SpriteRenderer.sprite = sprite;

        if (randomRotate)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360f));
        }
    }
}