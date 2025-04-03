using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Mulai musik saat MainMenu dimulai
    }

    void Update()
    {
        // Jika scene bukan MainMenu, hapus GameObject ini agar musik berhenti
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
