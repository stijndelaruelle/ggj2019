using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShrinkingSurface : MonoBehaviour
{
	[SerializeField]
	private float m_ShrinkAmount; 

	private void OnTriggerStay2D(Collider2D collision)
	{
		IGrowable growable = collision.GetComponent<IGrowable>();

		if (growable == null)
			return;

		growable.Grow(m_ShrinkAmount); 
	}
}
