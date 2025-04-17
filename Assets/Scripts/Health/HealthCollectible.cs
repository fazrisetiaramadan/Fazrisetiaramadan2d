using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    [Header("Audio Settings")]
    public AudioSource audioSource;          // AudioSource untuk memutar suara
    public AudioClip soundCollectHealth;     // Sound effect saat health diambil

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Tambahkan health ke player
            collision.GetComponent<Health>().AddHealth(healthValue);

            // Mainkan sound effect
            if (audioSource != null && soundCollectHealth != null)
            {
                audioSource.PlayOneShot(soundCollectHealth);
            }

            // Nonaktifkan objek health setelah diambil
            gameObject.SetActive(false);
        }
    }
}
