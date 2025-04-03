using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlagTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public Text coinText;
    public int levelIndex;
    private int earnedCoins;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            Time.timeScale = 0;

            earnedCoins = Random.Range(50, 101); // Koin acak antara 50 - 100
            coinText.text = "Coins: " + earnedCoins;

            // Simpan total koin di PlayerPrefs
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            totalCoins += earnedCoins;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();

            UnlockNextLevel();
        }
    }

    void UnlockNextLevel()
    {
        int nextLevel = levelIndex + 1;
        if (nextLevel <= 3)
        {
            PlayerPrefs.SetInt("Level" + nextLevel, 1);
            PlayerPrefs.Save();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    
}
