using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    public Button[] levelButtons;  // Array tombol level

    void Start()
    {
        UpdateLevelStatus();
    }

    void UpdateLevelStatus()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1; // Mulai dari Level 1
            bool isUnlocked = PlayerPrefs.GetInt("Level" + level, level == 1 ? 1 : 0) == 1;

            levelButtons[i].interactable = isUnlocked;  // Aktifkan tombol jika level terbuka
        }
    }
}
