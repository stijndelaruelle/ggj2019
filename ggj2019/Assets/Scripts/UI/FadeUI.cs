using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class FadeUI : MonoBehaviour
{
    private void Start()
    {
        Image image = GetComponent<Image>();

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
        image.DOFade(0.0f, 1.0f);
    }
}
