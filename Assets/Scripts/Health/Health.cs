using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float startingHealth = 3;
    public float currentHealth { get; private set; }
    private bool isDead;

    [Header("Invincibility Frames")]
    [SerializeField] private float iFramesDuration = 1f;
    [SerializeField] private int numberOfFlashes = 3;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public delegate void OnDeath();
    public event OnDeath onDeath;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        anim.SetTrigger(currentHealth > 0 ? "hurt" : "die");

        if (currentHealth > 0)
        {
            if (hurtSound != null && audioSource != null)
                audioSource.PlayOneShot(hurtSound);

            StartCoroutine(Invulnerability());
        }
        else
        {
            Die();
        }
    }

    public void AddHealth(float amount)
    {
        if (isDead) return;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (dieSound != null && audioSource != null)
            audioSource.PlayOneShot(dieSound);

        GetComponent<PlayerMove>().enabled = false;

        Level1Music levelMusic = FindObjectOfType<Level1Music>();
        if (levelMusic != null)
        {
            levelMusic.StopMusic();
        }

        onDeath?.Invoke();
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.8f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
