using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasPathFollower : MonoBehaviour
{
    public Transform[] puntos;      
    public Camera camara1;          
    public Camera camara2;          

    public float speed = 2f;        

    private int indexActual = 0;
    private bool mover = false;

    private Quaternion rotCamara1Inicial;
    private Quaternion rotCamara2Inicial;

    void Start()
    {
        rotCamara1Inicial = camara1.transform.rotation;
        rotCamara2Inicial = camara2.transform.rotation;
    }

    public void IniciarMovimiento()
    {
        mover = true;
    }

    void Update()
    {
        if (!mover || indexActual >= puntos.Length) return;

        Transform objetivo = puntos[indexActual];

        camara1.transform.position = Vector3.MoveTowards(camara1.transform.position, objetivo.position, speed * Time.deltaTime);
        camara2.transform.position = Vector3.MoveTowards(camara2.transform.position, objetivo.position, speed * Time.deltaTime);

        // Mantener rotación fija para ambas cámaras (la rotación inicial)
        camara1.transform.rotation = rotCamara1Inicial;
        camara2.transform.rotation = rotCamara2Inicial;

        // Cuando ambas cámaras estén cerca del punto actual, avanzamos al siguiente punto
        if (Vector3.Distance(camara1.transform.position, objetivo.position) < 0.1f &&
            Vector3.Distance(camara2.transform.position, objetivo.position) < 0.1f)
        {
            indexActual++;
        }
    }
}
