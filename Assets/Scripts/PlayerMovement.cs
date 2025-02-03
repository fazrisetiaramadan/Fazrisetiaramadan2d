using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f; // Kecepatan berjalan
    [SerializeField] private float runSpeed = 6f; // Kecepatan berlari
    [SerializeField] private float jumpPower = 10f; // Kekuatan lompat
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        // Grab references for rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Input horizontal dari pemain
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip karakter saat bergerak ke kiri atau kanan
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);

        // Cek apakah pemain sedang berlari atau berjalan
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0;
        bool isWalking = horizontalInput != 0 && !isRunning;

        // Set parameter Animator
        anim.SetBool("run", isRunning);
        anim.SetBool("walk", isWalking);
        anim.SetBool("grounded", isGrounded());

        // Tentukan kecepatan berdasarkan mode (run atau walk)
        float speed = isRunning ? runSpeed : walkSpeed;

        // Atur kecepatan karakter
        if (horizontalInput != 0)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else
        {
            // Hentikan karakter jika tidak ada input
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Logika wall jump
        if (wallJumpCooldown > 0.2f)
        {
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                // Wall jump saat pemain diam di dinding
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // Wall jump saat bergerak
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
