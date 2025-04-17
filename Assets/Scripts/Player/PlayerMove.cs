using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource jumpSound;

    public bool canMove = true; 

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private Vector3 originalScale;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!canMove)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && horizontalInput != 0;
        bool isWalking = horizontalInput != 0 && !isRunning;

        anim.SetBool("run", isRunning);
        anim.SetBool("walk", isWalking);
        anim.SetBool("grounded", isGrounded());

        float speed = isRunning ? runSpeed : walkSpeed;

        // Movement logic
        if (horizontalInput != 0)
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        else
            body.velocity = new Vector2(0, body.velocity.y);

        HandleSound(isWalking, isRunning);

        if (wallJumpCooldown > 0.2f)
        {
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void HandleSound(bool isWalking, bool isRunning)
    {
        if (isWalking && isGrounded())
        {
            if (!walkSound.isPlaying)
            {
                runSound.Stop();
                walkSound.Play();
            }
        }
        else if (isRunning && isGrounded())
        {
            if (!runSound.isPlaying)
            {
                walkSound.Stop();
                runSound.Play();
            }
        }
        else
        {
            walkSound.Stop();
            runSound.Stop();
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            jumpSound.Play();
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            wallJumpCooldown = 0;
            jumpSound.Play();
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
