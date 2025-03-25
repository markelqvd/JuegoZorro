using System.Collections;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    public float depth = 2f; // Profundidad a la que baja la plataforma
    public float delayBeforeMoving = 1f; // Tiempo de espera antes de que baje
    public float stayAtBottomTime = 2f; // Tiempo que permanece en el punto más bajo antes de subir
    public float speed = 2f; // Velocidad de movimiento de la plataforma

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y - depth, originalPosition.z);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartMovingAfterDelay());
        }
    }

    IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMoving);
        if (!isMoving)
        {
            yield return MovePlatform(targetPosition);
            yield return new WaitForSeconds(stayAtBottomTime);
            yield return MovePlatform(originalPosition);
        }
    }

    IEnumerator MovePlatform(Vector3 destination)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}