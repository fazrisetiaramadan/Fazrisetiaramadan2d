using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject settingsPanel;
    public GameObject howToPlayPanel; 

    void Start()
    {
        // Cek sinyal dari FlagTrigger untuk langsung buka Level Select
        if (PlayerPrefs.GetInt("GoToLevelPanel", 0) == 1)
        {
            PlayerPrefs.SetInt("GoToLevelPanel", 0); 
            mainMenuPanel.SetActive(false);
            levelSelectPanel.SetActive(true);
            settingsPanel.SetActive(false);
            howToPlayPanel.SetActive(false);
        }
        else
        {
            // Default: buka main menu
            mainMenuPanel.SetActive(true);
            levelSelectPanel.SetActive(false);
            settingsPanel.SetActive(false);
            howToPlayPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}
