using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;
    public float rotationSpeed = 10;
    public float jumpForce = 5; // Fuerza del salto
    public LayerMask groundLayer; // Capa del suelo para verificar colisión

    // Variables para doble salto
    public bool doubleJumpActive = false;
    private bool doubleJumpUsed = false;

    private Vector3 forward, right;
    private Rigidbody rb;
    private bool isGrounded;

    public Animator animator;

    // 🧊 Variables para hielo
    private Vector3 currentVelocity = Vector3.zero;
    private bool onIce = false;

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

        // Movimiento + deslizamiento
        if (onIce)
        {
            // Suaviza el cambio de velocidad y permite inercia
            Vector3 targetVelocity = direction * speed;
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 0.5f); // menos = más resbaloso
        }
        else
        {
            currentVelocity = direction * speed;
        }

        transform.position += currentVelocity * Time.deltaTime;

        // Rotación solo si te estás moviendo
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        bool isMoving = direction.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);

        // Verificar si el jugador está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.3f, groundLayer);

        // Si está en el suelo, se resetea el doble salto
        if (isGrounded)
        {
            doubleJumpUsed = false;
        }

        // Manejo del salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJumping", true);
            }
            else if (doubleJumpActive && !doubleJumpUsed)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJumpUsed = true;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            AnimatorStateInfo st = animator.GetCurrentAnimatorStateInfo(0);
            if (st.IsName("Jump") || st.IsName("Jump 1"))
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    // Detectar si está sobre hielo
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            onIce = true;
        }
        else
        {
            onIce = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ice"))
        {
            onIce = false;
        }
    }
}
