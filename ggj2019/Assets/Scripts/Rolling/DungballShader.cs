using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungballShader : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_DungBall;

    [SerializeField]
    private MeshRenderer m_MeshRenderer;
    private Vector2 m_UVOffset;


    private void Update()
    {
        m_UVOffset.x += Time.deltaTime;
        //m_UVOffset.y += Time.deltaTime;

        Material material = m_MeshRenderer.material;
        material.SetFloat("m_UOffset", m_UVOffset.x);
        material.SetFloat("m_VOffset", m_UVOffset.y);
    }
}
