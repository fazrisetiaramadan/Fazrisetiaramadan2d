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
        
        PlayerPrefs.DeleteKey("Level2");

        PlayerPrefs.Save();
        Debug.Log("Progress level direset! Koin tetap tersimpan.");

        
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
