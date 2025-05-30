using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovimientoConSonido : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 ultimaPosicion;
    public float umbralMovimiento = 0.01f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ultimaPosicion = transform.position;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, ultimaPosicion);

        if (distancia > umbralMovimiento)
        {
            // Está moviéndose
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            // Está quieto
            if (audioSource.isPlaying)
                audioSource.Pause();
        }

        ultimaPosicion = transform.position;
    }
}
