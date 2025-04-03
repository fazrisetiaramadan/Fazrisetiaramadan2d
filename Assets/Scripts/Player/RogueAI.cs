using UnityEngine;

public class RogueAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;
    public Transform attackPoint;

    [Header("Movement Settings")]
    public float walkSpeed = 1.5f;    // Kecepatan patroli
    public float runSpeed = 3f;       // Kecepatan kejar
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float spriteScale = 0.2f;
    public float patrolDistance = 3f;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    public int attackDamage = 1;
    public LayerMask playerLayer;

    private float lastAttackTime;
    private Vector2 startingPosition;
    private bool movingRight = true;
    private bool isChasing = false;

    void Start()
    {
        startingPosition = transform.position;
        animator.SetBool("IsPatrolling", true); // Set animasi patroli awal
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isChasing)
            {
                isChasing = true;
                animator.SetBool("IsPatrolling", false);
                animator.SetBool("IsChasing", true);
            }

            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else if (isChasing && distanceToPlayer > detectionRange * 1.5f)
        {
            isChasing = false;
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsPatrolling", true);
            startingPosition = transform.position; // Reset patrol point
        }
        else if (!isChasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float moveDirection = movingRight ? 1 : -1;
        transform.position += new Vector3(moveDirection * walkSpeed * Time.deltaTime, 0, 0);
        
        // Flip sprite
        transform.localScale = new Vector3(
            Mathf.Sign(moveDirection) * spriteScale, 
            spriteScale, 
            spriteScale
        );

        // Animasi patroli (walk)
        animator.SetFloat("Speed", walkSpeed);

        // Ganti arah jika mencapai batas
        if (Mathf.Abs(transform.position.x - startingPosition.x) >= patrolDistance)
        {
            movingRight = !movingRight;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += new Vector3(direction.x, 0, 0) * runSpeed * Time.deltaTime;
        
        // Flip sprite
        transform.localScale = new Vector3(
            Mathf.Sign(direction.x) * spriteScale, 
            spriteScale, 
            spriteScale
        );

        // Animasi kejar (run)
        animator.SetFloat("Speed", runSpeed);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    public void OnAttackHit()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D playerCollider in hitPlayers)
        {
            if (playerCollider.CompareTag("Player"))
            {
                playerCollider.GetComponent<Health>().TakeDamage(attackDamage);
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Patrol area
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector2(startingPosition.x - patrolDistance, startingPosition.y),
            new Vector2(startingPosition.x + patrolDistance, startingPosition.y)
        );

        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}