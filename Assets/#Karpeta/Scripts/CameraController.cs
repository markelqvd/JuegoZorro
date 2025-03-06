using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Referencia al jugador
    public float distance = 5.0f; // Distancia de la cámara al jugador
    public float smoothSpeed = 10f; // Velocidad de suavizado de la cámara
    public float rotationSpeed = 2f; // Sensibilidad de rotación

    private float yaw = 0f; // Rotación horizontal
    private float pitch = 15f; // Rotación vertical

    void LateUpdate()
    {
        if (!target) return;

        // Obtener entrada del joystick o mouse
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, 5f, 60f); // Limitar la inclinación de la cámara

        // Calcular la nueva posición de la cámara
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance);

        // Comprobar colisiones con obstáculos
        RaycastHit hit;
        if (Physics.Raycast(target.position, (targetPosition - target.position).normalized, out hit, distance))
        {
            targetPosition = hit.point + hit.normal * 0.3f; // Ajustar posición si choca con algo
        }

        // Aplicar suavizado a la cámara
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position);
    }
}
