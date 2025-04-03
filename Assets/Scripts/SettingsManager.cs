using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider; // Referensi ke Slider
    private AudioSource backgroundMusic; // Referensi ke AudioSource

    void Start()
    {
        // Cari GameObject yang memutar musik di MainMenu
        backgroundMusic = GameObject.FindWithTag("BackgroundMusic")?.GetComponent<AudioSource>();

        // Load volume dari PlayerPrefs (jika ada)
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = savedVolume;

        // Set volume awal
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = savedVolume;
        }

        // Tambahkan listener ke slider untuk ubah volume
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }

        // Simpan volume ke PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
