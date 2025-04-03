using UnityEngine;
using UnityEngine.UI;

public class ResetProgress : MonoBehaviour
{
    public Button resetButton;

    void Start()
    {
        resetButton.onClick.AddListener(ResetGameProgress);
    }

    void ResetGameProgress()
    {
        // Reset level progress saja (Level 2 dan Level 3 terkunci kembali)
        PlayerPrefs.DeleteKey("Level2");
        PlayerPrefs.DeleteKey("Level3");

        PlayerPrefs.Save();
        Debug.Log("Progress level direset! Koin tetap tersimpan.");

        // Restart scene agar perubahan langsung terlihat
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
