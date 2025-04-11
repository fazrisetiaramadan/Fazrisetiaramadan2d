using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

            // Mulai animasi coin naik dari 0 ke earnedCoins
            StartCoroutine(AnimateCoinText(earnedCoins));

            // Simpan total koin
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            totalCoins += earnedCoins;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.Save();

            UnlockNextLevel();
        }
    }

    IEnumerator AnimateCoinText(int targetCoins)
    {
        int displayCoins = 0;
        float duration = 1.5f; // durasi animasi
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime; // pakai unscaled karena timeScale = 0
            float progress = timer / duration;
            displayCoins = Mathf.FloorToInt(Mathf.Lerp(0, targetCoins, progress));
            coinText.text = "Coins: " + displayCoins;
            yield return null;
        }

        coinText.text = "Coins: " + targetCoins;
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
