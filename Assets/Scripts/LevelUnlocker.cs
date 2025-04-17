using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    public Button[] levelButtons;  

    void Start()
    {
        UpdateLevelStatus();
    }

    void UpdateLevelStatus()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1; 
            bool isUnlocked = PlayerPrefs.GetInt("Level" + level, level == 1 ? 1 : 0) == 1;

            levelButtons[i].interactable = isUnlocked;  
        }
    }
}
