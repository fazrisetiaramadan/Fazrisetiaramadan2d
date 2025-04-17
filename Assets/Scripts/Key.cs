using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip soundAmbilKunci;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeyManager.Instance.TambahKunci();  
            if (soundAmbilKunci != null && audioSource != null)
                audioSource.PlayOneShot(soundAmbilKunci);  

            gameObject.SetActive(false); 
        }
    }
}
