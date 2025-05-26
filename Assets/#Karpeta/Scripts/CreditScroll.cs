using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public RectTransform creditContent; // El Text o Panel que se mueve
    public float scrollSpeed = 50f;     // Velocidad del movimiento

    void Update()
    {
        creditContent.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
