using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShrinkingSurface : MonoBehaviour
{
	public delegate void VoidDelegate(); 

	[SerializeField]
	private float m_ShrinkAmount;

	public static event VoidDelegate ShrinkingSurfaceEnterEvent;
	public static event VoidDelegate ShrinkingSurfaceExitEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IGrowable growable = collision.GetComponent<IGrowable>();

		if (growable == null)
			return;

		if (ShrinkingSurfaceEnterEvent != null)
			ShrinkingSurfaceEnterEvent(); 
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		IGrowable growable = collision.GetComponent<IGrowable>();

		if (growable == null)
			return;

		growable.Grow(m_ShrinkAmount); 
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		IGrowable growable = collision.GetComponent<IGrowable>();

		if (growable == null)
			return;

		if (ShrinkingSurfaceExitEvent != null)
			ShrinkingSurfaceExitEvent(); 
	}
}
