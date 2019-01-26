using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleController : MonoBehaviour
{
	[SerializeField]
	private DungBallController m_DungBall;

	private void Update()
	{
		transform.position = m_DungBall.transform.position + (-m_DungBall.transform.up * m_DungBall.Radius); 
	}
}
