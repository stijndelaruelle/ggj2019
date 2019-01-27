using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private Transform m_Target;

	private void Update()
	{
		Vector3 pos = m_Target.position;
		pos.z = transform.position.z;

		transform.position = pos; 
	}
}
