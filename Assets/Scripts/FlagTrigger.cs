using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FlagTrigger : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject finishPanel;
    public Text coinText;

    [Header("Stars")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [Header("Level Info")]
    public int levelIndex;

    private int earnedCoins;
    private bool isFinished = false;

    private void Start()
    {
        // Pastikan panel awalnya nonaktif dan scale-nya nol
        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
            finishPanel.transform.localScale = Vector3.zero;
        }

        // Matikan semua bintang
        if (star1 != null) star1.SetActive(false);
        if (star2 != null) star2.SetActive(false);
        if (star3 != null) star3.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFinished) return;

        if (other.CompareTag("Player"))
        {
            isFinished = true;

            earnedCoins = Random.Range(50, 101);
            StartCoroutine(ShowFinishPanelWithAnimation());
        }
    }

    private IEnumerator ShowFinishPanelWithAnimation()
    {
        Time.timeScale = 0f;

        finishPanel.SetActive(true);
        yield return StartCoroutine(AnimatePanel(finishPanel)); 

        StartCoroutine(AnimateCoinText(earnedCoins));
        StartCoroutine(ShowStars(3)); 
        
        // Simpan koin ke PlayerPrefs
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += earnedCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();

        UnlockNextLevel();
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

    IEnumerator AnimateCoinText(int targetCoins)
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

    IEnumerator ShowStars(int starCount)
    {
        if (starCount >= 1)
            yield return StartCoroutine(AnimateStar(star1));

        if (starCount >= 2)
            yield return StartCoroutine(AnimateStar(star2));

        if (starCount >= 3)
            yield return StartCoroutine(AnimateStar(star3));
    }

    IEnumerator AnimateStar(GameObject star)
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

    void UnlockNextLevel()
    {
        int nextLevel = levelIndex + 1;
        if (nextLevel <= 5)
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

    public void GoToLevelMenu()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("GoToLevelPanel", 1);
        SceneManager.LoadScene("MainMenu");
    }
}
