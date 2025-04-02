using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float fuerzaEmpuje = 5f; // Ajusta la fuerza del empuje
    private Rigidbody rbPersonaje;

    void Start()
    {
        rbPersonaje = GetComponent<Rigidbody>(); // Obtiene el Rigidbody del personaje
    }

    private void OnCollisionStay(Collision collision)
    {
        // Verifica si el objeto es empujable y si se presiona "Q"
        if (collision.gameObject.CompareTag("Empujable") && Input.GetKey(KeyCode.Q))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            // Solo empuja si el personaje se está moviendo
            if (rb != null && rbPersonaje.velocity.magnitude > 0.1f)
            {
                Vector3 direccionEmpuje = transform.forward;
                rb.velocity = direccionEmpuje * fuerzaEmpuje;

            }
        }
    }
}
