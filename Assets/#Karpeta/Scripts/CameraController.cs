using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Referencia al jugador
    public float distance = 5.0f; // Distancia de la c�mara al jugador
    public float smoothSpeed = 10f; // Velocidad de suavizado de la c�mara
    public float rotationSpeed = 2f; // Sensibilidad de rotaci�n

    private float yaw = 0f; // Rotaci�n horizontal
    private float pitch = 15f; // Rotaci�n vertical

    void LateUpdate()
    {
        if (!target) return;

        // Obtener entrada del joystick o mouse
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 60f); // Limitar la inclinaci�n de la c�mara

        // Calcular la nueva posici�n de la c�mara
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance);

        // Comprobar colisiones con obst�culos
        RaycastHit hit;
        if (Physics.Raycast(target.position, (targetPosition - target.position).normalized, out hit, distance))
        {
            targetPosition = hit.point + hit.normal * 0.3f; // Ajustar posici�n si choca con algo
        }

        // Aplicar suavizado a la c�mara
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position);
    }
}
