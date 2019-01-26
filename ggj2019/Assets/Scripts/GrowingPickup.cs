
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GrowingPickup : MonoBehaviour
{
    [SerializeField]
    private PickupData m_PickupData;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

	private Collider2D m_Collider; 

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

        if (growable != null)
        {
            //Check if we should get picked up!
            if (growable.Size >= m_PickupData.PickupSize && m_Collider != null)
            {
                growable.Grow(m_PickupData.GrowAmount);
                gameObject.SetActive(false);
            }
        }
    }
}
