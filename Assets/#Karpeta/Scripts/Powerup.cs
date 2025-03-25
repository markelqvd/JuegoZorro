using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.doubleJumpActive = true;
                // Opcional: Puedes mostrar algún efecto visual o notificar que se ha activado el doble salto.
            }
            Destroy(gameObject);
        }
    }
}
