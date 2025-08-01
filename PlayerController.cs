using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Variabel internal
    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;

    // Start dipanggil sekali untuk setup
    void Start()
    {
        // Ambil komponen Rigidbody untuk digunakan nanti
        rb = GetComponent<Rigidbody>();
    }

    // Update dipanggil setiap frame untuk input
    void Update()
    {
        // 1. Cek apakah menyentuh tanah
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        // 2. Baca input gerakan (WASD)
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 3. Cek input lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Beri gaya ke atas secara instan
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // FixedUpdate dipanggil pada interval tetap untuk fisika
    void FixedUpdate()
    {
        // Terapkan kecepatan ke Rigidbody
        Vector3 targetVelocity = moveInput.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
}