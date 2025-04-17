using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Text coinText;

    [Header("Bintang")]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [Header("Pemain")]
    [SerializeField] private Health playerHealth;

    private int earnedCoins;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(Retry);

        if (playerHealth != null)
            playerHealth.onDeath += ShowGameOver;

        earnedCoins = 0;

        // Matikan semua bintang di awal
        if (star1 != null) star1.SetActive(false);
        if (star2 != null) star2.SetActive(false);
        if (star3 != null) star3.SetActive(false);
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            Invoke("EnableGameOverPanel", 1.5f); // Delay muncul panel
        }
    }

    private void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(AnimatePanel(gameOverPanel)); // ⬅️ Animasi pop-up panel

        earnedCoins = Random.Range(30, 61);
        AddCoinsAndUpdateUI(earnedCoins);

        StartCoroutine(ShowStars(3)); // Tampilkan semua bintang
    }

    private IEnumerator AnimatePanel(GameObject panel)
    {
        panel.transform.localScale = Vector3.zero;
        float timer = 0f;
        float duration = 0.4f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            panel.transform.localScale = new Vector3(t, t, t);
            yield return null;
        }

        panel.transform.localScale = Vector3.one;
    }

    private void AddCoinsAndUpdateUI(int coins)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        StartCoroutine(AnimateCoinText(coins));
    }

    private IEnumerator AnimateCoinText(int targetCoins)
    {
        int displayCoins = 0;
        float duration = 1.5f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / duration;
            displayCoins = Mathf.FloorToInt(Mathf.Lerp(0, targetCoins, progress));
            coinText.text = "+" + displayCoins.ToString();
            yield return null;
        }

        coinText.text = "+" + targetCoins.ToString();
    }

    private IEnumerator ShowStars(int starCount)
    {
        if (starCount >= 1)
            yield return StartCoroutine(AnimateStar(star1));

        if (starCount >= 2)
            yield return StartCoroutine(AnimateStar(star2));

        if (starCount >= 3)
            yield return StartCoroutine(AnimateStar(star3));
    }

    private IEnumerator AnimateStar(GameObject star)
    {
        star.SetActive(true);
        star.transform.localScale = Vector3.zero;

        float timer = 0f;
        float duration = 0.3f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / duration;
            float scale = Mathf.SmoothStep(0, 1, progress);
            star.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        star.transform.localScale = Vector3.one;
    }

    private void Retry()
    {
        GoToLevelMenu();
    }

    private void GoToLevelMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("GoToLevelPanel", 1);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onDeath -= ShowGameOver;
    }
}
