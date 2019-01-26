using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPickup : MonoBehaviour
{
    [SerializeField]
    private float m_GrowAmount;

    private void Start()
    {
        LevelDirector.Instance.LevelResetEvent += OnLevelReset;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IGrowable growable = collision.GetComponent<IGrowable>();

        if (growable != null)
        {
            growable.Grow(m_GrowAmount);
            gameObject.SetActive(false);
        } 
    }

    private void OnLevelReset()
    {
        gameObject.SetActive(true);
    }
}
