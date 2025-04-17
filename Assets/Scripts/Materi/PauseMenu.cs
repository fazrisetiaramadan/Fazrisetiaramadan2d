using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI;
    public Slider volumeSlider;

    [Header("Audio")]
    public AudioSource backgroundMusic;

    private bool isPaused = false;

    private void Start()
    {
        // Set slider ke volume terakhir yang disimpan
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        backgroundMusic.volume = volumeSlider.value;

        // Tambahkan event listener untuk perubahan slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
        
        pauseMenuUI.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game
        isPaused = false;
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
