using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public float rotationSpeed = 10;
    public float jumpForce = 5; // Fuerza del salto
    public LayerMask groundLayer; // Capa del suelo para verificar colisión

    private Vector3 forward, right;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = horizontalInput * right + verticalInput * forward;

        if (direction.magnitude > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si el jugador está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Saltar si el jugador está en el suelo y presiona la tecla de salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}